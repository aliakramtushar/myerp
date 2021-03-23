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
    public class CountryService : CommonGateway, ICountry
    {
        #region Maping
        private Country MapObject(NullHandler oReader)
        {
            Country oCountry = new Country();
            oCountry.CountryID = oReader.GetInt32("CountryID");
            oCountry.CountryName = oReader.GetString("CountryName");
            oCountry.ContinentID = (EnumContinent)oReader.GetInt16("ContinentID");
            oCountry.ActivityStatus = (EnumActivityStatus)oReader.GetInt16("ActivityStatus");
            oCountry.Remarks = oReader.GetString("Remarks");

            return oCountry;
        }
        private Country MakeObject(NullHandler oReader)
        {
            Country oCountry = new Country();
            oCountry = MapObject(oReader);
            return oCountry;
        }
        private List<Country> MakeObjects(IDataReader oReader)
        {
            List<Country> oCountrys = new List<Country>();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                Country oCountry = MapObject(oHandler);
                oCountrys.Add(oCountry);
            }
            return oCountrys;
        }
        #endregion


        #region Function Implementation
        public Country IUD(Country oCountry, EnumDBOperation oDBOperation, string sUserID)
        {
            Country _oCountry = new Country();
            try
            {
                Connection.Open();
                Command.CommandText = CountryDA.IUD(oCountry, oDBOperation, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oCountry = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oCountry = new Country();
                _oCountry.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oCountry;
        }
        public List<Country> Gets(string sSQL, string sUserID)
        {
            Country _oCountry = new Country();
            List<Country> _oCountrys = new List<Country>();
            try
            {
                Connection.Open();
                Command.CommandText = CountryDA.Gets(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oCountrys = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oCountry = new Country();
                _oCountrys = new List<Country>();
                _oCountry.ErrorMessage = e.Message.Split('~')[1];
                _oCountrys.Add(_oCountry);
            }
            return _oCountrys;
        }
        public Country Get(int nID, string sUserID)
        {
            Country _oCountry = new Country();
            try
            {
                Connection.Open();
                Command.CommandText = CountryDA.Get(nID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oCountry = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oCountry = new Country();
                _oCountry.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oCountry;
        }
        public string Delete(int nCountryID, string sUserID)
        {
            try
            {
                Connection.Open();
                Command.CommandText = CountryDA.Delete(nCountryID, sUserID);
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