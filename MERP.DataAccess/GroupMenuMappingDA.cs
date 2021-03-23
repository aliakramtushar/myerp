using MERP.Engine.GlobalClass;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.DataAccess
{
    public class GroupMenuMappingDA
    {
        public static string IUD(GroupMenuMapping oGroupMenuMapping, EnumDBOperation oDBOperation, string sUserID)
        {
            return GlobalHelpers.ExcecuteQurey("EXEC MOB.SP_IUD_GroupMenuMapping",
                oGroupMenuMapping.GroupMenuMappingID, oGroupMenuMapping.GroupID, oGroupMenuMapping.MenuID, "", (int)oDBOperation, sUserID);
        }
        public static string SaveGroupMenuMapping(int nGroupID, string sMenuIDs, EnumDBOperation oDBOperation, string sUserID)
        {
            return GlobalHelpers.ExcecuteQurey("EXEC MOB.SP_IUD_GroupMenuMapping",
                0, nGroupID, 0, sMenuIDs, (int)oDBOperation, sUserID);
        }
        public static string Gets(string sSQL, string sUserID)
        {
            if (sSQL == "") return "SELECT * FROM MOB.View_GroupMenuMapping";
            else return sSQL;
        }
        public static string Get(int nID, string sUserID)
        {
            return "SELECT * FROM MOB.View_GroupMenuMapping WHERE [GroupMenuMappingID] =" + nID;
        }
        public static string Delete(int nID, string sUserID)
        {
            return "DELETE FROM MOB.GroupMenuMapping WHERE GroupMenuMappingID = " + nID;
        }
    }
}