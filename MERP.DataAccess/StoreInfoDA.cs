using MERP.Engine.GlobalClass;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.DataAccess
{
    public class StoreInfoDA
    {
        public static string IUD(StoreInfo oStoreInfo, EnumDBOperation oDBOperation, string sUserID)
        {
            return GlobalHelpers.ExcecuteQurey("EXEC AST.SP_IUD_StoreInfo",
                oStoreInfo.StoreID, oStoreInfo.StoreName, oStoreInfo.ShortName, oStoreInfo.StoreCode, oStoreInfo.Address, oStoreInfo.IsInActive,
                (int)oDBOperation, sUserID);
        }
        public static string Gets(string sSQL, string sUserID)
        {
            if (sSQL == "") return "SELECT * FROM AST.StoreInfo ORDER BY StoreName";
            else return sSQL;
        }
        public static string Get(int nID, string sUserID)
        {
            return "SELECT * FROM AST.StoreInfo WHERE [StoreID] =" + nID;
        }
    }
}