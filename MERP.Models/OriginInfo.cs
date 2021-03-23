using MERP.Engine;
using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class OriginInfo
    {
        #region OriginInfo Defult
        public OriginInfo()
        {
            OriginID = 0;
            OriginCode = "Auto Generated";
            OriginName = "";
            IsInActive = false;
            ErrorMessage = "";
        }
        #endregion

        #region Properties
        public int OriginID { get; set; }
        public string OriginCode { get; set; }
        public string OriginName { get; set; }
        public bool IsInActive { get; set; }
        public string ErrorMessage { get; set; }

        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.OriginID; }
        }
        public string MModelString
        {
            get { return this.OriginName + " (" + this.OriginCode + ")"; }
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
        public OriginInfo Get(int nId, string sUserID)
        {
            return OriginInfo.Service.Get(nId, sUserID);
        }
        public OriginInfo IUD(OriginInfo oOriginInfo, EnumDBOperation oDBOperation, string sUserID)
        {
            return OriginInfo.Service.IUD(oOriginInfo, oDBOperation, sUserID);
        }
        public static List<OriginInfo> Gets(string sSQL, string sUserID)
        {
            return OriginInfo.Service.Gets(sSQL, sUserID);
        }
        public string Delete(int id, string sUserID)
        {
            return OriginInfo.Service.Delete(id, sUserID);
        }
        #endregion

        #region ServiceFactory
        internal static IOriginInfo Service
        {
            get { return (IOriginInfo)Services.Factory.CreateService(typeof(IOriginInfo)); }
        }
        #endregion
    }
    #region IOriginInfo interface
    public interface IOriginInfo
    {
        OriginInfo Get(int id, string sUserID);
        List<OriginInfo> Gets(string sSQL, string sUserID);
        OriginInfo IUD(OriginInfo oOriginInfo, EnumDBOperation oDBOperation, string sUserID);
        string Delete(int id, string sUserID);
    }
    #endregion
}