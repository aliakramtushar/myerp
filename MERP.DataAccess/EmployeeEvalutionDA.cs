using MERP.Engine.GlobalClass;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.DataAccess
{
    public class EmployeeEvalutionDA
    {
        public static string IUD(EmployeeEvalution oEmployeeEvalution, EnumDBOperation oDBOperation, string sUserID)
        {
            return "";
        }
        public static string Gets(string sSQL, string sUserID)
        {
            if (sSQL == "") return "SELECT * FROM MOB.View_EmployeeEvalution";
            else return sSQL;
        }
        public static string Get(int nID, string sUserIDID)
        {
            return "SELECT * FROM MOB.View_EmployeeEvalution WHERE [EmployeeEvalutionID] =" + nID;
        }
        public static string Delete(int nID, string sUserID)
        {
            return "DELETE FROM MOB.EmployeeEvalution WHERE EmployeeEvalutionID = " + nID;
        }
    }
}