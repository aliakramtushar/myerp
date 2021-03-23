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
    public class UserMenuMappingController : Controller
    {
        UserMenuMapping _oUserMenuMapping = new UserMenuMapping();
        List<UserMenuMapping> _oUserMenuMappings = new List<UserMenuMapping>();
        UserMenuMappingService _oUserMenuMappingService = new UserMenuMappingService();

        public ActionResult ViewUserMenuMappings()
        {
            GlobalSession.SessionIsAlive(Session, Response);
            List<User> oUsers = new List<User>();
            UserService oUserService = new UserService();
            oUsers = oUserService.Gets("", (string)Session[GlobalSession.UserID]);
            ViewBag.Users = oUsers;
            return View();
        }
        public ActionResult ViewUserMenuMapping()
        {
            GlobalSession.SessionIsAlive(Session, Response);
            List<User> oUsers = new List<User>();
            UserService oUserService = new UserService();
            oUsers = oUserService.Gets("", (string)Session[GlobalSession.UserID]);
            ViewBag.Users = oUsers;
            return View();
        }
        [HttpPost]
        public JsonResult SaveUserMenuMapping(User obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            try
            {
                if (obj.UserID != 0 && obj.ErrorMessage != "")
                {
                    _oUserMenuMappings = _oUserMenuMappingService.SaveUserMenuMapping(obj.UserID, obj.ErrorMessage, EnumDBOperation.SpecialCase1, (string)Session[GlobalSession.UserID]);
                    _oUserMenuMapping.UserMenuMappingID = _oUserMenuMappings.Count();
                    _oUserMenuMapping.ErrorMessage = FeedbackMessage.Updated;
                }
                else
                {
                    _oUserMenuMapping = new UserMenuMapping();
                    _oUserMenuMapping.ErrorMessage = FeedbackMessage.Invalid;
                }
            }
            catch (Exception ex)
            {
                _oUserMenuMapping = new UserMenuMapping();
                _oUserMenuMapping.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oUserMenuMapping);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
    }
}