using MERP.Engine.GlobalClass;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.DataAccess
{
    public class BranchDA
    {
        public static string IUD(Branch oBranch, EnumDBOperation oDBOperation, string sUserID)
        {
            return GlobalHelpers.ExcecuteQurey("EXEC MOB.SP_IUD_Branch",
                oBranch.BranchID, oBranch.BranchName, oBranch.LSOPrefix, oBranch.BranchAddress, oBranch.BranchCode, oBranch.BranchNote,
                oBranch.BranchPrefix, (int)oDBOperation, sUserID);
        }
        public static string Gets(string sSQL, string sUserID)
        {
            if (sSQL == "") return "SELECT * FROM [GEN].BranchInfo";
            else return sSQL;
        }
        public static string Get(int nID, string sUserIDID)
        {
            return "SELECT * FROM [GEN].BranchInfo WHERE [BranchID] =" + nID;
        }
        public static string Delete(int nID, string sUserID)
        {
            return "DELETE FROM [GEN].BranchInfo WHERE BranchID = " + nID;
        }
    }
}