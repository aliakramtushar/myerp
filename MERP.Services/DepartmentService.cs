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
    public class DepartmentService : CommonGateway, IDepartment
    {
        #region Maping
        private Department MapObject(NullHandler oReader)
        {
            Department oDepartment = new Department();
            oDepartment.DepartmentID = oReader.GetInt32("DepartmentID");
            oDepartment.DepartmentName = oReader.GetString("DepartmentName");
            oDepartment.ActivityStatus = (EnumActivityStatus)oReader.GetInt16("ActivityStatus");
            oDepartment.Remarks = oReader.GetString("Remarks");

            return oDepartment;
        }
        private Department MakeObject(NullHandler oReader)
        {
            Department oDepartment = new Department();
            oDepartment = MapObject(oReader);
            return oDepartment;
        }
        private List<Department> MakeObjects(IDataReader oReader)
        {
            List<Department> oDepartments = new List<Department>();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                Department oDepartment = MapObject(oHandler);
                oDepartments.Add(oDepartment);
            }
            return oDepartments;
        }
        #endregion


        #region Function Implementation
        public Department IUD(Department oDepartment, EnumDBOperation oDBOperation, string sUserID)
        {
            Department _oDepartment = new Department();
            try
            {
                Connection.Open();
                Command.CommandText = DepartmentDA.IUD(oDepartment, oDBOperation, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oDepartment = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oDepartment = new Department();
                _oDepartment.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oDepartment;
        }
        public List<Department> Gets(string sSQL, string sUserID)
        {
            Department _oDepartment = new Department();
            List<Department> _oDepartments = new List<Department>();
            try
            {
                Connection.Open();
                Command.CommandText = DepartmentDA.Gets(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oDepartments = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oDepartment = new Department();
                _oDepartments = new List<Department>();
                _oDepartment.ErrorMessage = e.Message.Split('~')[1];
                _oDepartments.Add(_oDepartment);
            }
            return _oDepartments;
        }
        public Department Get(int nID, string sUserID)
        {
            Department _oDepartment = new Department();
            try
            {
                Connection.Open();
                Command.CommandText = DepartmentDA.Get(nID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oDepartment = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oDepartment = new Department();
                _oDepartment.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oDepartment;
        }
        public string Delete(int nDepartmentID, string sUserID)
        {
            try
            {
                Connection.Open();
                Command.CommandText = DepartmentDA.Delete(nDepartmentID, sUserID);
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