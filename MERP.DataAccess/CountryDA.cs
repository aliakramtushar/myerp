using MERP.Engine.GlobalClass;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.DataAccess
{
    public class CountryDA
    {
        public static string IUD(Country oCountry, EnumDBOperation oDBOperation, string sUserID)
        {
            return GlobalHelpers.ExcecuteQurey("EXEC SP_IUD_Country",
                oCountry.CountryID, oCountry.CountryName, (int)oCountry.ContinentID, (int)oCountry.ActivityStatus, oCountry.Remarks, (int)oDBOperation, sUserID);
        }
        public static string Gets(string sSQL, string sUserID)
        {
            if (sSQL == "") return "SELECT * FROM View_Country";
            else return sSQL;
        }
        public static string Get(int nID, string sUserID)
        {
            return "SELECT * FROM View_Country WHERE [CountryID] =" + nID;
        }
        public static string Delete(int nCountryID, string sUserID)
        {
            return "DELETE FROM Country WHERE CountryID = " + nCountryID;
        }
    }
}