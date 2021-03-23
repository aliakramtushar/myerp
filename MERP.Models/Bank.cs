using MERP.Engine;
using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class Bank
    {
        #region Bank Defult
        public Bank()
        {
            BankID = 0;
            BankName = "";
            BankAddress = "";
            IsInActive = 0;
            Remarks = "";
            ErrorMessage = "";
        }
        #endregion

        #region Properties
        public int BankID { get; set; }
        public string BankName { get; set; }
        public string BankAddress { get; set; }
        public int IsInActive { get; set; }
        public string Remarks { get; set; }
        public string ErrorMessage { get; set; }
        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.BankID; }
        }
        public string MModelString
        {
            get { return this.BankName; }
        }
        public string IsInActiveSt
        {
            get { return this.IsInActive.ToString(); }
        }
        #endregion

        #region Functions
        public Bank Get(int nId, string sUserID)
        {
            return Bank.Service.Get(nId, sUserID);
        }
        public Bank IUD(Bank oBank, EnumDBOperation oDBOperation, string sUserID)
        {
            return Bank.Service.IUD(oBank, oDBOperation, sUserID);
        }
        public static List<Bank> Gets(string sSQL, string sUserID)
        {
            return Bank.Service.Gets(sSQL, sUserID);
        }
        #endregion

        #region ServiceFactory
        internal static IBank Service
        {
            get { return (IBank)Services.Factory.CreateService(typeof(IBank)); }
        }
        #endregion
    }
    #region IBank interface
    public interface IBank
    {
        Bank Get(int id, string sUserID);
        List<Bank> Gets(string sSQL, string sUserID);
        Bank IUD(Bank oBank, EnumDBOperation oDBOperation, string sUserID);
        string Delete(int id, string sUserID);
    }
    #endregion
}