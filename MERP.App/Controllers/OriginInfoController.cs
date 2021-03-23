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
    public class OriginInfoController : Controller
    {
        OriginInfo _oOriginInfo = new OriginInfo();
        List<OriginInfo> _oOriginInfos = new List<OriginInfo>();
        OriginInfoService _oOriginInfoService = new OriginInfoService();
        public ActionResult ViewOriginInfos()
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oOriginInfos = _oOriginInfoService.Gets("", (string)Session[GlobalSession.UserID]);
            ViewBag.OriginInfos = _oOriginInfos;
            return View();
        }
        [HttpPost]
        public JsonResult Save(OriginInfo obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            try
            {
                if (obj.OriginID == 0)
                {
                    _oOriginInfo = _oOriginInfoService.IUD(obj, EnumDBOperation.Insert, (string)Session[GlobalSession.UserID]); ;
                }
                else
                {
                    _oOriginInfo = _oOriginInfoService.IUD(obj, EnumDBOperation.Update, (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oOriginInfo = new OriginInfo();
                _oOriginInfo.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oOriginInfo);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(OriginInfo obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oOriginInfo = new OriginInfo();
            try
            {
                if (obj.OriginID > 0)
                {
                    _oOriginInfo.ErrorMessage = _oOriginInfoService.Delete(obj.OriginID, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oOriginInfo.ErrorMessage = FeedbackMessage.NoIdFound;
                }
            }
            catch (Exception ex)
            {
                _oOriginInfo = new OriginInfo();
                _oOriginInfo.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oOriginInfo);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Search(OriginInfo obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oOriginInfo = new OriginInfo();
            _oOriginInfos = new List<OriginInfo>() { };
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    _oOriginInfos = _oOriginInfoService.Gets(this.GeneralSearchMakeSQL(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oOriginInfo = new OriginInfo();
                _oOriginInfo.ErrorMessage = ex.Message;
                _oOriginInfos.Add(_oOriginInfo);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oOriginInfos);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        private string GeneralSearchMakeSQL(string sVal)
        {
            return "SELECT * FROM COM.OriginInfo WHERE "
                                + " OriginName LIKE '%" + sVal + "%' OR"
                                + " OriginCode LIKE '%" + sVal + "%'";

        }
    }
}