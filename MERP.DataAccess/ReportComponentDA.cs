using MERP.Engine.GlobalClass;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.DataAccess
{
    public class ReportComponentDA
    {
        public static string Gets(string sSQL, string sUserID)
        {
            return GlobalHelpers.ExcecuteQurey("EXEC MOB.Get_ReportComponent", sSQL, sUserID);
        }
        public static string Get(int nID, string sUserIDID)
        {
            return "SELECT * FROM MOB.ReportComponent WHERE [ReportComponentID] =" + nID;
        }
        public static string GetDataForReport(string sSQL, string sUserID)
        {
            return sSQL;
        }
        
    }
}