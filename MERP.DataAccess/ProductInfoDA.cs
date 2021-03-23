using MERP.Engine.GlobalClass;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.DataAccess
{
    public class ProductInfoDA
    {
        public static string IUD(ProductInfo oProductInfo, EnumDBOperation oDBOperation, string sUserID)
        {
            return GlobalHelpers.ExcecuteQurey("EXEC AST.SP_IUD_ProductInfo",
                oProductInfo.ProductID, oProductInfo.ProductCode, oProductInfo.ProductName, oProductInfo.ShortName, oProductInfo.ImagePath, oProductInfo.SerialNo, 
                oProductInfo.TagNo, oProductInfo.ModelNo, oProductInfo.ProductTypeID, oProductInfo.ProductCategoryID, oProductInfo.ProductSubCategoryID, oProductInfo.SegmentID,
                (int)oProductInfo.ProductStatus, oProductInfo.CostCenterID, oProductInfo.DepartmentID, oProductInfo.BrandID, oProductInfo.Description, oProductInfo.LotID,
                oProductInfo.SupplierPersonID, oProductInfo.LaunchingDate, oProductInfo.WarrantyInMonth, oProductInfo.ServiceLifeInMonth, oProductInfo.DepreciationRate,
                oProductInfo.RefCode, oProductInfo.Remarks, oProductInfo.PurchaseDate, (int)oDBOperation, sUserID);
        }
        public static string Gets(string sSQL, string sUserID)
        {
            if (sSQL == "") return "SELECT * FROM AST.View_ProductInfo ORDER BY ProductName";
            else return sSQL;
        }
        public static string Get(int nID, string sUserID)
        {
            return "SELECT * FROM AST.View_ProductInfo WHERE [ProductID] =" + nID;
        }
        public static string Delete(int nID, string sUserID)
        {
            return "DELETE FROM AST.ProductInfo WHERE ProductID = " + nID;
        }
    }
}