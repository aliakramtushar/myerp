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
    public class ProductCategoryController : Controller
    {
        ProductCategory _oProductCategory = new ProductCategory();
        List<ProductCategory> _oProductCategorys = new List<ProductCategory>();
        ProductCategoryService _oProductCategoryService = new ProductCategoryService();
        public ActionResult ViewProductCategorys()
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oProductCategorys = _oProductCategoryService.Gets("", (string)Session[GlobalSession.UserID]);
            List<ProductType> oProductTypes = new List<ProductType>();
            ProductTypeService oProductTypeService = new ProductTypeService();
            oProductTypes = oProductTypeService.Gets("", (string)Session[GlobalSession.UserID]);
            ViewBag.ProductTypes = oProductTypes;
            ViewBag.ProductCategorys = _oProductCategorys;
            return View();
        }
        [HttpPost]
        public JsonResult Save(ProductCategory obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            try
            {
                if (obj.ProductCategoryID == 0)
                {
                    _oProductCategory = _oProductCategoryService.IUD(obj, EnumDBOperation.Insert, (string)Session[GlobalSession.UserID]); ;
                }
                else
                {
                    _oProductCategory = _oProductCategoryService.IUD(obj, EnumDBOperation.Update, (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oProductCategory = new ProductCategory();
                _oProductCategory.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oProductCategory);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(ProductCategory obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oProductCategory = new ProductCategory();
            try
            {
                if (obj.ProductCategoryID > 0)
                {
                    _oProductCategory = _oProductCategoryService.IUD(obj, EnumDBOperation.Delete, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oProductCategory.ErrorMessage = FeedbackMessage.NoIdFound;
                }
            }
            catch (Exception ex)
            {
                _oProductCategory = new ProductCategory();
                _oProductCategory.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oProductCategory);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Search(ProductCategory obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oProductCategory = new ProductCategory();
            _oProductCategorys = new List<ProductCategory>() { };
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    _oProductCategorys = _oProductCategoryService.Gets(this.GeneralSearchMakeSQL(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oProductCategory = new ProductCategory();
                _oProductCategory.ErrorMessage = ex.Message;
                _oProductCategorys.Add(_oProductCategory);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oProductCategorys);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        private string GeneralSearchMakeSQL(string sVal)
        {
            return "SELECT * FROM AST.View_ProductCategory WHERE "
                                + " ProductCategoryName LIKE '%" + sVal + "%' OR"
                                + " ProductCategoryCode LIKE '%" + sVal + "%' OR"
                                + " ProductTypeName LIKE '%" + sVal + "%'";

        }
    }
}