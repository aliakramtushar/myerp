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
    public class StoreInfoController : Controller
    {
        StoreInfo _oStoreInfo = new StoreInfo();
        List<StoreInfo> _oStoreInfos = new List<StoreInfo>();
        StoreInfoService _oStoreInfoService = new StoreInfoService();
        public ActionResult ViewStoreInfos()
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oStoreInfos = _oStoreInfoService.Gets("", (string)Session[GlobalSession.UserID]);
            ViewBag.StoreInfos = _oStoreInfos;
            return View();
        }
        [HttpPost]
        public JsonResult Save(StoreInfo obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            try
            {
                if (obj.StoreID == 0)
                {
                    _oStoreInfo = _oStoreInfoService.IUD(obj, EnumDBOperation.Insert, (string)Session[GlobalSession.UserID]); ;
                }
                else
                {
                    _oStoreInfo = _oStoreInfoService.IUD(obj, EnumDBOperation.Update, (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oStoreInfo = new StoreInfo();
                _oStoreInfo.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oStoreInfo);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(StoreInfo obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oStoreInfo = new StoreInfo();
            try
            {
                if (obj.StoreID > 0)
                {
                    _oStoreInfo = _oStoreInfoService.IUD(obj, EnumDBOperation.Delete, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oStoreInfo.ErrorMessage = FeedbackMessage.NoIdFound;
                }
            }
            catch (Exception ex)
            {
                _oStoreInfo = new StoreInfo();
                _oStoreInfo.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oStoreInfo);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Search(StoreInfo obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oStoreInfo = new StoreInfo();
            _oStoreInfos = new List<StoreInfo>() { };
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    _oStoreInfos = _oStoreInfoService.Gets(this.GeneralSearchMakeSQL(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oStoreInfo = new StoreInfo();
                _oStoreInfo.ErrorMessage = ex.Message;
                _oStoreInfos.Add(_oStoreInfo);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oStoreInfos);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        private string GeneralSearchMakeSQL(string sVal)
        {
            return "SELECT * FROM AST.StoreInfo WHERE "
                                + " StoreName LIKE '%" + sVal + "%' OR"
                                + " ShortName LIKE '%" + sVal + "%' OR"
                                + " StoreCode LIKE '%" + sVal + "%' OR"
                                + " Address LIKE '%" + sVal + "%'";
        }
    }
}