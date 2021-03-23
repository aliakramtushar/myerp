using MERP.Engine;
using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class Employee
    {
        #region Employee Defult
        public Employee()
        {
            EmployeeID = 0;
            EmployeeCode = "";
            FirstName = "";
            MiddleName = "";
            LastName = "";
            FatherName = "";
            MotherName = "";
            SpouseName = "";
            MaritalStatus = EnumMaritalStatus.None;
            Phone = "";
            Mobile = "";
            Email = "";
            PresentAddress = "";
            PermanentAddress = "";
            DivisionID = 0;
            DivisionName = "";
            DistrictID = 0;
            DistrictName = "";
            LocationID = 0;
            LocationName = "";
            ActivityStatus = EnumActivityStatus.None;
            DateOfBirth = DateTime.MinValue;
            Remarks = "";
            BloodGroup = EnumBloodGroup.None;
            Height = "";
            Weight = "";
            ReligionID = EnumReligion.None;
            Gender = EnumGender.None;
            NationalIDNo = "";
            DrivingLicenceNo = "";
            PassportNo = "";
            Photo = "";
            ETIN = "";
            CountryID = 0;
            CountryName = "";
            PersonalBankID = 0;
            PersonalBankName = "";
            PersonalBankAccountNo = "";
            ExtraSkill = "";
            MachineNameIP = "";
        }
        #endregion

        #region Properties
        public int EmployeeID { get; set; }
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string SpouseName { get; set; }
        public EnumMaritalStatus MaritalStatus { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public int DivisionID { get; set; }
        public string DivisionName { get; set; }
        public int DistrictID { get; set; }
        public string DistrictName { get; set; }
        public int LocationID { get; set; }
        public string LocationName { get; set; }
        public EnumActivityStatus ActivityStatus { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Remarks { get; set; }
        public EnumBloodGroup BloodGroup { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public EnumReligion ReligionID { get; set; }
        public EnumGender Gender { get; set; }
        public string NationalIDNo { get; set; }
        public string DrivingLicenceNo { get; set; }
        public string PassportNo { get; set; }
        public string Photo { get; set; }
        public string ETIN { get; set; }
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public int PersonalBankID { get; set; }
        public string PersonalBankName { get; set; }
        public string PersonalBankAccountNo { get; set; }
        public string ExtraSkill { get; set; }
        public string MachineNameIP { get; set; }
        public string ErrorMessage { get; set; }
        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.EmployeeID; }
        }
        public string MModelString
        {
            get { return this.FullName; }
        }
        public string FullName
        {
            get { return this.FirstName + ' ' + this.MiddleName + ' ' + this.LastName; }
        }
        public string MaritalStatusSt
        {
            get { return this.MaritalStatus.ToString(); }
        }
        public string ActivityStatusSt
        {
            get { return this.ActivityStatus.ToString(); }
        }
        public string DateOfBirthSt
        {
            get { return this.DateOfBirth.ToString("dd MMM yyyy"); }
        }
        public string BloodGroupSt
        {
            get { return this.BloodGroup.ToString(); }
        }
        public string ReligionIDSt
        {
            get { return this.ReligionID.ToString(); }
        }
        public string GenderSt
        {
            get { return this.Gender.ToString(); }
        }
        #endregion

        #region Functions
        public Employee Get(int nId, string sUserID)
        {
            return Employee.Service.Get(nId, sUserID);
        }
        public Employee IUD(Employee oEmployee, EnumDBOperation oDBOperation, string sUserID)
        {
            return Employee.Service.IUD(oEmployee, oDBOperation, sUserID);
        }
        public static List<Employee> Gets(string sSQL, string sUserID)
        {
            return Employee.Service.Gets(sSQL, sUserID);
        }
        #endregion

        #region ServiceFactory
        internal static IEmployee Service
        {
            get { return (IEmployee)Services.Factory.CreateService(typeof(IEmployee)); }
        }
        #endregion
    }
    #region IEmployee interface
    public interface IEmployee
    {
        Employee Get(int id, string sUserID);
        List<Employee> Gets(string sSQL, string sUserID);
        Employee IUD(Employee oEmployee, EnumDBOperation oDBOperation, string sUserID);
        string Delete(int id, string sUserID);
    }
    #endregion
}