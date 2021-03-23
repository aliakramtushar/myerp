using MERP.Engine;
using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class SegmentInfo
    {
        #region SegmentInfo Defult
        public SegmentInfo()
        {
            SegmentID = 0;
            SegmentCode = "Auto Generated";
            SegmentName = "";
            IsInActive = false;
            ErrorMessage = "";
        }
        #endregion

        #region Properties
        public int SegmentID { get; set; }
        public string SegmentCode { get; set; }
        public string SegmentName { get; set; }
        public bool IsInActive { get; set; }
        public string ErrorMessage { get; set; }

        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.SegmentID; }
        }
        public string MModelString
        {
            get { return this.SegmentName + " (" + this.SegmentCode + ")"; }
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
        public SegmentInfo Get(int nId, string sUserID)
        {
            return SegmentInfo.Service.Get(nId, sUserID);
        }
        public SegmentInfo IUD(SegmentInfo oSegmentInfo, EnumDBOperation oDBOperation, string sUserID)
        {
            return SegmentInfo.Service.IUD(oSegmentInfo, oDBOperation, sUserID);
        }
        public static List<SegmentInfo> Gets(string sSQL, string sUserID)
        {
            return SegmentInfo.Service.Gets(sSQL, sUserID);
        }
        #endregion

        #region ServiceFactory
        internal static ISegmentInfo Service
        {
            get { return (ISegmentInfo)Services.Factory.CreateService(typeof(ISegmentInfo)); }
        }
        #endregion
    }
    #region ISegmentInfo interface
    public interface ISegmentInfo
    {
        SegmentInfo Get(int id, string sUserID);
        List<SegmentInfo> Gets(string sSQL, string sUserID);
        SegmentInfo IUD(SegmentInfo oSegmentInfo, EnumDBOperation oDBOperation, string sUserID);
    }
    #endregion
}