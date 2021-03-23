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
    public class ProductTypeController : Controller
    {
        ProductType _oProductType = new ProductType();
        List<ProductType> _oProductTypes = new List<ProductType>();
        ProductTypeService _oProductTypeService = new ProductTypeService();
        public ActionResult ViewProductTypes()
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oProductTypes = _oProductTypeService.Gets("", (string)Session[GlobalSession.UserID]);
            ViewBag.ProductTypes = _oProductTypes;
            return View();
        }
        [HttpPost]
        public JsonResult Save(ProductType obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            try
            {
                if (obj.ProductTypeID == 0)
                {
                    _oProductType = _oProductTypeService.IUD(obj, EnumDBOperation.Insert, (string)Session[GlobalSession.UserID]); ;
                }
                else
                {
                    _oProductType = _oProductTypeService.IUD(obj, EnumDBOperation.Update, (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oProductType = new ProductType();
                _oProductType.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oProductType);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(ProductType obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oProductType = new ProductType();
            try
            {
                if (obj.ProductTypeID > 0)
                {
                    _oProductType = _oProductTypeService.IUD(obj, EnumDBOperation.Delete, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oProductType.ErrorMessage = FeedbackMessage.NoIdFound;
                }
            }
            catch (Exception ex)
            {
                _oProductType = new ProductType();
                _oProductType.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oProductType);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Search(ProductType obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oProductType = new ProductType();
            _oProductTypes = new List<ProductType>() { };
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    _oProductTypes = _oProductTypeService.Gets(this.GeneralSearchMakeSQL(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oProductType = new ProductType();
                _oProductType.ErrorMessage = ex.Message;
                _oProductTypes.Add(_oProductType);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oProductTypes);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        private string GeneralSearchMakeSQL(string sVal)
        {
            return "SELECT * FROM AST.ProductType WHERE "
                                + " ProductTypeName LIKE '%" + sVal + "%' OR"
                                + " ProductTypeCode LIKE '%" + sVal + "%'";

        }
    }
}