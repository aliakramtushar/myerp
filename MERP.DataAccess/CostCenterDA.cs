using MERP.Engine.GlobalClass;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.DataAccess
{
    public class CostCenterDA
    {
        public static string IUD(CostCenter oCostCenter, EnumDBOperation oDBOperation, string sUserID)
        {
            return GlobalHelpers.ExcecuteQurey("EXEC AST.SP_IUD_CostCenter",
                oCostCenter.CostCenterID, oCostCenter.CostCenterName, (int)oCostCenter.ActivityStatus, (int)oDBOperation, sUserID);
        }
        public static string Gets(string sSQL, string sUserID)
        {
            if (sSQL == "") return "SELECT * FROM AST.CostCenter";
            else return sSQL;
        }
        public static string Get(int nID, string sUserID)
        {
            return "SELECT * FROM AST.CostCenter WHERE [CostCenterID] =" + nID;
        }
    }
}