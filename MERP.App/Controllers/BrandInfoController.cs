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
    public class BrandInfoController : Controller
    {
        BrandInfo _oBrandInfo = new BrandInfo();
        List<BrandInfo> _oBrandInfos = new List<BrandInfo>();
        BrandInfoService _oBrandInfoService = new BrandInfoService();
        public ActionResult ViewBrandInfos()
        {
            GlobalSession.SessionIsAlive(Session, Response);
            List<OriginInfo> oOriginInfos = new List<OriginInfo>();
            OriginInfoService oOriginInfoService = new OriginInfoService();
            oOriginInfos = oOriginInfoService.Gets("", (string)Session[GlobalSession.UserID]);
            _oBrandInfos = _oBrandInfoService.Gets("", (string)Session[GlobalSession.UserID]);
            ViewBag.OriginInfos = oOriginInfos;
            ViewBag.BrandInfos = _oBrandInfos;
            return View();
        }
        [HttpPost]
        public JsonResult Save(BrandInfo obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            try
            {
                if (obj.BrandID == 0)
                {
                    _oBrandInfo = _oBrandInfoService.IUD(obj, EnumDBOperation.Insert, (string)Session[GlobalSession.UserID]); ;
                }
                else
                {
                    _oBrandInfo = _oBrandInfoService.IUD(obj, EnumDBOperation.Update, (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oBrandInfo = new BrandInfo();
                _oBrandInfo.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oBrandInfo);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(BrandInfo obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oBrandInfo = new BrandInfo();
            try
            {
                if (obj.BrandID > 0)
                {
                    _oBrandInfo.ErrorMessage = _oBrandInfoService.Delete(obj.BrandID, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oBrandInfo.ErrorMessage = FeedbackMessage.NoIdFound;
                }
            }
            catch (Exception ex)
            {
                _oBrandInfo = new BrandInfo();
                _oBrandInfo.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oBrandInfo);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Search(BrandInfo obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oBrandInfo = new BrandInfo();
            _oBrandInfos = new List<BrandInfo>() { };
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    _oBrandInfos = _oBrandInfoService.Gets(this.GeneralSearchMakeSQL(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oBrandInfo = new BrandInfo();
                _oBrandInfo.ErrorMessage = ex.Message;
                _oBrandInfos.Add(_oBrandInfo);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oBrandInfos);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        private string GeneralSearchMakeSQL(string sVal)
        {
            return "SELECT * FROM COM.View_BrandInfo WHERE "
                                + " BrandName LIKE '%" + sVal + "%' OR"
                                + " BrandCode LIKE '%" + sVal + "%' OR"
                                + " OriginName LIKE '%" + sVal + "%'"; 
        }
    }
}