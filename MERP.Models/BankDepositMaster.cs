using MERP.Engine;
using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class BankDepositMaster
    {
        #region BankDepositMaster Defult
        public BankDepositMaster()
        {
            BankDepositMasterID = 0;
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
            BankBranchName = "";
            BankAccountName = "";
            BankAccountNo = "";
            ApprovedBy = "";
            ApprovedByDate = DateTime.Now;
            BankDepositDetails = new List<BankDepositDetail>();
            ErrorMessage = "";
        }
        #endregion

        #region Properties
        public int BankDepositMasterID { get; set; }
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
        public string BankBranchName { get; set; }
        public string BankAccountName { get; set; }
        public string BankAccountNo { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime ApprovedByDate { get; set; }
        public List<BankDepositDetail> BankDepositDetails { get; set; }
        public string ErrorMessage { get; set; }

        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.BankDepositMasterID; }
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
            get { return this.DepositDate.ToString("dd MMM yyyy"); }
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
            get { return this.ApprovedByDate.ToString("dd MMM yyyy"); }
        }

        #endregion

        #region Functions
        public BankDepositMaster Get(int nId, string sUserID)
        {
            return BankDepositMaster.Service.Get(nId, sUserID);
        }
        public BankDepositMaster IUD(BankDepositMaster oBankDepositMaster, EnumDBOperation oDBOperation, string sUserID)
        {
            return BankDepositMaster.Service.IUD(oBankDepositMaster, oDBOperation, sUserID);
        }
        public static List<BankDepositMaster> Gets(string sSQL, string sUserID)
        {
            return BankDepositMaster.Service.Gets(sSQL, sUserID);
        }
        public string Delete(int id, string sUserID)
        {
            return BankDepositMaster.Service.Delete(id, sUserID);
        }
        #endregion

        #region ServiceFactory
        internal static IBankDepositMaster Service
        {
            get { return (IBankDepositMaster)Services.Factory.CreateService(typeof(IBankDepositMaster)); }
        }
        #endregion
    }
    #region IBankDepositMaster interface
    public interface IBankDepositMaster
    {
        BankDepositMaster Get(int nID, string sUserID);
        List<BankDepositMaster> Gets(string sSQL, string sUserID);
        BankDepositMaster IUD(BankDepositMaster oBankDepositMaster, EnumDBOperation oDBOperation, string sUserID);
        string Delete(int nID, string sUserID);
    }
    #endregion
}