using MERP.Engine.GlobalClass;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.DataAccess
{
    public class LocationDA
    {
        public static string IUD(Location oLocation, EnumDBOperation oDBOperation, string sUserID)
        {
            return GlobalHelpers.ExcecuteQurey("EXEC SP_IUD_Location",
                oLocation.LocationID, oLocation.LocationName, (int)oLocation.DivisionID, oLocation.DistrictID, (int)oLocation.ActivityStatus, oLocation.Remarks, (int)oDBOperation, sUserID);
        }
        public static string Gets(string sSQL, string sUserID)
        {
            if (sSQL == "") return "SELECT * FROM View_Location";
            else return sSQL;
        }
        public static string Get(int nID, string sUserID)
        {
            return "SELECT * FROM View_Location WHERE [LocationID] =" + nID;
        }
        public static string Delete(int nLocationID, string sUserID)
        {
            return "DELETE FROM Location WHERE LocationID = " + nLocationID;
        }
    }
}