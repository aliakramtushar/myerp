using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Engine.GlobalClass
{
    public class GlobalEnum
    {
    }
    public class EnumClass
    {
        public int MModelPK { get; set; }
        public string MModelString { get; set; }
        public string Description { get; set; }

    }
    static public class FeedbackMessage
    {
        public static string Saved = "Data Saved Successfully !!";
        public static string Deleted = "Data Deleted Successfully !!";
        public static string Updated = "Data Updated Successfully !!";
        public static string Invalid = "Invalid Data !!";
        public static string NoIdFound = "No ID Found !!";
        public static string NoDataFound = "No Data Found !!";
        public static string Error = "There is an ERROR !!";
    }
    #region EnumDBOperation
    public enum EnumDBOperation
    {
        None = 0,
        Insert = 1,
        Update = 2,
        Delete = 3,
        Accept = 4,
        Reject = 5,
        Approve = 6,
        Revise = 7,
        SpecialCase1 = 8,
        SpecialCase2 = 9,
        SpecialCase3 = 10,
    }
    #endregion
    #region EnumActivityStatus
    public enum EnumActivityStatus
    {
        None = 0,
        Active = 1,
        Inactive = 2,
        Blocked = 3
    }
    #endregion
    #region EnumTrainingType
    public enum EnumTrainingType
    {
        None = 0,
        ProfessionalTraining = 1,
        Internship = 2,
    }
    #endregion
    #region EnumAccessStatus
    public enum EnumAccessStatus
    {
        No = 0,
        Yes = 1,
    }
    #endregion
    #region EnumPersonType
    public enum EnumPersonType
    {
        None = 0,
        Contractor = 1,
        Supplier = 2,
        Dealer = 3,
        Messenger = 4,
        Supperter = 5,
        BusinessPerson = 6
    }
    #endregion
    #region EnumStatus
    public enum EnumStatus
    {
        Initialized = 0,
        Approved = 1,
        Rejected = 2,
    }
    #endregion
    #region EnumControlType
    public enum EnumControlType
    {
        None = 0,
        TextBox = 1,
        DropDown = 2,
        Date = 3,
        CheckBox = 4,
        Number = 5,
        DateTime = 6,
        Radio = 7,
        Month = 8,
        Time = 9
    }
    #endregion

    #region EnumPaymentMedia
    public enum EnumPaymentMedia
    {
        None = 0,
        Bank = 1,
        Fast_Track = 2,
        CDM = 3,
        Mobile_Banking = 4,
    }
    #endregion 
    #region EnumEmployeeType
    public enum EnumEmployeeType
    {
        None = 0,
        Contractual = 1,
        Permanent = 2,
        Probationary = 3,
        Trainee = 4,
        Advisor = 5,
        Intern = 6
    }
    #endregion
    #region EnumStudentType
    public enum EnumStudentType
    {
        None = 0,
        Probationary = 1,
        Trainee = 2,
        Intern = 3
    }
    #endregion
    
    #region EnumMaritalStatus
    public enum EnumMaritalStatus
    {
        None = 0,
        Single = 1,
        Married = 2,
        Divorced = 3
    }
    #endregion
    #region EnumBloodGroup
    public enum EnumBloodGroup
    {
        None = 0,
        A_Positive = 1,
        A_Negative = 2,
        B_Positive = 3,
        B_Negative = 4,
        AB_Positive = 5,
        AB_Negative = 6,
        O_Positive = 7,
        O_Negative = 8
    }
    #endregion
    #region EnumGender
    public enum EnumGender
    {
        None = 0,
        Male = 1,
        Female = 2,
        Others = 3
    }
    #endregion
    #region EnumReligion
    public enum EnumReligion
    {
        None = 0,
        Muslims = 1,
        Hindus = 2,
        Buddhists = 3,
        Christians = 4,
        Sikhs = 5,
        Jews = 6
    }
    #endregion
    #region EnumDivision
    public enum EnumDivision
    {
        None = 0,
        Dhaka = 1,
        Chittagong = 2,
        Khulna = 3,
        Barisal = 4,
        Mymensingh = 5,
        Rajshahi = 6,
        Sylhet = 7,
        Rangpur = 8
    }
    #endregion
    #region EnumBankAccountType
    public enum EnumBankAccountType
    {
        None = 0,
        Current_Account = 1,
        Savings_Account = 2,
        Fixed_Deposit = 3
    }
    #endregion
    #region EnumContinent
    public enum EnumContinent
    {
        None = 0,
        Africa = 1,
        Antarctica = 2,
        Asia = 3,
        Australia = 4,
        Europe = 5,
        North_America = 6,
        South_America = 7,
        Oceania = 8
    }
    #endregion
    #region EnumRelation
    public enum EnumRelation
    {
        None = 0,
        Father = 1,
        Mother = 2,
        Brother = 3,
        Sister = 4,
        Husband = 5,
        Wife = 6
    }
    #endregion
    #region EnumReferenceType
    public enum EnumReferenceType
    {
        None = 0,
        Employee_Reference = 1,
        Poor_Student = 2,
        VIP_Student = 3,
    }
    #endregion
    #region EnumExperienceType
    public enum EnumExperienceType
    {
        None = 0,
        Training = 1,
        Certificate = 2,
        Workshop = 3,
        Job = 4,
        Project = 5,
    }
    #endregion
    #region EnumExamType
    public enum EnumExamType
    {
        None = 0,
        Doctoral = 1,
        Masters = 2,
        MBA = 3,
        Bachelor = 4,
        Diploma = 5,
        Degree = 6,
        Higher_Secondary = 7,
        Secondary = 8,
        JSC = 9,
        PSC = 10,
    }
    #endregion
    #region EnumWeekend
    public enum EnumWeekend
    {
        None = 0,
        Sat = 1,
        Sun = 2,
        Mon = 3,
        Tue = 4,
        Thr = 5,
        Wed = 6,
        Fri = 7,
        Thr_Fri = 8,
        Fri_Sat = 9
    }
    #endregion
    #region EnumDegree
    public enum EnumDegree
    {
        None = 0,
        PSC = 1,
        JSC = 2,
        SSC = 3,
        HSC = 4,
        Diploma = 5,
        Bachelor = 6,
        Masters = 7,
        Doctoral = 8,
    }
    #endregion
    #region EnumResultType
    public enum EnumResultType
    {
        None = 0,
        Grade = 1,
        Division = 2,
        Class = 3,
        CGPA = 3,
    }
    #endregion
    #region EnumEducationGroup
    public enum EnumEducationGroup
    {
        None = 0,
        Science = 1,
        Commerce = 2,
        Arts = 3,
        Madrasha = 4
    }
    #endregion
}
