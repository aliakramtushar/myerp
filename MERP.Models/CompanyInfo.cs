using MERP.Engine;
using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class CompanyInfo
    {
        #region CompanyInfo Defult
        public CompanyInfo()
        {
            CompanyID = 0;
            CompanyName = "";
            IsInActive = false;
            Remarks = "";
            ErrorMessage = "";
        }
        #endregion

        #region Properties
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public bool IsInActive { get; set; }
        public string Remarks { get; set; }
        public string ErrorMessage { get; set; }

        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.CompanyID; }
        }
        public string MModelString
        {
            get { return this.CompanyName; }
        }
        public string IsInActiveSt
        {
            get { return this.IsInActive.ToString(); }
        }
        public int IsInActiveInt
        {
            get { return Convert.ToInt16(this.IsInActive); }
        }
        #endregion

        #region Functions
        public CompanyInfo Get(int nId, string sUserID)
        {
            return CompanyInfo.Service.Get(nId, sUserID);
        }
        public CompanyInfo IUD(CompanyInfo oCompanyInfo, EnumDBOperation oDBOperation, string sUserID)
        {
            return CompanyInfo.Service.IUD(oCompanyInfo, oDBOperation, sUserID);
        }
        public static List<CompanyInfo> Gets(string sSQL, string sUserID)
        {
            return CompanyInfo.Service.Gets(sSQL, sUserID);
        }
        public static List<CompanyInfo> GetsActiveCompanys(string sUserID)
        {
            return CompanyInfo.Service.GetsActiveCompanys(sUserID);
        }
        #endregion

        #region ServiceFactory
        internal static ICompanyInfo Service
        {
            get { return (ICompanyInfo)Services.Factory.CreateService(typeof(ICompanyInfo)); }
        }
        #endregion
    }
    #region ICompanyInfo interface
    public interface ICompanyInfo
    {
        CompanyInfo Get(int id, string sUserID);
        List<CompanyInfo> Gets(string sSQL, string sUserID);
        List<CompanyInfo> GetsActiveCompanys(string sUserID);
        CompanyInfo IUD(CompanyInfo oCompanyInfo, EnumDBOperation oDBOperation, string sUserID);
    }
    #endregion
}