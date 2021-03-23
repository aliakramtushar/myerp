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
    public class BankAccountMappingController : Controller
    {
        BankAccountMapping _oBankAccountMapping = new BankAccountMapping();
        List<BankAccountMapping> _oBankAccountMappings = new List<BankAccountMapping>();
        BankAccountMappingService _oBankAccountMappingService = new BankAccountMappingService();
        public ActionResult ViewBankAccountMappings()
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oBankAccountMappings = _oBankAccountMappingService.Gets("", (string)Session[GlobalSession.UserID]);
            ViewBag.BankAccountMappings = _oBankAccountMappings;
            return View();
        }
        public ActionResult ViewBankAccountMapping(int nID)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oBankAccountMapping = new BankAccountMapping();
            if (nID > 0)
            {
                _oBankAccountMapping = _oBankAccountMappingService.Get(nID, (string)Session[GlobalSession.UserID]);
            }
            else
            {
                _oBankAccountMapping.ErrorMessage = FeedbackMessage.NoDataFound;
            }
            List<Bank> oBanks = new List<Bank>();
            BankService oBankService = new BankService();
            oBanks = oBankService.Gets("", (string)Session[GlobalSession.UserID]);

            ViewBag.BankAccountMapping = _oBankAccountMapping;
            ViewBag.Banks = oBanks;
            return View();
        }
        [HttpPost]
        public JsonResult Save(BankAccountMapping obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            try
            {
                if (obj.BankAccountMappingID == 0)
                {
                    _oBankAccountMapping = _oBankAccountMappingService.IUD(obj, EnumDBOperation.Insert, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oBankAccountMapping = _oBankAccountMappingService.IUD(obj, EnumDBOperation.Update, (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oBankAccountMapping = new BankAccountMapping();
                _oBankAccountMapping.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oBankAccountMapping);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(BankAccountMapping obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oBankAccountMapping = new BankAccountMapping();
            try
            {
                if (obj.BankAccountMappingID > 0)
                {
                    _oBankAccountMapping.ErrorMessage = _oBankAccountMappingService.Delete(obj.BankAccountMappingID, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oBankAccountMapping.ErrorMessage = FeedbackMessage.NoIdFound;
                }
            }
            catch (Exception ex)
            {
                _oBankAccountMapping = new BankAccountMapping();
                _oBankAccountMapping.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oBankAccountMapping);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Search(BankAccountMapping obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oBankAccountMapping = new BankAccountMapping();
            _oBankAccountMappings = new List<BankAccountMapping>() { };
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    _oBankAccountMappings = _oBankAccountMappingService.Gets(this.GeneralSearchMakeSQL(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oBankAccountMapping = new BankAccountMapping();
                _oBankAccountMapping.ErrorMessage = ex.Message;
                _oBankAccountMappings.Add(_oBankAccountMapping);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oBankAccountMappings);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        private string GeneralSearchMakeSQL(string sVal)
        {
            return "SELECT * FROM MOB.View_BankAccountMapping WHERE BankBranchName LIKE '%" + sVal + "%' OR"
                                + " BankAccountName LIKE '%" + sVal + "%' OR"
                                + " BankAccountNo LIKE '%" + sVal + "%' OR"
                                + " Remarks LIKE '%" + sVal + "%' OR"
                                + " BankName LIKE '%" + sVal + "%'";

        }
    }
}