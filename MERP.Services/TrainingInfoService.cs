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
    public class TrainingInfoService : CommonGateway, ITrainingInfo
    {
        #region Maping
        private TrainingInfo MapObject(NullHandler oReader)
        {
            TrainingInfo oTrainingInfo = new TrainingInfo();
            oTrainingInfo.TrainingID = oReader.GetInt32("TrainingID");
            oTrainingInfo.TrainingCode = oReader.GetString("TrainingCode");
            oTrainingInfo.TrainingName = oReader.GetString("TrainingName");
            oTrainingInfo.TrainingType = (EnumTrainingType)oReader.GetInt16("TrainingType");
            oTrainingInfo.Description = oReader.GetString("Description");
            oTrainingInfo.DurationInMonth = oReader.GetInt32("DurationInMonth");
            oTrainingInfo.ActivityStatus = (EnumActivityStatus)oReader.GetInt16("ActivityStatus");
            oTrainingInfo.Amount = oReader.GetInt32("Amount");
            return oTrainingInfo;
        }
        private TrainingInfo MakeObject(NullHandler oReader)
        {
            TrainingInfo oTrainingInfo = new TrainingInfo();
            oTrainingInfo = MapObject(oReader);
            return oTrainingInfo;
        }
        private List<TrainingInfo> MakeObjects(IDataReader oReader)
        {
            List<TrainingInfo> oTrainingInfos = new List<TrainingInfo>();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                TrainingInfo oTrainingInfo = MapObject(oHandler);
                oTrainingInfos.Add(oTrainingInfo);
            }
            return oTrainingInfos;
        }
        #endregion


        #region Function Implementation
        public TrainingInfo IUD(TrainingInfo oTrainingInfo, EnumDBOperation oDBOperation, string sUserID)
        {
            TrainingInfo _oTrainingInfo = new TrainingInfo();
            try
            {
                Connection.Open();
                Command.CommandText = TrainingInfoDA.IUD(oTrainingInfo, oDBOperation, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oTrainingInfo = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oTrainingInfo = new TrainingInfo();
                _oTrainingInfo.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oTrainingInfo;
        }
        public List<TrainingInfo> Gets(string sSQL, string sUserID)
        {
            TrainingInfo _oTrainingInfo = new TrainingInfo();
            List<TrainingInfo> _oTrainingInfos = new List<TrainingInfo>();
            try
            {
                Connection.Open();
                Command.CommandText = TrainingInfoDA.Gets(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oTrainingInfos = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oTrainingInfo = new TrainingInfo();
                _oTrainingInfos = new List<TrainingInfo>();
                _oTrainingInfo.ErrorMessage = e.Message.Split('~')[1];
                _oTrainingInfos.Add(_oTrainingInfo);
            }
            return _oTrainingInfos;
        }
        public TrainingInfo Get(int nID, string sUserID)
        {
            TrainingInfo _oTrainingInfo = new TrainingInfo();
            try
            {
                Connection.Open();
                Command.CommandText = TrainingInfoDA.Get(nID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oTrainingInfo = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oTrainingInfo = new TrainingInfo();
                _oTrainingInfo.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oTrainingInfo;
        }
        #endregion
    }
}