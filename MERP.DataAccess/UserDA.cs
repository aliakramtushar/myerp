using MERP.Engine.GlobalClass;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.DataAccess
{
    public class UserDA
    {
        public static string IUD(User oUser, EnumDBOperation oDBOperation, string sUserID)
        {
            return GlobalHelpers.ExcecuteQurey("EXEC SP_IUD_User",
                oUser.UserID, oUser.UserName, oUser.Password, (int)oDBOperation, sUserID);
        }
        public static string Gets(string sSQL, string sUserID)
        {
            if (sSQL == "") return "SELECT * FROM MOB.View_Users";
            else return sSQL;
        }
        public static string GetsActiveUsers(string sUserID)
        {
            return "SELECT * FROM MOB.View_Users WHERE IsInactive = 0 AND IsBlocked = 0";
        }
        public static string Get(int nID, string sUserID)
        {
            return "SELECT * FROM MOB.View_Users WHERE [UserID] =" + nID;
        }
        public static string Login(User oUser)
        {
            //return "SELECT * FROM Users.UserInfo WHERE UserCode = '" + oUser.UserCode + "' AND Password = '" + oUser.Password +"'";
            return "SELECT * FROM MOB.View_Users WHERE UserName = '" + oUser.UserName + "' OR Email = '" + oUser.UserName + "'";
        }
    }
}