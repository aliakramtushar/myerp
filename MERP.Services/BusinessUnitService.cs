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
    public class BusinessUnitService : CommonGateway, IBusinessUnit
    {
        #region Maping
        private BusinessUnit MapObject(NullHandler oReader)
        {
            BusinessUnit oBusinessUnit = new BusinessUnit();
            oBusinessUnit.BusinessUnitID = oReader.GetInt32("BusinessUnitID");
            oBusinessUnit.BusinessUnitName = oReader.GetString("BusinessUnitName");
            oBusinessUnit.CompanyID = oReader.GetInt32("CompanyID");
            oBusinessUnit.CompanyName = oReader.GetString("CompanyName");
            oBusinessUnit.BusinessOwnerName = oReader.GetString("BusinessOwnerName");
            oBusinessUnit.IsInActive = oReader.GetBoolean("IsInActive");
            oBusinessUnit.IsAuto = oReader.GetBoolean("IsAuto");
            oBusinessUnit.IsManual = oReader.GetBoolean("IsManual");
            oBusinessUnit.Remarks = oReader.GetString("Remarks");
            return oBusinessUnit;
        }
        private BusinessUnit MakeObject(NullHandler oReader)
        {
            BusinessUnit oBusinessUnit = new BusinessUnit();
            oBusinessUnit = MapObject(oReader);
            return oBusinessUnit;
        }
        private List<BusinessUnit> MakeObjects(IDataReader oReader)
        {
            List<BusinessUnit> oBusinessUnits = new List<BusinessUnit>();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                BusinessUnit oBusinessUnit = MapObject(oHandler);
                oBusinessUnits.Add(oBusinessUnit);
            }
            return oBusinessUnits;
        }
        #endregion

        #region Function Implementation
        public BusinessUnit IUD(BusinessUnit oBusinessUnit, EnumDBOperation oDBOperation, string sUserID)
        {
            BusinessUnit _oBusinessUnit = new BusinessUnit();
            try
            {
                Connection.Open();
                Command.CommandText = BusinessUnitDA.IUD(oBusinessUnit, oDBOperation, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oBusinessUnit = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oBusinessUnit = new BusinessUnit();
                _oBusinessUnit.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oBusinessUnit;
        }
        public List<BusinessUnit> Gets(string sSQL, string sUserID)
        {
            BusinessUnit _oBusinessUnit = new BusinessUnit();
            List<BusinessUnit> _oBusinessUnits = new List<BusinessUnit>();
            try
            {
                Connection.Open();
                Command.CommandText = BusinessUnitDA.Gets(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oBusinessUnits = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oBusinessUnit = new BusinessUnit();
                _oBusinessUnits = new List<BusinessUnit>();
                _oBusinessUnit.ErrorMessage = e.Message.Split('~')[1];
                _oBusinessUnits.Add(_oBusinessUnit);
            }
            return _oBusinessUnits;
        }
        public BusinessUnit Get(int nID, string sUserID)
        {
            BusinessUnit _oBusinessUnit = new BusinessUnit();
            try
            {
                Connection.Open();
                Command.CommandText = BusinessUnitDA.Get(nID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oBusinessUnit = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oBusinessUnit = new BusinessUnit();
                _oBusinessUnit.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oBusinessUnit;
        }
        #endregion
    }
}