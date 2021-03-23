using MERP.Engine.GlobalClass;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.DataAccess
{
    public class BankAccountMappingDA
    {
        public static string IUD(BankAccountMapping oBankAccountMapping, EnumDBOperation oDBOperation, string sUserID)
        {
            return GlobalHelpers.ExcecuteQurey("EXEC MOB.SP_IUD_BankAccountMapping",
                oBankAccountMapping.BankAccountMappingID, oBankAccountMapping.BankID, oBankAccountMapping.BankBranchName, 
                oBankAccountMapping.BankAccountName, oBankAccountMapping.BankAccountNo, oBankAccountMapping.Remarks, oBankAccountMapping.IsActive, (int)oDBOperation, sUserID);
        }
        public static string Gets(string sSQL, string sUserID)
        {
            if (sSQL == "") return "SELECT * FROM MOB.View_BankAccountMapping";
            else return sSQL;
        }
        public static string GetsActiveBankAccountMappings(string sUserID)
        {
            return "SELECT * FROM MOB.View_BankAccountMapping WHERE IsActive = 1";

        }
        public static string Get(int nID, string sUserIDID)
        {
            return "SELECT * FROM MOB.View_BankAccountMapping WHERE [BankAccountMappingID] =" + nID;
        }
        public static string Delete(int nBankAccountMappingID, string sUserID)
        {
            return "DELETE FROM MOB.BankAccountMapping WHERE BankAccountMappingID = " + nBankAccountMappingID;
        }
    }
}