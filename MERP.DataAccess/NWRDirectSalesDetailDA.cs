using MERP.Engine.GlobalClass;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.DataAccess
{
    public class NWRDirectSalesDetailDA
    {
        public static string IUD(NWRDirectSalesDetail oNWRDirectSalesDetail, EnumDBOperation oDBOperation, string sUserID)
        {
            return GlobalHelpers.ExcecuteQurey("EXEC MOB.SP_IUD_NWRDirectSalesDetail",
                oNWRDirectSalesDetail.RID, oNWRDirectSalesDetail.LSOInvoiceDetailsID, oNWRDirectSalesDetail.BranchID, oNWRDirectSalesDetail.ProductID,
                oNWRDirectSalesDetail.ShortName, oNWRDirectSalesDetail.SKUID, oNWRDirectSalesDetail.LSOCode, oNWRDirectSalesDetail.IsWarranty,
                oNWRDirectSalesDetail.InvoiceNo, oNWRDirectSalesDetail.InvoiceDate.ToString("yyyy-MM-dd"), oNWRDirectSalesDetail.SalesCategory, oNWRDirectSalesDetail.Unit,
                oNWRDirectSalesDetail.SKUPrice, oNWRDirectSalesDetail.ExchangeReplaceCharge, oNWRDirectSalesDetail.BackupHSCharge, oNWRDirectSalesDetail.ServiceChargeAmount,
                oNWRDirectSalesDetail.TotalAmount, oNWRDirectSalesDetail.DiscountedAmount, oNWRDirectSalesDetail.CollectedAmount, oNWRDirectSalesDetail.DiscountReasonID,
                oNWRDirectSalesDetail.RepairType, oNWRDirectSalesDetail.AccountName, oNWRDirectSalesDetail.BusinessUnitID,oNWRDirectSalesDetail.IscashDeposited, 
                oNWRDirectSalesDetail.DepositDate.ToString("yyyy-MM-dd"), oNWRDirectSalesDetail.DepositBy,oNWRDirectSalesDetail.AccountMappingID, oNWRDirectSalesDetail.BankDepositID,
                (int)oDBOperation, sUserID);
        }
        public static string GenerateNWRSalesDetailsByLSO(string sLSOCode, string sUserID)
        {
            return "EXEC [dbo].[rpt_AllNWR_DirectSalesDetail_Insert] '"+ sLSOCode + "'"; 
        }
        public static string Gets(string sSQL, string sUserID)
        {
            if (sSQL == "") return "SELECT * FROM MOB.View_NWRDirectSalesDetail WHERE BranchID = " + GlobalHelpers.GetBranchID(sUserID) + " ORDER BY LSOCode, BusinessUnitID, DateAdded";
            else return sSQL;
        }
        public static string GetsNonDepositedSales(int nBankAccountMappingID, DateTime dStartDate, DateTime dEndDate, int nBranchID, string sUserID)
        {
            //return "SELECT * FROM MOB.View_NWRDirectSalesDetail WHERE BusinessUnitID = " + nBusinessUnitID + " AND BranchID IN (SELECT BranchID FROM Users.Userinfo WHERE UserCode = '" + sUserID + "') AND CAST(InvoiceDate AS DATE) BETWEEN '2020-12-1' AND '2020-12-10'";
            return "SELECT * FROM MOB.View_NWRDirectSalesDetail WHERE BranchID = "+ nBranchID + " AND BusinessUnitID IN(SELECT BusinessUnitID FROM MOB.BUBankAccountMapping WHERE BankAccountMappingID = " + nBankAccountMappingID + ") AND IscashDeposited IS NULL AND CAST(InvoiceDate AS DATE) BETWEEN '" + dStartDate.ToString("yyyy-MM-dd") + "' AND '" + dEndDate.ToString("yyyy-MM-dd") + "' ORDER BY LSOCode, BusinessUnitID, DateAdded";
        }
        public static string Get(int nID, string sUserID)
        {
            return "SELECT * FROM MOB.View_NWRDirectSalesDetail WHERE [NWRDirectSalesDetailID] =" + nID + " ORDER BY LSOCode, BusinessUnitID, DateAdded";
        }
        public static string Delete(int nNWRDirectSalesDetailID, string sUserID)
        {
            //return "DELETE FROM NWRDirectSalesDetail WHERE NWRDirectSalesDetailID = " + nNWRDirectSalesDetailID;
            return "";
        }
        public static string GetsByParentID(int nParentID, string sUserID)
        {
            return "SELECT * FROM MOB.View_NWRDirectSalesDetail WHERE BankDepositID = " + nParentID + " ORDER BY LSOCode, BusinessUnitID, DateAdded";
        }
        public static string GetsByBranchID(int nBranchID, string sUserID)
        {
            return "SELECT * FROM MOB.View_NWRDirectSalesDetail WHERE BranchID = " + nBranchID + " ORDER BY DateAdded";
        }
        public static string GetNWRSalesDetailsByLSO(string sLSOCode, string sUserID)
        {
            return "SELECT * FROM MOB.View_NWRDirectSalesDetail WHERE LSOCode = '" + sLSOCode + "'";
        }
        public static string DeleteByIDs(int nBankDepositID, string nBankDepositDetailIDs, string sUserID)
        {
            return "UPDATE [MOB].NWRDirectSalesDetail SET IscashDeposited = NULL, DepositDate = NULL, DepositBy = NULL, AccountMappingID = NULL, BankDepositID = NULL WHERE BankDepositID = " + nBankDepositID + " AND RID NOT IN (" + nBankDepositDetailIDs + ")";
        }
    }
}