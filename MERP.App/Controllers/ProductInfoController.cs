using MERP.Engine.GlobalClass;
using MERP.Models;
using MERP.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MERP.App.Controllers
{
    public class ProductInfoController : Controller
    {
        ProductInfo _oProductInfo = new ProductInfo();
        List<ProductInfo> _oProductInfos = new List<ProductInfo>();
        ProductInfoService _oProductInfoService = new ProductInfoService();
        public ActionResult ViewProductInfos()
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oProductInfos = _oProductInfoService.Gets("", (string)Session[GlobalSession.UserID]);
            ViewBag.ProductInfos = _oProductInfos;
            return View();
        }
        public ActionResult ViewProductInfo(int nID)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oProductInfo = new ProductInfo();
            if (nID > 0)
            {
                _oProductInfo = _oProductInfoService.Get(nID, (string)Session[GlobalSession.UserID]);
            }
            List<BrandInfo> oBrandInfos = new List<BrandInfo>();
            BrandInfoService oBrandInfoService = new BrandInfoService();
            oBrandInfos = oBrandInfoService.Gets("", (string)Session[GlobalSession.UserID]);

            List<ProductType> oProductTypes = new List<ProductType>();
            ProductTypeService oProductTypeService = new ProductTypeService();
            oProductTypes = oProductTypeService.Gets("", (string)Session[GlobalSession.UserID]);

            List<ProductCategory> oProductCategorys = new List<ProductCategory>();
            ProductCategoryService oProductCategoryService = new ProductCategoryService();
            oProductCategorys = oProductCategoryService.Gets("", (string)Session[GlobalSession.UserID]);

            List<ProductSubCategory> oProductSubCategorys = new List<ProductSubCategory>();
            ProductSubCategoryService oProductSubCategoryService = new ProductSubCategoryService();
            oProductSubCategorys = oProductSubCategoryService.Gets("", (string)Session[GlobalSession.UserID]);

            List<SegmentInfo> oSegmentInfos = new List<SegmentInfo>();
            SegmentInfoService oSegmentInfoService = new SegmentInfoService();
            oSegmentInfos = oSegmentInfoService.Gets("", (string)Session[GlobalSession.UserID]);

            List<CostCenter> oCostCenters = new List<CostCenter>();
            CostCenterService oCostCenterService = new CostCenterService();
            oCostCenters = oCostCenterService.Gets("", (string)Session[GlobalSession.UserID]);

            List<PersonInfo> oPersonInfos = new List<PersonInfo>();
            PersonInfoService oPersonInfoService = new PersonInfoService();
            oPersonInfos = oPersonInfoService.Gets("", (string)Session[GlobalSession.UserID]);

            ViewBag.ActivityStatus = Enum.GetValues(typeof(EnumActivityStatus)).Cast<EnumActivityStatus>().Select(e => new EnumClass { MModelPK = ((int)e), MModelString = e.ToString() });
            ViewBag.BrandInfos = oBrandInfos;
            ViewBag.ProductTypes = oProductTypes;
            ViewBag.ProductCategorys = oProductCategorys;
            ViewBag.ProductSubCategorys = oProductSubCategorys;
            ViewBag.SegmentInfos = oSegmentInfos;
            ViewBag.CostCenters = oCostCenters;
            ViewBag.PersonInfos = oPersonInfos;
            ViewBag.ProductInfo = _oProductInfo;
            return View();
        }
        [HttpPost]
        public JsonResult Save(ProductInfo obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            try
            {
                if (obj.ProductID == 0)
                {
                    _oProductInfo = _oProductInfoService.IUD(obj, EnumDBOperation.Insert, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oProductInfo = _oProductInfoService.IUD(obj, EnumDBOperation.Update, (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oProductInfo = new ProductInfo();
                _oProductInfo.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oProductInfo);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(ProductInfo obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oProductInfo = new ProductInfo();
            try
            {
                if (obj.ProductID > 0)
                {
                    _oProductInfo.ErrorMessage = _oProductInfoService.Delete(obj.ProductID, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oProductInfo.ErrorMessage = FeedbackMessage.NoIdFound;
                }
            }
            catch (Exception ex)
            {
                _oProductInfo = new ProductInfo();
                _oProductInfo.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oProductInfo);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Search(ProductInfo obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oProductInfo = new ProductInfo();
            _oProductInfos = new List<ProductInfo>() { };
            try
            {
                _oProductInfos = _oProductInfoService.Gets(this.GeneralSearchMakeSQL(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
            }
            catch (Exception ex)
            {
                _oProductInfo = new ProductInfo();
                _oProductInfo.ErrorMessage = ex.Message;
                _oProductInfos.Add(_oProductInfo);
            }
            var jsonResult = Json(_oProductInfos, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [HttpPost]
        public JsonResult SearchByProductNameAndCode(ProductInfo obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oProductInfo = new ProductInfo();
            _oProductInfos = new List<ProductInfo>() { };
            try
            {
                _oProductInfos = _oProductInfoService.Gets("SELECT * FROM AST.View_ProductInfo WHERE ProductName LIKE '%"+ obj.ErrorMessage + "%' OR ProductCode LIKE '%"+ obj.ErrorMessage + "%'", (string)Session[GlobalSession.UserID]);
            }
            catch (Exception ex)
            {
                _oProductInfo = new ProductInfo();
                _oProductInfo.ErrorMessage = ex.Message;
                _oProductInfos.Add(_oProductInfo);
            }
            var jsonResult = Json(_oProductInfos, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        private string GeneralSearchMakeSQL(string sString)
        {
            int nCount = 0;
            string sVal = sString.Split('~')[nCount++];
            bool isDate = Convert.ToBoolean(sString.Split('~')[nCount++]);
            string sStartDate = sString.Split('~')[nCount++];
            string sEndDate = sString.Split('~')[nCount++];

            string sTable = "SELECT * FROM MOB.View_ProductInfo WHERE ProductID IN (SELECT ProductID FROM MOB.View_ProductInfo WHERE ";
            string commonSQL1 = " BankName LIKE '%" + sVal + "%' OR"
                    + " BankAccountName LIKE '%" + sVal + "%' OR"
                    + " BankAccountNo LIKE '%" + sVal + "%' OR"
                    + " Remarks LIKE '%" + sVal + "%' OR"
                    + " DepositCode LIKE '%" + sVal + "%' OR"
                    + " ApprovedBy LIKE '%" + sVal + "%' OR"
                    + " BranchName LIKE '%" + sVal + "%' OR"
                    + " RefNo LIKE '%" + sVal + "%'";
            string commonSQL2 = " CAST(DepositDate AS DATE) BETWEEN '" + GlobalHelpers.GetDateSt(sStartDate) + "' AND '" + GlobalHelpers.GetDateSt(sEndDate) + "'";
            string finalSQL = sTable + (isDate ? commonSQL2 : commonSQL1) + ") ";
            if ((int)Session[GlobalSession.BranchID] != 1) finalSQL = finalSQL + " AND BranchID = " + (int)Session[GlobalSession.BranchID];
            return finalSQL + " ORDER BY DepositDate DESC";
        }
    }
}