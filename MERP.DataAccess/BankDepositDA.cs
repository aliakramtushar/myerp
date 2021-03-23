using MERP.Engine.GlobalClass;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.DataAccess
{
    public class BankDepositDA
    {
        public static string IUD(BankDeposit oBankDeposit, EnumDBOperation oDBOperation, string sUserID)
        {
            return GlobalHelpers.ExcecuteQurey("EXEC MOB.SP_IUD_BankDeposit",
                oBankDeposit.BankDepositID, oBankDeposit.BankAccountMappingID, oBankDeposit.BranchID, oBankDeposit.DepositDate.ToString("yyyy-MM-dd"), 
                oBankDeposit.TotalDepositAmount, oBankDeposit.RefNo, (Int16)oBankDeposit.PaymentMedia, oBankDeposit.Remarks, oBankDeposit.ErrorMessage,
                (Int16)oBankDeposit.Status,(int)oDBOperation, sUserID);
        }
        public static string Gets(string sSQL, string sUserID)
        {
            if (sSQL == "") return "SELECT * FROM MOB.View_BankDeposit WHERE BranchID = (SELECT BranchID FROM Users.UserInfo WHERE UserCode = '" + sUserID + "') ORDER BY CAST(DepositDate AS DATE) DESC";
            //if (sSQL == "") return "SELECT * FROM MOB.View_BankDeposit WHERE BranchID = " + GlobalSession.BranchID;
            else return sSQL;
        }
        public static string Gets(int nBranchID, EnumStatus oEnumStatus, string sUserID)
        {
            if (nBranchID == 1)
            { return "SELECT * FROM MOB.View_BankDeposit WHERE Status = " + (int)oEnumStatus + " ORDER BY CAST(DepositDate AS DATE) DESC"; }
            return "SELECT * FROM MOB.View_BankDeposit WHERE BranchID = " + nBranchID + " AND Status = " + (int)oEnumStatus + "  ORDER BY CAST(DepositDate AS DATE) DESC";
        }
        public static string Get(int nID, string sUserIDID)
        {
            return "SELECT * FROM MOB.View_BankDeposit WHERE [BankDepositID] =" + nID;
        }
        public static string Delete(int nBankDepositID, string sUserID)
        {
            return "DELETE FROM MOB.BankDeposit WHERE BankDepositID = " + nBankDepositID;
        }
    }
}