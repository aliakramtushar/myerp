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
    public class EmployeeController : Controller
    {
        Employee _oEmployee = new Employee();
        List<Employee> _oEmployees = new List<Employee>();
        EmployeeService _oEmployeeService = new EmployeeService();
        public ActionResult ViewEmployees()
        {
            _oEmployees = _oEmployeeService.Gets("", (string)Session[GlobalSession.UserID]);
            ViewBag.Employees = _oEmployees;
            return View();
        }
        public ActionResult ViewEmployee(int nID)
        {
            _oEmployee = _oEmployeeService.Get(nID, (string)Session[GlobalSession.UserID]);
            ViewBag.Employee = _oEmployee;
            ViewBag.ActivityStatus = Enum.GetValues(typeof(EnumActivityStatus)).Cast<EnumActivityStatus>().Select(e => new EnumClass { MModelPK = ((int)e), MModelString = e.ToString() });
            return View();
        }
        [HttpPost]
        public JsonResult Save(Employee obj)
        {
            try
            {
                if (obj.EmployeeID == 0)
                {
                    _oEmployee = _oEmployeeService.IUD(obj, EnumDBOperation.Insert, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oEmployee = _oEmployeeService.IUD(obj, EnumDBOperation.Update, (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oEmployee = new Employee();
                _oEmployee.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oEmployee);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(Employee obj)
        {
            _oEmployee = new Employee();
            try
            {
                if (obj.EmployeeID > 0)
                {
                    _oEmployee.ErrorMessage = _oEmployeeService.Delete(obj.EmployeeID, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oEmployee.ErrorMessage = FeedbackMessage.NoIdFound;
                }
            }
            catch (Exception ex)
            {
                _oEmployee = new Employee();
                _oEmployee.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oEmployee);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Search(Employee obj)
        {
            _oEmployee = new Employee();
            _oEmployees = new List<Employee>() { };
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    _oEmployees = _oEmployeeService.Gets(this.GeneralSearchMakeSQL(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oEmployee = new Employee();
                _oEmployee.ErrorMessage = ex.Message;
                _oEmployees.Add(_oEmployee);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oEmployees);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        private string GeneralSearchMakeSQL(string sVal)
        {
            return "SELECT * FROM View_Employee WHERE EmployeeName LIKE '%" + sVal + "%' OR" +
                " Address LIKE '%" + sVal + "%' OR" +
                " Remarks LIKE '%" + sVal + "%'";
        }
    }
}