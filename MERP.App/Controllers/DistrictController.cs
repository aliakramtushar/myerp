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
    public class DistrictController : Controller
    {
        District _oDistrict = new District();
        List<District> _oDistricts = new List<District>();
        DistrictService _oDistrictService = new DistrictService();
        public ActionResult ViewDistricts()
        {
            _oDistricts = _oDistrictService.Gets("", (string)Session[GlobalSession.UserID]);
            ViewBag.Districts = _oDistricts;
            ViewBag.Divisions = Enum.GetValues(typeof(EnumDivision)).Cast<EnumDivision>().Select(e => new EnumClass { MModelPK = ((int)e), MModelString = e.ToString() });
            ViewBag.ActivityStatus = Enum.GetValues(typeof(EnumActivityStatus)).Cast<EnumActivityStatus>().Select(e => new EnumClass { MModelPK = ((int)e), MModelString = e.ToString() });
            return View();
        }
        [HttpPost]
        public JsonResult GetByID(District obj)
        {
            _oDistrict = new District();
            try
            {
                if (obj.MModelPK > 0)
                {
                    _oDistrict = _oDistrictService.Get(obj.MModelPK, (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oDistrict = new District();
                _oDistrict.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oDistrict);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Save(District obj)
        {
            try
            {
                if (obj.DistrictID == 0)
                {
                    _oDistrict = _oDistrictService.IUD(obj, EnumDBOperation.Insert, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oDistrict = _oDistrictService.IUD(obj, EnumDBOperation.Update, (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oDistrict = new District();
                _oDistrict.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oDistrict);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(District obj)
        {
            _oDistrict = new District();
            try
            {
                if (obj.DistrictID > 0)
                {
                    _oDistrict.ErrorMessage = _oDistrictService.Delete(obj.DistrictID, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oDistrict.ErrorMessage = FeedbackMessage.NoIdFound;
                }
            }
            catch (Exception ex)
            {
                _oDistrict = new District();
                _oDistrict.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oDistrict);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Search(District obj)
        {
            _oDistrict = new District();
            _oDistricts = new List<District>() { };
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    _oDistricts = _oDistrictService.Gets(this.GeneralSearchMakeSQL(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oDistrict = new District();
                _oDistrict.ErrorMessage = ex.Message;
                _oDistricts.Add(_oDistrict);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oDistricts);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        private string GeneralSearchMakeSQL(string sVal)
        {
            return "SELECT * FROM View_District WHERE DistrictName LIKE '%" + sVal + "%' OR" + " Remarks LIKE '%" + sVal + "%'";
        }
    }
}