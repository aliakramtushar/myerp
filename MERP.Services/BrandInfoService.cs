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
    public class BrandInfoService : CommonGateway, IBrandInfo
    {
        #region Maping
        private BrandInfo MapObject(NullHandler oReader)
        {
            BrandInfo oBrandInfo = new BrandInfo();
            oBrandInfo.BrandID = oReader.GetInt32("BrandID");
            oBrandInfo.BrandCode = oReader.GetString("BrandCode");
            oBrandInfo.BrandName = oReader.GetString("BrandName");
            oBrandInfo.IsInActive = oReader.GetBoolean("IsInActive");
            oBrandInfo.OriginID = oReader.GetInt32("OriginID");
            oBrandInfo.OriginName = oReader.GetString("OriginName");
            return oBrandInfo;
        }
        private BrandInfo MakeObject(NullHandler oReader)
        {
            BrandInfo oBrandInfo = new BrandInfo();
            oBrandInfo = MapObject(oReader);
            return oBrandInfo;
        }
        private List<BrandInfo> MakeObjects(IDataReader oReader)
        {
            List<BrandInfo> oBrandInfos = new List<BrandInfo>();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                BrandInfo oBrandInfo = MapObject(oHandler);
                oBrandInfos.Add(oBrandInfo);
            }
            return oBrandInfos;
        }
        #endregion


        #region Function Implementation
        public BrandInfo IUD(BrandInfo oBrandInfo, EnumDBOperation oDBOperation, string sUserID)
        {
            BrandInfo _oBrandInfo = new BrandInfo();
            try
            {
                Connection.Open();
                Command.CommandText = BrandInfoDA.IUD(oBrandInfo, oDBOperation, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oBrandInfo = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oBrandInfo = new BrandInfo();
                _oBrandInfo.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oBrandInfo;
        }
        public List<BrandInfo> Gets(string sSQL, string sUserID)
        {
            BrandInfo _oBrandInfo = new BrandInfo();
            List<BrandInfo> _oBrandInfos = new List<BrandInfo>();
            try
            {
                Connection.Open();
                Command.CommandText = BrandInfoDA.Gets(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oBrandInfos = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oBrandInfo = new BrandInfo();
                _oBrandInfos = new List<BrandInfo>();
                _oBrandInfo.ErrorMessage = e.Message;
                _oBrandInfos.Add(_oBrandInfo);
            }
            return _oBrandInfos;
        }
        public BrandInfo Get(int nID, string sUserID)
        {
            BrandInfo _oBrandInfo = new BrandInfo();
            try
            {
                Connection.Open();
                Command.CommandText = BrandInfoDA.Get(nID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oBrandInfo = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oBrandInfo = new BrandInfo();
                _oBrandInfo.ErrorMessage = e.Message;
            }
            return _oBrandInfo;
        }
        public string Delete(int nBrandInfoID, string sUserID)
        {
            try
            {
                Connection.Open();
                Command.CommandText = BrandInfoDA.Delete(nBrandInfoID, sUserID);
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