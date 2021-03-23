using MERP.Engine.GlobalClass;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.DataAccess
{
    public class GroupDA
    {
        public static string IUD(Group oGroup, EnumDBOperation oDBOperation, string sUserID)
        {
            return GlobalHelpers.ExcecuteQurey("EXEC SP_IUD_UserGroup",
                oGroup.GroupID, oGroup.GroupCode, oGroup.GroupName, oGroup.GroupDesc, (int)oGroup.IsInActive, (int)oDBOperation, sUserID);
    }
        public static string Gets(string sSQL, string sUserID)
        {
            if (sSQL == "") return "SELECT UGI.*, CASE WHEN(UGI.GroupID IN (SELECT DISTINCT(GroupID) FROM MOB.GroupMenuMapping)) THEN 1 ELSE 0 END AS HasAccess FROM Users.UserGroupInfo UGI";
            else return sSQL;
        }
        public static string GetsActiveAndHasAccessList(string sUserID)
        {
            return "SELECT * FROM Users.UserGroupInfo WHERE IsInactive = 0 AND GroupID IN (SELECT GroupID FROM MOB.GroupMenuMapping)";
        }
        public static string Get(int nID, string sUserID)
        {
            return "SELECT * FROM Users.UserGroupInfo WHERE [GroupID] =" + nID;
        }
        public static string Delete(int nGroupID, string sUserID)
        {
            return "";
            //return "DELETE FROM [Group] WHERE GroupID = " + nGroupID;

        }
    }
}