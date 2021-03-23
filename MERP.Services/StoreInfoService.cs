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
    public class StoreInfoService : CommonGateway, IStoreInfo
    {
        #region Maping
        private StoreInfo MapObject(NullHandler oReader)
        {
            StoreInfo oStoreInfo = new StoreInfo();
            oStoreInfo.StoreID = oReader.GetInt32("StoreID");
            oStoreInfo.StoreName = oReader.GetString("StoreName");
            oStoreInfo.ShortName = oReader.GetString("ShortName");
            oStoreInfo.StoreCode = oReader.GetString("StoreCode");
            oStoreInfo.Address = oReader.GetString("Address");
            oStoreInfo.IsInActive = oReader.GetBoolean("IsInActive");
            return oStoreInfo;
        }
        private StoreInfo MakeObject(NullHandler oReader)
        {
            StoreInfo oStoreInfo = new StoreInfo();
            oStoreInfo = MapObject(oReader);
            return oStoreInfo;
        }
        private List<StoreInfo> MakeObjects(IDataReader oReader)
        {
            List<StoreInfo> oStoreInfos = new List<StoreInfo>();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                StoreInfo oStoreInfo = MapObject(oHandler);
                oStoreInfos.Add(oStoreInfo);
            }
            return oStoreInfos;
        }
        #endregion


        #region Function Implementation
        public StoreInfo IUD(StoreInfo oStoreInfo, EnumDBOperation oDBOperation, string sUserID)
        {
            StoreInfo _oStoreInfo = new StoreInfo();
            try
            {
                Connection.Open();
                Command.CommandText = StoreInfoDA.IUD(oStoreInfo, oDBOperation, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oStoreInfo = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oStoreInfo = new StoreInfo();
                _oStoreInfo.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oStoreInfo;
        }
        public List<StoreInfo> Gets(string sSQL, string sUserID)
        {
            StoreInfo _oStoreInfo = new StoreInfo();
            List<StoreInfo> _oStoreInfos = new List<StoreInfo>();
            try
            {
                Connection.Open();
                Command.CommandText = StoreInfoDA.Gets(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oStoreInfos = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oStoreInfo = new StoreInfo();
                _oStoreInfos = new List<StoreInfo>();
                _oStoreInfo.ErrorMessage = e.Message;
                _oStoreInfos.Add(_oStoreInfo);
            }
            return _oStoreInfos;
        }
        public StoreInfo Get(int nID, string sUserID)
        {
            StoreInfo _oStoreInfo = new StoreInfo();
            try
            {
                Connection.Open();
                Command.CommandText = StoreInfoDA.Get(nID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oStoreInfo = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oStoreInfo = new StoreInfo();
                _oStoreInfo.ErrorMessage = e.Message;
            }
            return _oStoreInfo;
        }
        #endregion
    }
}