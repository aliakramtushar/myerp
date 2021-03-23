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
    public class UserMenuMappingService : CommonGateway, IUserMenuMapping
    {
        #region Maping
        private UserMenuMapping MapObject(NullHandler oReader)
        {
            UserMenuMapping oUserMenuMapping = new UserMenuMapping();
            oUserMenuMapping.UserMenuMappingID = oReader.GetInt32("UserMenuMappingID");
            oUserMenuMapping.UserID = oReader.GetInt32("UserID");
            oUserMenuMapping.MenuID = oReader.GetInt32("MenuID");
            oUserMenuMapping.ParentID = oReader.GetInt32("ParentID");
            return oUserMenuMapping;
        }
        private UserMenuMapping MakeObject(NullHandler oReader)
        {
            UserMenuMapping oUserMenuMapping = new UserMenuMapping();
            oUserMenuMapping = MapObject(oReader);
            return oUserMenuMapping;
        }
        private List<UserMenuMapping> MakeObjects(IDataReader oReader)
        {
            List<UserMenuMapping> oUserMenuMappings = new List<UserMenuMapping>();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                UserMenuMapping oUserMenuMapping = MapObject(oHandler);
                oUserMenuMappings.Add(oUserMenuMapping);
            }
            return oUserMenuMappings;
        }
        #endregion


        #region Function Implementation
        public UserMenuMapping IUD(UserMenuMapping oUserMenuMapping, EnumDBOperation oDBOperation, string sUserID)
        {
            UserMenuMapping _oUserMenuMapping = new UserMenuMapping();
            try
            {
                Connection.Open();
                Command.CommandText = UserMenuMappingDA.IUD(oUserMenuMapping, oDBOperation, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oUserMenuMapping = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oUserMenuMapping = new UserMenuMapping();
                _oUserMenuMapping.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oUserMenuMapping;
        }
        public List<UserMenuMapping> Gets(string sSQL, string sUserID)
        {
            UserMenuMapping _oUserMenuMapping = new UserMenuMapping();
            List<UserMenuMapping> _oUserMenuMappings = new List<UserMenuMapping>();
            try
            {
                Connection.Open();
                Command.CommandText = UserMenuMappingDA.Gets(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oUserMenuMappings = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oUserMenuMapping = new UserMenuMapping();
                _oUserMenuMappings = new List<UserMenuMapping>();
                _oUserMenuMapping.ErrorMessage = e.Message.Split('~')[1];
                _oUserMenuMappings.Add(_oUserMenuMapping);
            }
            return _oUserMenuMappings;
        }
        public UserMenuMapping Get(int nID, string sUserID)
        {
            UserMenuMapping _oUserMenuMapping = new UserMenuMapping();
            try
            {
                Connection.Open();
                Command.CommandText = UserMenuMappingDA.Get(nID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oUserMenuMapping = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oUserMenuMapping = new UserMenuMapping();
                _oUserMenuMapping.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oUserMenuMapping;
        }
        public string Delete(int nUserMenuMappingID, string sUserID)
        {
            try
            {
                Connection.Open();
                Command.CommandText = UserMenuMappingDA.Delete(nUserMenuMappingID, sUserID);
                Command.ExecuteNonQuery();
                Connection.Close();
            }
            catch (Exception e)
            {
                FeedbackMessage.Deleted = e.Message.Split('~')[0];
            }
            return FeedbackMessage.Deleted;
        }
        public List<UserMenuMapping> SaveUserMenuMapping(int nUserID, string sMenuIDs, EnumDBOperation oDBOperation, string sUserID)
        {
            List<UserMenuMapping> _oUserMenuMappings = new List<UserMenuMapping>();
            try
            {
                Connection.Open();
                Command.CommandText = UserMenuMappingDA.SaveUserMenuMapping(nUserID, sMenuIDs, oDBOperation, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oUserMenuMappings = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                UserMenuMapping _oUserMenuMapping = new UserMenuMapping();
                _oUserMenuMapping.ErrorMessage = e.Message.Split('~')[1];
                _oUserMenuMappings.Add(_oUserMenuMapping);
            }
            return _oUserMenuMappings;
        }
        #endregion
    }
}