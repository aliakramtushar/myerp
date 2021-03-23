using MERP.Engine;
using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class CostCenter
    {
        #region CostCenter Defult
        public CostCenter()
        {
            CostCenterID = 0;
            CostCenterName = "";
            ActivityStatus = EnumActivityStatus.None;
            ErrorMessage = "";
        }
        #endregion

        #region Properties
        public int CostCenterID { get; set; }
        public string CostCenterName { get; set; }
        public EnumActivityStatus ActivityStatus { get; set; }
        public string ErrorMessage { get; set; }
        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.CostCenterID; }
        }
        public string MModelString
        {
            get { return this.CostCenterName; }
        }
        public string ActivityStatusSt
        {
            get { return this.ActivityStatus.ToString(); }
        }
        #endregion

        #region Functions
        public CostCenter Get(int nId, string sUserID)
        {
            return CostCenter.Service.Get(nId, sUserID);
        }
        public CostCenter IUD(CostCenter oCostCenter, EnumDBOperation oDBOperation, string sUserID)
        {
            return CostCenter.Service.IUD(oCostCenter, oDBOperation, sUserID);
        }
        public static List<CostCenter> Gets(string sSQL, string sUserID)
        {
            return CostCenter.Service.Gets(sSQL, sUserID);
        }
        #endregion

        #region ServiceFactory
        internal static ICostCenter Service
        {
            get { return (ICostCenter)Services.Factory.CreateService(typeof(ICostCenter)); }
        }
        #endregion
    }
    #region ICostCenter interface
    public interface ICostCenter
    {
        CostCenter Get(int id, string sUserID);
        List<CostCenter> Gets(string sSQL, string sUserID);
        CostCenter IUD(CostCenter oCostCenter, EnumDBOperation oDBOperation, string sUserID);
    }
    #endregion
}