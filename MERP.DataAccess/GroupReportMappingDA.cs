using MERP.Engine.GlobalClass;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.DataAccess
{
    public class GroupReportMappingDA
    {
        public static string IUD(GroupReportMapping oGroupReportMapping, EnumDBOperation oDBOperation, string sUserID)
        {
            return GlobalHelpers.ExcecuteQurey("EXEC MOB.SP_IUD_GroupReportMapping",
                oGroupReportMapping.GroupReportMappingID, oGroupReportMapping.ReportMasterID, oGroupReportMapping.GroupID, oGroupReportMapping.ReportMasterIDs, oGroupReportMapping.GroupIDs, (int)oDBOperation, sUserID);
        }
        public static string Gets(string sSQL, string sUserID)
        {
            if (sSQL == "") return "SELECT * FROM MOB.View_GroupReportMapping";
            else return sSQL;
        }
        public static string GetsByReportMasterID(int nReportMasterID, string sUserID)
        {
            return "SELECT * from MOB.View_GroupReportMapping WHERE ReportMasterID = " + nReportMasterID;
        }
        public static string GetsByGroupID(int nGroupID, string sUserID)
        {
            return "SELECT * from MOB.View_GroupReportMapping WHERE GroupID = " + nGroupID;
        }
        public static string Get(int nID, string sUserID)
        {
            return "SELECT * FROM MOB.View_GroupReportMapping WHERE [GroupReportMappingID] =" + nID;
        }
        public static string Delete(int nID, string sUserID)
        {
            return "DELETE FROM MOB.GroupReportMapping WHERE GroupReportMappingID = " + nID;
        }
    }
}