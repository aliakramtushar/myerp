using MERP.Engine;
using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class Designation
    {
        #region Designation Defult
        public Designation()
        {
            DesignationID = 0;
            DesignationName = "";
            ActivityStatus = EnumActivityStatus.None;
            Remarks = "";
            ErrorMessage = "";
        }
        #endregion

        #region Properties
        public int DesignationID { get; set; }
        public string DesignationName { get; set; }
        public EnumActivityStatus ActivityStatus { get; set; }
        public string Remarks { get; set; }
        public string ErrorMessage { get; set; }
        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.DesignationID; }
        }
        public string MModelString
        {
            get { return this.DesignationName; }
        }
        public string ActivityStatusSt
        {
            get { return this.ActivityStatus.ToString(); }
        }
        #endregion

        #region Functions
        public Designation Get(int nId, string sUserID)
        {
            return Designation.Service.Get(nId, sUserID);
        }
        public Designation IUD(Designation oDesignation, EnumDBOperation oDBOperation, string sUserID)
        {
            return Designation.Service.IUD(oDesignation, oDBOperation, sUserID);
        }
        public static List<Designation> Gets(string sSQL, string sUserID)
        {
            return Designation.Service.Gets(sSQL, sUserID);
        }
        #endregion

        #region ServiceFactory
        internal static IDesignation Service
        {
            get { return (IDesignation)Services.Factory.CreateService(typeof(IDesignation)); }
        }
        #endregion
    }
    #region IDesignation interface
    public interface IDesignation
    {
        Designation Get(int id, string sUserID);
        List<Designation> Gets(string sSQL, string sUserID);
        Designation IUD(Designation oDesignation, EnumDBOperation oDBOperation, string sUserID);
        string Delete(int id, string sUserID);
    }
    #endregion
}