using MERP.Engine;
using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class BUBankAccountMapping
    {
        #region BUBankAccountMapping Defult
        public BUBankAccountMapping()
        {
            BUBankAccountMappingID = 0;
            BusinessUnitID = 0;
            BankAccountMappingID = 0;
            BusinessUnitName = "";
            BankName = "";
            BankAccountName = "";
            BankAccountNo = "";
            ErrorMessage = "";
            IsActive = false;
        }
        #endregion

        #region Properties
        public int BUBankAccountMappingID { get; set; }
        public int BusinessUnitID { get; set; }
        public int BankAccountMappingID { get; set; }
        public string BusinessUnitName { get; set; }
        public string BankName { get; set; }
        public string BankAccountName { get; set; }
        public string BankAccountNo { get; set; }
        public bool IsActive { get; set; }
        public string ErrorMessage { get; set; }

        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.BUBankAccountMappingID; }
        }
        public string MModelString
        {
            get { return this.BusinessUnitName; }
        }
        public string IsActiveSt
        {
            get { return this.IsActive.ToString() ; }
        }
        
        #endregion

        #region Functions
        public BUBankAccountMapping Get(int nId, string sUserID)
        {
            return BUBankAccountMapping.Service.Get(nId, sUserID);
        }
        public BUBankAccountMapping IUD(BUBankAccountMapping oBUBankAccountMapping, EnumDBOperation oDBOperation, string sUserID)
        {
            return BUBankAccountMapping.Service.IUD(oBUBankAccountMapping, oDBOperation, sUserID);
        }
        public static List<BUBankAccountMapping> Gets(string sSQL, string sUserID)
        {
            return BUBankAccountMapping.Service.Gets(sSQL, sUserID);
        }
        public static List<BUBankAccountMapping> GetsByBankAccountMappingID(int nBankAccountMappingID, string sUserID)
        {
            return BUBankAccountMapping.Service.GetsByBankAccountMappingID(nBankAccountMappingID, sUserID);
        }
        public string Delete(int id, string sUserID)
        {
            return BUBankAccountMapping.Service.Delete(id, sUserID);
        }
        #endregion

        #region ServiceFactory
        internal static IBUBankAccountMapping Service
        {
            get { return (IBUBankAccountMapping)Services.Factory.CreateService(typeof(IBUBankAccountMapping)); }
        }
        #endregion
    }
    #region IBUBankAccountMapping interface
    public interface IBUBankAccountMapping
    {
        BUBankAccountMapping Get(int id, string sUserID);
        List<BUBankAccountMapping> Gets(string sSQL, string sUserID);
        List<BUBankAccountMapping> GetsByBankAccountMappingID(int nBankAccountMappingID, string sUserID);
        BUBankAccountMapping IUD(BUBankAccountMapping oBUBankAccountMapping, EnumDBOperation oDBOperation, string sUserID);
        string Delete(int id, string sUserID);
    }
    #endregion
}