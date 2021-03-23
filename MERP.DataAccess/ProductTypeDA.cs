using MERP.Engine.GlobalClass;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.DataAccess
{
    public class ProductTypeDA
    {
        public static string IUD(ProductType oProductType, EnumDBOperation oDBOperation, string sUserID)
        {
            return GlobalHelpers.ExcecuteQurey("EXEC AST.SP_IUD_ProductType",
                oProductType.ProductTypeID, oProductType.ProductTypeCode, oProductType.ProductTypeName, oProductType.IsInActive,
                (int)oDBOperation, sUserID);
        }
        public static string Gets(string sSQL, string sUserID)
        {
            if (sSQL == "") return "SELECT * FROM AST.ProductType ORDER BY ProductTypeName";
            else return sSQL;
        }
        public static string Get(int nID, string sUserID)
        {
            return "SELECT * FROM AST.ProductType WHERE [ProductTypeID] =" + nID;
        }
    }
}