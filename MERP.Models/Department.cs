using MERP.Engine;
using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class Department
    {
        #region Department Defult
        public Department()
        {
            DepartmentID = 0;
            DepartmentName = "";
            ActivityStatus = EnumActivityStatus.None;
            Remarks = "";
            ErrorMessage = "";
        }
        #endregion

        #region Properties
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public EnumActivityStatus ActivityStatus { get; set; }
        public string Remarks { get; set; }
        public string ErrorMessage { get; set; }
        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.DepartmentID; }
        }
        public string MModelString
        {
            get { return this.DepartmentName; }
        }
        public string ActivityStatusSt
        {
            get { return this.ActivityStatus.ToString(); }
        }
        #endregion

        #region Functions
        public Department Get(int nId, string sUserID)
        {
            return Department.Service.Get(nId, sUserID);
        }
        public Department IUD(Department oDepartment, EnumDBOperation oDBOperation, string sUserID)
        {
            return Department.Service.IUD(oDepartment, oDBOperation, sUserID);
        }
        public static List<Department> Gets(string sSQL, string sUserID)
        {
            return Department.Service.Gets(sSQL, sUserID);
        }
        #endregion

        #region ServiceFactory
        internal static IDepartment Service
        {
            get { return (IDepartment)Services.Factory.CreateService(typeof(IDepartment)); }
        }
        #endregion
    }
    #region IDepartment interface
    public interface IDepartment
    {
        Department Get(int id, string sUserID);
        List<Department> Gets(string sSQL, string sUserID);
        Department IUD(Department oDepartment, EnumDBOperation oDBOperation, string sUserID);
        string Delete(int id, string sUserID);
    }
    #endregion
}