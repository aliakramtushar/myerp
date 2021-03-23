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
    public class DepartmentController : Controller
    {
        Department _oDepartment = new Department();
        List<Department> _oDepartments = new List<Department>();
        DepartmentService _oDepartmentService = new DepartmentService();
        public ActionResult ViewDepartments()
        {
            _oDepartments = _oDepartmentService.Gets("", (string)Session[GlobalSession.UserID]);
            ViewBag.Departments = _oDepartments;
            return View();
        }
        public ActionResult ViewDepartment(int nID)
        {
            _oDepartment = _oDepartmentService.Get(nID, (string)Session[GlobalSession.UserID]);

            ViewBag.Department = _oDepartment;
            ViewBag.ActivityStatus = Enum.GetValues(typeof(EnumActivityStatus)).Cast<EnumActivityStatus>().Select(e => new EnumClass { MModelPK = ((int)e), MModelString = e.ToString() });
            return View();
        }
        [HttpPost]
        public JsonResult Save(Department obj)
        {
            try
            {
                if (obj.DepartmentID == 0)
                {
                    _oDepartment = _oDepartmentService.IUD(obj, EnumDBOperation.Insert, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oDepartment = _oDepartmentService.IUD(obj, EnumDBOperation.Update, (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oDepartment = new Department();
                _oDepartment.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oDepartment);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(Department obj)
        {
            _oDepartment = new Department();
            try
            {
                if (obj.DepartmentID > 0)
                {
                    _oDepartment.ErrorMessage = _oDepartmentService.Delete(obj.DepartmentID, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oDepartment.ErrorMessage = FeedbackMessage.NoIdFound;
                }
            }
            catch (Exception ex)
            {
                _oDepartment = new Department();
                _oDepartment.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oDepartment);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Search(Department obj)
        {
            _oDepartment = new Department();
            _oDepartments = new List<Department>() { };
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    _oDepartments = _oDepartmentService.Gets(this.GeneralSearchMakeSQL(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oDepartment = new Department();
                _oDepartment.ErrorMessage = ex.Message;
                _oDepartments.Add(_oDepartment);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oDepartments);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        private string GeneralSearchMakeSQL(string sVal)
        {
            return "SELECT * FROM View_Department WHERE DepartmentName LIKE '%" + sVal + "%' OR" + " Remarks LIKE '%" + sVal + "%'";
        }
    }
}