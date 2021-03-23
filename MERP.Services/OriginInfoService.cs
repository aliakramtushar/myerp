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
    public class OriginInfoService : CommonGateway, IOriginInfo
    {
        #region Maping
        private OriginInfo MapObject(NullHandler oReader)
        {
            OriginInfo oOriginInfo = new OriginInfo();
            oOriginInfo.OriginID = oReader.GetInt32("OriginID");
            oOriginInfo.OriginCode = oReader.GetString("OriginCode");
            oOriginInfo.OriginName = oReader.GetString("OriginName");
            oOriginInfo.IsInActive = oReader.GetBoolean("IsInActive");
            return oOriginInfo;
        }
        private OriginInfo MakeObject(NullHandler oReader)
        {
            OriginInfo oOriginInfo = new OriginInfo();
            oOriginInfo = MapObject(oReader);
            return oOriginInfo;
        }
        private List<OriginInfo> MakeObjects(IDataReader oReader)
        {
            List<OriginInfo> oOriginInfos = new List<OriginInfo>();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                OriginInfo oOriginInfo = MapObject(oHandler);
                oOriginInfos.Add(oOriginInfo);
            }
            return oOriginInfos;
        }
        #endregion


        #region Function Implementation
        public OriginInfo IUD(OriginInfo oOriginInfo, EnumDBOperation oDBOperation, string sUserID)
        {
            OriginInfo _oOriginInfo = new OriginInfo();
            try
            {
                Connection.Open();
                Command.CommandText = OriginInfoDA.IUD(oOriginInfo, oDBOperation, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oOriginInfo = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oOriginInfo = new OriginInfo();
                _oOriginInfo.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oOriginInfo;
        }
        public List<OriginInfo> Gets(string sSQL, string sUserID)
        {
            OriginInfo _oOriginInfo = new OriginInfo();
            List<OriginInfo> _oOriginInfos = new List<OriginInfo>();
            try
            {
                Connection.Open();
                Command.CommandText = OriginInfoDA.Gets(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oOriginInfos = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oOriginInfo = new OriginInfo();
                _oOriginInfos = new List<OriginInfo>();
                _oOriginInfo.ErrorMessage = e.Message;
                _oOriginInfos.Add(_oOriginInfo);
            }
            return _oOriginInfos;
        }
        public OriginInfo Get(int nID, string sUserID)
        {
            OriginInfo _oOriginInfo = new OriginInfo();
            try
            {
                Connection.Open();
                Command.CommandText = OriginInfoDA.Get(nID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oOriginInfo = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oOriginInfo = new OriginInfo();
                _oOriginInfo.ErrorMessage = e.Message;
            }
            return _oOriginInfo;
        }
        public string Delete(int nOriginInfoID, string sUserID)
        {
            try
            {
                Connection.Open();
                Command.CommandText = OriginInfoDA.Delete(nOriginInfoID, sUserID);
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