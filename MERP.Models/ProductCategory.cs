using MERP.Engine;
using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class ProductCategory
    {
        #region ProductCategory Defult
        public ProductCategory()
        {
            ProductCategoryID = 0;
            ProductTypeID = 0;
            ProductCategoryCode = "";
            ProductCategoryName = "";
            ProductTypeName = "";
            IsInActive = false;
            ErrorMessage = "";
        }
        #endregion

        #region Properties
        public int ProductCategoryID { get; set; }
        public int ProductTypeID { get; set; }
        public string ProductCategoryCode { get; set; }
        public string ProductCategoryName { get; set; }
        public string ProductTypeName { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsInActive { get; set; }

        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.ProductCategoryID; }
        }
        public string MModelString
        {
            get { return this.ProductCategoryName; }
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
        public ProductCategory Get(int nId, string sUserID)
        {
            return ProductCategory.Service.Get(nId, sUserID);
        }
        public ProductCategory IUD(ProductCategory oProductCategory, EnumDBOperation oDBOperation, string sUserID)
        {
            return ProductCategory.Service.IUD(oProductCategory, oDBOperation, sUserID);
        }
        public static List<ProductCategory> Gets(string sSQL, string sUserID)
        {
            return ProductCategory.Service.Gets(sSQL, sUserID);
        }
        #endregion

        #region ServiceFactory
        internal static IProductCategory Service
        {
            get { return (IProductCategory)Services.Factory.CreateService(typeof(IProductCategory)); }
        }
        #endregion
    }
    #region IProductCategory interface
    public interface IProductCategory
    {
        ProductCategory Get(int id, string sUserID);
        List<ProductCategory> Gets(string sSQL, string sUserID);
        ProductCategory IUD(ProductCategory oProductCategory, EnumDBOperation oDBOperation, string sUserID);
    }
    #endregion
}