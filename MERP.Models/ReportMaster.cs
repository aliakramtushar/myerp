using MERP.Engine;
using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class ReportMaster
    {
        #region ReportMaster Defult
        public ReportMaster()
        {
            ReportMasterID = 0;
            ReportName = "";
            Query = "";
            IsSP = false;
            IsExcel = false;
            IsPDF = false;
            IsInActive = false;
            RptFileName = "";
            Remarks = "";
            ReportDetails = new List<ReportDetail>();
            ErrorMessage = "";
        }
        #endregion

        #region Properties
        public int ReportMasterID { get; set; }
        public string ReportName { get; set; }
        public string Query { get; set; }
        public bool IsSP { get; set; }
        public bool IsExcel { get; set; }
        public bool IsPDF { get; set; }
        public bool IsInActive { get; set; }
        public string RptFileName { get; set; }
        public string Remarks { get; set; }
        public List<ReportDetail> ReportDetails { get; set; }
        public string ErrorMessage { get; set; }

        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.ReportMasterID; }
        }
        public string MModelString
        {
            get { return this.ReportName; }
        }
        public string IsInActiveSt
        {
            get { return this.IsInActive.ToString(); }
        }
        public string IsSPSt
        {
            get { return this.IsSP.ToString(); }
        }
        #endregion

        #region Functions
        public ReportMaster Get(int nId, string sUserID)
        {
            return ReportMaster.Service.Get(nId, sUserID);
        }
        public ReportMaster IUD(ReportMaster oReportMaster, EnumDBOperation oDBOperation, string sUserID)
        {
            return ReportMaster.Service.IUD(oReportMaster, oDBOperation, sUserID);
        }
        public static List<ReportMaster> Gets(string sSQL, string sUserID)
        {
            return ReportMaster.Service.Gets(sSQL, sUserID);
        }
        public static List<ReportMaster> GetsAccessedReports(string sUserID)
        {
            return ReportMaster.Service.GetsAccessedReports(sUserID);
        }
        public static List<ReportMaster> GetsActiveReports(string sUserID)
        {
            return ReportMaster.Service.GetsActiveReports(sUserID);
        }
        public string Delete(int id, string sUserID)
        {
            return ReportMaster.Service.Delete(id, sUserID);
        }
        #endregion

        #region ServiceFactory
        internal static IReportMaster Service
        {
            get { return (IReportMaster)Services.Factory.CreateService(typeof(IReportMaster)); }
        }
        #endregion
    }
    #region IReportMaster interface
    public interface IReportMaster
    {
        ReportMaster Get(int id, string sUserID);
        List<ReportMaster> Gets(string sSQL, string sUserID);
        List<ReportMaster> GetsAccessedReports(string sUserID);
        List<ReportMaster> GetsActiveReports(string sUserID);
        ReportMaster IUD(ReportMaster oReportMaster, EnumDBOperation oDBOperation, string sUserID);
        string Delete(int id, string sUserID);
    }
    #endregion
}