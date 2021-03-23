using MERP.Engine;
using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class BankDeposit
    {
        #region BankDeposit Defult
        public BankDeposit()
        {
            BankDepositID = 0;
            DepositCode = "";
            BankAccountMappingID = 0;
            BranchID = 0;
            DepositDate = DateTime.Now;
            TotalDepositAmount = 0.00;
            RefNo = "";
            PaymentMedia = EnumPaymentMedia.None;
            Remarks = "";
            Status = EnumStatus.Initialized;
            BankID = 0;
            BankName = "";
            BankAccountName = "";
            BankAccountNo = "";
            ApprovedBy = "";
            BranchName = "";
            ApprovedByDate = DateTime.MinValue;
            NWRDirectSalesDetails = new List<NWRDirectSalesDetail>();
            ErrorMessage = "";
        }
        #endregion

        #region Properties
        public int BankDepositID { get; set; }
        public string DepositCode { get; set; }
        public int BankAccountMappingID { get; set; }
        public int BranchID { get; set; }
        public DateTime DepositDate { get; set; }
        public double TotalDepositAmount { get; set; }
        public string RefNo { get; set; }
        public EnumPaymentMedia PaymentMedia { get; set; }
        public string Remarks { get; set; }
        public EnumStatus Status { get; set; }
        public int BankID { get; set; }
        public string BankName { get; set; }
        public string BankAccountName { get; set; }
        public string BankAccountNo { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime ApprovedByDate { get; set; }
        public List<NWRDirectSalesDetail> NWRDirectSalesDetails { get; set; }
        public string BranchName { get; set; }
        public string ErrorMessage { get; set; }

        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.BankDepositID; }
        }
        public string MModelString
        {
            get { return this.BankAccountName; }
        }
        public string TotalDepositAmountSt
        {
            get { return this.TotalDepositAmount.ToString() + "/="; }
        }
        public string DepositDateSt
        {
            get { return (this.DepositDate == DateTime.MinValue) ? "" : this.DepositDate.ToString("dd-MM-yyyy"); }
        }
        public string StatusSt
        {
            get { return this.Status.ToString(); }
        }
        public string PaymentMediaSt
        {
            get { return this.PaymentMedia.ToString(); }
        }
        public string ApprovedByDateSt
        {
            get { return (this.ApprovedByDate == DateTime.MinValue)? "" : this.ApprovedByDate.ToString("dd-MM-yyyy"); }
        }
        //((SELECT 'D' + REPLACE(CONVERT(VARCHAR, GETDATE(),1),'/','') +REPLACE(CONVERT(VARCHAR, GETDATE(),108),':','') + '-' + UPPER((SELECT LSOPrefix FROM GEN.BranchInfo WHERE BranchID = @BranchID))))
        #endregion

        #region Functions
        public BankDeposit Get(int nId, string sUserID)
        {
            return BankDeposit.Service.Get(nId, sUserID);
        }
        public BankDeposit IUD(BankDeposit oBankDeposit, EnumDBOperation oDBOperation, string sUserID)
        {
            return BankDeposit.Service.IUD(oBankDeposit, oDBOperation, sUserID);
        }
        public static List<BankDeposit> Gets(string sSQL, string sUserID)
        {
            return BankDeposit.Service.Gets(sSQL, sUserID);
        }
        public static List<BankDeposit> Gets(int nBranchID, EnumStatus oEnumStatus, string sUserID)
        {
            return BankDeposit.Service.Gets(nBranchID, oEnumStatus, sUserID);
        }
        public string Delete(int id, string sUserID)
        {
            return BankDeposit.Service.Delete(id, sUserID);
        }
        #endregion

        #region ServiceFactory
        internal static IBankDeposit Service
        {
            get { return (IBankDeposit)Services.Factory.CreateService(typeof(IBankDeposit)); }
        }
        #endregion
    }
    #region IBankDeposit interface
    public interface IBankDeposit
    {
        BankDeposit Get(int nID, string sUserID);
        List<BankDeposit> Gets(string sSQL, string sUserID);
        List<BankDeposit> Gets(int nBranchID, EnumStatus oEnumStatus, string sUserID);
        BankDeposit IUD(BankDeposit oBankDeposit, EnumDBOperation oDBOperation, string sUserID);
        string Delete(int nID, string sUserID);
    }
    #endregion
}