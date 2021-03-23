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
    public class PersonInfoService : CommonGateway, IPersonInfo
    {
        #region Maping
        private PersonInfo MapObject(NullHandler oReader)
        {
            PersonInfo oPersonInfo = new PersonInfo();
            oPersonInfo.PersonID = oReader.GetInt32("PersonID");
            oPersonInfo.PersonName = oReader.GetString("PersonName");
            oPersonInfo.Mobile = oReader.GetString("Mobile");
            oPersonInfo.Address = oReader.GetString("Address");
            oPersonInfo.PersonType = (EnumPersonType)oReader.GetInt32("PersonType");
            return oPersonInfo;
        }
        private PersonInfo MakeObject(NullHandler oReader)
        {
            PersonInfo oPersonInfo = new PersonInfo();
            oPersonInfo = MapObject(oReader);
            return oPersonInfo;
        }
        private List<PersonInfo> MakeObjects(IDataReader oReader)
        {
            List<PersonInfo> oPersonInfos = new List<PersonInfo>();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                PersonInfo oPersonInfo = MapObject(oHandler);
                oPersonInfos.Add(oPersonInfo);
            }
            return oPersonInfos;
        }
        #endregion


        #region Function Implementation
        public PersonInfo IUD(PersonInfo oPersonInfo, EnumDBOperation oDBOperation, string sUserID)
        {
            PersonInfo _oPersonInfo = new PersonInfo();
            try
            {
                Connection.Open();
                Command.CommandText = PersonInfoDA.IUD(oPersonInfo, oDBOperation, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oPersonInfo = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oPersonInfo = new PersonInfo();
                _oPersonInfo.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oPersonInfo;
        }
        public List<PersonInfo> Gets(string sSQL, string sUserID)
        {
            PersonInfo _oPersonInfo = new PersonInfo();
            List<PersonInfo> _oPersonInfos = new List<PersonInfo>();
            try
            {
                Connection.Open();
                Command.CommandText = PersonInfoDA.Gets(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oPersonInfos = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oPersonInfo = new PersonInfo();
                _oPersonInfos = new List<PersonInfo>();
                _oPersonInfo.ErrorMessage = e.Message;
                _oPersonInfos.Add(_oPersonInfo);
            }
            return _oPersonInfos;
        }
        public List<PersonInfo> GetsPersonByType(EnumPersonType oPersonType, string sUserID)
        {
            PersonInfo _oPersonInfo = new PersonInfo();
            List<PersonInfo> _oPersonInfos = new List<PersonInfo>();
            try
            {
                Connection.Open();
                Command.CommandText = PersonInfoDA.GetsPersonByType(oPersonType, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oPersonInfos = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oPersonInfo = new PersonInfo();
                _oPersonInfos = new List<PersonInfo>();
                _oPersonInfo.ErrorMessage = e.Message;
                _oPersonInfos.Add(_oPersonInfo);
            }
            return _oPersonInfos;
        }
        public PersonInfo Get(int nID, string sUserID)
        {
            PersonInfo _oPersonInfo = new PersonInfo();
            try
            {
                Connection.Open();
                Command.CommandText = PersonInfoDA.Get(nID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oPersonInfo = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oPersonInfo = new PersonInfo();
                _oPersonInfo.ErrorMessage = e.Message;
            }
            return _oPersonInfo;
        }
        public string Delete(int nPersonInfoID, string sUserID)
        {
            try
            {
                Connection.Open();
                Command.CommandText = PersonInfoDA.Delete(nPersonInfoID, sUserID);
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