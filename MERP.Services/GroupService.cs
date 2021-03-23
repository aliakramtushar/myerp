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
    public class GroupService : CommonGateway, IGroup
    {
        #region Maping
        private Group MapObject(NullHandler oReader)
        {
            Group oGroup = new Group();
            oGroup.GroupID = oReader.GetInt32("GroupID");
            oGroup.GroupCode = oReader.GetString("GroupCode");
            oGroup.GroupName = oReader.GetString("GroupName");
            oGroup.GroupDesc = oReader.GetString("GroupDesc");
            oGroup.IsInActive = (EnumActivityStatus)oReader.GetInt16("IsInActive");
            oGroup.HasAccess = (EnumAccessStatus)oReader.GetInt16("HasAccess");
            return oGroup;
        }
        private Group MakeObject(NullHandler oReader)
        {
            Group oGroup = new Group();
            oGroup = MapObject(oReader);
            return oGroup;
        }
        private List<Group> MakeObjects(IDataReader oReader)
        {
            List<Group> oGroups = new List<Group>();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                Group oGroup = MapObject(oHandler);
                oGroups.Add(oGroup);
            }
            return oGroups;
        }
        #endregion


        #region Function Implementation
        public Group IUD(Group oGroup, EnumDBOperation oDBOperation, string sUserID)
        {
            Group _oGroup = new Group();
            try
            {
                Connection.Open();
                Command.CommandText = GroupDA.IUD(oGroup, oDBOperation, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oGroup = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oGroup = new Group();
                _oGroup.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oGroup;
        }
        public List<Group> Gets(string sSQL, string sUserID)
        {
            Group _oGroup = new Group();
            List<Group> _oGroups = new List<Group>();
            try
            {
                Connection.Open();
                Command.CommandText = GroupDA.Gets(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oGroups = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oGroup = new Group();
                _oGroups = new List<Group>();
                _oGroup.ErrorMessage = e.Message.Split('~')[1];
                _oGroups.Add(_oGroup);
            }
            return _oGroups;
        }
        public List<Group> GetsActiveAndHasAccessList(string sUserID)
        {
            Group _oGroup = new Group();
            List<Group> _oGroups = new List<Group>();
            try
            {
                Connection.Open();
                Command.CommandText = GroupDA.GetsActiveAndHasAccessList(sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oGroups = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oGroup = new Group();
                _oGroups = new List<Group>();
                _oGroup.ErrorMessage = e.Message.Split('~')[1];
                _oGroups.Add(_oGroup);
            }
            return _oGroups;
        }
        public Group Get(int nID, string sUserID)
        {
            Group _oGroup = new Group();
            try
            {
                Connection.Open();
                Command.CommandText = GroupDA.Get(nID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oGroup = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oGroup = new Group();
                _oGroup.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oGroup;
        }
        public string Delete(int nGroupID, string sUserID)
        {
            try
            {
                Connection.Open();
                Command.CommandText = GroupDA.Delete(nGroupID, sUserID);
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