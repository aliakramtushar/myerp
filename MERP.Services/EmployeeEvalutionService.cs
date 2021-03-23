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
    public class EmployeeEvalutionService : CommonGateway, IEmployeeEvalution
    {
        #region Maping
        private EmployeeEvalution MapObject(NullHandler oReader)
        {
            EmployeeEvalution oEmployeeEvalution = new EmployeeEvalution();
            oEmployeeEvalution.QuestionID = oReader.GetInt32("QuestionID");
            oEmployeeEvalution.QuestionType = oReader.GetString("QuestionType");
            oEmployeeEvalution.QuestionName = oReader.GetString("QuestionName");
            oEmployeeEvalution.GradeID = oReader.GetString("GradeID");
            oEmployeeEvalution.SpecialEmpType = oReader.GetString("SpecialEmpType");
            oEmployeeEvalution.QuestionMark = oReader.GetString("QuestionMark");
            oEmployeeEvalution.EmployeeSupervisorCode = oReader.GetString("EmployeeSupervisorCode");
            oEmployeeEvalution.QuestionName = oReader.GetString("QuestionName");
            oEmployeeEvalution.EmployeeCode = oReader.GetString("EmployeeCode");
            oEmployeeEvalution.EvaluateFor = oReader.GetString("EvaluateFor");

            return oEmployeeEvalution;
    }
        private EmployeeEvalution MakeObject(NullHandler oReader)
        {
            EmployeeEvalution oEmployeeEvalution = new EmployeeEvalution();
            oEmployeeEvalution = MapObject(oReader);
            return oEmployeeEvalution;
        }
        private List<EmployeeEvalution> MakeObjects(IDataReader oReader)
        {
            List<EmployeeEvalution> oEmployeeEvalutions = new List<EmployeeEvalution>();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                EmployeeEvalution oEmployeeEvalution = MapObject(oHandler);
                oEmployeeEvalutions.Add(oEmployeeEvalution);
            }
            return oEmployeeEvalutions;
        }
        #endregion


        #region Function Implementation
        public EmployeeEvalution IUD(EmployeeEvalution oEmployeeEvalution, EnumDBOperation oDBOperation, string sUserID)
        {
            EmployeeEvalution _oEmployeeEvalution = new EmployeeEvalution();
            try
            {
                Connection.Open();
                Command.CommandText = EmployeeEvalutionDA.IUD(oEmployeeEvalution, oDBOperation, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oEmployeeEvalution = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oEmployeeEvalution = new EmployeeEvalution();
                _oEmployeeEvalution.ErrorMessage = e.Message.Split('~')[1];;
            }
            return _oEmployeeEvalution;
        }
        public List<EmployeeEvalution> Gets(string sSQL, string sUserID)
        {
            EmployeeEvalution _oEmployeeEvalution = new EmployeeEvalution();
            List<EmployeeEvalution> _oEmployeeEvalutions = new List<EmployeeEvalution>();
            try
            {
                Connection.Open();
                Command.CommandText = EmployeeEvalutionDA.Gets(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oEmployeeEvalutions = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oEmployeeEvalution = new EmployeeEvalution();
                _oEmployeeEvalutions = new List<EmployeeEvalution>();
                _oEmployeeEvalution.ErrorMessage = e.Message.Split('~')[1];;
                _oEmployeeEvalutions.Add(_oEmployeeEvalution);
            }
            return _oEmployeeEvalutions;
        }
        public EmployeeEvalution Get(int nID, string sUserID)
        {
            EmployeeEvalution _oEmployeeEvalution = new EmployeeEvalution();
            try
            {
                Connection.Open();
                Command.CommandText = EmployeeEvalutionDA.Get(nID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oEmployeeEvalution = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oEmployeeEvalution = new EmployeeEvalution();
                _oEmployeeEvalution.ErrorMessage = e.Message.Split('~')[1];;
            }
            return _oEmployeeEvalution;
        }
        public string Delete(int nEmployeeEvalutionID, string sUserID)
        {
            try
            {
                Connection.Open();
                Command.CommandText = EmployeeEvalutionDA.Delete(nEmployeeEvalutionID, sUserID);
                Command.ExecuteNonQuery();
                Connection.Close();
            }
            catch (Exception e)
            {
                FeedbackMessage.Deleted = e.Message.Split('~')[1];
            }
            return FeedbackMessage.Deleted;
        }
        #endregion
    }
}