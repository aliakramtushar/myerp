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
    public class GroupController : Controller
    {
        Group _oGroup = new Group();
        List<Group> _oGroups = new List<Group>();
        GroupService _oGroupService = new GroupService();
        public ActionResult ViewGroups()
        {
            _oGroups = _oGroupService.Gets("", (string)Session[GlobalSession.UserID]);
            ViewBag.Groups = _oGroups.OrderBy(x=>x.GroupName);
            return View();
        }
        public ActionResult ViewGroup(int nID)
        {
            _oGroup = _oGroupService.Get(nID, (string)Session[GlobalSession.UserID]);

            PersonInfo oPersonInfo = new PersonInfo();
            PersonInfoService oPersonInfoService = new PersonInfoService();
            List<PersonInfo> oPersonInfos = new List<PersonInfo>();
            oPersonInfos = oPersonInfoService.Gets("", (string)Session[GlobalSession.UserID]);

            ViewBag.Group = _oGroup;
            ViewBag.ContactPersonInfos = oPersonInfos;
            ViewBag.ActivityStatus = Enum.GetValues(typeof(EnumActivityStatus)).Cast<EnumActivityStatus>().Select(e => new EnumClass { MModelPK = ((int)e), MModelString = e.ToString() });
            return View();
        }
        [HttpPost]
        public JsonResult Save(Group obj)
        {
            try
            {
                if (obj.GroupID == 0)
                {
                    _oGroup = _oGroupService.IUD(obj, EnumDBOperation.Insert, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oGroup = _oGroupService.IUD(obj, EnumDBOperation.Update, (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oGroup = new Group();
                _oGroup.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oGroup);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(Group obj)
        {
            _oGroup = new Group();
            try
            {
                if (obj.GroupID > 0)
                {
                    _oGroup.ErrorMessage = _oGroupService.Delete(obj.GroupID, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oGroup.ErrorMessage = FeedbackMessage.NoIdFound;
                }
            }
            catch (Exception ex)
            {
                _oGroup = new Group();
                _oGroup.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oGroup);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetsActiveAndHasAccessList(Group obj)
        {
            _oGroup = new Group();
            _oGroups = new List<Group>() { };
            try
            {
                _oGroups = _oGroupService.GetsActiveAndHasAccessList((string)Session[GlobalSession.UserID]);
            }
            catch (Exception ex)
            {
                _oGroup = new Group();
                _oGroup.ErrorMessage = ex.Message;
                _oGroups.Add(_oGroup);
            }
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //string sjson = serializer.Serialize(_oGroups);
            //return Json(sjson, JsonRequestBehavior.AllowGet);
            var jsonResult = Json(_oGroups, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [HttpPost]
        public JsonResult Search(Group obj)
        {
            _oGroup = new Group();
            _oGroups = new List<Group>() { };
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    _oGroups = _oGroupService.Gets(this.GeneralSearchMakeSQL(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oGroup = new Group();
                _oGroup.ErrorMessage = ex.Message;
                _oGroups.Add(_oGroup);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oGroups);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        private string GeneralSearchMakeSQL(string sVal)
        {
            return "SELECT * FROM Users.UserGroupInfo WHERE GroupCode LIKE '%" + sVal + "%' OR"
                                + " GroupName LIKE '%" + sVal + "%' OR"
                                + " GroupDesc LIKE '%" + sVal + "%'";
        }
    }
}