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
    public class SegmentInfoService : CommonGateway, ISegmentInfo
    {
        #region Maping
        private SegmentInfo MapObject(NullHandler oReader)
        {
            SegmentInfo oSegmentInfo = new SegmentInfo();
            oSegmentInfo.SegmentID = oReader.GetInt32("SegmentID");
            oSegmentInfo.SegmentCode = oReader.GetString("SegmentCode");
            oSegmentInfo.SegmentName = oReader.GetString("SegmentName");
            oSegmentInfo.IsInActive = oReader.GetBoolean("IsInActive");
            return oSegmentInfo;
        }
        private SegmentInfo MakeObject(NullHandler oReader)
        {
            SegmentInfo oSegmentInfo = new SegmentInfo();
            oSegmentInfo = MapObject(oReader);
            return oSegmentInfo;
        }
        private List<SegmentInfo> MakeObjects(IDataReader oReader)
        {
            List<SegmentInfo> oSegmentInfos = new List<SegmentInfo>();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                SegmentInfo oSegmentInfo = MapObject(oHandler);
                oSegmentInfos.Add(oSegmentInfo);
            }
            return oSegmentInfos;
        }
        #endregion


        #region Function Implementation
        public SegmentInfo IUD(SegmentInfo oSegmentInfo, EnumDBOperation oDBOperation, string sUserID)
        {
            SegmentInfo _oSegmentInfo = new SegmentInfo();
            try
            {
                Connection.Open();
                Command.CommandText = SegmentInfoDA.IUD(oSegmentInfo, oDBOperation, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oSegmentInfo = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oSegmentInfo = new SegmentInfo();
                _oSegmentInfo.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oSegmentInfo;
        }
        public List<SegmentInfo> Gets(string sSQL, string sUserID)
        {
            SegmentInfo _oSegmentInfo = new SegmentInfo();
            List<SegmentInfo> _oSegmentInfos = new List<SegmentInfo>();
            try
            {
                Connection.Open();
                Command.CommandText = SegmentInfoDA.Gets(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oSegmentInfos = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oSegmentInfo = new SegmentInfo();
                _oSegmentInfos = new List<SegmentInfo>();
                _oSegmentInfo.ErrorMessage = e.Message;
                _oSegmentInfos.Add(_oSegmentInfo);
            }
            return _oSegmentInfos;
        }
        public SegmentInfo Get(int nID, string sUserID)
        {
            SegmentInfo _oSegmentInfo = new SegmentInfo();
            try
            {
                Connection.Open();
                Command.CommandText = SegmentInfoDA.Get(nID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oSegmentInfo = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oSegmentInfo = new SegmentInfo();
                _oSegmentInfo.ErrorMessage = e.Message;
            }
            return _oSegmentInfo;
        }
        #endregion
    }
}