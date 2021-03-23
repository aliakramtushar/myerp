using MERP.Engine;
using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class NWRDirectSalesDetail
    {
        #region NWRDirectSalesDetail Defult
        public NWRDirectSalesDetail()
        {
            RID = 0;
            LSOInvoiceDetailsID = 0;
            BranchID = 0;
            ProductID = 0;
            ShortName = "";
            SKUID = 0;
            LSOCode = "";
            IsWarranty = 0;
            InvoiceNo = "";
            InvoiceDate = DateTime.Now;
            SalesCategory = "";
            Unit = 0;
            SKUPrice = 0;
            ExchangeReplaceCharge = 0;
            BackupHSCharge = 0;
            ServiceChargeAmount = 0;
            TotalAmount = 0;
            DiscountedAmount = 0;
            CollectedAmount = 0;
            DiscountReasonID = 0;
            RepairType = "";
            AccountName = "";
            BusinessUnitID = 0;
            DateAdded = DateTime.Now;
            AddedBy = "";
            IscashDeposited = 0;
            DepositDate = DateTime.Now;
            DepositBy = "";
            AccountMappingID = 0;
            BankDepositID = 0;
            SKUName = "";
            BusinessUnitName = "";
            BranchName = "";
            ErrorMessage = "";
        }
        #endregion

        #region Properties
        public int RID { get; set; }
        public int LSOInvoiceDetailsID { get; set; }
        public int BranchID { get; set; }
        public int ProductID { get; set; }
        public string ShortName { get; set; }
        public int SKUID { get; set; }
        public string LSOCode { get; set; }
        public int IsWarranty { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string SalesCategory { get; set; }
        public int Unit { get; set; }
        public double SKUPrice { get; set; }
        public double ExchangeReplaceCharge { get; set; }
        public double BackupHSCharge { get; set; }
        public double ServiceChargeAmount { get; set; }
        public double TotalAmount { get; set; }
        public double DiscountedAmount { get; set; }
        public double CollectedAmount { get; set; }
        public int DiscountReasonID { get; set; }
        public string RepairType { get; set; }
        public string AccountName { get; set; }
        public int BusinessUnitID { get; set; }
        public DateTime DateAdded { get; set; }
        public string AddedBy { get; set; }
        public int IscashDeposited { get; set; }
        public DateTime DepositDate { get; set; }
        public string DepositBy { get; set; }
        public int AccountMappingID { get; set; }
        public int BankDepositID { get; set; }
        public string SKUName { get; set; }
        public string BusinessUnitName { get; set; }
        public string BranchName { get; set; }
        public string ErrorMessage { get; set; }
        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.RID; }
        }
        public string MModelString
        {
            get { return this.LSOInvoiceDetailsID.ToString(); }
        }
        public string IscashDepositedSt
        {
            get { return this.IscashDeposited.ToString(); }
        }
        public string InvoiceDateSt
        {
            get { return (this.InvoiceDate == DateTime.MinValue) ? "" : this.InvoiceDate.ToString("dd-MM-yyyy"); }
        }
        public string DepositDateSt
        {
            get { return (this.DepositDate == DateTime.MinValue) ? "" : this.DepositDate.ToString("dd-MM-yyyy"); }
        }
        #endregion

        #region Functions
        public NWRDirectSalesDetail Get(int nId, string sUserID)
        {
            return NWRDirectSalesDetail.Service.Get(nId, sUserID);
        }

        public NWRDirectSalesDetail IUD(NWRDirectSalesDetail oNWRDirectSalesDetail, EnumDBOperation oDBOperation, string sUserID)
        {
            return NWRDirectSalesDetail.Service.IUD(oNWRDirectSalesDetail, oDBOperation, sUserID);
        }
        public static List<NWRDirectSalesDetail> Gets(string sSQL, string sUserID)
        {
            return NWRDirectSalesDetail.Service.Gets(sSQL, sUserID);
        }
        public static List<NWRDirectSalesDetail> GetsByParentID(int nParentID, string sUserID)
        {
            return NWRDirectSalesDetail.Service.GetsByParentID(nParentID, sUserID);
        }
        public static List<NWRDirectSalesDetail> GetsByBranchID(int nBranchID, string sUserID)
        {
            return NWRDirectSalesDetail.Service.GetsByBranchID(nBranchID, sUserID);
        }
        public static List<NWRDirectSalesDetail> GetNWRSalesDetailsByLSO(string sLSOCode, string sUserID)
        {
            return NWRDirectSalesDetail.Service.GetNWRSalesDetailsByLSO(sLSOCode, sUserID);
        }
        public static List<NWRDirectSalesDetail> GetsNonDepositedSales(int nBankAccountMappingID,DateTime dStartDate, DateTime dEndDate, int nBranchID, string sUserID)
        {
            return NWRDirectSalesDetail.Service.GetsNonDepositedSales(nBankAccountMappingID,dStartDate, dEndDate, nBranchID, sUserID);
        }
        public static List<NWRDirectSalesDetail> GenerateNWRSalesDetailsByLSO(string sLSOCode, string sUserID)
        {
            return NWRDirectSalesDetail.Service.GenerateNWRSalesDetailsByLSO(sLSOCode, sUserID);
        }
        
        public string DeleteByIDs(int nBankDepositID, string sIDs, string sUserID)
        {
            return NWRDirectSalesDetail.Service.DeleteByIDs(nBankDepositID, sIDs, sUserID);
        }
        #endregion

        #region ServiceFactory
        internal static INWRDirectSalesDetail Service
        {
            get { return (INWRDirectSalesDetail)Services.Factory.CreateService(typeof(INWRDirectSalesDetail)); }
        }
        #endregion
    }
    #region INWRDirectSalesDetail interface
    public interface INWRDirectSalesDetail
    {
        NWRDirectSalesDetail Get(int id, string sUserID);
        List<NWRDirectSalesDetail> Gets(string sSQL, string sUserID);
        List<NWRDirectSalesDetail> GetsNonDepositedSales(int nBankAccountMappingID, DateTime dStartDate, DateTime dEndDate, int nBranchID, string sUserID);
        List<NWRDirectSalesDetail> GetsByParentID(int nParentID, string sUserID);
        List<NWRDirectSalesDetail> GetsByBranchID(int nBranchID, string sUserID);
        List<NWRDirectSalesDetail> GetNWRSalesDetailsByLSO(string sLSOCode, string sUserID);
        List<NWRDirectSalesDetail> GenerateNWRSalesDetailsByLSO(string sLSOCode, string sUserID);
        string DeleteByIDs(int nBankDepositID, string sIDs, string sUserID);
        NWRDirectSalesDetail IUD(NWRDirectSalesDetail oNWRDirectSalesDetail, EnumDBOperation oDBOperation, string sUserID);
        string Delete(int id, string sUserID);
    }
    #endregion
}