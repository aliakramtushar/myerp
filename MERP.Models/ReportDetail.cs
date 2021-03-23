using MERP.Engine;
using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class ReportDetail
    {
        #region ReportDetail Defult
        public ReportDetail()
        {
            ReportDetailID = 0;
            ReportMasterID = 0;
            ControlType = EnumControlType.None;
            ControlID = "";
            ControlName = "";
            FieldName = "";
            LabelName = "";
            Sequence = 0;
            ErrorMessage = "";
        }
        #endregion

        #region Properties
        public int ReportDetailID { get; set; }
        public int ReportMasterID { get; set; }
        public EnumControlType ControlType { get; set; }
        public string ControlID { get; set; }
        public string ControlName { get; set; }
        public string FieldName { get; set; }
        public string LabelName { get; set; }
        public int Sequence { get; set; }
        public string ErrorMessage { get; set; }

        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.ReportDetailID; }
        }
        public string MModelString
        {
            get { return this.ControlType.ToString(); }
        }
        public string ControlTypeSt
        {
            get { return this.ControlType.ToString(); }
        }
        #endregion

        #region Functions
        public ReportDetail Get(int nId, string sUserID)
        {
            return ReportDetail.Service.Get(nId, sUserID);
        }
        public ReportDetail IUD(ReportDetail oReportDetail, EnumDBOperation oDBOperation, string sUserID)
        {
            return ReportDetail.Service.IUD(oReportDetail, oDBOperation, sUserID);
        }
        public static List<ReportDetail> Gets(string sSQL, string sUserID)
        {
            return ReportDetail.Service.Gets(sSQL, sUserID);
        }
        public static List<ReportDetail> GetsByParentID(int nReportmMsterID, string sUserID)
        {
            return ReportDetail.Service.GetsByParentID(nReportmMsterID, sUserID);
        }
        public string DeleteByIDs(int nReportMasterID, string sIDs, string sUserID)
        {
            return ReportDetail.Service.DeleteByIDs(nReportMasterID, sIDs, sUserID);
        }
        public string DeleteByParentID(int nReportMasterID, string sUserID)
        {
            return ReportDetail.Service.DeleteByParentID(nReportMasterID, sUserID);
        }
        public string Delete(int id, string sUserID)
        {
            return ReportDetail.Service.Delete(id, sUserID);
        }
        #endregion

        #region ServiceFactory
        internal static IReportDetail Service
        {
            get { return (IReportDetail)Services.Factory.CreateService(typeof(IReportDetail)); }
        }
        #endregion
    }
    #region IReportDetail interface
    public interface IReportDetail
    {
        ReportDetail Get(int id, string sUserID);
        List<ReportDetail> Gets(string sSQL, string sUserID);
        List<ReportDetail> GetsByParentID(int nReportMasterID, string sUserID);
        ReportDetail IUD(ReportDetail oReportDetail, EnumDBOperation oDBOperation, string sUserID);
        string Delete(int id, string sUserID);
        string DeleteByIDs(int nReportMasterID, string sIDs, string sUserID);
        string DeleteByParentID(int nReportMasterID, string sUserID);

    }
    #endregion
}