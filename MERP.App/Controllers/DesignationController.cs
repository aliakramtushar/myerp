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
    public class DesignationController : Controller
    {
        Designation _oDesignation = new Designation();
        List<Designation> _oDesignations = new List<Designation>();
        DesignationService _oDesignationService = new DesignationService();
        public ActionResult ViewDesignations()
        {
            _oDesignations = _oDesignationService.Gets("", (string)Session[GlobalSession.UserID]);
            ViewBag.Designations = _oDesignations;
            return View();
        }
        public ActionResult ViewDesignation(int nID)
        {
            _oDesignation = _oDesignationService.Get(nID, (string)Session[GlobalSession.UserID]);
            ViewBag.Designation = _oDesignation;
            ViewBag.ActivityStatus = Enum.GetValues(typeof(EnumActivityStatus)).Cast<EnumActivityStatus>().Select(e => new EnumClass { MModelPK = ((int)e), MModelString = e.ToString() });
            return View();
        }
        [HttpPost]
        public JsonResult Save(Designation obj)
        {
            try
            {
                if (obj.DesignationID == 0)
                {
                    _oDesignation = _oDesignationService.IUD(obj, EnumDBOperation.Insert, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oDesignation = _oDesignationService.IUD(obj, EnumDBOperation.Update, (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oDesignation = new Designation();
                _oDesignation.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oDesignation);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(Designation obj)
        {
            _oDesignation = new Designation();
            try
            {
                if (obj.DesignationID > 0)
                {
                    _oDesignation.ErrorMessage = _oDesignationService.Delete(obj.DesignationID, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oDesignation.ErrorMessage = FeedbackMessage.NoIdFound;
                }
            }
            catch (Exception ex)
            {
                _oDesignation = new Designation();
                _oDesignation.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oDesignation);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Search(Designation obj)
        {
            _oDesignation = new Designation();
            _oDesignations = new List<Designation>() { };
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    _oDesignations = _oDesignationService.Gets(this.GeneralSearchMakeSQL(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oDesignation = new Designation();
                _oDesignation.ErrorMessage = ex.Message;
                _oDesignations.Add(_oDesignation);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oDesignations);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        private string GeneralSearchMakeSQL(string sVal)
        {
            return "SELECT * FROM View_Designation WHERE DesignationName LIKE '%" + sVal + "%' OR" + " Remarks LIKE '%" + sVal + "%'";
        }
    }
}