using MERP.Engine.GlobalClass;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.DataAccess
{
    public class UserMenuMappingDA
    {
        public static string IUD(UserMenuMapping oUserMenuMapping, EnumDBOperation oDBOperation, string sUserID)
        {
            return GlobalHelpers.ExcecuteQurey("EXEC MOB.SP_IUD_UserMenuMapping",
                oUserMenuMapping.UserMenuMappingID, oUserMenuMapping.UserID, oUserMenuMapping.MenuID, "", (int)oDBOperation, sUserID);
        }
        public static string SaveUserMenuMapping(int nUserID, string sMenuIDs, EnumDBOperation oDBOperation, string sUserID)
        {
            return GlobalHelpers.ExcecuteQurey("EXEC MOB.SP_IUD_UserMenuMapping",
                0, nUserID, 0, sMenuIDs, (int)oDBOperation, sUserID);
        }
        public static string Gets(string sSQL, string sUserID)
        {
            if (sSQL == "") return "SELECT * FROM MOB.View_UserMenuMapping";
            else return sSQL;
        }
        public static string Get(int nID, string sUserID)
        {
            return "SELECT * FROM MOB.View_UserMenuMapping WHERE [UserMenuMappingID] =" + nID;
        }
        public static string Delete(int nID, string sUserID)
        {
            return "DELETE FROM MOB.UserMenuMapping WHERE UserMenuMappingID = " + nID;
        }
    }
}