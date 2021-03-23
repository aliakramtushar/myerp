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
    public class DistrictService : CommonGateway, IDistrict
    {
        #region Maping
        private District MapObject(NullHandler oReader)
        {
            District oDistrict = new District();
            oDistrict.DistrictID = oReader.GetInt32("DistrictID");
            oDistrict.DistrictName = oReader.GetString("DistrictName");
            oDistrict.DivisionID = (EnumDivision)oReader.GetInt16("DivisionID");
            oDistrict.ActivityStatus = (EnumActivityStatus)oReader.GetInt16("ActivityStatus");
            oDistrict.Remarks = oReader.GetString("Remarks");

            return oDistrict;
        }
        private District MakeObject(NullHandler oReader)
        {
            District oDistrict = new District();
            oDistrict = MapObject(oReader);
            return oDistrict;
        }
        private List<District> MakeObjects(IDataReader oReader)
        {
            List<District> oDistricts = new List<District>();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                District oDistrict = MapObject(oHandler);
                oDistricts.Add(oDistrict);
            }
            return oDistricts;
        }
        #endregion


        #region Function Implementation
        public District IUD(District oDistrict, EnumDBOperation oDBOperation, string sUserID)
        {
            District _oDistrict = new District();
            try
            {
                Connection.Open();
                Command.CommandText = DistrictDA.IUD(oDistrict, oDBOperation, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oDistrict = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oDistrict = new District();
                _oDistrict.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oDistrict;
        }
        public List<District> Gets(string sSQL, string sUserID)
        {
            District _oDistrict = new District();
            List<District> _oDistricts = new List<District>();
            try
            {
                Connection.Open();
                Command.CommandText = DistrictDA.Gets(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oDistricts = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oDistrict = new District();
                _oDistricts = new List<District>();
                _oDistrict.ErrorMessage = e.Message.Split('~')[1];
                _oDistricts.Add(_oDistrict);
            }
            return _oDistricts;
        }
        public District Get(int nID, string sUserID)
        {
            District _oDistrict = new District();
            try
            {
                Connection.Open();
                Command.CommandText = DistrictDA.Get(nID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oDistrict = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oDistrict = new District();
                _oDistrict.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oDistrict;
        }
        public string Delete(int nDistrictID, string sUserID)
        {
            try
            {
                Connection.Open();
                Command.CommandText = DistrictDA.Delete(nDistrictID, sUserID);
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