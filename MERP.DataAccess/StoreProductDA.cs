using MERP.Engine.GlobalClass;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.DataAccess
{
    public class StoreProductDA
    {
        public static string IUD(StoreProduct oStoreProduct, EnumDBOperation oDBOperation, string sUserID)
        {
            return GlobalHelpers.ExcecuteQurey("EXEC AST.SP_IUD_StoreProduct",
                oStoreProduct.StoreProductID, oStoreProduct.StoreID, oStoreProduct.ProductID, oStoreProduct.GoodQty, oStoreProduct.FaultyQty, oStoreProduct.ScrapQty,
                oStoreProduct.TransitGoodQty, oStoreProduct.TransitFaultyQty, oStoreProduct.TransitScrapQty, oStoreProduct.Remarks, oStoreProduct.SupplierID,
                oStoreProduct.TransitStoreID, (int)oDBOperation, sUserID);
        }
        public static string Gets(string sSQL, string sUserID)
        {
            if (sSQL == "") return "SELECT * FROM AST.View_StoreProduct ORDER BY ProductName";
            else return sSQL;
        }
        public static string GetsByStore(int nStoreID, string sUserID)
        {
            return "SELECT * FROM AST.View_StoreProduct WHERE StoreID = " + nStoreID + " ORDER BY ProductName";
        }
        public static string Get(int nID, string sUserID)
        {
            return "SELECT * FROM AST.View_StoreProduct WHERE [StoreProductID] =" + nID;
        }
        public static string GetByStoreAndProductID(StoreProduct oStoreProduct, string sUserID)
        {
            return "SELECT * FROM AST.View_StoreProduct WHERE StoreID =" + oStoreProduct.StoreID + " AND ProductID = " + oStoreProduct.ProductID;
        }
    }
}