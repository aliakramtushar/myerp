using MERP.Engine.GlobalClass;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.DataAccess
{
    public class ReportMasterDA
    {
        public static string IUD(ReportMaster oReportMaster, EnumDBOperation oDBOperation, string sUserID)
        {
            return GlobalHelpers.ExcecuteQurey("EXEC MOB.SP_IUD_ReportMaster",
                oReportMaster.ReportMasterID, oReportMaster.ReportName, oReportMaster.Query, oReportMaster.IsSP, oReportMaster.IsExcel,
                oReportMaster.IsPDF, oReportMaster.IsInActive, oReportMaster.RptFileName, oReportMaster.Remarks, (int)oDBOperation, sUserID);
        }
        public static string Gets(string sSQL, string sUserID)
        {
            if (sSQL == "") return "SELECT * FROM MOB.ReportMaster";
            else return sSQL;
        }
        public static string GetsAccessedReports(string sUserID)
        {
            return "SELECT * FROM MOB.ReportMaster WHERE IsInActive = 0 AND ReportMasterID IN(SELECT ReportMasterID FROM MOB.GroupReportMapping WHERE GroupID IN(SELECT GroupID FROM Users.UserInfo WHERE UserCode = '" + sUserID + "'))";
        }
        public static string GetsActiveReports(string sUserID)
        {
            return "SELECT * FROM MOB.ReportMaster WHERE IsInActive = 0";
        }
        public static string Get(int nID, string sUserIDID)
        {
            return "SELECT * FROM MOB.ReportMaster WHERE [ReportMasterID] =" + nID;
        }
        public static string Delete(int nID, string sUserID)
        {
            return "DELETE FROM MOB.ReportDetail WHERE ReportMasterID = " + nID + "  DELETE FROM MOB.ReportMaster WHERE ReportMasterID = " + nID;
        }
    }
}