using MERP.Engine;
using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class Branch
    {
        #region Branch Defult
        public Branch()
        {
            BranchID = 0;
            BranchName = "";
            LSOPrefix = "";
            BranchAddress = "";
            BranchCode = "";
            BranchPrefix = "";
            BranchNote = "";
            ErrorMessage = "";
        }
        #endregion

        #region Properties
        public int BranchID { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public string BranchCode { get; set; }
        public string LSOPrefix { get; set; }
        public string BranchPrefix { get; set; }
        public string BranchNote { get; set; }
        public string ErrorMessage { get; set; }

        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.BranchID; }
        }
        public string MModelString
        {
            get { return this.BranchName; }
        }
        #endregion

        #region Functions
        public Branch Get(int nId, string sUserID)
        {
            return Branch.Service.Get(nId, sUserID);
        }
        public Branch IUD(Branch oBranch, EnumDBOperation oDBOperation, string sUserID)
        {
            return Branch.Service.IUD(oBranch, oDBOperation, sUserID);
        }
        public static List<Branch> Gets(string sSQL, string sUserID)
        {
            return Branch.Service.Gets(sSQL, sUserID);
        }
        public string Delete(int id, string sUserID)
        {
            return Branch.Service.Delete(id, sUserID);
        }
        #endregion

        #region ServiceFactory
        internal static IBranch Service
        {
            get { return (IBranch)Services.Factory.CreateService(typeof(IBranch)); }
        }
        #endregion
    }
    #region IBranch interface
    public interface IBranch
    {
        Branch Get(int id, string sUserID);
        List<Branch> Gets(string sSQL, string sUserID);
        Branch IUD(Branch oBranch, EnumDBOperation oDBOperation, string sUserID);
        string Delete(int id, string sUserID);
    }
    #endregion
}