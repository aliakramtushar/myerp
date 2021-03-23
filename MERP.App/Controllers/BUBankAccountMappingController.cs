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
    public class BUBankAccountMappingController : Controller
    {
        BUBankAccountMapping _oBUBankAccountMapping = new BUBankAccountMapping();
        List<BUBankAccountMapping> _oBUBankAccountMappings = new List<BUBankAccountMapping>();
        BUBankAccountMappingService _oBUBankAccountMappingService = new BUBankAccountMappingService();
        public ActionResult ViewBUBankAccountMappings()
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oBUBankAccountMappings = _oBUBankAccountMappingService.Gets("", (string)Session[GlobalSession.UserID]);

            List<BusinessUnit> oBusinessUnits = new List<BusinessUnit>();
            BusinessUnitService oBusinessUnitService = new BusinessUnitService();
            oBusinessUnits = oBusinessUnitService.Gets("", (string)Session[GlobalSession.UserID]);

            List<BankAccountMapping> oBankAccountMappings = new List<BankAccountMapping>();
            BankAccountMappingService oBankAccountMappingService = new BankAccountMappingService();
            oBankAccountMappings = oBankAccountMappingService.Gets("", (string)Session[GlobalSession.UserID]);

            ViewBag.BusinessUnits = oBusinessUnits;
            ViewBag.BankAccountMappings = oBankAccountMappings;
            ViewBag.BUBankAccountMappings = _oBUBankAccountMappings;
            return View();
        }
        [HttpPost]
        public JsonResult GetsByBankAccountMappingID(BUBankAccountMapping obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oBUBankAccountMapping = new BUBankAccountMapping();
            _oBUBankAccountMappings = new List<BUBankAccountMapping>() { };
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    _oBUBankAccountMappings = _oBUBankAccountMappingService.GetsByBankAccountMappingID(Convert.ToInt32((obj.ErrorMessage)), (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oBUBankAccountMapping = new BUBankAccountMapping();
                _oBUBankAccountMapping.ErrorMessage = ex.Message;
                _oBUBankAccountMappings.Add(_oBUBankAccountMapping);
            }
            var jsonResult = Json(_oBUBankAccountMappings, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [HttpPost]
        public JsonResult Save(BUBankAccountMapping obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            try
            {
                if (obj.BUBankAccountMappingID == 0)
                {
                    _oBUBankAccountMapping = _oBUBankAccountMappingService.IUD(obj, EnumDBOperation.Insert, (string)Session[GlobalSession.UserID]); ;
                }
                else
                {
                    _oBUBankAccountMapping = _oBUBankAccountMappingService.IUD(obj, EnumDBOperation.Update, (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oBUBankAccountMapping = new BUBankAccountMapping();
                _oBUBankAccountMapping.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oBUBankAccountMapping);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(BUBankAccountMapping obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oBUBankAccountMapping = new BUBankAccountMapping();
            try
            {
                if (obj.BUBankAccountMappingID > 0)
                {
                    _oBUBankAccountMapping.ErrorMessage = _oBUBankAccountMappingService.Delete(obj.BUBankAccountMappingID, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oBUBankAccountMapping.ErrorMessage = FeedbackMessage.NoIdFound;
                }
            }
            catch (Exception ex)
            {
                _oBUBankAccountMapping = new BUBankAccountMapping();
                _oBUBankAccountMapping.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oBUBankAccountMapping);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Search(BUBankAccountMapping obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oBUBankAccountMapping = new BUBankAccountMapping();
            _oBUBankAccountMappings = new List<BUBankAccountMapping>() { };
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    _oBUBankAccountMappings = _oBUBankAccountMappingService.Gets(this.GeneralSearchMakeSQL(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oBUBankAccountMapping = new BUBankAccountMapping();
                _oBUBankAccountMapping.ErrorMessage = ex.Message;
                _oBUBankAccountMappings.Add(_oBUBankAccountMapping);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oBUBankAccountMappings);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        private string GeneralSearchMakeSQL(string sVal)
        {
            return "SELECT * FROM MOB.View_BUBankAccountMapping WHERE "
                                + " BusinessUnitName LIKE '%" + sVal + "%' OR"
                                + " BankName LIKE '%" + sVal + "%' OR"
                                + " BankAccountName LIKE '%" + sVal + "%' OR"
                                + " BankAccountNo LIKE '%" + sVal + "%'";


        }
    }
}