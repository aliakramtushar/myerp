using MERP.Engine;
using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class StudentInfo
    {
        #region StudentInfo Defult
        public StudentInfo()
        {
            StudentID = 0;
            StudentCode = "";
            StudentFirstName = "";
            StudentMiddleName = "";
            StudentLastName = "";
            Password = "";
            RollNo = "";
            StudentType = EnumStudentType.None;
            ContactNo_1 = "";
            ContactNo_2 = "";
            Email = "";
            PresentAddress = "";
            PermanentAddress = "";
            //ReferenceName = "";
            //ReferenceType = EnumRelation.None;
            //ReferenceContactNo = "";
            //ReferenceRelation = "";
            StudentPhotoPath = "";
            Gender = EnumGender.None;
            DateOfBirth = DateTime.Now;
            Nationality = "";
            ReligionID = EnumReligion.None;
            FatherName = "";
            MotherName = "";
            NID = "";
            DrivingLicence = "";
            Passport = "";
            OtherIdentity = "";
            ActivityStatus = EnumActivityStatus.None;
            ErrorMessage = "";
        }
        #endregion

        #region Properties
        public int StudentID { get; set; }
        public string StudentCode { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentMiddleName { get; set; }
        public string StudentLastName { get; set; }
        public string Password { get; set; }
        public string RollNo { get; set; }
        public EnumStudentType StudentType { get; set; } 
        public string ContactNo_1 { get; set; }
        public string ContactNo_2 { get; set; }
        public string Email { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        //public string ReferenceName { get; set; }
        //public EnumRelation ReferenceType { get; set; }
        //public string ReferenceContactNo { get; set; }
        //public string ReferenceRelation { get; set; }
        public string StudentPhotoPath { get; set; }
        public FileStyleUriParser StudentPhotoFile { get; set; }
        public EnumGender Gender { get; set; } 
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public EnumReligion ReligionID { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string NID { get; set; }
        public string DrivingLicence { get; set; }
        public string Passport { get; set; }
        public string OtherIdentity { get; set; }
        public EnumActivityStatus ActivityStatus { get; set; }
        public string ErrorMessage { get; set; }

        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.StudentID; }
        }
        public string MModelString
        {
            get { return this.StudentFullName + "(" + this.StudentCode + ")"; }
        }
        public string StudentTypesSt
        {
            get { return this.StudentType.ToString(); }
        }
        public string GenderSt
        {
            get { return this.Gender.ToString(); }
        }
        public string ReligionIDSt
        {
            get { return this.ReligionID.ToString(); }
        }
        public string ActivityStatusSt
        {
            get { return this.ActivityStatus.ToString(); }
        }
        public string StudentFullName
        {
            get { return this.StudentFirstName + " " + this.StudentMiddleName + " " + this.StudentLastName; }
        }
        #endregion

        #region Functions
        public StudentInfo Get(int nId, string sUserID)
        {
            return StudentInfo.Service.Get(nId, sUserID);
        }
        public StudentInfo IUD(StudentInfo oStudentInfo, EnumDBOperation oDBOperation, string sUserID)
        {
            return StudentInfo.Service.IUD(oStudentInfo, oDBOperation, sUserID);
        }
        public static List<StudentInfo> Gets(string sSQL, string sUserID)
        {
            return StudentInfo.Service.Gets(sSQL, sUserID);
        }
        #endregion

        #region ServiceFactory
        internal static IStudentInfo Service
        {
            get { return (IStudentInfo)Services.Factory.CreateService(typeof(IStudentInfo)); }
        }
        #endregion
    }
    #region IStudentInfo interface
    public interface IStudentInfo
    {
        StudentInfo Get(int id, string sUserID);
        List<StudentInfo> Gets(string sSQL, string sUserID);
        StudentInfo IUD(StudentInfo oStudentInfo, EnumDBOperation oDBOperation, string sUserID);
    }
    #endregion
}