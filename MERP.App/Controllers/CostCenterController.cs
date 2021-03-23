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
    public class CostCenterController : Controller
    {
        CostCenter _oCostCenter = new CostCenter();
        List<CostCenter> _oCostCenters = new List<CostCenter>();
        CostCenterService _oCostCenterService = new CostCenterService();
        public ActionResult ViewCostCenters()
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oCostCenters = _oCostCenterService.Gets("", (string)Session[GlobalSession.UserID]);
            ViewBag.CostCenters = _oCostCenters;
            ViewBag.ActivityStatus = Enum.GetValues(typeof(EnumActivityStatus)).Cast<EnumActivityStatus>().Select(e => new EnumClass { MModelPK = ((int)e), MModelString = e.ToString() });
            return View();
        }
        [HttpPost]
        public JsonResult Save(CostCenter obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            try
            {
                if (obj.CostCenterID == 0)
                {
                    _oCostCenter = _oCostCenterService.IUD(obj, EnumDBOperation.Insert, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oCostCenter = _oCostCenterService.IUD(obj, EnumDBOperation.Update, (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oCostCenter = new CostCenter();
                _oCostCenter.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oCostCenter);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(CostCenter obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oCostCenter = new CostCenter();
            try
            {
                if (obj.CostCenterID > 0)
                {
                    _oCostCenter = _oCostCenterService.IUD(obj, EnumDBOperation.Delete, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oCostCenter.ErrorMessage = FeedbackMessage.NoIdFound;
                }
            }
            catch (Exception ex)
            {
                _oCostCenter = new CostCenter();
                _oCostCenter.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oCostCenter);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Search(CostCenter obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oCostCenter = new CostCenter();
            _oCostCenters = new List<CostCenter>() { };
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    _oCostCenters = _oCostCenterService.Gets(this.GeneralSearchMakeSQL(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oCostCenter = new CostCenter();
                _oCostCenter.ErrorMessage = ex.Message;
                _oCostCenters.Add(_oCostCenter);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oCostCenters);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        private string GeneralSearchMakeSQL(string sVal)
        {
            return "SELECT * FROM AST.CostCenter WHERE CostCenterName LIKE '%" + sVal + "%'";
        }
    }
}