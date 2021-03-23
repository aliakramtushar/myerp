using MERP.Engine.GlobalClass;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.DataAccess
{
    public class BUBankAccountMappingDA
    {
        public static string IUD(BUBankAccountMapping oBUBankAccountMapping, EnumDBOperation oDBOperation, string sUserID)
        {
            return GlobalHelpers.ExcecuteQurey("EXEC MOB.SP_IUD_BUBankAccountMapping",
                oBUBankAccountMapping.BUBankAccountMappingID, oBUBankAccountMapping.BusinessUnitID, oBUBankAccountMapping.BankAccountMappingID, oBUBankAccountMapping.IsActive,
                (int)oDBOperation, sUserID);
        }
        public static string Gets(string sSQL, string sUserID)
        {
            if (sSQL == "") return "SELECT * FROM MOB.View_BUBankAccountMapping";
            else return sSQL;
        }
        public static string GetsByBankAccountMappingID(int nBankAccountMappingID, string sUserID)
        {
            return "SELECT * FROM MOB.View_BUBankAccountMapping WHERE IsActive = 1 AND BankAccountMappingID = " + nBankAccountMappingID;
        }
        public static string Get(int nID, string sUserIDID)
        {
            return "SELECT * FROM MOB.View_BUBankAccountMapping WHERE [BUBankAccountMappingID] =" + nID;
        }
        public static string Delete(int nID, string sUserID)
        {
            return "DELETE FROM MOB.BUBankAccountMapping WHERE BUBankAccountMappingID = " + nID;
        }
    }
}