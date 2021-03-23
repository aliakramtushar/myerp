using MERP.Engine.GlobalClass;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.DataAccess
{
    public class MenuDA
    {
        public static string IUD(Menu oMenu, EnumDBOperation oDBOperation, string sUserID)
        {
            return GlobalHelpers.ExcecuteQurey("EXEC MOB.SP_IUD_Menu",
                oMenu.MenuID, oMenu.MenuName, oMenu.ParentID, oMenu.ControllerName, oMenu.ActionName, oMenu.MenuSequence, (int)oMenu.ActivityStatus, oMenu.Remarks, (int)oDBOperation, sUserID);
        }
        public static string Gets(string sSQL, string sUserID)
        {
            if (sSQL == "") return "SELECT * FROM MOB.Menu ORDER BY ParentID, MenuSequence";
            else return sSQL;
        }
        public static string GetUserAccessedMenus(string sUserID)
        {
            //return "SELECT * FROM MOB.Menu WHERE MenuID IN (SELECT MenuID FROM MOB.GroupMenuMapping WHERE GroupID = (SELECT GroupID FROM Users.Userinfo WHERE UserCode = '" + sUserID + "')) AND  ActivityStatus = " + (int)EnumActivityStatus.Active + " ORDER BY ParentID, MenuSequence";
            return "SELECT * FROM MOB.Menu WHERE MenuID IN (SELECT MenuID FROM MOB.GroupMenuMapping WHERE GroupID = (SELECT GroupID FROM Users.Userinfo WHERE UserCode = '" + sUserID + "')) OR MenuID IN (SELECT MenuID FROM MOB.View_UserMenuMapping WHERE UserCode = '" + sUserID + "') ORDER BY ParentID, MenuSequence";
        }
        public static string GetMenusByGroupID(int nGroupID, string sUserID)
        {
            return "SELECT * FROM MOB.Menu WHERE MenuID IN (SELECT MenuID FROM MOB.GroupMenuMapping WHERE GroupID = "+ nGroupID + ")";
        }
        public static string GetMenusWithoutParentsByGroupID(int nGroupID, string sUserID)
        {
            return "SELECT * FROM MOB.Menu WHERE MenuID IN (SELECT MenuID FROM MOB.GroupMenuMapping WHERE GroupID = "+ nGroupID + ") AND MenuID NOT IN (SELECT ParentID FROM MOB.View_GroupMenuMapping WHERE GroupID = "+ nGroupID + ")";
        }
        public static string GetMenusWithoutParentsByUserID(int nUserID, string sUserID)
        {
            return "SELECT * FROM MOB.Menu WHERE MenuID IN (SELECT MenuID FROM MOB.UserMenuMapping WHERE UserID = " + nUserID + ") AND MenuID NOT IN (SELECT ParentID FROM MOB.View_UserMenuMapping WHERE UserID = " + nUserID + ")";
        }
        public static string Get(int nID, string sUserID)
        {
            return "SELECT * FROM Menu WHERE [MenuID] =" + nID;
        }
        public static string Delete(int nMenuID, string sUserID)
        {
            return "DELETE FROM MOB.Menu WHERE MenuID = " + nMenuID;
        }
    }
}