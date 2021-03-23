using MERP.Engine.GlobalClass;
using MERP.Models;
using MERP.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MERP.App.Controllers
{
    public class StudentTrainingMappingController : Controller
    {
        StudentTrainingMapping _oStudentTrainingMapping = new StudentTrainingMapping();
        List<StudentTrainingMapping> _oStudentTrainingMappings = new List<StudentTrainingMapping>();
        StudentTrainingMappingService _oStudentTrainingMappingService = new StudentTrainingMappingService();
        public ActionResult ViewStudentTrainingMappings()
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oStudentTrainingMappings = _oStudentTrainingMappingService.Gets("", (string)Session[GlobalSession.UserID]);
            ViewBag.StudentTrainingMappings = _oStudentTrainingMappings;
            return View();
        }
        public ActionResult ViewStudentTrainingMapping()
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oStudentTrainingMapping = new StudentTrainingMapping();
            List<Branch> oBranchs = new List<Branch>();
            BranchService oBranchService = new BranchService();
            oBranchs = oBranchService.Gets("", (string)Session[GlobalSession.UserID]);

            List<BankAccountMapping> oBankAccountMappings = new List<BankAccountMapping>();
            BankAccountMappingService oBankAccountMappingService = new BankAccountMappingService();
            oBankAccountMappings = oBankAccountMappingService.GetsActiveBankAccountMappings((string)Session[GlobalSession.UserID]);

            NWRDirectSalesDetailService oNWRDirectSalesDetailService = new NWRDirectSalesDetailService();

            ViewBag.Branchs = oBranchs;
            ViewBag.PaymentMedias = Enum.GetValues(typeof(EnumPaymentMedia)).Cast<EnumPaymentMedia>().Select(e => new EnumClass { MModelPK = ((int)e), MModelString = e.ToString() });
            ViewBag.BankAccountMappings = oBankAccountMappings;
            ViewBag.StudentTrainingMapping = _oStudentTrainingMapping;
            return View();
        }
        [HttpPost]
        public JsonResult Save(StudentTrainingMapping obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            try
            {
                if (obj.StudentTrainingMappingID == 0)
                {
                    _oStudentTrainingMapping = _oStudentTrainingMappingService.IUD(obj, EnumDBOperation.Insert, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oStudentTrainingMapping = _oStudentTrainingMappingService.IUD(obj, EnumDBOperation.Update, (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oStudentTrainingMapping = new StudentTrainingMapping();
                _oStudentTrainingMapping.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oStudentTrainingMapping);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(StudentTrainingMapping obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oStudentTrainingMapping = new StudentTrainingMapping();
            try
            {
                if (obj.StudentTrainingMappingID > 0)
                {
                    _oStudentTrainingMapping = _oStudentTrainingMappingService.IUD(obj, EnumDBOperation.Delete, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oStudentTrainingMapping.ErrorMessage = FeedbackMessage.NoIdFound;
                }
            }
            catch (Exception ex)
            {
                _oStudentTrainingMapping = new StudentTrainingMapping();
                _oStudentTrainingMapping.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oStudentTrainingMapping);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Search(StudentTrainingMapping obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oStudentTrainingMapping = new StudentTrainingMapping();
            _oStudentTrainingMappings = new List<StudentTrainingMapping>() { };
            try
            {
                _oStudentTrainingMappings = _oStudentTrainingMappingService.Gets(this.GeneralSearchMakeSQL(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
            }
            catch (Exception ex)
            {
                _oStudentTrainingMapping = new StudentTrainingMapping();
                _oStudentTrainingMapping.ErrorMessage = ex.Message;
                _oStudentTrainingMappings.Add(_oStudentTrainingMapping);
            }
            var jsonResult = Json(_oStudentTrainingMappings, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [HttpPost]
        public JsonResult SearchNWRDirectSalesDetail(StudentTrainingMapping obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oStudentTrainingMapping = new StudentTrainingMapping();
            _oStudentTrainingMappings = new List<StudentTrainingMapping>() { };
            try
            {
                _oStudentTrainingMappings = _oStudentTrainingMappingService.Gets(this.GeneralSearchMakeSQL(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
            }
            catch (Exception ex)
            {
                _oStudentTrainingMapping = new StudentTrainingMapping();
                _oStudentTrainingMapping.ErrorMessage = ex.Message;
                _oStudentTrainingMappings.Add(_oStudentTrainingMapping);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oStudentTrainingMappings);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        private string GeneralSearchMakeSQL(string sString)
        {
            int nCount = 0;
            string sVal = sString.Split('~')[nCount++];
            bool isDate = Convert.ToBoolean(sString.Split('~')[nCount++]);
            string sStartDate = sString.Split('~')[nCount++];
            string sEndDate = sString.Split('~')[nCount++];

            string sTable = "SELECT * FROM MOB.View_StudentTrainingMapping WHERE StudentTrainingMappingID IN (SELECT StudentTrainingMappingID FROM MOB.View_StudentTrainingMapping WHERE ";
            string commonSQL1 = " BankName LIKE '%" + sVal + "%' OR"
                    + " BankAccountName LIKE '%" + sVal + "%' OR"
                    + " BankAccountNo LIKE '%" + sVal + "%' OR"
                    + " Remarks LIKE '%" + sVal + "%' OR"
                    + " DepositCode LIKE '%" + sVal + "%' OR"
                    + " ApprovedBy LIKE '%" + sVal + "%' OR"
                    + " BranchName LIKE '%" + sVal + "%' OR"
                    + " RefNo LIKE '%" + sVal + "%'";
            string commonSQL2 = " CAST(DepositDate AS DATE) BETWEEN '" + GlobalHelpers.GetDateSt(sStartDate) + "' AND '" + GlobalHelpers.GetDateSt(sEndDate) + "'";
            string finalSQL = sTable + (isDate ? commonSQL2 : commonSQL1) + ") ";
            if ((int)Session[GlobalSession.BranchID] != 1) finalSQL = finalSQL + " AND BranchID = " + (int)Session[GlobalSession.BranchID];
            return finalSQL + " ORDER BY DepositDate DESC";
        }
    }
}