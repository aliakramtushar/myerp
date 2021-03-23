using MERP.Engine;
using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class BrandInfo
    {
        #region BrandInfo Defult
        public BrandInfo()
        {
            BrandID = 0;
            BrandCode = "Auto Generated";
            BrandName = "";
            IsInActive = false;
            OriginID = 0;
            OriginName = "";
            ErrorMessage = "";
        }
        #endregion

        #region Properties
        public int BrandID { get; set; }
        public string BrandCode { get; set; }
        public string BrandName { get; set; }
        public bool IsInActive { get; set; }
        public int OriginID { get; set; }
        public string OriginName { get; set; }
        public string ErrorMessage { get; set; }

        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.BrandID; }
        }
        public string MModelString
        {
            get { return this.BrandName + " (" + this.BrandCode + ")"; }
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
        public BrandInfo Get(int nId, string sUserID)
        {
            return BrandInfo.Service.Get(nId, sUserID);
        }
        public BrandInfo IUD(BrandInfo oBrandInfo, EnumDBOperation oDBOperation, string sUserID)
        {
            return BrandInfo.Service.IUD(oBrandInfo, oDBOperation, sUserID);
        }
        public static List<BrandInfo> Gets(string sSQL, string sUserID)
        {
            return BrandInfo.Service.Gets(sSQL, sUserID);
        }
        public string Delete(int id, string sUserID)
        {
            return BrandInfo.Service.Delete(id, sUserID);
        }
        #endregion

        #region ServiceFactory
        internal static IBrandInfo Service
        {
            get { return (IBrandInfo)Services.Factory.CreateService(typeof(IBrandInfo)); }
        }
        #endregion
    }
    #region IBrandInfo interface
    public interface IBrandInfo
    {
        BrandInfo Get(int id, string sUserID);
        List<BrandInfo> Gets(string sSQL, string sUserID);
        BrandInfo IUD(BrandInfo oBrandInfo, EnumDBOperation oDBOperation, string sUserID);
        string Delete(int id, string sUserID);
    }
    #endregion
}