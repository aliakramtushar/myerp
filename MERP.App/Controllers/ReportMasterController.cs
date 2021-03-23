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
    public class ReportMasterController : Controller
    {
        ReportMaster _oReportMaster = new ReportMaster();
        List<ReportMaster> _oReportMasters = new List<ReportMaster>();
        ReportMasterService _oReportMasterService = new ReportMasterService();
        public ActionResult ViewReportMasters()
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oReportMasters = _oReportMasterService.Gets("", (string)Session[GlobalSession.UserID]);
            ViewBag.ReportMasters = _oReportMasters;
            return View();
        }
        public ActionResult ViewReportMaster(int nID)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oReportMaster = new ReportMaster();
            ReportDetailService oReportDetailService = new ReportDetailService();
            if (nID > 0)
            {
                _oReportMaster = _oReportMasterService.Get(nID, (string)Session[GlobalSession.UserID]);
                _oReportMaster.ReportDetails = oReportDetailService.GetsByParentID(_oReportMaster.ReportMasterID, (string)Session[GlobalSession.UserID]);
            }
            ViewBag.ReportMaster = _oReportMaster;
            ViewBag.ControlTypes = Enum.GetValues(typeof(EnumControlType)).Cast<EnumControlType>().Select(e => new EnumClass { MModelPK = ((int)e), MModelString = e.ToString() });
            return View();
        }
        public ActionResult GetReports()
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oReportMaster = new ReportMaster();
            List<ReportMaster> oReportMasters = new List<ReportMaster>();
            oReportMasters = _oReportMasterService.GetsAccessedReports((string)Session[GlobalSession.UserID]);
            ViewBag.ReportMasters = oReportMasters;
            return View();
        }

        [HttpPost]
        public JsonResult GetsActiveReports(ReportMaster obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oReportMaster = new ReportMaster();
            _oReportMasters = new List<ReportMaster>();
            try
            {
                _oReportMasters = _oReportMasterService.GetsActiveReports((string)Session[GlobalSession.UserID]);
            }
            catch (Exception ex)
            {
                _oReportMaster = new ReportMaster();
                _oReportMaster.ErrorMessage = ex.Message;
                _oReportMasters.Add(_oReportMaster);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oReportMasters);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Save(ReportMaster obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            try
            {
                if (obj.ReportMasterID == 0)
                {
                    _oReportMaster = _oReportMasterService.IUD(obj, EnumDBOperation.Insert, (string)Session[GlobalSession.UserID]); ;
                }
                else
                {
                    _oReportMaster = _oReportMasterService.IUD(obj, EnumDBOperation.Update, (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oReportMaster = new ReportMaster();
                _oReportMaster.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oReportMaster);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(ReportMaster obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oReportMaster = new ReportMaster();
            try
            {
                if (obj.ReportMasterID > 0)
                {
                    _oReportMaster.ErrorMessage = _oReportMasterService.Delete(obj.ReportMasterID, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oReportMaster.ErrorMessage = FeedbackMessage.NoIdFound;
                }
            }
            catch (Exception ex)
            {
                _oReportMaster = new ReportMaster();
                _oReportMaster.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oReportMaster);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetReportMaster(ReportMaster obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oReportMaster = new ReportMaster();
            try
            {
                if (obj.ReportMasterID > 0)
                {
                    _oReportMaster = _oReportMasterService.Get(obj.ReportMasterID, (string)Session[GlobalSession.UserID]);
                    ReportDetailService oReportDetailService = new ReportDetailService();
                    _oReportMaster.ReportDetails = oReportDetailService.GetsByParentID(_oReportMaster.ReportMasterID, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oReportMaster.ErrorMessage = FeedbackMessage.NoIdFound;
                }
            }
            catch (Exception ex)
            {
                _oReportMaster = new ReportMaster();
                _oReportMaster.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oReportMaster);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Search(ReportMaster obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oReportMaster = new ReportMaster();
            _oReportMasters = new List<ReportMaster>() { };
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    _oReportMasters = _oReportMasterService.Gets(this.GeneralSearchMakeSQL(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oReportMaster = new ReportMaster();
                _oReportMaster.ErrorMessage = ex.Message;
                _oReportMasters.Add(_oReportMaster);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oReportMasters);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        private string GeneralSearchMakeSQL(string sVal)
        {
            return "SELECT * FROM MOB.ReportMaster WHERE "
                                + " ReportName LIKE '%" + sVal + "%'";
        }
    }
}