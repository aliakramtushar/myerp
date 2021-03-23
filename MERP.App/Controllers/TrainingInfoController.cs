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
    public class TrainingInfoController : Controller
    {
        TrainingInfo _oTrainingInfo = new TrainingInfo();
        List<TrainingInfo> _oTrainingInfos = new List<TrainingInfo>();
        TrainingInfoService _oTrainingInfoService = new TrainingInfoService();
        public ActionResult ViewTrainingInfos()
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oTrainingInfos = _oTrainingInfoService.Gets("", (string)Session[GlobalSession.UserID]);
            ViewBag.TrainingInfos = _oTrainingInfos;
            return View();
        }
        public ActionResult ViewTrainingInfo(int nID)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oTrainingInfo = new TrainingInfo();
            if (nID > 0)
            {
                _oTrainingInfo = _oTrainingInfoService.Get(nID, (string)Session[GlobalSession.UserID]);
            }
            else
            {
                _oTrainingInfo.ErrorMessage = FeedbackMessage.NoDataFound;
            }
            ViewBag.ActivityStatus = Enum.GetValues(typeof(EnumActivityStatus)).Cast<EnumActivityStatus>().Select(e => new EnumClass { MModelPK = ((int)e), MModelString = e.ToString() });
            ViewBag.TrainingTypes = Enum.GetValues(typeof(EnumTrainingType)).Cast<EnumTrainingType>().Select(e => new EnumClass { MModelPK = ((int)e), MModelString = e.ToString() });
            ViewBag.TrainingInfo = _oTrainingInfo;
            return View();
        }
        [HttpPost]
        public JsonResult Save(TrainingInfo obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            try
            {
                if (obj.TrainingID == 0)
                {
                    _oTrainingInfo = _oTrainingInfoService.IUD(obj, EnumDBOperation.Insert, (string)Session[GlobalSession.UserID]); ;
                }
                else
                {
                    _oTrainingInfo = _oTrainingInfoService.IUD(obj, EnumDBOperation.Update, (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oTrainingInfo = new TrainingInfo();
                _oTrainingInfo.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oTrainingInfo);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(TrainingInfo obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oTrainingInfo = new TrainingInfo();
            try
            {
                if (obj.TrainingID > 0)
                {
                    _oTrainingInfo = _oTrainingInfoService.IUD(obj, EnumDBOperation.Delete, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oTrainingInfo.ErrorMessage = FeedbackMessage.NoIdFound;
                }
            }
            catch (Exception ex)
            {
                _oTrainingInfo = new TrainingInfo();
                _oTrainingInfo.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oTrainingInfo);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Search(TrainingInfo obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oTrainingInfo = new TrainingInfo();
            _oTrainingInfos = new List<TrainingInfo>() { };
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    _oTrainingInfos = _oTrainingInfoService.Gets(this.GeneralSearchMakeSQL(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oTrainingInfo = new TrainingInfo();
                _oTrainingInfo.ErrorMessage = ex.Message;
                _oTrainingInfos.Add(_oTrainingInfo);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oTrainingInfos);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        private string GeneralSearchMakeSQL(string sVal)
        {
            return "SELECT * FROM PSDP.TrainingInfo WHERE "
                                + " TrainingCode LIKE '%" + sVal + "%' OR"
                                + " TrainingName LIKE '%" + sVal + "%'";

        }
    }
}