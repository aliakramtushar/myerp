using MERP.Engine.GlobalClass;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.DataAccess
{
    public class BusinessUnitDA
    {
        public static string IUD(BusinessUnit oBusinessUnit, EnumDBOperation oDBOperation, string sUserID)
        {
            return GlobalHelpers.ExcecuteQurey("EXEC MOB.SP_IUD_BusinessUnit",
                oBusinessUnit.BusinessUnitID, oBusinessUnit.BusinessUnitName, oBusinessUnit.CompanyID, oBusinessUnit.BusinessOwnerName, oBusinessUnit.IsInActive, 
                oBusinessUnit.IsAuto, oBusinessUnit.IsManual, oBusinessUnit.Remarks, (int)oDBOperation, sUserID);
        }
        public static string Gets(string sSQL, string sUserID)
        {
            if (sSQL == "") return "SELECT * FROM MOB.View_BusinessUnit";
            else return sSQL;
        }
        public static string Get(int nID, string sUserID)
        {
            return "SELECT * FROM MOB.View_BusinessUnit WHERE [BusinessUnitID] =" + nID;
        }
    }
}