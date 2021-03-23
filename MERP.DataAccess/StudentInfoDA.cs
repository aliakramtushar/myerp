using MERP.Engine.GlobalClass;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.DataAccess
{
    public class StudentInfoDA
    {
        public static string IUD(StudentInfo oStudentInfo, EnumDBOperation oDBOperation, string sUserID)
        {
            return GlobalHelpers.ExcecuteQurey("EXEC PSDP.SP_IUD_StudentInfo",
                oStudentInfo.StudentID, oStudentInfo.StudentCode, oStudentInfo.StudentFirstName, oStudentInfo.StudentMiddleName, oStudentInfo.StudentLastName,
                oStudentInfo.Password, oStudentInfo.RollNo, (int)oStudentInfo.StudentType, oStudentInfo.ContactNo_1, oStudentInfo.ContactNo_2, oStudentInfo.Email,
                oStudentInfo.PresentAddress, oStudentInfo.PermanentAddress, oStudentInfo.StudentPhotoPath, (int)oStudentInfo.Gender, oStudentInfo.DateOfBirth, 
                oStudentInfo.Nationality, (int)oStudentInfo.ReligionID,oStudentInfo.FatherName, oStudentInfo.MotherName, oStudentInfo.NID, oStudentInfo.DrivingLicence, 
                oStudentInfo.Passport, oStudentInfo.OtherIdentity,(int)oStudentInfo.ActivityStatus, (int)oDBOperation, sUserID);
        }
        public static string Gets(string sSQL, string sUserID)
        {
            if (sSQL == "") return "SELECT * FROM PSDP.View_StudentInfo";
            else return sSQL;
        }
        public static string Get(int nID, string sUserID)
        {
            return "SELECT * FROM PSDP.View_StudentInfo WHERE [StudentID] =" + nID;
        }
    }
}