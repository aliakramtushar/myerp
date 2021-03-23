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
    public class LocationController : Controller
    {
        Location _oLocation = new Location();
        List<Location> _oLocations = new List<Location>();
        LocationService _oLocationService = new LocationService();
        public ActionResult ViewLocations()
        {
            _oLocations = _oLocationService.Gets("", (string)Session[GlobalSession.UserID]);
            ViewBag.Locations = _oLocations;
            return View();
        }
        public ActionResult ViewLocation(int nID)
        {
            _oLocation = _oLocationService.Get(nID, (string)Session[GlobalSession.UserID]);
            District oDistrict = new District();
            DistrictService oDistrictService = new DistrictService();
            List<District> oDistricts = new List<District>();
            oDistricts = oDistrictService.Gets("", (string)Session[GlobalSession.UserID]);

            ViewBag.Districts = oDistricts;
            ViewBag.Location = _oLocation;
            ViewBag.Divisions = Enum.GetValues(typeof(EnumDivision)).Cast<EnumDivision>().Select(e => new EnumClass { MModelPK = ((int)e), MModelString = e.ToString() });
            ViewBag.ActivityStatus = Enum.GetValues(typeof(EnumActivityStatus)).Cast<EnumActivityStatus>().Select(e => new EnumClass { MModelPK = ((int)e), MModelString = e.ToString() });
            return View();
        }
        [HttpPost]
        public JsonResult Save(Location obj)
        {
            try
            {
                if (obj.LocationID == 0)
                {
                    _oLocation = _oLocationService.IUD(obj, EnumDBOperation.Insert, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oLocation = _oLocationService.IUD(obj, EnumDBOperation.Update, (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oLocation = new Location();
                _oLocation.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oLocation);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(Location obj)
        {
            _oLocation = new Location();
            try
            {
                if (obj.LocationID > 0)
                {
                    _oLocation.ErrorMessage = _oLocationService.Delete(obj.LocationID, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oLocation.ErrorMessage = FeedbackMessage.NoIdFound;
                }
            }
            catch (Exception ex)
            {
                _oLocation = new Location();
                _oLocation.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oLocation);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Search(Location obj)
        {
            _oLocation = new Location();
            _oLocations = new List<Location>() { };
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    _oLocations = _oLocationService.Gets(this.GeneralSearchMakeSQL(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oLocation = new Location();
                _oLocation.ErrorMessage = ex.Message;
                _oLocations.Add(_oLocation);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oLocations);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        private string GeneralSearchMakeSQL(string sVal)
        {
            return "SELECT * FROM View_Location WHERE LocationName LIKE '%" + sVal + "%' OR" +
                " Remarks LIKE '%" + sVal + "%' OR" +
                " DistrictName LIKE '%" + sVal + "%'";
        }
    }
}