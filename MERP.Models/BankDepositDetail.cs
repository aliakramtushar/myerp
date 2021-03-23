using MERP.Engine;
using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class BankDepositDetail
    {
        #region BankDepositDetail Defult
        public BankDepositDetail()
        {
            BankDepositDetailID = 0;
            BankDepositMasterID = 0;
            BusinessUnitID = 0;
            DepositAmount = 0.0;
            BusinessUnitName = "";
            Remarks = "";
            ErrorMessage = "";
        }
        #endregion

        #region Properties
        public int BankDepositDetailID { get; set; }
        public int BankDepositMasterID { get; set; }
        public int BusinessUnitID { get; set; }
        public double DepositAmount { get; set; }
        public string BusinessUnitName { get; set; }
        public string Remarks { get; set; }
        public string ErrorMessage { get; set; }

        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.BankDepositDetailID; }
        }
        public string MModelString
        {
            get { return this.BusinessUnitName + ':' + this.DepositAmount.ToString() ; }
        }
        public string DepositAmountSt
        {
            get { return this.DepositAmount + "/="; }
        }
        
        #endregion

        #region Functions
        public BankDepositDetail Get(int nId, string sUserID)
        {
            return BankDepositDetail.Service.Get(nId, sUserID);
        }
        public BankDepositDetail IUD(BankDepositDetail oBankDepositDetail, EnumDBOperation oDBOperation, string sUserID)
        {
            return BankDepositDetail.Service.IUD(oBankDepositDetail, oDBOperation, sUserID);
        }
        public static List<BankDepositDetail> Gets(string sSQL, string sUserID)
        {
            return BankDepositDetail.Service.Gets(sSQL, sUserID);
        }
        public string Delete(int id, string sUserID)
        {
            return BankDepositDetail.Service.Delete(id, sUserID);
        }
        public string DeleteByIDs(int nBankDepositMasterID, string sIDs, string sUserID)
        {
            return BankDepositDetail.Service.DeleteByIDs(nBankDepositMasterID, sIDs, sUserID);
        }
        #endregion

        #region ServiceFactory
        internal static IBankDepositDetail Service
        {
            get { return (IBankDepositDetail)Services.Factory.CreateService(typeof(IBankDepositDetail)); }
        }
        #endregion
    }
    #region IBankDepositDetail interface
    public interface IBankDepositDetail
    {
        BankDepositDetail Get(int nID, string sUserID);
        List<BankDepositDetail> Gets(string sSQL, string sUserID);
        BankDepositDetail IUD(BankDepositDetail oBankDepositDetail, EnumDBOperation oDBOperation, string sUserID);
        string Delete(int nID, string sUserID);
        string DeleteByIDs(int nBankDepositMasterID, string sIDs, string sUserID);

    }
    #endregion
}