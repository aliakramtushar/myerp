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
    public class CostCenterService : CommonGateway, ICostCenter
    {
        #region Maping
        private CostCenter MapObject(NullHandler oReader)
        {
            CostCenter oCostCenter = new CostCenter();
            oCostCenter.CostCenterID = oReader.GetInt32("CostCenterID");
            oCostCenter.CostCenterName = oReader.GetString("CostCenterName");
            oCostCenter.ActivityStatus = (EnumActivityStatus)oReader.GetInt16("ActivityStatus");
            return oCostCenter;
        }
        private CostCenter MakeObject(NullHandler oReader)
        {
            CostCenter oCostCenter = new CostCenter();
            oCostCenter = MapObject(oReader);
            return oCostCenter;
        }
        private List<CostCenter> MakeObjects(IDataReader oReader)
        {
            List<CostCenter> oCostCenters = new List<CostCenter>();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                CostCenter oCostCenter = MapObject(oHandler);
                oCostCenters.Add(oCostCenter);
            }
            return oCostCenters;
        }
        #endregion


        #region Function Implementation
        public CostCenter IUD(CostCenter oCostCenter, EnumDBOperation oDBOperation, string sUserID)
        {
            CostCenter _oCostCenter = new CostCenter();
            try
            {
                Connection.Open();
                Command.CommandText = CostCenterDA.IUD(oCostCenter, oDBOperation, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oCostCenter = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oCostCenter = new CostCenter();
                _oCostCenter.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oCostCenter;
        }
        public List<CostCenter> Gets(string sSQL, string sUserID)
        {
            CostCenter _oCostCenter = new CostCenter();
            List<CostCenter> _oCostCenters = new List<CostCenter>();
            try
            {
                Connection.Open();
                Command.CommandText = CostCenterDA.Gets(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oCostCenters = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oCostCenter = new CostCenter();
                _oCostCenters = new List<CostCenter>();
                _oCostCenter.ErrorMessage = e.Message.Split('~')[1];
                _oCostCenters.Add(_oCostCenter);
            }
            return _oCostCenters;
        }
        public CostCenter Get(int nID, string sUserID)
        {
            CostCenter _oCostCenter = new CostCenter();
            try
            {
                Connection.Open();
                Command.CommandText = CostCenterDA.Get(nID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oCostCenter = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oCostCenter = new CostCenter();
                _oCostCenter.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oCostCenter;
        }
        #endregion
    }
}