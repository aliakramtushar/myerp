using MERP.Engine;
using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class Country
    {
        #region Country Defult
        public Country()
        {
            CountryID = 0;
            CountryName = "";
            ContinentID = 0;
            ActivityStatus = EnumActivityStatus.None;
            Remarks = "";
            ErrorMessage = "";
        }
        #endregion

        #region Properties
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public EnumContinent ContinentID { get; set; }
        public EnumActivityStatus ActivityStatus { get; set; }
        public string Remarks { get; set; }
        public string ErrorMessage { get; set; }
        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.CountryID; }
        }
        public string MModelString
        {
            get { return this.CountryName; }
        }
        public string ActivityStatusSt
        {
            get { return this.ActivityStatus.ToString(); }
        }
        public string ContinentName
        {
            get { return this.ContinentID.ToString(); }
        }
        #endregion

        #region Functions
        public Country Get(int nId, string sUserID)
        {
            return Country.Service.Get(nId, sUserID);
        }
        public Country IUD(Country oCountry, EnumDBOperation oDBOperation, string sUserID)
        {
            return Country.Service.IUD(oCountry, oDBOperation, sUserID);
        }
        public static List<Country> Gets(string sSQL, string sUserID)
        {
            return Country.Service.Gets(sSQL, sUserID);
        }
        #endregion

        #region ServiceFactory
        internal static ICountry Service
        {
            get { return (ICountry)Services.Factory.CreateService(typeof(ICountry)); }
        }
        #endregion
    }
    #region ICountry interface
    public interface ICountry
    {
        Country Get(int id, string sUserID);
        List<Country> Gets(string sSQL, string sUserID);
        Country IUD(Country oCountry, EnumDBOperation oDBOperation, string sUserID);
        string Delete(int id, string sUserID);
    }
    #endregion
}