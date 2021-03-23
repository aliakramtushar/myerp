using MERP.Engine;
using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class BankAccountMapping
    {
        #region BankAccountMapping Defult
        public BankAccountMapping()
        {
            BankAccountMappingID = 0;
            BankID = 0;
            BankName = "";
            BankBranchName = "";
            BankAccountName = "";
            BankAccountNo = "";
            Remarks = "";
            IsActive = false;
            ErrorMessage = "";
        }
        #endregion

        #region Properties
        public int BankAccountMappingID { get; set; }
        public int BankID { get; set; }
        public string BankName { get; set; }
        public string BankBranchName { get; set; }
        public string BankAccountName { get; set; }
        public string BankAccountNo { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public string ErrorMessage { get; set; }

        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.BankAccountMappingID; }
        }
        public string MModelString
        {
            get { return this.BankAccountNo + '(' +  this.BankName + ')'; }
        }
        public string IsActiveSt
        {
            get { return this.IsActive.ToString(); }
        }
        public int IsActiveInt
        {
            get { return (this.IsActive) ? 1 : 0; }
        }
        #endregion

        #region Functions
        public BankAccountMapping Get(int nId, string sUserID)
        {
            return BankAccountMapping.Service.Get(nId, sUserID);
        }
        public BankAccountMapping IUD(BankAccountMapping oBankAccountMapping, EnumDBOperation oDBOperation, string sUserID)
        {
            return BankAccountMapping.Service.IUD(oBankAccountMapping, oDBOperation, sUserID);
        }
        public static List<BankAccountMapping> Gets(string sSQL, string sUserID)
        {
            return BankAccountMapping.Service.Gets(sSQL, sUserID);
        }
        public static List<BankAccountMapping> GetsActiveBankAccountMappings(string sUserID)
        {
            return BankAccountMapping.Service.GetsActiveBankAccountMappings(sUserID);
        }
        public string Delete(int id, string sUserID)
        {
            return BankAccountMapping.Service.Delete(id, sUserID);
        }
        #endregion

        #region ServiceFactory
        internal static IBankAccountMapping Service
        {
            get { return (IBankAccountMapping)Services.Factory.CreateService(typeof(IBankAccountMapping)); }
        }
        #endregion
    }
    #region IBankAccountMapping interface
    public interface IBankAccountMapping
    {
        BankAccountMapping Get(int id, string sUserID);
        List<BankAccountMapping> Gets(string sSQL, string sUserID);
        List<BankAccountMapping> GetsActiveBankAccountMappings(string sUserID);
        BankAccountMapping IUD(BankAccountMapping oBankAccountMapping, EnumDBOperation oDBOperation, string sUserID);
        string Delete(int id, string sUserID);
    }
    #endregion
}