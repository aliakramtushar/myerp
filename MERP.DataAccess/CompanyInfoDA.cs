using MERP.Engine.GlobalClass;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.DataAccess
{
    public class CompanyInfoDA
    {
        public static string IUD(CompanyInfo oCompanyInfo, EnumDBOperation oDBOperation, string sUserID)
        {
            return GlobalHelpers.ExcecuteQurey("EXEC COM.SP_IUD_CompanyInfo",
                oCompanyInfo.CompanyID, oCompanyInfo.CompanyName, oCompanyInfo.IsInActiveInt, oCompanyInfo.Remarks,
                (int)oDBOperation, sUserID);
        }
        public static string Gets(string sSQL, string sUserID)
        {
            if (sSQL == "") return "SELECT * FROM COM.CompanyInfo ORDER BY CompanyName";
            else return sSQL;
        }
        public static string GetsActiveCompanys(string sUserID)
        {
            return "SELECT * FROM COM.CompanyInfo WHERE IsInActive = 0 ORDER BY CompanyName";
        }
        public static string Get(int nID, string sUserID)
        {
            return "SELECT * FROM COM.CompanyInfo WHERE [CompanyID] =" + nID;
        }
    }
}