using MERP.Engine.GlobalClass;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.DataAccess
{
    public class BankDepositMasterDA
    {
        public static string IUD(BankDepositMaster oBankDepositMaster, EnumDBOperation oDBOperation, string sUserID)
        {
            return GlobalHelpers.ExcecuteQurey("EXEC MOB.SP_IUD_BankDepositMaster",
                oBankDepositMaster.BankDepositMasterID, oBankDepositMaster.BankAccountMappingID, oBankDepositMaster.BranchID, oBankDepositMaster.DepositDateSt, 
                oBankDepositMaster.TotalDepositAmount, oBankDepositMaster.RefNo, (Int16)oBankDepositMaster.PaymentMedia, oBankDepositMaster.Remarks,
                (Int16)oBankDepositMaster.Status, (int)oDBOperation, sUserID);
        }
        public static string Gets(string sSQL, string sUserID)
        {
            if (sSQL == "") return "SELECT * FROM MOB.View_BankDepositMaster";
            else return sSQL;
        }
        public static string Get(int nID, string sUserIDID)
        {
            return "SELECT * FROM MOB.View_BankDepositMaster WHERE [BankDepositMasterID] =" + nID;
        }
        public static string Delete(int nBankDepositMasterID, string sUserID)
        {
            return "DELETE FROM MOB.BankDepositMaster WHERE BankDepositMasterID = " + nBankDepositMasterID;
        }
    }
}