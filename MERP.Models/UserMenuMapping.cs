using MERP.Engine;
using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class UserMenuMapping
    {
        #region UserMenuMapping Defult
        public UserMenuMapping()
        {
            UserMenuMappingID = 0;
            UserID = 0;
            MenuID = 0;
            ParentID = 0;
            ErrorMessage = "";
        }
        #endregion

        #region Properties
        public int UserMenuMappingID { get; set; }
        public int UserID { get; set; }
        public int MenuID { get; set; }
        public int ParentID { get; set; }
        public string ErrorMessage { get; set; }

        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.UserMenuMappingID; }
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
        public UserMenuMapping Get(int nId, string sUserID)
        {
            return UserMenuMapping.Service.Get(nId, sUserID);
        }
        public UserMenuMapping IUD(UserMenuMapping oUserMenuMapping, EnumDBOperation oDBOperation, string sUserID)
        {
            return UserMenuMapping.Service.IUD(oUserMenuMapping, oDBOperation, sUserID);
        }
        public List<UserMenuMapping> SaveUserMenuMapping(int nUserID, string sMenuIDs, EnumDBOperation oDBOperation, string sUserID)
        {
            return UserMenuMapping.Service.SaveUserMenuMapping(nUserID, sMenuIDs, oDBOperation, sUserID);
        }
        public static List<UserMenuMapping> Gets(string sSQL, string sUserID)
        {
            return UserMenuMapping.Service.Gets(sSQL, sUserID);
        }
        public string Delete(int id, string sUserID)
        {
            return UserMenuMapping.Service.Delete(id, sUserID);
        }
        #endregion

        #region ServiceFactory
        internal static IUserMenuMapping Service
        {
            get { return (IUserMenuMapping)Services.Factory.CreateService(typeof(IUserMenuMapping)); }
        }
        #endregion
    }
    #region IUserMenuMapping interface
    public interface IUserMenuMapping
    {
        UserMenuMapping Get(int id, string sUserID);
        List<UserMenuMapping> Gets(string sSQL, string sUserID);
        UserMenuMapping IUD(UserMenuMapping oUserMenuMapping, EnumDBOperation oDBOperation, string sUserID);
        List<UserMenuMapping> SaveUserMenuMapping(int nUserID, string sMenuIDs, EnumDBOperation oDBOperation, string sUserID);
        string Delete(int id, string sUserID);
    }
    #endregion
}