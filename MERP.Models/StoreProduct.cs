using MERP.Engine;
using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class StoreProduct
    {
        #region StoreProduct Defult
        public StoreProduct()
        {
            StoreProductID = 0;
            StoreID = 0;
            ProductID = 0;
            GoodQty = 0;
            FaultyQty = 0;
            ScrapQty = 0;
            TransitGoodQty = 0;
            TransitFaultyQty = 0;
            TransitScrapQty = 0;
            Remarks = "";
            StoreName = "";
            ProductName = "";
            ErrorMessage = "";
        }
        #endregion

        #region Properties
        public int StoreProductID { get; set; }
        public int StoreID { get; set; }
        public int ProductID { get; set; }
        public int GoodQty { get; set; }
        public int FaultyQty { get; set; }
        public int ScrapQty { get; set; }
        public int TransitGoodQty { get; set; }
        public int TransitFaultyQty { get; set; }
        public int TransitScrapQty { get; set; }
        public string Remarks { get; set; }
        public int SupplierID { get; set; }
        public int TransitStoreID { get; set; }
        public string StoreName { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ShortName { get; set; }
        public string ErrorMessage { get; set; }
        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.StoreProductID; }
        }
        public string MModelString
        {
            get { return this.StoreName; }
        }
        #endregion

        #region Functions
        public StoreProduct Get(int nId, string sUserID)
        {
            return StoreProduct.Service.Get(nId, sUserID);
        }
        public StoreProduct GetByStoreAndProductID(StoreProduct oStoreProduct, string sUserID)
        {
            return StoreProduct.Service.GetByStoreAndProductID(oStoreProduct, sUserID);
        }
        
        public StoreProduct IUD(StoreProduct oStoreProduct, EnumDBOperation oDBOperation, string sUserID)
        {
            return StoreProduct.Service.IUD(oStoreProduct, oDBOperation, sUserID);
        }
        public static List<StoreProduct> Gets(string sSQL, string sUserID)
        {
            return StoreProduct.Service.Gets(sSQL, sUserID);
        }
        public static List<StoreProduct> GetsByStore(int nStoreID, string sUserID)
        {
            return StoreProduct.Service.GetsByStore(nStoreID, sUserID);
        }
        
        #endregion

        #region ServiceFactory
        internal static IStoreProduct Service
        {
            get { return (IStoreProduct)Services.Factory.CreateService(typeof(IStoreProduct)); }
        }
        #endregion
    }
    #region IStoreProduct interface
    public interface IStoreProduct
    {
        StoreProduct Get(int id, string sUserID);
        StoreProduct GetByStoreAndProductID(StoreProduct oStoreProduct, string sUserID);
        List<StoreProduct> Gets(string sSQL, string sUserID);
        List<StoreProduct> GetsByStore(int nStoreID, string sUserID);
        StoreProduct IUD(StoreProduct oStoreProduct, EnumDBOperation oDBOperation, string sUserID);
    }
    #endregion
}