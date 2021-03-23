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
    public class CompanyInfoController : Controller
    {
        CompanyInfo _oCompanyInfo = new CompanyInfo();
        List<CompanyInfo> _oCompanyInfos = new List<CompanyInfo>();
        CompanyInfoService _oCompanyInfoService = new CompanyInfoService();
        public ActionResult ViewCompanyInfos()
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oCompanyInfos = _oCompanyInfoService.Gets("", (string)Session[GlobalSession.UserID]);
            ViewBag.CompanyInfos = _oCompanyInfos;
            return View();
        }
        [HttpPost]
        public JsonResult Save(CompanyInfo obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            try
            {
                if (obj.CompanyID == 0)
                {
                    _oCompanyInfo = _oCompanyInfoService.IUD(obj, EnumDBOperation.Insert, (string)Session[GlobalSession.UserID]); ;
                }
                else
                {
                    _oCompanyInfo = _oCompanyInfoService.IUD(obj, EnumDBOperation.Update, (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oCompanyInfo = new CompanyInfo();
                _oCompanyInfo.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oCompanyInfo);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(CompanyInfo obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oCompanyInfo = new CompanyInfo();
            try
            {
                if (obj.CompanyID > 0)
                {
                    _oCompanyInfo = _oCompanyInfoService.IUD(obj, EnumDBOperation.Delete, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oCompanyInfo.ErrorMessage = FeedbackMessage.NoIdFound;
                }
            }
            catch (Exception ex)
            {
                _oCompanyInfo = new CompanyInfo();
                _oCompanyInfo.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oCompanyInfo);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Search(CompanyInfo obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oCompanyInfo = new CompanyInfo();
            _oCompanyInfos = new List<CompanyInfo>() { };
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    _oCompanyInfos = _oCompanyInfoService.Gets(this.GeneralSearchMakeSQL(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oCompanyInfo = new CompanyInfo();
                _oCompanyInfo.ErrorMessage = ex.Message;
                _oCompanyInfos.Add(_oCompanyInfo);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oCompanyInfos);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        private string GeneralSearchMakeSQL(string sVal)
        {
            return "SELECT * FROM COM.CompanyInfo WHERE "
                                + " CompanyName LIKE '%" + sVal + "%' OR"
                                + " CompanyCode LIKE '%" + sVal + "%' OR"
                                + " OriginName LIKE '%" + sVal + "%'";
        }
    }
}