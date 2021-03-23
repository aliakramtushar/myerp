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
    public class DesignationService : CommonGateway, IDesignation
    {
        #region Maping
        private Designation MapObject(NullHandler oReader)
        {
            Designation oDesignation = new Designation();
            oDesignation.DesignationID = oReader.GetInt32("DesignationID");
            oDesignation.DesignationName = oReader.GetString("DesignationName");
            oDesignation.ActivityStatus = (EnumActivityStatus)oReader.GetInt16("ActivityStatus");
            oDesignation.Remarks = oReader.GetString("Remarks");

            return oDesignation;
        }
        private Designation MakeObject(NullHandler oReader)
        {
            Designation oDesignation = new Designation();
            oDesignation = MapObject(oReader);
            return oDesignation;
        }
        private List<Designation> MakeObjects(IDataReader oReader)
        {
            List<Designation> oDesignations = new List<Designation>();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                Designation oDesignation = MapObject(oHandler);
                oDesignations.Add(oDesignation);
            }
            return oDesignations;
        }
        #endregion


        #region Function Implementation
        public Designation IUD(Designation oDesignation, EnumDBOperation oDBOperation, string sUserID)
        {
            Designation _oDesignation = new Designation();
            try
            {
                Connection.Open();
                Command.CommandText = DesignationDA.IUD(oDesignation, oDBOperation, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oDesignation = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oDesignation = new Designation();
                _oDesignation.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oDesignation;
        }
        public List<Designation> Gets(string sSQL, string sUserID)
        {
            Designation _oDesignation = new Designation();
            List<Designation> _oDesignations = new List<Designation>();
            try
            {
                Connection.Open();
                Command.CommandText = DesignationDA.Gets(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oDesignations = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oDesignation = new Designation();
                _oDesignations = new List<Designation>();
                _oDesignation.ErrorMessage = e.Message.Split('~')[1];;
                _oDesignations.Add(_oDesignation);
            }
            return _oDesignations;
        }
        public Designation Get(int nID, string sUserID)
        {
            Designation _oDesignation = new Designation();
            try
            {
                Connection.Open();
                Command.CommandText = DesignationDA.Get(nID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oDesignation = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oDesignation = new Designation();
                _oDesignation.ErrorMessage = e.Message.Split('~')[1];;
            }
            return _oDesignation;
        }
        public string Delete(int nDesignationID, string sUserID)
        {
            try
            {
                Connection.Open();
                Command.CommandText = DesignationDA.Delete(nDesignationID, sUserID);
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