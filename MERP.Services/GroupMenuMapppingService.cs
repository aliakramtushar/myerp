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
    public class GroupMenuMappingService : CommonGateway, IGroupMenuMapping
    {
        #region Maping
        private GroupMenuMapping MapObject(NullHandler oReader)
        {
            GroupMenuMapping oGroupMenuMapping = new GroupMenuMapping();
            oGroupMenuMapping.GroupMenuMappingID = oReader.GetInt32("GroupMenuMappingID");
            oGroupMenuMapping.GroupID = oReader.GetInt32("GroupID");
            oGroupMenuMapping.MenuID = oReader.GetInt32("MenuID");
            oGroupMenuMapping.ParentID = oReader.GetInt32("ParentID");
            return oGroupMenuMapping;
        }
        private GroupMenuMapping MakeObject(NullHandler oReader)
        {
            GroupMenuMapping oGroupMenuMapping = new GroupMenuMapping();
            oGroupMenuMapping = MapObject(oReader);
            return oGroupMenuMapping;
        }
        private List<GroupMenuMapping> MakeObjects(IDataReader oReader)
        {
            List<GroupMenuMapping> oGroupMenuMappings = new List<GroupMenuMapping>();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                GroupMenuMapping oGroupMenuMapping = MapObject(oHandler);
                oGroupMenuMappings.Add(oGroupMenuMapping);
            }
            return oGroupMenuMappings;
        }
        #endregion


        #region Function Implementation
        public GroupMenuMapping IUD(GroupMenuMapping oGroupMenuMapping, EnumDBOperation oDBOperation, string sUserID)
        {
            GroupMenuMapping _oGroupMenuMapping = new GroupMenuMapping();
            try
            {
                Connection.Open();
                Command.CommandText = GroupMenuMappingDA.IUD(oGroupMenuMapping, oDBOperation, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oGroupMenuMapping = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oGroupMenuMapping = new GroupMenuMapping();
                _oGroupMenuMapping.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oGroupMenuMapping;
        }
        public List<GroupMenuMapping> Gets(string sSQL, string sUserID)
        {
            GroupMenuMapping _oGroupMenuMapping = new GroupMenuMapping();
            List<GroupMenuMapping> _oGroupMenuMappings = new List<GroupMenuMapping>();
            try
            {
                Connection.Open();
                Command.CommandText = GroupMenuMappingDA.Gets(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oGroupMenuMappings = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oGroupMenuMapping = new GroupMenuMapping();
                _oGroupMenuMappings = new List<GroupMenuMapping>();
                _oGroupMenuMapping.ErrorMessage = e.Message.Split('~')[1];
                _oGroupMenuMappings.Add(_oGroupMenuMapping);
            }
            return _oGroupMenuMappings;
        }
        public GroupMenuMapping Get(int nID, string sUserID)
        {
            GroupMenuMapping _oGroupMenuMapping = new GroupMenuMapping();
            try
            {
                Connection.Open();
                Command.CommandText = GroupMenuMappingDA.Get(nID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oGroupMenuMapping = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oGroupMenuMapping = new GroupMenuMapping();
                _oGroupMenuMapping.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oGroupMenuMapping;
        }
        public string Delete(int nGroupMenuMappingID, string sUserID)
        {
            try
            {
                Connection.Open();
                Command.CommandText = GroupMenuMappingDA.Delete(nGroupMenuMappingID, sUserID);
                Command.ExecuteNonQuery();
                Connection.Close();
            }
            catch (Exception e)
            {
                FeedbackMessage.Deleted = e.Message.Split('~')[0];
            }
            return FeedbackMessage.Deleted;
        }
        public List<GroupMenuMapping> SaveGroupMenuMapping(int nGroupID, string sMenuIDs, EnumDBOperation oDBOperation, string sUserID)
        {
            List<GroupMenuMapping> _oGroupMenuMappings = new List<GroupMenuMapping>();
            try
            {
                Connection.Open();
                Command.CommandText = GroupMenuMappingDA.SaveGroupMenuMapping(nGroupID, sMenuIDs, oDBOperation, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oGroupMenuMappings = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                GroupMenuMapping _oGroupMenuMapping = new GroupMenuMapping();
                _oGroupMenuMapping.ErrorMessage = e.Message.Split('~')[1];
                _oGroupMenuMappings.Add(_oGroupMenuMapping);
            }
            return _oGroupMenuMappings;
        }
        #endregion
    }
}