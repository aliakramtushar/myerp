using MERP.Engine;
using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class BusinessUnit
    {
        #region BusinessUnit Defult
        public BusinessUnit()
        {
            BusinessUnitID = 0;
            BusinessUnitName = "";
            CompanyID = 0;
            CompanyName = "";
            BusinessOwnerName = "";
            IsInActive = false;
            IsAuto = false;
            IsManual = false;
            Remarks = "";
            ErrorMessage = "";
        }
        #endregion

        #region Properties
        public int BusinessUnitID { get; set; }
        public string BusinessUnitName { get; set; }
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string BusinessOwnerName { get; set; }
        public bool IsInActive { get; set; }
        public bool IsAuto { get; set; }
        public bool IsManual { get; set; }
        public string Remarks { get; set; }
        public string ErrorMessage { get; set; }

        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.BusinessUnitID; }
        }
        public string MModelString
        {
            get { return this.BusinessUnitName; }
        }
        public string IsInActiveSt
        {
            get { return IsInActive ? "In Active" : "Active"; }
        }
        public string IsAutoSt
        {
            get { return this.IsAuto.ToString(); }
        }
        public string IsManualSt
        {
            get { return this.IsManual.ToString(); }
        }
        #endregion

        #region Functions
        public BusinessUnit Get(int nId, string sUserID)
        {
            return BusinessUnit.Service.Get(nId, sUserID);
        }
        public BusinessUnit IUD(BusinessUnit oBusinessUnit, EnumDBOperation oDBOperation, string sUserID)
        {
            return BusinessUnit.Service.IUD(oBusinessUnit, oDBOperation, sUserID);
        }
        public static List<BusinessUnit> Gets(string sSQL, string sUserID)
        {
            return BusinessUnit.Service.Gets(sSQL, sUserID);
        }
        #endregion

        #region ServiceFactory
        internal static IBusinessUnit Service
        {
            get { return (IBusinessUnit)Services.Factory.CreateService(typeof(IBusinessUnit)); }
        }
        #endregion
    }
    #region IBusinessUnit interface
    public interface IBusinessUnit
    {
        BusinessUnit Get(int id, string sUserID);
        List<BusinessUnit> Gets(string sSQL, string sUserID);
        BusinessUnit IUD(BusinessUnit oBusinessUnit, EnumDBOperation oDBOperation, string sUserID);
    }
    #endregion
}