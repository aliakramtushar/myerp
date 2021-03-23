using MERP.Engine;
using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class ProductSubCategory
    {
        #region ProductSubCategory Defult
        public ProductSubCategory()
        {
            ProductSubCategoryID = 0;
            ProductCategoryID = 0;
            ProductSubCategoryCode = "";
            ProductSubCategoryName = "";
            ProductCategoryName = "";
            IsInActive = false;
            ErrorMessage = "";
        }
        #endregion

        #region Properties
        public int ProductSubCategoryID { get; set; }
        public int ProductCategoryID { get; set; }
        public string ProductSubCategoryCode { get; set; }
        public string ProductSubCategoryName { get; set; }
        public string ProductCategoryName { get; set; }
        public bool IsInActive { get; set; }
        public string ErrorMessage { get; set; }

        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.ProductSubCategoryID; }
        }
        public string MModelString
        {
            get { return this.ProductSubCategoryName; }
        }
        public string IsInActiveSt
        {
            get { return this.IsInActive.ToString(); }
        }
        public int IsInActiveInt
        {
            get { return Convert.ToInt16(this.IsInActive); }
        }
        #endregion

        #region Functions
        public ProductSubCategory Get(int nId, string sUserID)
        {
            return ProductSubCategory.Service.Get(nId, sUserID);
        }
        public ProductSubCategory IUD(ProductSubCategory oProductSubCategory, EnumDBOperation oDBOperation, string sUserID)
        {
            return ProductSubCategory.Service.IUD(oProductSubCategory, oDBOperation, sUserID);
        }
        public static List<ProductSubCategory> Gets(string sSQL, string sUserID)
        {
            return ProductSubCategory.Service.Gets(sSQL, sUserID);
        }
        #endregion

        #region ServiceFactory
        internal static IProductSubCategory Service
        {
            get { return (IProductSubCategory)Services.Factory.CreateService(typeof(IProductSubCategory)); }
        }
        #endregion
    }
    #region IProductSubCategory interface
    public interface IProductSubCategory
    {
        ProductSubCategory Get(int id, string sUserID);
        List<ProductSubCategory> Gets(string sSQL, string sUserID);
        ProductSubCategory IUD(ProductSubCategory oProductSubCategory, EnumDBOperation oDBOperation, string sUserID);
    }
    #endregion
}