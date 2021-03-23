using MERP.Engine.GlobalClass;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.DataAccess
{
    public class ProductSubCategoryDA
    {
        public static string IUD(ProductSubCategory oProductSubCategory, EnumDBOperation oDBOperation, string sUserID)
        {
            return GlobalHelpers.ExcecuteQurey("EXEC AST.SP_IUD_ProductSubCategory",
                oProductSubCategory.ProductSubCategoryID, oProductSubCategory.ProductCategoryID, oProductSubCategory.ProductSubCategoryCode, oProductSubCategory.ProductSubCategoryName,
                oProductSubCategory.IsInActive, (int)oDBOperation, sUserID);
        }
        public static string Gets(string sSQL, string sUserID)
        {
            if (sSQL == "") return "SELECT * FROM AST.View_ProductSubCategory";
            else return sSQL;
        }
        public static string Get(int nID, string sUserID)
        {
            return "SELECT * FROM AST.View_ProductSubCategory WHERE [ProductSubCategoryID] =" + nID;
        }
    }
}