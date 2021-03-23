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
    public class NWRDirectSalesDetailController : Controller
    {
        NWRDirectSalesDetail _oNWRDirectSalesDetail = new NWRDirectSalesDetail();
        List<NWRDirectSalesDetail> _oNWRDirectSalesDetails = new List<NWRDirectSalesDetail>();
        NWRDirectSalesDetailService _oNWRDirectSalesDetailService = new NWRDirectSalesDetailService();
        public ActionResult ViewSalesSummary()
        {
            GlobalSession.SessionIsAlive(Session, Response);
            NWRDirectSalesDetail oNWRDirectSalesDetail = new NWRDirectSalesDetail();
            List<NWRDirectSalesDetail> oNWRDirectSalesDetails = new List<NWRDirectSalesDetail>();
            NWRDirectSalesDetailService oNWRDirectSalesDetailService = new NWRDirectSalesDetailService();

            oNWRDirectSalesDetails = oNWRDirectSalesDetailService.GetsByBranchID((int)Session[GlobalSession.BranchID], (string)Session[GlobalSession.UserID]);
            ViewBag.NWRDirectSalesDetails = oNWRDirectSalesDetails;
            return View();
        }

        [HttpPost]
        public JsonResult Search(NWRDirectSalesDetail obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oNWRDirectSalesDetail = new NWRDirectSalesDetail();
            _oNWRDirectSalesDetails = new List<NWRDirectSalesDetail>() { };
            try
            {
                _oNWRDirectSalesDetails = _oNWRDirectSalesDetailService.Gets(this.GeneralSearchMakeSQL(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
            }
            catch (Exception ex)
            {
                _oNWRDirectSalesDetail = new NWRDirectSalesDetail();
                _oNWRDirectSalesDetail.ErrorMessage = ex.Message;
                _oNWRDirectSalesDetails.Add(_oNWRDirectSalesDetail);
            }
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //string sjson = serializer.Serialize(_oNWRDirectSalesDetails);
            //return Json(sjson, JsonRequestBehavior.AllowGet);

            var jsonResult = Json(_oNWRDirectSalesDetails, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        private string GeneralSearchMakeSQL(string sString)
        {
            int nCount = 0;
            string sVal = sString.Split('~')[nCount++];
            bool isDate = Convert.ToBoolean(sString.Split('~')[nCount++]);
            string sStartDate = sString.Split('~')[nCount++];
            string sEndDate = sString.Split('~')[nCount++];

            string sTable = "SELECT * FROM MOB.View_NWRDirectSalesDetail WHERE RID IN (SELECT RID FROM MOB.View_NWRDirectSalesDetail WHERE ";
            string commonSQL1 = " LSOCode LIKE '%" + sVal + "%' OR"
                                + " InvoiceNo LIKE '%" + sVal + "%' OR"
                                + " AddedBy LIKE '%" + sVal + "%' OR"
                                + " BranchName LIKE '%" + sVal + "%' OR"
                                + " ProductName LIKE '%" + sVal + "%' OR"
                                + " SKUName LIKE '%" + sVal + "%' OR"
                                + " BusinessUnitName LIKE '%" + sVal + "%'";
            string commonSQL2 = " CAST(InvoiceDate AS DATE) BETWEEN '" + GlobalHelpers.GetDateSt(sStartDate) + "' AND '" + GlobalHelpers.GetDateSt(sEndDate) + "'";
            string finalSQL = sTable + (isDate ? commonSQL2 : commonSQL1) + ") ";
            if((int)Session[GlobalSession.BranchID] != 1) finalSQL = finalSQL + " AND BranchID = " + (int)Session[GlobalSession.BranchID];
            return finalSQL;
        }
    }
}