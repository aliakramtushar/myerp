using MERP.Engine.GlobalClass;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.DataAccess
{
    public class ProductCategoryDA
    {
        public static string IUD(ProductCategory oProductCategory, EnumDBOperation oDBOperation, string sUserID)
        {
            return GlobalHelpers.ExcecuteQurey("EXEC AST.SP_IUD_ProductCategory",
                oProductCategory.ProductCategoryID, oProductCategory.ProductTypeID, oProductCategory.ProductCategoryCode, oProductCategory.ProductCategoryName, 
                oProductCategory.IsInActive, (int)oDBOperation, sUserID);
        }
        public static string Gets(string sSQL, string sUserID)
        {
            if (sSQL == "") return "SELECT * FROM AST.View_ProductCategory";
            else return sSQL;
        }
        public static string Get(int nID, string sUserID)
        {
            return "SELECT * FROM AST.View_ProductCategory WHERE [ProductCategoryID] =" + nID;
        }
    }
}