using MERP.Engine;
using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class District
    {
        #region District Defult
        public District()
        {
            DistrictID = 0;
            DistrictName = "";
            DivisionID = 0;
            ActivityStatus = EnumActivityStatus.None;
            Remarks = "";
            ErrorMessage = "";
        }
        #endregion

        #region Properties
        public int DistrictID { get; set; }
        public string DistrictName { get; set; }
        public EnumDivision DivisionID { get; set; }
        public EnumActivityStatus ActivityStatus { get; set; }
        public string Remarks { get; set; }
        public string ErrorMessage { get; set; }
        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.DistrictID; }
        }
        public string MModelString
        {
            get { return this.DistrictName; }
        }
        public string ActivityStatusSt
        {
            get { return this.ActivityStatus.ToString(); }
        }
        public string DivisionName
        {
            get { return this.DivisionID.ToString(); }
        }
        #endregion

        #region Functions
        public District Get(int nId, string sUserID)
        {
            return District.Service.Get(nId, sUserID);
        }
        public District IUD(District oDistrict, EnumDBOperation oDBOperation, string sUserID)
        {
            return District.Service.IUD(oDistrict, oDBOperation, sUserID);
        }
        public static List<District> Gets(string sSQL, string sUserID)
        {
            return District.Service.Gets(sSQL, sUserID);
        }
        #endregion

        #region ServiceFactory
        internal static IDistrict Service
        {
            get { return (IDistrict)Services.Factory.CreateService(typeof(IDistrict)); }
        }
        #endregion
    }
    #region IDistrict interface
    public interface IDistrict
    {
        District Get(int id, string sUserID);
        List<District> Gets(string sSQL, string sUserID);
        District IUD(District oDistrict, EnumDBOperation oDBOperation, string sUserID);
        string Delete(int id, string sUserID);
    }
    #endregion
}