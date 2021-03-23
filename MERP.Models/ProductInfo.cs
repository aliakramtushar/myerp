using MERP.Engine;
using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class ProductInfo
    {
        #region ProductInfo Defult
        public ProductInfo()
        {
            ProductID = 0;
            ProductCode = "";
            ProductName = "";
            ShortName = "";
            ImagePath = "";
            SerialNo = "";
            TagNo = "";
            ModelNo = "";
            ProductTypeID = 0;
            ProductTypeName = "";
            ProductCategoryID = 0;
            ProductCategoryName = "";
            ProductSubCategoryID = 0;
            ProductSubCategoryName = "";
            SegmentID = 0;
            SegmentName = "";
            ProductStatus = EnumActivityStatus.None;
            CostCenterID = 0;
            CostCenterName = "";
            DepartmentID = 0;
            DepartmentName = "";
            BrandID = 0;
            BrandName = "";
            Description = "";
            LotID = 0;
            SupplierPersonID = 0;
            SupplierPersonName = "";
            LaunchingDate = DateTime.MinValue;
            WarrantyInMonth = 0;
            ServiceLifeInMonth = 0;
            DepreciationRate = 0.00;
            RefCode = "";
            PurchaseDate = DateTime.MinValue;
            Remarks = "";
            ErrorMessage = "";
        }
        #endregion

        #region Properties
        public int ProductID { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ShortName { get; set; }
        public string ImagePath { get; set; }
        public string SerialNo { get; set; }
        public string TagNo { get; set; }
        public string ModelNo { get; set; }
        public int ProductTypeID { get; set; }
        public string ProductTypeName { get; set; }
        public int ProductCategoryID { get; set; }
        public string ProductCategoryName { get; set; }
        public int ProductSubCategoryID { get; set; }
        public string ProductSubCategoryName { get; set; }
        public int SegmentID { get; set; }
        public string SegmentName { get; set; }
        public EnumActivityStatus ProductStatus { get; set; }
        public int CostCenterID { get; set; }
        public string CostCenterName { get; set; }
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int BrandID { get; set; }
        public string BrandName { get; set; }
        public string Description { get; set; }
        public int LotID { get; set; }
        public int SupplierPersonID { get; set; }
        public string SupplierPersonName { get; set; }
        public DateTime LaunchingDate { get; set; }
        public int WarrantyInMonth { get; set; }
        public int ServiceLifeInMonth { get; set; }
        public double DepreciationRate { get; set; }
        public string RefCode { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Remarks { get; set; }
        public string ErrorMessage { get; set; }

        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.ProductID; }
        }
        public string MModelString
        {
            get { return this.ProductName; }
        }
        public string ProductStatusSt
        {
            get { return this.ProductStatus.ToString(); }
        }
        public string LaunchingDateSt
        {
            get { return (this.LaunchingDate == DateTime.MinValue) ? "" : this.LaunchingDate.ToString("dd-MM-yyyy"); }
        }
        public string PurchaseDateSt
        {
            get { return (this.PurchaseDate == DateTime.MinValue) ? "" : this.PurchaseDate.ToString("dd-MM-yyyy"); }
        }
        #endregion

        #region Functions
        public ProductInfo Get(int nId, string sUserID)
        {
            return ProductInfo.Service.Get(nId, sUserID);
        }
        public ProductInfo IUD(ProductInfo oProductInfo, EnumDBOperation oDBOperation, string sUserID)
        {
            return ProductInfo.Service.IUD(oProductInfo, oDBOperation, sUserID);
        }
        public static List<ProductInfo> Gets(string sSQL, string sUserID)
        {
            return ProductInfo.Service.Gets(sSQL, sUserID);
        }
        public string Delete(int id, string sUserID)
        {
            return ProductInfo.Service.Delete(id, sUserID);
        }
        #endregion

        #region ServiceFactory
        internal static IProductInfo Service
        {
            get { return (IProductInfo)Services.Factory.CreateService(typeof(IProductInfo)); }
        }
        #endregion
    }
    #region IProductInfo interface
    public interface IProductInfo
    {
        ProductInfo Get(int id, string sUserID);
        List<ProductInfo> Gets(string sSQL, string sUserID);
        ProductInfo IUD(ProductInfo oProductInfo, EnumDBOperation oDBOperation, string sUserID);
        string Delete(int id, string sUserID);
    }
    #endregion
}