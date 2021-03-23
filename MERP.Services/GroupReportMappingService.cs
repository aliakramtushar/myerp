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
    public class GroupReportMappingService : CommonGateway, IGroupReportMapping
    {
        #region Maping
        private GroupReportMapping MapObject(NullHandler oReader)
        {
            GroupReportMapping oGroupReportMapping = new GroupReportMapping();
            oGroupReportMapping.GroupReportMappingID = oReader.GetInt32("GroupReportMappingID");
            oGroupReportMapping.GroupID = oReader.GetInt32("GroupID");
            oGroupReportMapping.ReportMasterID = oReader.GetInt32("ReportMasterID");
            oGroupReportMapping.GroupName = oReader.GetString("GroupName");
            oGroupReportMapping.ReportName = oReader.GetString("ReportName");
            return oGroupReportMapping;
        }
        private GroupReportMapping MakeObject(NullHandler oReader)
        {
            GroupReportMapping oGroupReportMapping = new GroupReportMapping();
            oGroupReportMapping = MapObject(oReader);
            return oGroupReportMapping;
        }
        private List<GroupReportMapping> MakeObjects(IDataReader oReader)
        {
            List<GroupReportMapping> oGroupReportMappings = new List<GroupReportMapping>();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                GroupReportMapping oGroupReportMapping = MapObject(oHandler);
                oGroupReportMappings.Add(oGroupReportMapping);
            }
            return oGroupReportMappings;
        }
        #endregion


        #region Function Implementation
        public GroupReportMapping IUD(GroupReportMapping oGroupReportMapping, EnumDBOperation oDBOperation, string sUserID)
        {
            GroupReportMapping _oGroupReportMapping = new GroupReportMapping();
            try
            {
                Connection.Open();
                Command.CommandText = GroupReportMappingDA.IUD(oGroupReportMapping, oDBOperation, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oGroupReportMapping = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oGroupReportMapping = new GroupReportMapping();
                _oGroupReportMapping.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oGroupReportMapping;
        }
        public GroupReportMapping SaveByReportMasterID(GroupReportMapping oGroupReportMapping, string sUserID)
        {
            GroupReportMapping _oGroupReportMapping = new GroupReportMapping();
            try
            {
                Connection.Open();
                Command.CommandText = GroupReportMappingDA.IUD(oGroupReportMapping, EnumDBOperation.SpecialCase1, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oGroupReportMapping = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oGroupReportMapping = new GroupReportMapping();
                _oGroupReportMapping.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oGroupReportMapping;
        }
        public GroupReportMapping SaveByGroupID(GroupReportMapping oGroupReportMapping, string sUserID)
        {
            GroupReportMapping _oGroupReportMapping = new GroupReportMapping();
            try
            {
                Connection.Open();
                Command.CommandText = GroupReportMappingDA.IUD(oGroupReportMapping, EnumDBOperation.SpecialCase2, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oGroupReportMapping = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oGroupReportMapping = new GroupReportMapping();
                _oGroupReportMapping.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oGroupReportMapping;
        }
        public List<GroupReportMapping> Gets(string sSQL, string sUserID)
        {
            GroupReportMapping _oGroupReportMapping = new GroupReportMapping();
            List<GroupReportMapping> _oGroupReportMappings = new List<GroupReportMapping>();
            try
            {
                Connection.Open();
                Command.CommandText = GroupReportMappingDA.Gets(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oGroupReportMappings = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oGroupReportMapping = new GroupReportMapping();
                _oGroupReportMappings = new List<GroupReportMapping>();
                _oGroupReportMapping.ErrorMessage = e.Message.Split('~')[1];
                _oGroupReportMappings.Add(_oGroupReportMapping);
            }
            return _oGroupReportMappings;
        }
        public List<GroupReportMapping> GetsByReportMasterID(int nReportMasterID, string sUserID)
        {
            GroupReportMapping _oGroupReportMapping = new GroupReportMapping();
            List<GroupReportMapping> _oGroupReportMappings = new List<GroupReportMapping>();
            try
            {
                Connection.Open();
                Command.CommandText = GroupReportMappingDA.GetsByReportMasterID(nReportMasterID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oGroupReportMappings = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oGroupReportMapping = new GroupReportMapping();
                _oGroupReportMappings = new List<GroupReportMapping>();
                _oGroupReportMapping.ErrorMessage = e.Message.Split('~')[1];
                _oGroupReportMappings.Add(_oGroupReportMapping);
            }
            return _oGroupReportMappings;
        }
        public List<GroupReportMapping> GetsByGroupID(int nGroupID, string sUserID)
        {
            GroupReportMapping _oGroupReportMapping = new GroupReportMapping();
            List<GroupReportMapping> _oGroupReportMappings = new List<GroupReportMapping>();
            try
            {
                Connection.Open();
                Command.CommandText = GroupReportMappingDA.GetsByGroupID(nGroupID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oGroupReportMappings = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oGroupReportMapping = new GroupReportMapping();
                _oGroupReportMappings = new List<GroupReportMapping>();
                _oGroupReportMapping.ErrorMessage = e.Message.Split('~')[1];
                _oGroupReportMappings.Add(_oGroupReportMapping);
            }
            return _oGroupReportMappings;
        }
        public GroupReportMapping Get(int nID, string sUserID)
        {
            GroupReportMapping _oGroupReportMapping = new GroupReportMapping();
            try
            {
                Connection.Open();
                Command.CommandText = GroupReportMappingDA.Get(nID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oGroupReportMapping = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oGroupReportMapping = new GroupReportMapping();
                _oGroupReportMapping.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oGroupReportMapping;
        }
        public string Delete(int nGroupReportMappingID, string sUserID)
        {
            try
            {
                Connection.Open();
                Command.CommandText = GroupReportMappingDA.Delete(nGroupReportMappingID, sUserID);
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