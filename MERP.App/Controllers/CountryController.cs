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
    public class CountryController : Controller
    {
        Country _oCountry = new Country();
        List<Country> _oCountrys = new List<Country>();
        CountryService _oCountryService = new CountryService();
        public ActionResult ViewCountrys()
        {
            _oCountrys = _oCountryService.Gets("", (string)Session[GlobalSession.UserID]);
            ViewBag.Countrys = _oCountrys;
            return View();
        }
        public ActionResult ViewCountry(int nID)
        {
            _oCountry = _oCountryService.Get(nID, (string)Session[GlobalSession.UserID]);
            ViewBag.Country = _oCountry;
            ViewBag.Continents = Enum.GetValues(typeof(EnumContinent)).Cast<EnumContinent>().Select(e => new EnumClass { MModelPK = ((int)e), MModelString = e.ToString() });
            ViewBag.ActivityStatus = Enum.GetValues(typeof(EnumActivityStatus)).Cast<EnumActivityStatus>().Select(e => new EnumClass { MModelPK = ((int)e), MModelString = e.ToString() });
            return View();
        }
        [HttpPost]
        public JsonResult Save(Country obj)
        {
            try
            {
                if (obj.CountryID == 0)
                {
                    _oCountry = _oCountryService.IUD(obj, EnumDBOperation.Insert, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oCountry = _oCountryService.IUD(obj, EnumDBOperation.Update, (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oCountry = new Country();
                _oCountry.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oCountry);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(Country obj)
        {
            _oCountry = new Country();
            try
            {
                if (obj.CountryID > 0)
                {
                    _oCountry.ErrorMessage = _oCountryService.Delete(obj.CountryID, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oCountry.ErrorMessage = FeedbackMessage.NoIdFound;
                }
            }
            catch (Exception ex)
            {
                _oCountry = new Country();
                _oCountry.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oCountry);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Search(Country obj)
        {
            _oCountry = new Country();
            _oCountrys = new List<Country>() { };
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    _oCountrys = _oCountryService.Gets(this.GeneralSearchMakeSQL(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oCountry = new Country();
                _oCountry.ErrorMessage = ex.Message;
                _oCountrys.Add(_oCountry);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oCountrys);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        private string GeneralSearchMakeSQL(string sVal)
        {
            return "SELECT * FROM View_Country WHERE CountryName LIKE '%" + sVal + "%' OR" + " Remarks LIKE '%" + sVal + "%'";
        }
    }
}