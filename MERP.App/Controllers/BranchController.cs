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
    public class BranchController : Controller
    {
        Branch _oBranch = new Branch();
        List<Branch> _oBranchs = new List<Branch>();
        BranchService _oBranchService = new BranchService();
        //public ActionResult ViewBranchs()
        //{
        //    _oBranchs = _oBranchService.Gets("", (string)Session[GlobalSession.UserID]);
        //    ViewBag.Branchs = _oBranchs;
        //    return View();
        //}
        //public ActionResult ViewBranch(int nID)
        //{
        //    _oBranch = _oBranchService.Get(nID, (string)Session[GlobalSession.UserID]);
        //    ViewBag.Branch = _oBranch;
        //    ViewBag.ActivityStatus = Enum.GetValues(typeof(EnumActivityStatus)).Cast<EnumActivityStatus>().Select(e => new EnumClass { MModelPK = ((int)e), MModelString = e.ToString() });
        //    return View();
        //}
        //[HttpPost]
        //public JsonResult Save(Branch obj)
        //{
        //    try
        //    {
        //        if (obj.BranchID == 0)
        //        {
        //            _oBranch = _oBranchService.IUD(obj, EnumDBOperation.Insert, (string)Session[GlobalSession.UserID]);
        //        }
        //        else
        //        {
        //            _oBranch = _oBranchService.IUD(obj, EnumDBOperation.Update, (string)Session[GlobalSession.UserID]);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _oBranch = new Branch();
        //        _oBranch.ErrorMessage = ex.Message;
        //    }
        //    JavaScriptSerializer serializer = new JavaScriptSerializer();
        //    string sjson = serializer.Serialize(_oBranch);
        //    return Json(sjson, JsonRequestBehavior.AllowGet);
        //}
        //[HttpPost]
        //public JsonResult Delete(Branch obj)
        //{
        //    _oBranch = new Branch();
        //    try
        //    {
        //        if (obj.BranchID > 0)
        //        {
        //            _oBranch.ErrorMessage = _oBranchService.Delete(obj.BranchID, (string)Session[GlobalSession.UserID]);
        //        }
        //        else
        //        {
        //            _oBranch.ErrorMessage = FeedbackMessage.NoIdFound;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _oBranch = new Branch();
        //        _oBranch.ErrorMessage = ex.Message;
        //    }
        //    JavaScriptSerializer serializer = new JavaScriptSerializer();
        //    string sjson = serializer.Serialize(_oBranch);
        //    return Json(sjson, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult Search(Branch obj)
        {
            _oBranch = new Branch();
            _oBranchs = new List<Branch>() { };
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    _oBranchs = _oBranchService.Gets(this.GeneralSearchMakeSQL(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oBranch = new Branch();
                _oBranch.ErrorMessage = ex.Message;
                _oBranchs.Add(_oBranch);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oBranchs);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        private string GeneralSearchMakeSQL(string sVal)
        {
            return "SELECT * FROM View_Branch WHERE BranchName LIKE '%" + sVal + "%' OR" +
                " Address LIKE '%" + sVal + "%' OR" +
                " Remarks LIKE '%" + sVal + "%'";
        }
    }
}