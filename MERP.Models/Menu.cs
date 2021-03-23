using MERP.Engine;
using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class Menu
    {
        #region Menu Defult
        public Menu()
        {
            MenuID = 0;
            MenuName = "";
            ParentID = 0;
            ControllerName = "";
            ActionName = "";
            MenuSequence = 0;
            ActivityStatus = EnumActivityStatus.None;
            Remarks = "";
            ErrorMessage = "";
        }
        #endregion

        #region Properties
        public int MenuID { get; set; }
        public string MenuName { get; set; }
        public int ParentID { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public int MenuSequence { get; set; }
        public EnumActivityStatus ActivityStatus { get; set; }
        public string Remarks { get; set; }
        public string ErrorMessage { get; set; }
        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.MenuID; }
        }
        public string MModelString
        {
            get { return this.MenuName; }
        }
        public string ActivityStatusSt
        {
            get { return this.ActivityStatus.ToString(); }
        }






        public IEnumerable<Menu> ChildNodes { get; set; }
        public Menu Parent { get; set; }
        public bool IsChild { get; set; }
        public bool IsSibling { get; set; }
        public string DisplayMessage { get; set; }
        public List<Menu> Menus { get; set; }
        public int[] Permissions { get; set; }

        #endregion

        #region Functions
        public Menu Get(int nId, string sUserID)
        {
            return Menu.Service.Get(nId, sUserID);
        }
        public Menu IUD(Menu oMenu, EnumDBOperation oDBOperation, string sUserID)
        {
            return Menu.Service.IUD(oMenu, oDBOperation, sUserID);
        }
        public static List<Menu> Gets(string sSQL, string sUserID)
        {
            return Menu.Service.Gets(sSQL, sUserID);
        }
        public static List<Menu> GetUserAccessedMenus(string sUserID)
        {
            return Menu.Service.GetUserAccessedMenus(sUserID);
        }
        public static List<Menu> GetMenusByGroupID(int nGroupID, string sUserID)
        {
            return Menu.Service.GetMenusByGroupID(nGroupID, sUserID);
        }
        public static List<Menu> GetMenusWithoutParentsByGroupID(int nGroupID, string sUserID)
        {
            return Menu.Service.GetMenusWithoutParentsByGroupID(nGroupID, sUserID);
        }
        public static List<Menu> GetMenusWithoutParentsByUserID(int nUserID, string sUserID)
        {
            return Menu.Service.GetMenusWithoutParentsByUserID(nUserID, sUserID);
        }
        #endregion

        #region ServiceFactory
        internal static IMenu Service
        {
            get { return (IMenu)Services.Factory.CreateService(typeof(IMenu)); }
        }
        #endregion
    }
    #region IMenu interface
    public interface IMenu
    {
        Menu Get(int id, string sUserID);
        List<Menu> Gets(string sSQL, string sUserID);
        List<Menu> GetUserAccessedMenus(string sUserID);
        List<Menu> GetMenusWithoutParentsByGroupID(int nGroupID, string sUserID);
        List<Menu> GetMenusWithoutParentsByUserID(int nUserID, string sUserID);
        List<Menu> GetMenusByGroupID(int nGroupID, string sUserID);
        Menu IUD(Menu oMenu, EnumDBOperation oDBOperation, string sUserID);
        string Delete(int id, string sUserID);
    }
    #endregion

    #region TMenu
    public class TMenu
    {
        public TMenu()
        {
            id = 0;
            text = "";
            state = "open";
            parentid = 0;
            ControllerName = "";
            BUName = "";
            IsWithBU = false;
            BUID = 0;
            ActionName = "";
            MenuSequence = 0;
            ActivityStatus = EnumActivityStatus.Active;
            Remarks = "";
        }
        public int id { get; set; }                 //: node id, which is important to load remote data
        public string text { get; set; }            //: node text to show
        public string state { get; set; }           //: node state, 'open' or 'closed', default is 'open'. When set to 'closed', the node have children nodes and will load them from remote site        
        public string ControllerName { get; set; }  //: node state, 'open' or 'closed', default is 'open'. When set to 'closed', the node have children nodes and will load them from remote site        
        public string ActionName { get; set; }      //: custom attributes can be added to a node // attributes set a sting that hold action name & controller namr seperated by '~'
        public string attributes { get; set; }      //: custom attributes can be added to a node // attributes set a sting that hold action name & controller namr seperated by '~'
        public int parentid { get; set; }
        public string BUName { get; set; }
        public bool IsWithBU { get; set; }
        public int BUID { get; set; }
        public int MenuSequence { get; set; }
        public EnumActivityStatus ActivityStatus { get; set; }
        public string ActivityStatusSt { get { return this.ActivityStatus.ToString(); } }
        public IEnumerable<TMenu> children { get; set; }//: an array nodes defines some children nodes
        public CompanyInfo CompanyInfo { get; set; }
        public List<TMenu> TMenus { get; set; }
        public string Remarks { get; set; }
    }
    #endregion
}