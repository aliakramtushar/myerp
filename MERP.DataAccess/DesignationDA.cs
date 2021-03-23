using MERP.Engine.GlobalClass;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.DataAccess
{
    public class DesignationDA
    {
        public static string IUD(Designation oDesignation, EnumDBOperation oDBOperation, string sUserID)
        {
            return GlobalHelpers.ExcecuteQurey("EXEC SP_IUD_Designation",
                oDesignation.DesignationID, oDesignation.DesignationName, (int)oDesignation.ActivityStatus, oDesignation.Remarks, (int)oDBOperation, sUserID);
        }
        public static string Gets(string sSQL, string sUserID)
        {
            if (sSQL == "") return "SELECT * FROM View_Designation";
            else return sSQL;
        }
        public static string Get(int nID, string sUserID)
        {
            return "SELECT * FROM View_Designation WHERE [DesignationID] =" + nID;
        }
        public static string Delete(int nDesignationID, string sUserID)
        {
            return "DELETE FROM Designation WHERE DesignationID = " + nDesignationID;
        }
    }
}