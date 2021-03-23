using MERP.Engine;
using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class Location
    {
        #region Location Defult
        public Location()
        {
            LocationID = 0;
            LocationName = "";
            ActivityStatus = EnumActivityStatus.None;
            Remarks = "";
            ErrorMessage = "";
        }
        #endregion

        #region Properties
        public int LocationID { get; set; }
        public string LocationName { get; set; }
        public EnumDivision DivisionID { get; set; }
        public int DistrictID { get; set; }
        public string DistrictName { get; set; }
        public EnumActivityStatus ActivityStatus { get; set; }
        public string Remarks { get; set; }
        public string ErrorMessage { get; set; }
        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.LocationID; }
        }
        public string MModelString
        {
            get { return this.LocationName; }
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
        public Location Get(int nId, string sUserID)
        {
            return Location.Service.Get(nId, sUserID);
        }
        public Location IUD(Location oLocation, EnumDBOperation oDBOperation, string sUserID)
        {
            return Location.Service.IUD(oLocation, oDBOperation, sUserID);
        }
        public static List<Location> Gets(string sSQL, string sUserID)
        {
            return Location.Service.Gets(sSQL, sUserID);
        }
        #endregion

        #region ServiceFactory
        internal static ILocation Service
        {
            get { return (ILocation)Services.Factory.CreateService(typeof(ILocation)); }
        }
        #endregion
    }
    #region ILocation interface
    public interface ILocation
    {
        Location Get(int id, string sUserID);
        List<Location> Gets(string sSQL, string sUserID);
        Location IUD(Location oLocation, EnumDBOperation oDBOperation, string sUserID);
        string Delete(int id, string sUserID);
    }
    #endregion
}