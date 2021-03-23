using MERP.Engine;
using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class StoreInfo
    {
        #region StoreInfo Defult
        public StoreInfo()
        {
            StoreID = 0;
            StoreName = "";
            ShortName = "";
            StoreCode = "Auto Generated";
            Address = "";
            IsInActive = false;
            ErrorMessage = "";
        }
        #endregion

        #region Properties
        public int StoreID { get; set; }
        public string StoreName { get; set; }
        public string ShortName { get; set; }
        public string StoreCode { get; set; }
        public string Address { get; set; }
        public bool IsInActive { get; set; }
        public string ErrorMessage { get; set; }

        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.StoreID; }
        }
        public string MModelString
        {
            get { return this.StoreName + " (" + this.StoreCode + ")"; }
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
        public StoreInfo Get(int nId, string sUserID)
        {
            return StoreInfo.Service.Get(nId, sUserID);
        }
        public StoreInfo IUD(StoreInfo oStoreInfo, EnumDBOperation oDBOperation, string sUserID)
        {
            return StoreInfo.Service.IUD(oStoreInfo, oDBOperation, sUserID);
        }
        public static List<StoreInfo> Gets(string sSQL, string sUserID)
        {
            return StoreInfo.Service.Gets(sSQL, sUserID);
        }
        #endregion

        #region ServiceFactory
        internal static IStoreInfo Service
        {
            get { return (IStoreInfo)Services.Factory.CreateService(typeof(IStoreInfo)); }
        }
        #endregion
    }
    #region IStoreInfo interface
    public interface IStoreInfo
    {
        StoreInfo Get(int id, string sUserID);
        List<StoreInfo> Gets(string sSQL, string sUserID);
        StoreInfo IUD(StoreInfo oStoreInfo, EnumDBOperation oDBOperation, string sUserID);
    }
    #endregion
}