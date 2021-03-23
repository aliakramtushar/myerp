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
    public class ProductSubCategoryController : Controller
    {
        ProductSubCategory _oProductSubCategory = new ProductSubCategory();
        List<ProductSubCategory> _oProductSubCategorys = new List<ProductSubCategory>();
        ProductSubCategoryService _oProductSubCategoryService = new ProductSubCategoryService();
        public ActionResult ViewProductSubCategorys()
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oProductSubCategorys = _oProductSubCategoryService.Gets("", (string)Session[GlobalSession.UserID]);
            List<ProductCategory> oProductCategorys = new List<ProductCategory>();
            ProductCategoryService oProductCategoryService = new ProductCategoryService();
            oProductCategorys = oProductCategoryService.Gets("", (string)Session[GlobalSession.UserID]);
            ViewBag.ProductCategorys = oProductCategorys;
            ViewBag.ProductSubCategorys = _oProductSubCategorys;
            return View();
        }
        [HttpPost]
        public JsonResult Save(ProductSubCategory obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            try
            {
                if (obj.ProductSubCategoryID == 0)
                {
                    _oProductSubCategory = _oProductSubCategoryService.IUD(obj, EnumDBOperation.Insert, (string)Session[GlobalSession.UserID]); ;
                }
                else
                {
                    _oProductSubCategory = _oProductSubCategoryService.IUD(obj, EnumDBOperation.Update, (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oProductSubCategory = new ProductSubCategory();
                _oProductSubCategory.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oProductSubCategory);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(ProductSubCategory obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oProductSubCategory = new ProductSubCategory();
            try
            {
                if (obj.ProductSubCategoryID > 0)
                {
                    _oProductSubCategory = _oProductSubCategoryService.IUD(obj, EnumDBOperation.Delete, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oProductSubCategory.ErrorMessage = FeedbackMessage.NoIdFound;
                }
            }
            catch (Exception ex)
            {
                _oProductSubCategory = new ProductSubCategory();
                _oProductSubCategory.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oProductSubCategory);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Search(ProductSubCategory obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oProductSubCategory = new ProductSubCategory();
            _oProductSubCategorys = new List<ProductSubCategory>() { };
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    _oProductSubCategorys = _oProductSubCategoryService.Gets(this.GeneralSearchMakeSQL(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oProductSubCategory = new ProductSubCategory();
                _oProductSubCategory.ErrorMessage = ex.Message;
                _oProductSubCategorys.Add(_oProductSubCategory);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oProductSubCategorys);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        private string GeneralSearchMakeSQL(string sVal)
        {
            return "SELECT * FROM AST.View_ProductSubCategory WHERE "
                                + " ProductSubCategoryName LIKE '%" + sVal + "%' OR"
                                + " ProductSubCategoryCode LIKE '%" + sVal + "%' OR"
                                + " ProductCategoryName LIKE '%" + sVal + "%'";

        }
    }
}