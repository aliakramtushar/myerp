using MERP.Engine.GlobalClass;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.DataAccess
{
    public class BankDepositDetailDA
    {
        public static string IUD(BankDepositDetail oBankDepositDetail, EnumDBOperation oDBOperation, string sUserID)
        {
            return GlobalHelpers.ExcecuteQurey("EXEC MOB.SP_IUD_BankDepositDetail",
                oBankDepositDetail.BankDepositDetailID, oBankDepositDetail.BankDepositMasterID, oBankDepositDetail.BusinessUnitID, oBankDepositDetail.DepositAmount,
                oBankDepositDetail.Remarks, (int)oDBOperation, sUserID);
        }
        public static string Gets(string sSQL, string sUserID)
        {
            if (sSQL == "") return "SELECT * FROM MOB.View_BankDepositDetail";
            else return sSQL;
        }
        public static string Get(int nID, string sUserID)
        {
            return "SELECT * FROM MOB.View_BankDepositDetail WHERE [BankDepositDetailID] =" + nID;
        }
        public static string Delete(int nID, string sUserID)
        {
            return "DELETE FROM MOB.BankDepositDetail WHERE BankDepositDetailID = " + nID;
        }
        public static string DeleteByIDs(int nBankDepositMasterID, string nBankDepositDetailIDs, string sUserID)
        {
            return "DELETE FROM MOB.BankDepositDetail WHERE BankDepositMasterID = " + nBankDepositMasterID + " AND BankDepositDetailID NOT IN (" + nBankDepositDetailIDs + ")";
        }
        public static string GetsByParentID(int nParentID, string sUserID)
        {
            return "SELECT * FROM MOB.View_BankDepositDetail WHERE BankDepositMasterID = " + nParentID;
        }
    }
}