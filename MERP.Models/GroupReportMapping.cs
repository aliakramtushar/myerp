using MERP.Engine;
using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class GroupReportMapping
    {
        #region GroupReportMapping Defult
        public GroupReportMapping()
        {
            GroupReportMappingID = 0;
            GroupID = 0;
            ReportMasterID = 0;
            GroupName = "";
            ReportName = "";
            ErrorMessage = "";
        }
        #endregion

        #region Properties
        public int GroupReportMappingID { get; set; }
        public int GroupID { get; set; }
        public int ReportMasterID { get; set; }
        public string GroupName { get; set; }
        public string ReportName { get; set; }
        public string ReportMasterIDs { get; set; }
        public string GroupIDs { get; set; }
        public string ErrorMessage { get; set; }
        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.GroupReportMappingID; }
        }
        public string MModelString
        {
            get { return ""; }
        }
        #endregion

        #region Functions
        public GroupReportMapping Get(int nId, string sUserID)
        {
            return GroupReportMapping.Service.Get(nId, sUserID);
        }
        public GroupReportMapping IUD(GroupReportMapping oGroupReportMapping, EnumDBOperation oDBOperation, string sUserID)
        {
            return GroupReportMapping.Service.IUD(oGroupReportMapping, oDBOperation, sUserID);
        }
        public GroupReportMapping SaveByReportMasterID(GroupReportMapping oGroupReportMapping, string sUserID)
        {
            return GroupReportMapping.Service.SaveByReportMasterID(oGroupReportMapping, sUserID);
        }
        public GroupReportMapping SaveByGroupID(GroupReportMapping oGroupReportMapping, string sUserID)
        {
            return GroupReportMapping.Service.SaveByGroupID(oGroupReportMapping, sUserID);
        }
        public static List<GroupReportMapping> Gets(string sSQL, string sUserID)
        {
            return GroupReportMapping.Service.Gets(sSQL, sUserID);
        }
        public static List<GroupReportMapping> GetsByReportMasterID(int nReportMasterID, string sUserID)
        {
            return GroupReportMapping.Service.GetsByReportMasterID(nReportMasterID, sUserID);
        }
        public static List<GroupReportMapping> GetsByGroupID(int nGroupID, string sUserID)
        {
            return GroupReportMapping.Service.GetsByGroupID(nGroupID, sUserID);
        }
        public string Delete(int id, string sUserID)
        {
            return GroupReportMapping.Service.Delete(id, sUserID);
        }
        #endregion

        #region ServiceFactory
        internal static IGroupReportMapping Service
        {
            get { return (IGroupReportMapping)Services.Factory.CreateService(typeof(IGroupReportMapping)); }
        }
        #endregion
    }
    #region IGroupReportMapping interface
    public interface IGroupReportMapping
    {
        GroupReportMapping Get(int id, string sUserID);
        List<GroupReportMapping> Gets(string sSQL, string sUserID);
        List<GroupReportMapping> GetsByReportMasterID(int nReportMasterID, string sUserID);
        List<GroupReportMapping> GetsByGroupID(int nGroupID, string sUserID);
        GroupReportMapping IUD(GroupReportMapping oGroupReportMapping, EnumDBOperation oDBOperation, string sUserID);
        GroupReportMapping SaveByReportMasterID(GroupReportMapping oGroupReportMapping, string sUserID);
        GroupReportMapping SaveByGroupID(GroupReportMapping oGroupReportMapping, string sUserID);
        string Delete(int id, string sUserID);
    }
    #endregion
}