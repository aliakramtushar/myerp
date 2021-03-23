using MERP.Engine;
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
    public class LoginController : Controller
    {
        User _oUser = new User();
        UserService _oUserService = new UserService();
        // GET: Login
        public ActionResult ViewLogin()
        {
            Session.Clear();
            return View();
        }
        public ActionResult TimeOut()
        {
            Session.Clear();
            return View();
        }
        [HttpPost]
        public JsonResult UserLogin(User oUser)
        {
            _oUser = new User();
            _oUserService = new UserService();
            Session.Clear();
            try
            {
                if (!string.IsNullOrEmpty(oUser.UserName) && !string.IsNullOrEmpty(oUser.Password))
                {
                    _oUser = _oUserService.Login(oUser);
                    if (_oUser.UserID > 0)
                    {
                        if(_oUser.IsInactive == 0 && oUser.Password == MEncryption.Decrypt(_oUser.Password))
                        {
                            _oUser.ErrorMessage = "Login Successfull!!";
                            InitializeSessions(_oUser);
                        }
                        else if (_oUser.IsInactive !=0)
                        {
                            _oUser = new User();
                            _oUser.ErrorMessage = "Login Failed!! InActive User. Please Contact with IT Department";
                        }
                        //else if (_oUser.IsBlocked != 0)
                        //{
                        //    _oUser = new User();
                        //    _oUser.ErrorMessage = "Login Failed!! Blocked User. Please Contact with IT Department";
                        //}
                        else if (oUser.Password != MEncryption.Decrypt(_oUser.Password))
                        {
                            _oUser = new User();
                            _oUser.ErrorMessage = "Login Failed!! Password Mismatched !!";
                        }
                        else
                        {
                            _oUser = new User();
                            _oUser.ErrorMessage = "Login Failed!! Invalid User";
                        }
                    }
                    else
                    {
                        _oUser = new User();
                        _oUser.ErrorMessage = "User ID or Password Mismatched !!";
                    }
                }
                else
                {
                    _oUser = new User();
                    _oUser.ErrorMessage = "Please Enter User Name & Password !!";
                }
            }
            catch (Exception ex)
            {
                Session.Clear();
                _oUser = new User();
                _oUser.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oUser);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        private void InitializeSessions(User oUser)
        {
            Session[GlobalSession.UserID] = oUser.UserName;
            Session[GlobalSession.UserName] = oUser.UserName;
            Session[GlobalSession.MUser] = oUser.UserName;
            Session[GlobalSession.GroupID] = oUser.GroupID;
            Session[GlobalSession.BranchID] = oUser.BranchId;
            Session[GlobalSession.BranchTypeID] = oUser.BranchTypeId;
            Session[GlobalSession.UserMenuIDs] = GlobalSession.UserMenuIDs;
        }
    }
}