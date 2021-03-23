using MERP.Engine;
using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class ProductType
    {
        #region ProductType Defult
        public ProductType()
        {
            ProductTypeID = 0;
            ProductTypeCode = "Auto Generated";
            ProductTypeName = "";
            IsInActive = false;
            ErrorMessage = "";
        }
        #endregion

        #region Properties
        public int ProductTypeID { get; set; }
        public string ProductTypeCode { get; set; }
        public string ProductTypeName { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsInActive { get; set; }

        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.ProductTypeID; }
        }
        public string MModelString
        {
            get { return this.ProductTypeName + " (" + this.ProductTypeCode + ")"; }
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
        public ProductType Get(int nId, string sUserID)
        {
            return ProductType.Service.Get(nId, sUserID);
        }
        public ProductType IUD(ProductType oProductType, EnumDBOperation oDBOperation, string sUserID)
        {
            return ProductType.Service.IUD(oProductType, oDBOperation, sUserID);
        }
        public static List<ProductType> Gets(string sSQL, string sUserID)
        {
            return ProductType.Service.Gets(sSQL, sUserID);
        }
        #endregion

        #region ServiceFactory
        internal static IProductType Service
        {
            get { return (IProductType)Services.Factory.CreateService(typeof(IProductType)); }
        }
        #endregion
    }
    #region IProductType interface
    public interface IProductType
    {
        ProductType Get(int id, string sUserID);
        List<ProductType> Gets(string sSQL, string sUserID);
        ProductType IUD(ProductType oProductType, EnumDBOperation oDBOperation, string sUserID);
    }
    #endregion
}