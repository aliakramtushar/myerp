using MERP.Engine;
using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class Group
    {
        #region Group Defult
        public Group()
        {
            GroupID = 0;
            GroupCode = "";
            GroupName = "";
            GroupDesc = "";
            IsInActive = EnumActivityStatus.Active;
            HasAccess = EnumAccessStatus.No;
            ErrorMessage = "";
        }
        #endregion

        #region Properties
        public int GroupID { get; set; }
        public string GroupCode { get; set; }
        public string GroupName { get; set; }
        public string GroupDesc { get; set; }
        public EnumActivityStatus IsInActive { get; set; }
        public EnumAccessStatus HasAccess { get; set; }
        public string ErrorMessage { get; set; }
        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.GroupID; }
        }
        public string MModelString
        {
            get { return this.GroupName; }
        }
        public string IsInActiveSt
        {
            get { return this.IsInActive.ToString(); }
        }
        public string HasAccessSt
        {
            get { return this.HasAccess.ToString(); }
        }
        #endregion

        #region Functions
        public Group Get(int nId, string sUserID)
        {
            return Group.Service.Get(nId, sUserID);
        }
        public Group IUD(Group oGroup, EnumDBOperation oDBOperation, string sUserID)
        {
            return Group.Service.IUD(oGroup, oDBOperation, sUserID);
        }
        public static List<Group> Gets(string sSQL, string sUserID)
        {
            return Group.Service.Gets(sSQL, sUserID);
        }
        public static List<Group> GetsActiveAndHasAccessList(string sUserID)
        {
            return Group.Service.GetsActiveAndHasAccessList(sUserID);
        }
        
        public string Delete(int id, string sUserID)
        {
            return Group.Service.Delete(id, sUserID);
        }
        #endregion

        #region ServiceFactory
        internal static IGroup Service
        {
            get { return (IGroup)Services.Factory.CreateService(typeof(IGroup)); }
        }
        #endregion
    }
    #region IGroup interface
    public interface IGroup
    {
        Group Get(int id, string sUserID);
        List<Group> Gets(string sSQL, string sUserID);
        List<Group> GetsActiveAndHasAccessList(string sUserID);
        Group IUD(Group oGroup, EnumDBOperation oDBOperation, string sUserID);
        string Delete(int id, string sUserID);
    }
    #endregion
}