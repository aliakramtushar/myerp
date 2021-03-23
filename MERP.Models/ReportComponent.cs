using MERP.Engine;
using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class ReportComponent
    {
        #region ReportComponent Defult
        public ReportComponent()
        {
            ReportComponentID = 0;
            ReportComponentName = "";
            Remarks = "";
            ErrorMessage = "";
        }
        #endregion

        #region Properties
        public int ReportComponentID { get; set; }
        public string ReportComponentName { get; set; }
        public string Remarks { get; set; }
        public string ErrorMessage { get; set; }

        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.ReportComponentID; }
        }
        public string MModelString
        {
            get { return this.ReportComponentName; }
        }
        #endregion

        #region Functions
        public ReportComponent Get(int nId, string sUserID)
        {
            return ReportComponent.Service.Get(nId, sUserID);
        }
        public static List<ReportComponent> Gets(string sSQL, string sUserID)
        {
            return ReportComponent.Service.Gets(sSQL, sUserID);
        }
        #endregion

        #region ServiceFactory
        internal static IReportComponent Service
        {
            get { return (IReportComponent)Services.Factory.CreateService(typeof(IReportComponent)); }
        }
        #endregion
    }
    #region IReportComponent interface
    public interface IReportComponent
    {
        ReportComponent Get(int id, string sUserID);
        List<ReportComponent> Gets(string sSQL, string sUserID);
    }
    #endregion
}