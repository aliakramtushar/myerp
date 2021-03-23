using MERP.Engine.GlobalClass;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.DataAccess
{
    public class ReportDetailDA
    {
        public static string IUD(ReportDetail oReportDetail, EnumDBOperation oDBOperation, string sUserID)
        {
            return GlobalHelpers.ExcecuteQurey("EXEC MOB.SP_IUD_ReportDetail",
                oReportDetail.ReportDetailID, oReportDetail.ReportMasterID, (int)oReportDetail.ControlType, oReportDetail.ControlID, oReportDetail.ControlName,
                oReportDetail.FieldName, oReportDetail.LabelName, oReportDetail.Sequence, (int)oDBOperation, sUserID);
        }
        public static string Gets(string sSQL, string sUserID)
        {
            if (sSQL == "") return "SELECT * FROM MOB.ReportDetail";
            else return sSQL;
        }
        public static string GetsByParentID(int nReportMasterID, string sUserID)
        {
            return "SELECT * FROM MOB.ReportDetail WHERE ReportMasterID = " + nReportMasterID + " ORDER BY Sequence";
        }
        public static string Get(int nID, string sUserIDID)
        {
            return "SELECT * FROM MOB.ReportDetail WHERE [ReportDetailID] =" + nID;
        }
        public static string Delete(int nID, string sUserID)
        {
            return "DELETE FROM MOB.ReportDetail WHERE ReportDetailID = " + nID;
        }
        public static string DeleteByIDs(int nReportMasterID, string nReportDetailIDs, string sUserID)
        {
            return "DELETE FROM MOB.ReportDetail WHERE ReportMasterID = " + nReportMasterID + " AND ReportDetailID NOT IN (" + nReportDetailIDs + ")";
        }
        public static string DeleteByParentID(int nReportMasterID, string sUserID)
        {
            return "DELETE FROM MOB.ReportDetail WHERE ReportMasterID = " + nReportMasterID;
        }

        
    }
}