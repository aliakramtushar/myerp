using MERP.Engine.GlobalClass;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.DataAccess
{
    public class DepartmentDA
    {
        public static string IUD(Department oDepartment, EnumDBOperation oDBOperation, string sUserID)
        {
            return GlobalHelpers.ExcecuteQurey("EXEC SP_IUD_Department",
                oDepartment.DepartmentID, oDepartment.DepartmentName, (int)oDepartment.ActivityStatus, oDepartment.Remarks, (int)oDBOperation, sUserID);
        }
        public static string Gets(string sSQL, string sUserID)
        {
            if (sSQL == "") return "SELECT * FROM View_Department";
            else return sSQL;
        }
        public static string Get(int nID, string sUserID)
        {
            return "SELECT * FROM View_Department WHERE [DepartmentID] =" + nID;
        }
        public static string Delete(int nDepartmentID, string sUserID)
        {
            return "DELETE FROM Department WHERE DepartmentID = " + nDepartmentID;
        }
    }
}