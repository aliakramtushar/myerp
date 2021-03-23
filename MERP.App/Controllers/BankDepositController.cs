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
    public class BankDepositController : Controller
    {
        BankDeposit _oBankDeposit = new BankDeposit();
        List<BankDeposit> _oBankDeposits = new List<BankDeposit>();
        BankDepositService _oBankDepositService = new BankDepositService();
        public ActionResult ViewBankDeposits()
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oBankDeposits = _oBankDepositService.Gets((int)Session[GlobalSession.BranchID], EnumStatus.Initialized, (string)Session[GlobalSession.UserID]);
            ViewBag.BankDeposits = _oBankDeposits;
            return View();
        }
        public ActionResult ViewBankDeposit(int nID)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oBankDeposit = new BankDeposit();
            List<Branch> oBranchs = new List<Branch>();
            BranchService oBranchService = new BranchService();
            oBranchs = oBranchService.Gets("", (string)Session[GlobalSession.UserID]);

            List<BankAccountMapping> oBankAccountMappings = new List<BankAccountMapping>();
            BankAccountMappingService oBankAccountMappingService = new BankAccountMappingService();
            oBankAccountMappings = oBankAccountMappingService.GetsActiveBankAccountMappings((string)Session[GlobalSession.UserID]);

            NWRDirectSalesDetailService oNWRDirectSalesDetailService = new NWRDirectSalesDetailService();
            _oBankDeposit.NWRDirectSalesDetails = oNWRDirectSalesDetailService.GetsByParentID(_oBankDeposit.BankDepositID, (string)Session[GlobalSession.UserID]);

            ViewBag.Branchs = oBranchs;
            ViewBag.PaymentMedias = Enum.GetValues(typeof(EnumPaymentMedia)).Cast<EnumPaymentMedia>().Select(e => new EnumClass { MModelPK = ((int)e), MModelString = e.ToString() });
            ViewBag.BankAccountMappings = oBankAccountMappings;
            ViewBag.BankDeposit = _oBankDeposit;
            return View();
        }
        public ActionResult ViewBankDepositEdit(int nID)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oBankDeposit = new BankDeposit();
            if (nID > 0)
            {
                _oBankDeposit = _oBankDepositService.Get(nID, (string)Session[GlobalSession.UserID]);
            }
            else
            {
                _oBankDeposit.ErrorMessage = FeedbackMessage.NoDataFound;
            }
            List<Branch> oBranchs = new List<Branch>();
            BranchService oBranchService = new BranchService();
            oBranchs = oBranchService.Gets("", (string)Session[GlobalSession.UserID]);

            List<BankAccountMapping> oBankAccountMappings = new List<BankAccountMapping>();
            BankAccountMappingService oBankAccountMappingService = new BankAccountMappingService();
            oBankAccountMappings = oBankAccountMappingService.GetsActiveBankAccountMappings((string)Session[GlobalSession.UserID]);

            NWRDirectSalesDetailService oNWRDirectSalesDetailService = new NWRDirectSalesDetailService();
            _oBankDeposit.NWRDirectSalesDetails = oNWRDirectSalesDetailService.GetsByParentID(_oBankDeposit.BankDepositID, (string)Session[GlobalSession.UserID]);

            ViewBag.Branchs = oBranchs;
            ViewBag.PaymentMedias = Enum.GetValues(typeof(EnumPaymentMedia)).Cast<EnumPaymentMedia>().Select(e => new EnumClass { MModelPK = ((int)e), MModelString = e.ToString() });
            ViewBag.BankAccountMappings = oBankAccountMappings;
            ViewBag.BankDeposit = _oBankDeposit;
            return View();
        }
        public ActionResult ViewBankDepositManualLSOEntry()
        {
            GlobalSession.SessionIsAlive(Session, Response);
            return View();
        }
        [HttpPost]
        public JsonResult Save(BankDeposit obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            try
            {
                if (obj.BankDepositID == 0)
                {
                    _oBankDeposit = _oBankDepositService.IUD(obj, EnumDBOperation.Insert, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oBankDeposit = _oBankDepositService.IUD(obj, EnumDBOperation.Update, (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oBankDeposit = new BankDeposit();
                _oBankDeposit.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oBankDeposit);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Approve(BankDeposit obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            try
            {
                if (obj.BankDepositID > 0 && obj.Status != EnumStatus.Approved)
                {
                    _oBankDeposit = _oBankDepositService.IUD(obj, EnumDBOperation.Approve, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oBankDeposit.ErrorMessage = "This is not in Approval State !";
                }
            }
            catch (Exception ex)
            {
                _oBankDeposit = new BankDeposit();
                _oBankDeposit.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oBankDeposit);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Reject(BankDeposit obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            try
            {
                if (obj.BankDepositID > 0 && obj.Status == EnumStatus.Approved)
                {
                    _oBankDeposit = _oBankDepositService.IUD(obj, EnumDBOperation.Reject, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oBankDeposit.ErrorMessage = "This is not Approved !";
                }
            }
            catch (Exception ex)
            {
                _oBankDeposit = new BankDeposit();
                _oBankDeposit.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oBankDeposit);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(BankDeposit obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oBankDeposit = new BankDeposit();
            try
            {
                if (obj.BankDepositID > 0)
                {
                    _oBankDeposit.ErrorMessage = _oBankDepositService.Delete(obj.BankDepositID, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oBankDeposit.ErrorMessage = FeedbackMessage.NoIdFound;
                }
            }
            catch (Exception ex)
            {
                _oBankDeposit = new BankDeposit();
                _oBankDeposit.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oBankDeposit);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetsNonDepositedSales(BankDeposit obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            List<NWRDirectSalesDetail> oNWRDirectSalesDetails = new List<NWRDirectSalesDetail>();
            NWRDirectSalesDetailService oNWRDirectSalesDetailService = new NWRDirectSalesDetailService();
            CultureInfo culture = new CultureInfo("en-US");
            try
            {
                int nCount = 0;
                if (!String.IsNullOrEmpty(obj.ErrorMessage))
                {
                    int nBankAccountMappingID = Convert.ToInt32(obj.ErrorMessage.Split('~')[nCount++]);
                    string sStartDate = obj.ErrorMessage.Split('~')[nCount++];
                    string sEndDate = obj.ErrorMessage.Split('~')[nCount++];
                    oNWRDirectSalesDetails = oNWRDirectSalesDetailService.GetsNonDepositedSales(nBankAccountMappingID, GlobalHelpers.GetDate(sStartDate), GlobalHelpers.GetDate(sEndDate), (int)Session[GlobalSession.BranchID], (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                NWRDirectSalesDetail oNWRDirectSalesDetail = new NWRDirectSalesDetail();
                oNWRDirectSalesDetail.ErrorMessage = ex.Message;
                oNWRDirectSalesDetails = new List<NWRDirectSalesDetail>();
                oNWRDirectSalesDetails.Add(oNWRDirectSalesDetail);
            }
            var jsonResult = Json(oNWRDirectSalesDetails, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [HttpPost]
        public JsonResult GetsSaleProjectWise(NWRDirectSalesDetail obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            BusinessUnit oBusinessUnit = new BusinessUnit();
            BusinessUnitService oBusinessUnitService = new BusinessUnitService();

            List<BusinessUnit> oBusinessUnits = new List<BusinessUnit>();
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    oBusinessUnits = oBusinessUnitService.Gets("SELECT BusinessUnitID, BusinessUnitName, SUM(CollectedAmount) AS 'Remarks' from MOB.View_NWRDirectSalesDetail WHERE BankDepositID = " + obj.ErrorMessage + " GROUP BY BusinessUnitID, BusinessUnitName", (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                oBusinessUnit = new BusinessUnit();
                oBusinessUnit.ErrorMessage = ex.Message;
                oBusinessUnits.Add(oBusinessUnit);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(oBusinessUnits);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetNWRSalesDetailsByLSO(NWRDirectSalesDetail obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            NWRDirectSalesDetail oNWRDirectSalesDetail = new NWRDirectSalesDetail();
            List<NWRDirectSalesDetail> oNWRDirectSalesDetails = new List<NWRDirectSalesDetail>();
            NWRDirectSalesDetailService oNWRDirectSalesDetailService = new NWRDirectSalesDetailService();
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    oNWRDirectSalesDetails = oNWRDirectSalesDetailService.GetNWRSalesDetailsByLSO(obj.ErrorMessage, (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                oNWRDirectSalesDetail = new NWRDirectSalesDetail();
                oNWRDirectSalesDetail.ErrorMessage = ex.Message;
                oNWRDirectSalesDetails.Add(oNWRDirectSalesDetail);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(oNWRDirectSalesDetails);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GenerateNWRSalesDetailsByLSO(NWRDirectSalesDetail obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            NWRDirectSalesDetail oNWRDirectSalesDetail = new NWRDirectSalesDetail();
            List<NWRDirectSalesDetail> oNWRDirectSalesDetails = new List<NWRDirectSalesDetail>();
            NWRDirectSalesDetailService oNWRDirectSalesDetailService = new NWRDirectSalesDetailService();
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    oNWRDirectSalesDetails = oNWRDirectSalesDetailService.GenerateNWRSalesDetailsByLSO(obj.ErrorMessage, (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                oNWRDirectSalesDetail = new NWRDirectSalesDetail();
                oNWRDirectSalesDetail.ErrorMessage = ex.Message;
                oNWRDirectSalesDetails.Add(oNWRDirectSalesDetail);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(oNWRDirectSalesDetails);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public JsonResult GetChildListByID(NWRDirectSalesDetail obj)
        //{
        //    GlobalSession.SessionIsAlive(Session, Response);
        //    NWRDirectSalesDetail oNWRDirectSalesDetail = new NWRDirectSalesDetail();
        //    List<NWRDirectSalesDetail> oNWRDirectSalesDetails = new List<NWRDirectSalesDetail>();
        //    NWRDirectSalesDetailService oNWRDirectSalesDetailService = new NWRDirectSalesDetailService();
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(obj.ErrorMessage))
        //        {
        //            oNWRDirectSalesDetails = oNWRDirectSalesDetailService.GetsByParentID(Convert.ToInt32(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        oNWRDirectSalesDetail = new NWRDirectSalesDetail();
        //        oNWRDirectSalesDetail.ErrorMessage = ex.Message;
        //        oNWRDirectSalesDetails.Add(oNWRDirectSalesDetail);
        //    }
        //    JavaScriptSerializer serializer = new JavaScriptSerializer();
        //    string sjson = serializer.Serialize(oNWRDirectSalesDetails);
        //    return Json(sjson, JsonRequestBehavior.AllowGet);
        //}
        [HttpPost]
        public JsonResult Search(BankDeposit obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oBankDeposit = new BankDeposit();
            _oBankDeposits = new List<BankDeposit>() { };
            try
            {
                _oBankDeposits = _oBankDepositService.Gets(this.GeneralSearchMakeSQL(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
            }
            catch (Exception ex)
            {
                _oBankDeposit = new BankDeposit();
                _oBankDeposit.ErrorMessage = ex.Message;
                _oBankDeposits.Add(_oBankDeposit);
            }
            var jsonResult = Json(_oBankDeposits, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [HttpPost]
        public JsonResult SearchNWRDirectSalesDetail(BankDeposit obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oBankDeposit = new BankDeposit();
            _oBankDeposits = new List<BankDeposit>() { };
            try
            {
                _oBankDeposits = _oBankDepositService.Gets(this.GeneralSearchMakeSQL(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
            }
            catch (Exception ex)
            {
                _oBankDeposit = new BankDeposit();
                _oBankDeposit.ErrorMessage = ex.Message;
                _oBankDeposits.Add(_oBankDeposit);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oBankDeposits);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        private string GeneralSearchMakeSQL(string sString)
        {
            int nCount = 0;
            string sVal = sString.Split('~')[nCount++];
            bool isDate = Convert.ToBoolean(sString.Split('~')[nCount++]);
            string sStartDate = sString.Split('~')[nCount++];
            string sEndDate = sString.Split('~')[nCount++];

            string sTable = "SELECT * FROM MOB.View_BankDeposit WHERE BankDepositID IN (SELECT BankDepositID FROM MOB.View_BankDeposit WHERE ";
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
            //string sTable = "SELECT * FROM MOB.View_BankDeposit WHERE BankDepositID IN (SELECT BankDepositID FROM MOB.View_BankDeposit WHERE ";

            //string commonSQL = " BankName LIKE '%" + sVal + "%' OR"
            //                    + " BankAccountName LIKE '%" + sVal + "%' OR"
            //                    + " BankAccountNo LIKE '%" + sVal + "%' OR"
            //                    + " Remarks LIKE '%" + sVal + "%' OR"
            //                    + " ApprovedBy LIKE '%" + sVal + "%' OR"
            //                    + " RefNo LIKE '%" + sVal + "%'";
            //try
            //{
            //    DateTime dt = Convert.ToDateTime(sVal);
            //    commonSQL = commonSQL + " OR CAST(DepositDate AS DATE) = '" + (sVal) + "'";
            //    commonSQL = commonSQL + " OR CAST(ApprovedByDate AS DATE) = '" + (sVal) + "'";
            //}
            //catch { }
            //try
            //{
            //    commonSQL = commonSQL + " OR Status LIKE '%" + (int)((EnumStatus)Enum.Parse(typeof(EnumStatus), sVal)) + "%'";
            //}
            //catch { }
            //string finalSQL = sTable + commonSQL + ")";
            //if ((int)Session[GlobalSession.BranchID] != 1) finalSQL = finalSQL + " AND BranchID = " + (int)Session[GlobalSession.BranchID];
            //return finalSQL;
        }
    }
}