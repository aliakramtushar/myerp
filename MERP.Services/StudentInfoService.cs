using MERP.DataAccess;
using MERP.Engine;
using MERP.Engine.GlobalClass;
using MERP.Engine.GlobalGateway;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MERP.Services
{
    public class StudentInfoService : CommonGateway, IStudentInfo
    {
        #region Maping
        private StudentInfo MapObject(NullHandler oReader)
        {
            StudentInfo oStudentInfo = new StudentInfo();
            oStudentInfo.StudentID = oReader.GetInt32("StudentID");
            oStudentInfo.StudentCode = oReader.GetString("StudentCode");
            oStudentInfo.StudentFirstName = oReader.GetString("StudentFirstName");
            oStudentInfo.StudentMiddleName = oReader.GetString("StudentMiddleName");
            oStudentInfo.StudentLastName = oReader.GetString("StudentLastName");
            oStudentInfo.Password = oReader.GetString("Password");
            oStudentInfo.RollNo = oReader.GetString("RollNo");
            oStudentInfo.StudentType = (EnumStudentType)oReader.GetInt16("StudentType");
            oStudentInfo.ContactNo_1 = oReader.GetString("ContactNo_1");
            oStudentInfo.ContactNo_2 = oReader.GetString("ContactNo_2");
            oStudentInfo.Email = oReader.GetString("Email");
            oStudentInfo.PresentAddress = oReader.GetString("PresentAddress");
            oStudentInfo.PermanentAddress = oReader.GetString("PermanentAddress");
            oStudentInfo.StudentPhotoPath = oReader.GetString("StudentPhotoPath");
            oStudentInfo.Gender = (EnumGender)oReader.GetInt16("Gender");
            oStudentInfo.DateOfBirth = oReader.GetDateTime("DateOfBirth");
            oStudentInfo.Nationality = oReader.GetString("Nationality");
            oStudentInfo.ReligionID = (EnumReligion)oReader.GetInt16("ReligionID");
            oStudentInfo.FatherName = oReader.GetString("FatherName");
            oStudentInfo.MotherName = oReader.GetString("MotherName");
            oStudentInfo.NID = oReader.GetString("NID");
            oStudentInfo.DrivingLicence = oReader.GetString("DrivingLicence");
            oStudentInfo.Passport = oReader.GetString("Passport");
            oStudentInfo.OtherIdentity = oReader.GetString("OtherIdentity");
            oStudentInfo.ActivityStatus = (EnumActivityStatus)oReader.GetInt16("ActivityStatus");
            return oStudentInfo;
        }
        private StudentInfo MakeObject(NullHandler oReader)
        {
            StudentInfo oStudentInfo = new StudentInfo();
            oStudentInfo = MapObject(oReader);
            return oStudentInfo;
        }
        private List<StudentInfo> MakeObjects(IDataReader oReader)
        {
            List<StudentInfo> oStudentInfos = new List<StudentInfo>();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                StudentInfo oStudentInfo = MapObject(oHandler);
                oStudentInfos.Add(oStudentInfo);
            }
            return oStudentInfos;
        }
        #endregion


        #region Function Implementation
        public StudentInfo IUD(StudentInfo oStudentInfo, EnumDBOperation oDBOperation, string sUserID)
        {
            StudentInfo _oStudentInfo = new StudentInfo();
            try
            {
                Connection.Open();
                Command.CommandText = StudentInfoDA.IUD(oStudentInfo, oDBOperation, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oStudentInfo = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oStudentInfo = new StudentInfo();
                _oStudentInfo.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oStudentInfo;
        }
        public List<StudentInfo> Gets(string sSQL, string sUserID)
        {
            StudentInfo _oStudentInfo = new StudentInfo();
            List<StudentInfo> _oStudentInfos = new List<StudentInfo>();
            try
            {
                Connection.Open();
                Command.CommandText = StudentInfoDA.Gets(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oStudentInfos = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oStudentInfo = new StudentInfo();
                _oStudentInfos = new List<StudentInfo>();
                _oStudentInfo.ErrorMessage = e.Message.Split('~')[1];
                _oStudentInfos.Add(_oStudentInfo);
            }
            return _oStudentInfos;
        }
        public StudentInfo Get(int nID, string sUserID)
        {
            StudentInfo _oStudentInfo = new StudentInfo();
            try
            {
                Connection.Open();
                Command.CommandText = StudentInfoDA.Get(nID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oStudentInfo = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oStudentInfo = new StudentInfo();
                _oStudentInfo.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oStudentInfo;
        }
        #endregion
    }
}