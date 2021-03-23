using MERP.Engine;
using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class GroupMenuMapping
    {
        #region GroupMenuMapping Defult
        public GroupMenuMapping()
        {
            GroupMenuMappingID = 0;
            GroupID = 0;
            MenuID = 0;
            ParentID = 0;
            ErrorMessage = "";
        }
        #endregion

        #region Properties
        public int GroupMenuMappingID { get; set; }
        public int GroupID { get; set; }
        public int MenuID { get; set; }
        public int ParentID { get; set; }
        public string ErrorMessage { get; set; }

        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.GroupMenuMappingID; }
        }
        public string MModelString
        {
            get { return ""; }
        }
        public string MenuIDSt
        {
            get { return this.MenuID.ToString(); }
        }
        #endregion

        #region Functions
        public GroupMenuMapping Get(int nId, string sUserID)
        {
            return GroupMenuMapping.Service.Get(nId, sUserID);
        }
        public GroupMenuMapping IUD(GroupMenuMapping oGroupMenuMapping, EnumDBOperation oDBOperation, string sUserID)
        {
            return GroupMenuMapping.Service.IUD(oGroupMenuMapping, oDBOperation, sUserID);
        }
        public List<GroupMenuMapping> SaveGroupMenuMapping(int nGroupID, string sMenuIDs, EnumDBOperation oDBOperation, string sUserID)
        {
            return GroupMenuMapping.Service.SaveGroupMenuMapping(nGroupID, sMenuIDs, oDBOperation, sUserID);
        }
        public static List<GroupMenuMapping> Gets(string sSQL, string sUserID)
        {
            return GroupMenuMapping.Service.Gets(sSQL, sUserID);
        }
        public string Delete(int id, string sUserID)
        {
            return GroupMenuMapping.Service.Delete(id, sUserID);
        }
        #endregion

        #region ServiceFactory
        internal static IGroupMenuMapping Service
        {
            get { return (IGroupMenuMapping)Services.Factory.CreateService(typeof(IGroupMenuMapping)); }
        }
        #endregion
    }
    #region IGroupMenuMapping interface
    public interface IGroupMenuMapping
    {
        GroupMenuMapping Get(int id, string sUserID);
        List<GroupMenuMapping> Gets(string sSQL, string sUserID);
        GroupMenuMapping IUD(GroupMenuMapping oGroupMenuMapping, EnumDBOperation oDBOperation, string sUserID);
        List<GroupMenuMapping> SaveGroupMenuMapping(int nGroupID, string sMenuIDs, EnumDBOperation oDBOperation, string sUserID);
        string Delete(int id, string sUserID);
    }
    #endregion
}