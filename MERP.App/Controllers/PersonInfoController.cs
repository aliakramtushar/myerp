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
    public class PersonInfoController : Controller
    {
        PersonInfo _oPersonInfo = new PersonInfo();
        List<PersonInfo> _oPersonInfos = new List<PersonInfo>();
        PersonInfoService _oPersonInfoService = new PersonInfoService();
        public ActionResult ViewPersonInfos()
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oPersonInfos = _oPersonInfoService.Gets("", (string)Session[GlobalSession.UserID]);
            ViewBag.PersonTypes = Enum.GetValues(typeof(EnumPersonType)).Cast<EnumPersonType>().Select(e => new EnumClass { MModelPK = ((int)e), MModelString = e.ToString() });
            ViewBag.PersonInfos = _oPersonInfos;
            return View();
        }
        [HttpPost]
        public JsonResult Save(PersonInfo obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            try
            {
                if (obj.PersonID == 0)
                {
                    _oPersonInfo = _oPersonInfoService.IUD(obj, EnumDBOperation.Insert, (string)Session[GlobalSession.UserID]); ;
                }
                else
                {
                    _oPersonInfo = _oPersonInfoService.IUD(obj, EnumDBOperation.Update, (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oPersonInfo = new PersonInfo();
                _oPersonInfo.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oPersonInfo);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(PersonInfo obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oPersonInfo = new PersonInfo();
            try
            {
                if (obj.PersonID > 0)
                {
                    _oPersonInfo.ErrorMessage = _oPersonInfoService.Delete(obj.PersonID, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oPersonInfo.ErrorMessage = FeedbackMessage.NoIdFound;
                }
            }
            catch (Exception ex)
            {
                _oPersonInfo = new PersonInfo();
                _oPersonInfo.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oPersonInfo);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Search(PersonInfo obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oPersonInfo = new PersonInfo();
            _oPersonInfos = new List<PersonInfo>() { };
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    _oPersonInfos = _oPersonInfoService.Gets(this.GeneralSearchMakeSQL(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oPersonInfo = new PersonInfo();
                _oPersonInfo.ErrorMessage = ex.Message;
                _oPersonInfos.Add(_oPersonInfo);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oPersonInfos);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        private string GeneralSearchMakeSQL(string sVal)
        {
            return "SELECT * FROM COM.PersonInfo WHERE "
                                + " PersonName LIKE '%" + sVal + "%' OR"
                                + " Mobile LIKE '%" + sVal + "%' OR"
                                + " Address LIKE '%" + sVal + "%'";
        }
    }
}