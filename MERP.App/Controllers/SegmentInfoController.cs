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
    public class SegmentInfoController : Controller
    {
        SegmentInfo _oSegmentInfo = new SegmentInfo();
        List<SegmentInfo> _oSegmentInfos = new List<SegmentInfo>();
        SegmentInfoService _oSegmentInfoService = new SegmentInfoService();
        public ActionResult ViewSegmentInfos()
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oSegmentInfos = _oSegmentInfoService.Gets("", (string)Session[GlobalSession.UserID]);
            ViewBag.SegmentInfos = _oSegmentInfos;
            return View();
        }
        [HttpPost]
        public JsonResult Save(SegmentInfo obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            try
            {
                if (obj.SegmentID == 0)
                {
                    _oSegmentInfo = _oSegmentInfoService.IUD(obj, EnumDBOperation.Insert, (string)Session[GlobalSession.UserID]); ;
                }
                else
                {
                    _oSegmentInfo = _oSegmentInfoService.IUD(obj, EnumDBOperation.Update, (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oSegmentInfo = new SegmentInfo();
                _oSegmentInfo.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oSegmentInfo);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(SegmentInfo obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oSegmentInfo = new SegmentInfo();
            try
            {
                if (obj.SegmentID > 0)
                {
                    _oSegmentInfo = _oSegmentInfoService.IUD(obj, EnumDBOperation.Delete, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oSegmentInfo.ErrorMessage = FeedbackMessage.NoIdFound;
                }
            }
            catch (Exception ex)
            {
                _oSegmentInfo = new SegmentInfo();
                _oSegmentInfo.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oSegmentInfo);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Search(SegmentInfo obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oSegmentInfo = new SegmentInfo();
            _oSegmentInfos = new List<SegmentInfo>() { };
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    _oSegmentInfos = _oSegmentInfoService.Gets(this.GeneralSearchMakeSQL(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oSegmentInfo = new SegmentInfo();
                _oSegmentInfo.ErrorMessage = ex.Message;
                _oSegmentInfos.Add(_oSegmentInfo);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oSegmentInfos);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        private string GeneralSearchMakeSQL(string sVal)
        {
            return "SELECT * FROM AST.SegmentInfo WHERE "
                                + " SegmentName LIKE '%" + sVal + "%' OR"
                                + " SegmentCode LIKE '%" + sVal + "%'";

        }
    }
}