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
    public class StoreProductController : Controller
    {
        StoreProduct _oStoreProduct = new StoreProduct();
        List<StoreProduct> _oStoreProducts = new List<StoreProduct>();
        StoreProductService _oStoreProductService = new StoreProductService();
        public ActionResult ViewStoreProducts()
        {
            GlobalSession.SessionIsAlive(Session, Response);
            List<StoreInfo> oStoreInfos = new List<StoreInfo>();
            StoreInfoService oStoreInfoService = new StoreInfoService();
            oStoreInfos = oStoreInfoService.Gets("SELECT * FROM AST.StoreInfo WHERE IsInActive = 0 ORDER BY StoreName", (string)Session[GlobalSession.UserID]);
            ViewBag.StoreInfos = oStoreInfos;
            return View();
        }
        public ActionResult ViewStoreProductAdd(int nID)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            StoreInfo oStoreInfo = new StoreInfo();
            StoreInfoService oStoreInfoService = new StoreInfoService();

            List<StoreProduct> oStoreProducts = new List<StoreProduct>();
            StoreProductService oStoreProductService = new StoreProductService();

            if (nID > 0)
            {
                oStoreInfo = oStoreInfoService.Get(nID, (string)Session[GlobalSession.UserID]);
                oStoreProducts = oStoreProductService.Gets("SELECT * FROM AST.View_StoreProduct WHERE StoreID = " + oStoreInfo.StoreID + " ORDER BY ProductName", (string)Session[GlobalSession.UserID]);
            }
            else
            {
                oStoreInfo.ErrorMessage = FeedbackMessage.NoDataFound;
            }

            List<PersonInfo> oPersonInfos = new List<PersonInfo>();
            PersonInfoService oPersonInfoService = new PersonInfoService();
            oPersonInfos = oPersonInfoService.Gets("", (string)Session[GlobalSession.UserID]);

            List<StoreInfo> oStoreInfos = new List<StoreInfo>();
            oStoreInfos = oStoreInfoService.Gets("", (string)Session[GlobalSession.UserID]);

            ViewBag.StoreInfo = oStoreInfo;
            ViewBag.StoreProducts = oStoreProducts;
            ViewBag.PersonInfos = oPersonInfos;
            ViewBag.StoreInfos = oStoreInfos;
            return View();
        }
        public ActionResult ViewStoreProductDelete(int nID)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            StoreInfo oStoreInfo = new StoreInfo();
            StoreInfoService oStoreInfoService = new StoreInfoService();

            List<StoreProduct> oStoreProducts = new List<StoreProduct>();
            StoreProductService oStoreProductService = new StoreProductService();

            if (nID > 0)
            {
                oStoreInfo = oStoreInfoService.Get(nID, (string)Session[GlobalSession.UserID]);
                oStoreProducts = oStoreProductService.Gets("SELECT * FROM AST.View_StoreProduct WHERE StoreID = " + oStoreInfo.StoreID + " ORDER BY ProductName", (string)Session[GlobalSession.UserID]);
            }
            else
            {
                oStoreInfo.ErrorMessage = FeedbackMessage.NoDataFound;
            }

            List<PersonInfo> oPersonInfos = new List<PersonInfo>();
            PersonInfoService oPersonInfoService = new PersonInfoService();
            oPersonInfos = oPersonInfoService.Gets("", (string)Session[GlobalSession.UserID]);

            List<StoreInfo> oStoreInfos = new List<StoreInfo>();
            oStoreInfos = oStoreInfoService.Gets("", (string)Session[GlobalSession.UserID]);

            ViewBag.StoreInfo = oStoreInfo;
            ViewBag.StoreProducts = oStoreProducts;
            ViewBag.PersonInfos = oPersonInfos;
            ViewBag.StoreInfos = oStoreInfos;
            return View();
        }
        public ActionResult ViewStoreProductList()
        {
            GlobalSession.SessionIsAlive(Session, Response);
            List<StoreInfo> oStoreInfos = new List<StoreInfo>();
            StoreInfoService oStoreInfoService = new StoreInfoService();
            oStoreInfos = oStoreInfoService.Gets("SELECT * FROM AST.StoreInfo WHERE IsInActive = 0 ORDER BY StoreName", (string)Session[GlobalSession.UserID]);
            ViewBag.StoreInfos = oStoreInfos;
            return View();
        }
        [HttpPost]
        public JsonResult Save(StoreProduct obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            try
            {
                if (obj.ErrorMessage == "ADD" && obj.StoreID > 0 && obj.ProductID > 0)
                {
                    _oStoreProduct = _oStoreProductService.IUD(obj, EnumDBOperation.SpecialCase1, (string)Session[GlobalSession.UserID]);
                }
                else if (obj.ErrorMessage == "DELETE" && obj.StoreID > 0 && obj.ProductID > 0)
                {
                    _oStoreProduct = _oStoreProductService.IUD(obj, EnumDBOperation.SpecialCase2, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oStoreProduct = new StoreProduct();
                    _oStoreProduct.ErrorMessage = FeedbackMessage.NoDataFound;
                }
            }
            catch (Exception ex)
            {
                _oStoreProduct = new StoreProduct();
                _oStoreProduct.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oStoreProduct);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(StoreProduct obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oStoreProduct = new StoreProduct();
            try
            {
                if (obj.StoreProductID > 0)
                {
                    _oStoreProduct = _oStoreProductService.IUD(obj, EnumDBOperation.Delete, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oStoreProduct.ErrorMessage = FeedbackMessage.NoIdFound;
                }
            }
            catch (Exception ex)
            {
                _oStoreProduct = new StoreProduct();
                _oStoreProduct.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oStoreProduct);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetStoreProductByStoreAndProductID(StoreProduct obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oStoreProduct = new StoreProduct();
            try
            {
                if (obj.StoreID > 0 && obj.ProductID > 0)
                {
                    _oStoreProduct = _oStoreProductService.GetByStoreAndProductID(obj, (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oStoreProduct = new StoreProduct();
                _oStoreProduct.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oStoreProduct);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SearchByStore(StoreProduct obj) 
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oStoreProduct = new StoreProduct();
            _oStoreProducts = new List<StoreProduct>() { };
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    _oStoreProducts = _oStoreProductService.GetsByStore(Convert.ToInt32(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oStoreProduct = new StoreProduct();
                _oStoreProduct.ErrorMessage = ex.Message;
                _oStoreProducts.Add(_oStoreProduct);
            }
            var jsonResult = Json(_oStoreProducts, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [HttpPost]
        public JsonResult SearchProducts(StoreProduct obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oStoreProduct = new StoreProduct();
            _oStoreProducts = new List<StoreProduct>() { };
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    _oStoreProducts = _oStoreProductService.Gets(this.GeneralSearchMakeSQL(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oStoreProduct = new StoreProduct();
                _oStoreProduct.ErrorMessage = ex.Message;
                _oStoreProducts.Add(_oStoreProduct);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oStoreProducts);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        private string GeneralSearchMakeSQL(string sVal)
        {
            return "SELECT * FROM AST.ProductInfo WHERE "
                                + " StoreProductName LIKE '%" + sVal + "%' OR"
                                + " StoreProductCode LIKE '%" + sVal + "%'";

        }
    }
}