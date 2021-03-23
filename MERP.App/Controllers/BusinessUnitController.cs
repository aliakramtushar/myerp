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
    public class BusinessUnitController : Controller
    {
        BusinessUnit _oBusinessUnit = new BusinessUnit();
        List<BusinessUnit> _oBusinessUnits = new List<BusinessUnit>();
        BusinessUnitService _oBusinessUnitService = new BusinessUnitService();
        public ActionResult ViewBusinessUnits()
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oBusinessUnits = _oBusinessUnitService.Gets("", (string)Session[GlobalSession.UserID]);
            ViewBag.BusinessUnits = _oBusinessUnits;
            return View();
        }
        public ActionResult ViewBusinessUnit(int nID)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oBusinessUnit = new BusinessUnit();
            if (nID > 0)
            {
                _oBusinessUnit = _oBusinessUnitService.Get(nID, (string)Session[GlobalSession.UserID]);
            }
            else
            {
                _oBusinessUnit.ErrorMessage = FeedbackMessage.NoDataFound;
            }
            List<CompanyInfo> oCompanyInfos = new List<CompanyInfo>();
            CompanyInfoService oCompanyInfoService = new CompanyInfoService();
            oCompanyInfos = oCompanyInfoService.GetsActiveCompanys((string)Session[GlobalSession.UserID]);

            ViewBag.BusinessUnit = _oBusinessUnit;
            ViewBag.CompanyInfos = oCompanyInfos;
            return View();
        }
        [HttpPost]
        public JsonResult Save(BusinessUnit obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            try
            {
                if (obj.BusinessUnitID == 0)
                {
                    _oBusinessUnit = _oBusinessUnitService.IUD(obj, EnumDBOperation.Insert, (string)Session[GlobalSession.UserID]); ;
                }
                else
                {
                    _oBusinessUnit = _oBusinessUnitService.IUD(obj, EnumDBOperation.Update, (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oBusinessUnit = new BusinessUnit();
                _oBusinessUnit.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oBusinessUnit);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(BusinessUnit obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oBusinessUnit = new BusinessUnit();
            try
            {
                if (obj.BusinessUnitID > 0)
                {
                    _oBusinessUnit = _oBusinessUnitService.IUD(obj, EnumDBOperation.Delete, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oBusinessUnit.ErrorMessage = FeedbackMessage.NoIdFound;
                }
            }
            catch (Exception ex)
            {
                _oBusinessUnit = new BusinessUnit();
                _oBusinessUnit.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oBusinessUnit);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Search(BusinessUnit obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oBusinessUnit = new BusinessUnit();
            _oBusinessUnits = new List<BusinessUnit>() { };
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    _oBusinessUnits = _oBusinessUnitService.Gets(this.GeneralSearchMakeSQL(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oBusinessUnit = new BusinessUnit();
                _oBusinessUnit.ErrorMessage = ex.Message;
                _oBusinessUnits.Add(_oBusinessUnit);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oBusinessUnits);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        private string GeneralSearchMakeSQL(string sVal)
        {
            return "SELECT * FROM MOB.View_BusinessUnit WHERE "
                                + " BusinessUnitName LIKE '%" + sVal + "%' OR"
                                + " Remarks LIKE '%" + sVal + "%'";

        }
    }
}