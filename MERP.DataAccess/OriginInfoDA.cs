using MERP.Engine.GlobalClass;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.DataAccess
{
    public class OriginInfoDA
    {
        public static string IUD(OriginInfo oOriginInfo, EnumDBOperation oDBOperation, string sUserID)
        {
            return GlobalHelpers.ExcecuteQurey("EXEC COM.SP_IUD_OriginInfo",
                oOriginInfo.OriginID, oOriginInfo.OriginCode, oOriginInfo.OriginName, oOriginInfo.IsInActive,
                (int)oDBOperation, sUserID);
        }
        public static string Gets(string sSQL, string sUserID)
        {
            if (sSQL == "") return "SELECT * FROM COM.OriginInfo ORDER BY OriginName";
            else return sSQL;
        }
        public static string Get(int nID, string sUserID)
        {
            return "SELECT * FROM COM.OriginInfo WHERE [OriginID] =" + nID;
        }
        public static string Delete(int nID, string sUserID)
        {
            return "DELETE FROM COM.OriginInfo WHERE OriginID = " + nID;
        }
    }
}