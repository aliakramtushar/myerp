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
    public class EmployeeService : CommonGateway, IEmployee
    {
        #region Maping
        private Employee MapObject(NullHandler oReader)
        {
            Employee oEmployee = new Employee();
            oEmployee.EmployeeID = oReader.GetInt32("EmployeeID");
            oEmployee.EmployeeCode = oReader.GetString("EmployeeCode");
            oEmployee.FirstName = oReader.GetString("FirstName");
            oEmployee.MiddleName = oReader.GetString("MiddleName");
            oEmployee.LastName = oReader.GetString("LastName");
            oEmployee.FatherName = oReader.GetString("FatherName");
            oEmployee.MotherName = oReader.GetString("MotherName");
            oEmployee.SpouseName = oReader.GetString("SpouseName");
            oEmployee.MaritalStatus = (EnumMaritalStatus)oReader.GetInt16("MaritalStatus");
            oEmployee.Phone = oReader.GetString("Phone");
            oEmployee.Mobile = oReader.GetString("Mobile");
            oEmployee.Email = oReader.GetString("Email");
            oEmployee.PresentAddress = oReader.GetString("PresentAddress");
            oEmployee.PermanentAddress = oReader.GetString("PermanentAddress");
            oEmployee.DivisionID = oReader.GetInt32("DivisionID");
            oEmployee.DivisionName = oReader.GetString("DivisionName");
            oEmployee.DistrictID = oReader.GetInt32("DistrictID");
            oEmployee.DistrictName = oReader.GetString("DistrictName");
            oEmployee.LocationID = oReader.GetInt32("LocationID");
            oEmployee.LocationName = oReader.GetString("LocationName");
            oEmployee.ActivityStatus = (EnumActivityStatus)oReader.GetInt16("ActivityStatus");
            oEmployee.DateOfBirth = oReader.GetDateTime("DateOfBirth");
            oEmployee.Remarks = oReader.GetString("Remarks");
            oEmployee.BloodGroup = (EnumBloodGroup)oReader.GetInt16("BloodGroup");
            oEmployee.Height = oReader.GetString("Height");
            oEmployee.Weight = oReader.GetString("Weight");
            oEmployee.ReligionID = (EnumReligion)oReader.GetInt16("ReligionID");
            oEmployee.Gender = (EnumGender)oReader.GetInt16("Gender");
            oEmployee.NationalIDNo = oReader.GetString("NationalIDNo");
            oEmployee.DrivingLicenceNo = oReader.GetString("DrivingLicenceNo");
            oEmployee.PassportNo = oReader.GetString("PassportNo");
            oEmployee.Photo = oReader.GetString("Photo");
            oEmployee.ETIN = oReader.GetString("ETIN");
            oEmployee.CountryID = oReader.GetInt32("CountryID");
            oEmployee.CountryName = oReader.GetString("CountryName");
            oEmployee.PersonalBankID = oReader.GetInt32("PersonalBankID");
            oEmployee.PersonalBankName = oReader.GetString("PersonalBankName");
            oEmployee.PersonalBankAccountNo = oReader.GetString("PersonalBankAccountNo");
            oEmployee.ExtraSkill = oReader.GetString("MachineNameIP");
            oEmployee.MachineNameIP = oReader.GetString("MachineNameIP");
            return oEmployee;
        }
        private Employee MakeObject(NullHandler oReader)
        {
            Employee oEmployee = new Employee();
            oEmployee = MapObject(oReader);
            return oEmployee;
        }
        private List<Employee> MakeObjects(IDataReader oReader)
        {
            List<Employee> oEmployees = new List<Employee>();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                Employee oEmployee = MapObject(oHandler);
                oEmployees.Add(oEmployee);
            }
            return oEmployees;
        }
        #endregion


        #region Function Implementation
        public Employee IUD(Employee oEmployee, EnumDBOperation oDBOperation, string sUserID)
        {
            Employee _oEmployee = new Employee();
            try
            {
                Connection.Open();
                Command.CommandText = EmployeeDA.IUD(oEmployee, oDBOperation, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oEmployee = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oEmployee = new Employee();
                _oEmployee.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oEmployee;
        }
        public List<Employee> Gets(string sSQL, string sUserID)
        {
            Employee _oEmployee = new Employee();
            List<Employee> _oEmployees = new List<Employee>();
            try
            {
                Connection.Open();
                Command.CommandText = EmployeeDA.Gets(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oEmployees = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oEmployee = new Employee();
                _oEmployees = new List<Employee>();
                _oEmployee.ErrorMessage = e.Message.Split('~')[1];
                _oEmployees.Add(_oEmployee);
            }
            return _oEmployees;
        }
        public Employee Get(int nID, string sUserID)
        {
            Employee _oEmployee = new Employee();
            try
            {
                Connection.Open();
                Command.CommandText = EmployeeDA.Get(nID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oEmployee = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oEmployee = new Employee();
                _oEmployee.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oEmployee;
        }
        public string Delete(int nEmployeeID, string sUserID)
        {
            try
            {
                Connection.Open();
                Command.CommandText = EmployeeDA.Delete(nEmployeeID, sUserID);
                Command.ExecuteNonQuery();
                Connection.Close();
            }
            catch (Exception e)
            {
                FeedbackMessage.Deleted = e.Message.Split('~')[0];
            }
            return FeedbackMessage.Deleted;
        }
        #endregion
    }
}