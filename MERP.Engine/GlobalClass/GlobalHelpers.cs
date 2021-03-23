using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Engine.GlobalClass
{
    public class GlobalHelpers
    {
        public static string ExcecuteQurey(string StoreProcedure, params object[] param)
        {
            string args = "";
            foreach (var oitem in param)
            {
                args += oitem + "','";
            }
            args = args.Remove(args.LastIndexOf(","), 2);
            return StoreProcedure + " '" + args;
        }
        public static string GetBranchID(string sUserID)
        {
            return "(SELECT BranchID FROM Users.UserInfo WHERE UserCode = '" + sUserID + "')";
        }
        public static DateTime GetDate(string sDatetime)
        {
            string sDateString = String.Join("-", sDatetime.Split('-').Reverse());
            DateTime dDate = Convert.ToDateTime(sDateString);
            return dDate;
        }
        public static string GetDateSt(string sDatetime)
        {
            string sDateString = String.Join("-", sDatetime.Split('-').Reverse());
            return sDateString;
        }
    }
}