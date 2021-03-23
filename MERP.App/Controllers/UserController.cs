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
    public class UserController : Controller
    {
        User _oUser = new User();
        List<User> _oUsers = new List<User>();
        UserService _oUserService = new UserService();
        public ActionResult ViewUsers()
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oUsers = _oUserService.Gets("", (string)Session[GlobalSession.UserID]);
            ViewBag.Users = _oUsers;
            return View();
        }
        public ActionResult ViewUser(int nID)
        {
            _oUser = _oUserService.Get(nID, (string)Session[GlobalSession.UserID]);
            ViewBag.User = _oUser;
            return View();
        }
        [HttpPost]
        public JsonResult Search(User obj)
        {
            _oUser = new User();
            _oUsers = new List<User>() { };
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    _oUsers = _oUserService.Gets(this.GeneralSearchMakeSQL(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oUser = new User();
                _oUser.ErrorMessage = ex.Message;
                _oUsers.Add(_oUser);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oUsers);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        private string GeneralSearchMakeSQL(string sVal)
        {
            return "SELECT * FROM MOB.View_Users WHERE EmployeeCode LIKE '%" + sVal + "%' OR" +
                " FullName LIKE '%" + sVal + "%' OR" +
                " UserCode LIKE '%" + sVal + "%' OR" +
                " BranchName LIKE '%" + sVal + "%' OR" +
                " GroupName LIKE '%" + sVal + "%' OR" +
                " UserRoleName LIKE '%" + sVal + "%' OR" +
                " BranchTypeName LIKE '%" + sVal + "%' OR" +
                " MobileNo LIKE '%" + sVal + "%'";
        }
    }
}