using MERP.Engine.GlobalClass;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.DataAccess
{
    public class DistrictDA
    {
        public static string IUD(District oDistrict, EnumDBOperation oDBOperation, string sUserID)
        {
            return GlobalHelpers.ExcecuteQurey("EXEC SP_IUD_District",
                oDistrict.DistrictID, oDistrict.DistrictName, (int)oDistrict.DivisionID, (int)oDistrict.ActivityStatus, oDistrict.Remarks, (int)oDBOperation, sUserID);
        }
        public static string Gets(string sSQL, string sUserID)
        {
            if (sSQL == "") return "SELECT * FROM View_District";
            else return sSQL;
        }
        public static string Get(int nID, string sUserID)
        {
            return "SELECT * FROM View_District WHERE [DistrictID] =" + nID;
        }
        public static string Delete(int nDistrictID, string sUserID)
        {
            return "DELETE FROM District WHERE DistrictID = " + nDistrictID;
        }
    }
}