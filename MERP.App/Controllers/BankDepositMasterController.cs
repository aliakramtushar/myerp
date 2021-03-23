using MERP.Engine.GlobalClass;
using MERP.Models;
using MERP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MERP.App.Controllers
{
    public class BankDepositMasterController : Controller
    {
        BankDepositMaster _oBankDepositMaster = new BankDepositMaster();
        List<BankDepositMaster> _oBankDepositMasters = new List<BankDepositMaster>();
        BankDepositMasterService _oBankDepositMasterService = new BankDepositMasterService();
        public ActionResult ViewBankDepositMasters()
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oBankDepositMasters = _oBankDepositMasterService.Gets("SELECT TOP(200) * FROM MOB.View_BankDepositMaster WHERE Status IN (" + (int)EnumStatus.Initialized + "," + (int)EnumStatus.Rejected + ")", (string)Session[GlobalSession.UserID]);
            ViewBag.BankDepositMasters = _oBankDepositMasters;
            return View();
        }
        public ActionResult ViewBankDepositMasterApproved()
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oBankDepositMasters = _oBankDepositMasterService.Gets("SELECT * FROM MOB.View_BankDepositMaster WHERE Status = " + (int)EnumStatus.Approved, (string)Session[GlobalSession.UserID]);
            ViewBag.BankDepositMasters = _oBankDepositMasters;
            return View();
        }
        public ActionResult ViewBankDepositMaster(int nID)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oBankDepositMaster = new BankDepositMaster();
            if (nID > 0)
            {
                _oBankDepositMaster = _oBankDepositMasterService.Get(nID, (string)Session[GlobalSession.UserID]);
            }
            else
            {
                _oBankDepositMaster.ErrorMessage = FeedbackMessage.NoDataFound;
            }
            List<Branch> oBranchs = new List<Branch>();
            BranchService oBranchService = new BranchService();
            oBranchs = oBranchService.Gets("", (string)Session[GlobalSession.UserID]);

            List<BankAccountMapping> oBankAccountMappings = new List<BankAccountMapping>();
            BankAccountMappingService oBankAccountMappingService = new BankAccountMappingService();
            oBankAccountMappings = oBankAccountMappingService.GetsActiveBankAccountMappings((string)Session[GlobalSession.UserID]);

            List<BusinessUnit> oBusinessUnits = new List<BusinessUnit>();
            BusinessUnitService oBusinessUnitService = new BusinessUnitService();
            oBusinessUnits = oBusinessUnitService.Gets("SELECT * FROM MOB.View_BusinessUnit WHERE IsInActive = 0 AND IsManual = 1 ORDER BY BusinessUnitName", (string)Session[GlobalSession.UserID]);

            BankDepositDetailService oBankDepositDetailService = new BankDepositDetailService();
            _oBankDepositMaster.BankDepositDetails = oBankDepositDetailService.GetsByParentID(_oBankDepositMaster.BankDepositMasterID, (string)Session[GlobalSession.UserID]);

            ViewBag.Branchs = oBranchs;
            ViewBag.PaymentMedias = Enum.GetValues(typeof(EnumPaymentMedia)).Cast<EnumPaymentMedia>().Select(e => new EnumClass { MModelPK = ((int)e), MModelString = e.ToString() });
            ViewBag.BankAccountMappings = oBankAccountMappings;
            ViewBag.BusinessUnits = oBusinessUnits;
            ViewBag.BankDepositMaster = _oBankDepositMaster;
            return View();
        }
        [HttpPost]
        public JsonResult Save(BankDepositMaster obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            try
            {
                if (obj.BankDepositMasterID == 0)
                {
                    _oBankDepositMaster = _oBankDepositMasterService.IUD(obj, EnumDBOperation.Insert, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oBankDepositMaster = _oBankDepositMasterService.IUD(obj, EnumDBOperation.Update, (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oBankDepositMaster = new BankDepositMaster();
                _oBankDepositMaster.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oBankDepositMaster);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Approve(BankDepositMaster obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            try
            {
                if (obj.BankDepositMasterID > 0 && obj.Status != EnumStatus.Approved)
                {
                    _oBankDepositMaster = _oBankDepositMasterService.IUD(obj, EnumDBOperation.Approve, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oBankDepositMaster.ErrorMessage = "This is not in Approval State !";
                }
            }
            catch (Exception ex)
            {
                _oBankDepositMaster = new BankDepositMaster();
                _oBankDepositMaster.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oBankDepositMaster);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Reject(BankDepositMaster obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            try
            {
                if (obj.BankDepositMasterID > 0 && obj.Status == EnumStatus.Approved)
                {
                    _oBankDepositMaster = _oBankDepositMasterService.IUD(obj, EnumDBOperation.Reject, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oBankDepositMaster.ErrorMessage = "This is not Approved !";
                }
            }
            catch (Exception ex)
            {
                _oBankDepositMaster = new BankDepositMaster();
                _oBankDepositMaster.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oBankDepositMaster);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(BankDepositMaster obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oBankDepositMaster = new BankDepositMaster();
            try
            {
                if (obj.BankDepositMasterID > 0)
                {
                    _oBankDepositMaster.ErrorMessage = _oBankDepositMasterService.Delete(obj.BankDepositMasterID, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oBankDepositMaster.ErrorMessage = FeedbackMessage.NoIdFound;
                }
            }
            catch (Exception ex)
            {
                _oBankDepositMaster = new BankDepositMaster();
                _oBankDepositMaster.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oBankDepositMaster);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetChildListByID(BankDepositDetail obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            BankDepositDetail oBankDepositDetail = new BankDepositDetail();
            List<BankDepositDetail> oBankDepositDetails = new List<BankDepositDetail>();
            BankDepositDetailService oBankDepositDetailService = new BankDepositDetailService();
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    oBankDepositDetails = oBankDepositDetailService.GetsByParentID(Convert.ToInt32(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                oBankDepositDetail = new BankDepositDetail();
                oBankDepositDetail.ErrorMessage = ex.Message;
                oBankDepositDetails.Add(oBankDepositDetail);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(oBankDepositDetails);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Search(BankDepositMaster obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oBankDepositMaster = new BankDepositMaster();
            _oBankDepositMasters = new List<BankDepositMaster>() { };
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    _oBankDepositMasters = _oBankDepositMasterService.Gets(this.GeneralSearchMakeSQL(obj.ErrorMessage, ""), (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oBankDepositMaster = new BankDepositMaster();
                _oBankDepositMaster.ErrorMessage = ex.Message;
                _oBankDepositMasters.Add(_oBankDepositMaster);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oBankDepositMasters);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SearchOnlyApproved(BankDepositMaster obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oBankDepositMaster = new BankDepositMaster();
            _oBankDepositMasters = new List<BankDepositMaster>() { };
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    _oBankDepositMasters = _oBankDepositMasterService.Gets(this.GeneralSearchMakeSQL(obj.ErrorMessage, "1"), (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oBankDepositMaster = new BankDepositMaster();
                _oBankDepositMaster.ErrorMessage = ex.Message;
                _oBankDepositMasters.Add(_oBankDepositMaster);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oBankDepositMasters);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        private string GeneralSearchMakeSQL(string sVal, string sType)
        {
            string sTable = "SELECT TOP(200)* FROM MOB.View_BankDepositMaster WHERE ";

            string commonSQL = "BankName LIKE '%" + sVal + "%' OR"
                                + " BankBranchName LIKE '%" + sVal + "%' OR"
                                + " BankAccountName LIKE '%" + sVal + "%' OR"
                                + " BankAccountNo LIKE '%" + sVal + "%' OR"
                                + " Remarks LIKE '%" + sVal + "%' OR"
                                + " RefNo LIKE '%" + sVal + "%' OR"
                                + " CAST(DepositDate AS DATE) LIKE '%" + (sVal) + "%'";
            string finalSQL = sTable + commonSQL;
            if (sType != "")
            {
                finalSQL = sTable + "Status IN (" + sType + ") AND (" + commonSQL + ")";
            }
            return finalSQL;
        }
    }
}