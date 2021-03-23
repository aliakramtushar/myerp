using MERP.Engine.GlobalClass;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.DataAccess
{
    public class EmployeeDA
    {
        public static string IUD(Employee oEmployee, EnumDBOperation oDBOperation, string sUserID)
        {
            return GlobalHelpers.ExcecuteQurey("EXEC SP_IUD_Employee",
                oEmployee.EmployeeID, oEmployee.EmployeeCode, oEmployee.FirstName, oEmployee.MiddleName, oEmployee.LastName, oEmployee.FatherName, oEmployee.MotherName, 
                oEmployee.SpouseName, (int)oEmployee.MaritalStatus, oEmployee.Phone, oEmployee.Mobile, oEmployee.Email, oEmployee.PresentAddress, oEmployee.PermanentAddress,
                oEmployee.DivisionID,oEmployee.DivisionName,oEmployee.DistrictID,oEmployee.DistrictName,oEmployee.LocationID,oEmployee.LocationName, (int)oEmployee.ActivityStatus,
                oEmployee.DateOfBirth,oEmployee.Remarks, (int)oEmployee.BloodGroup,oEmployee.Height,oEmployee.Weight, (int)oEmployee.ReligionID, (int)oEmployee.Gender,oEmployee.NationalIDNo,
                oEmployee.DateOfBirth,oEmployee.Remarks, (int)oEmployee.BloodGroup,oEmployee.Height,oEmployee.Weight, (int)oEmployee.ReligionID, (int)oEmployee.Gender,oEmployee.NationalIDNo,
                oEmployee.DrivingLicenceNo,oEmployee.PassportNo,oEmployee.Photo,oEmployee.ETIN,oEmployee.CountryID,oEmployee.CountryName,oEmployee.PersonalBankID,oEmployee.PersonalBankName,
                oEmployee.PersonalBankAccountNo,oEmployee.ExtraSkill,oEmployee.MachineNameIP, (int)oDBOperation, sUserID);
        }
        public static string Gets(string sSQL, string sUserID)
        {
            if (sSQL == "") return "SELECT * FROM View_Employee";
            else return sSQL;
        }
        public static string Get(int nID, string sUserID)
        {
            return "SELECT * FROM View_Employee WHERE [EmployeeID] =" + nID;
        }
        public static string Delete(int nID, string sUserID)
        {
            return "DELETE FROM Employee WHERE EmployeeID = " + nID;
        }
    }
}