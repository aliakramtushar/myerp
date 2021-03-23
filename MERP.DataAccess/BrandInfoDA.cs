using MERP.Engine.GlobalClass;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.DataAccess
{
    public class BrandInfoDA
    {
        public static string IUD(BrandInfo oBrandInfo, EnumDBOperation oDBOperation, string sUserID)
        {
            return GlobalHelpers.ExcecuteQurey("EXEC COM.SP_IUD_BrandInfo",
                oBrandInfo.BrandID, oBrandInfo.BrandCode, oBrandInfo.BrandName, oBrandInfo.IsInActive, oBrandInfo.OriginID,
                (int)oDBOperation, sUserID);
        }
        public static string Gets(string sSQL, string sUserID)
        {
            if (sSQL == "") return "SELECT * FROM COM.View_BrandInfo ORDER BY BrandName";
            else return sSQL;
        }
        public static string Get(int nID, string sUserID)
        {
            return "SELECT * FROM COM.View_BrandInfo WHERE [BrandID] =" + nID;
        }
        public static string Delete(int nID, string sUserID)
        {
            return "DELETE FROM COM.BrandInfo WHERE BrandID = " + nID;
        }
    }
}