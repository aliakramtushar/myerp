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
    public class StudentTrainingMappingService : CommonGateway, IStudentTrainingMapping
    {
        #region Maping
        private StudentTrainingMapping MapObject(NullHandler oReader)
        {
            StudentTrainingMapping oStudentTrainingMapping = new StudentTrainingMapping();
            oStudentTrainingMapping.StudentTrainingMappingID = oReader.GetInt32("StudentTrainingMappingID");
            oStudentTrainingMapping.StudentID = oReader.GetInt32("StudentID");
            oStudentTrainingMapping.TrainingID = oReader.GetInt32("TrainingID");
            oStudentTrainingMapping.BranchID = oReader.GetInt32("BranchID");
            oStudentTrainingMapping.Amount = oReader.GetDouble("Amount");
            oStudentTrainingMapping.DiscountAmount = oReader.GetDouble("DiscountAmount");
            oStudentTrainingMapping.PaidAmount = oReader.GetDouble("PaidAmount");
            oStudentTrainingMapping.AdjustmentAmount = oReader.GetDouble("AdjustmentAmount");
            oStudentTrainingMapping.ReferenceType = (EnumReferenceType)oReader.GetInt16("ReferenceType");
            oStudentTrainingMapping.ReferenceName = oReader.GetString("ReferenceName");
            oStudentTrainingMapping.ReferenceContactNo = oReader.GetString("ReferenceContactNo");
            oStudentTrainingMapping.StartDate = oReader.GetDateTime("StartDate");
            oStudentTrainingMapping.Remarks = oReader.GetString("Remarks");
            oStudentTrainingMapping.BlankField_1 = oReader.GetString("BlankField_1");
            oStudentTrainingMapping.BlankField_2 = oReader.GetString("BlankField_2");
            oStudentTrainingMapping.BlankField_3 = oReader.GetString("BlankField_3");
            oStudentTrainingMapping.BlankField_4 = oReader.GetInt32("BlankField_4");
            oStudentTrainingMapping.BlankField_5 = oReader.GetDateTime("BlankField_5");
            return oStudentTrainingMapping;
        }
        private StudentTrainingMapping MakeObject(NullHandler oReader)
        {
            StudentTrainingMapping oStudentTrainingMapping = new StudentTrainingMapping();
            oStudentTrainingMapping = MapObject(oReader);
            return oStudentTrainingMapping;
        }
        private List<StudentTrainingMapping> MakeObjects(IDataReader oReader)
        {
            List<StudentTrainingMapping> oStudentTrainingMappings = new List<StudentTrainingMapping>();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                StudentTrainingMapping oStudentTrainingMapping = MapObject(oHandler);
                oStudentTrainingMappings.Add(oStudentTrainingMapping);
            }
            return oStudentTrainingMappings;
        }
        #endregion


        #region Function Implementation
        public StudentTrainingMapping IUD(StudentTrainingMapping oStudentTrainingMapping, EnumDBOperation oDBOperation, string sUserID)
        {
            StudentTrainingMapping _oStudentTrainingMapping = new StudentTrainingMapping();
            try
            {
                Connection.Open();
                Command.CommandText = StudentTrainingMappingDA.IUD(oStudentTrainingMapping, oDBOperation, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oStudentTrainingMapping = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oStudentTrainingMapping = new StudentTrainingMapping();
                _oStudentTrainingMapping.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oStudentTrainingMapping;
        }
        public List<StudentTrainingMapping> Gets(string sSQL, string sUserID)
        {
            StudentTrainingMapping _oStudentTrainingMapping = new StudentTrainingMapping();
            List<StudentTrainingMapping> _oStudentTrainingMappings = new List<StudentTrainingMapping>();
            try
            {
                Connection.Open();
                Command.CommandText = StudentTrainingMappingDA.Gets(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oStudentTrainingMappings = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oStudentTrainingMapping = new StudentTrainingMapping();
                _oStudentTrainingMappings = new List<StudentTrainingMapping>();
                _oStudentTrainingMapping.ErrorMessage = e.Message.Split('~')[1];
                _oStudentTrainingMappings.Add(_oStudentTrainingMapping);
            }
            return _oStudentTrainingMappings;
        }
        public StudentTrainingMapping Get(int nID, string sUserID)
        {
            StudentTrainingMapping _oStudentTrainingMapping = new StudentTrainingMapping();
            try
            {
                Connection.Open();
                Command.CommandText = StudentTrainingMappingDA.Get(nID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oStudentTrainingMapping = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oStudentTrainingMapping = new StudentTrainingMapping();
                _oStudentTrainingMapping.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oStudentTrainingMapping;
        }
        #endregion
    }
}