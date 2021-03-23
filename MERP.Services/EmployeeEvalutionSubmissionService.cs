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
    public class EmployeeEvalutionSubmissionService : CommonGateway, IEmployeeEvalutionSubmission
    {
        #region Maping
        private EmployeeEvalutionSubmission MapObject(NullHandler oReader)
        {
            EmployeeEvalutionSubmission oEmployeeEvalutionSubmission = new EmployeeEvalutionSubmission();
            oEmployeeEvalutionSubmission.EmployeeEvalutionSubmissionID = oReader.GetInt32("EmployeeEvalutionSubmissionID");
            oEmployeeEvalutionSubmission.QuestionID = oReader.GetInt32("QuestionID");
            oEmployeeEvalutionSubmission.EmployeeCode = oReader.GetString("EmployeeCode");
            oEmployeeEvalutionSubmission.EvaluateFor = oReader.GetString("EvaluateFor");
            oEmployeeEvalutionSubmission.QuestionMark = oReader.GetString("QuestionMark");
            oEmployeeEvalutionSubmission.RelationType = oReader.GetInt16("RelationType");

            return oEmployeeEvalutionSubmission;
        }
        private EmployeeEvalutionSubmission MakeObject(NullHandler oReader)
        {
            EmployeeEvalutionSubmission oEmployeeEvalutionSubmission = new EmployeeEvalutionSubmission();
            oEmployeeEvalutionSubmission = MapObject(oReader);
            return oEmployeeEvalutionSubmission;
        }
        private List<EmployeeEvalutionSubmission> MakeObjects(IDataReader oReader)
        {
            List<EmployeeEvalutionSubmission> oEmployeeEvalutionSubmissions = new List<EmployeeEvalutionSubmission>();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                EmployeeEvalutionSubmission oEmployeeEvalutionSubmission = MapObject(oHandler);
                oEmployeeEvalutionSubmissions.Add(oEmployeeEvalutionSubmission);
            }
            return oEmployeeEvalutionSubmissions;
        }
        #endregion


        #region Function Implementation
        public EmployeeEvalutionSubmission IUD(EmployeeEvalutionSubmission oEmployeeEvalutionSubmission, EnumDBOperation oDBOperation, string sUserID)
        {
            EmployeeEvalutionSubmission _oEmployeeEvalutionSubmission = new EmployeeEvalutionSubmission();
            try
            {
                Connection.Open();
                Command.CommandText = EmployeeEvalutionSubmissionDA.IUD(oEmployeeEvalutionSubmission, oDBOperation, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oEmployeeEvalutionSubmission = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oEmployeeEvalutionSubmission = new EmployeeEvalutionSubmission();
                _oEmployeeEvalutionSubmission.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oEmployeeEvalutionSubmission;
        }
        public List<EmployeeEvalutionSubmission> Gets(string sSQL, string sUserID)
        {
            EmployeeEvalutionSubmission _oEmployeeEvalutionSubmission = new EmployeeEvalutionSubmission();
            List<EmployeeEvalutionSubmission> _oEmployeeEvalutionSubmissions = new List<EmployeeEvalutionSubmission>();
            try
            {
                Connection.Open();
                Command.CommandText = EmployeeEvalutionSubmissionDA.Gets(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oEmployeeEvalutionSubmissions = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oEmployeeEvalutionSubmission = new EmployeeEvalutionSubmission();
                _oEmployeeEvalutionSubmissions = new List<EmployeeEvalutionSubmission>();
                _oEmployeeEvalutionSubmission.ErrorMessage = e.Message.Split('~')[1];
                _oEmployeeEvalutionSubmissions.Add(_oEmployeeEvalutionSubmission);
            }
            return _oEmployeeEvalutionSubmissions;
        }
        public EmployeeEvalutionSubmission Get(int nID, string sUserID)
        {
            EmployeeEvalutionSubmission _oEmployeeEvalutionSubmission = new EmployeeEvalutionSubmission();
            try
            {
                Connection.Open();
                Command.CommandText = EmployeeEvalutionSubmissionDA.Get(nID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oEmployeeEvalutionSubmission = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oEmployeeEvalutionSubmission = new EmployeeEvalutionSubmission();
                _oEmployeeEvalutionSubmission.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oEmployeeEvalutionSubmission;
        }
        public string Delete(int nEmployeeEvalutionSubmissionID, string sUserID)
        {
            try
            {
                Connection.Open();
                Command.CommandText = EmployeeEvalutionSubmissionDA.Delete(nEmployeeEvalutionSubmissionID, sUserID);
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