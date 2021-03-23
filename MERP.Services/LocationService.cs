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
    public class LocationService : CommonGateway, ILocation
    {
        #region Maping
        private Location MapObject(NullHandler oReader)
        {
            Location oLocation = new Location();
            oLocation.LocationID = oReader.GetInt32("LocationID");
            oLocation.LocationName = oReader.GetString("LocationName");
            oLocation.DistrictID = oReader.GetInt32("DistrictID");
            oLocation.DistrictName = oReader.GetString("DistrictName");
            oLocation.DivisionID = (EnumDivision)oReader.GetInt16("DivisionID");
            oLocation.ActivityStatus = (EnumActivityStatus)oReader.GetInt16("ActivityStatus");
            oLocation.Remarks = oReader.GetString("Remarks");
            return oLocation;
        }
        private Location MakeObject(NullHandler oReader)
        {
            Location oLocation = new Location();
            oLocation = MapObject(oReader);
            return oLocation;
        }
        private List<Location> MakeObjects(IDataReader oReader)
        {
            List<Location> oLocations = new List<Location>();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                Location oLocation = MapObject(oHandler);
                oLocations.Add(oLocation);
            }
            return oLocations;
        }
        #endregion


        #region Function Implementation
        public Location IUD(Location oLocation, EnumDBOperation oDBOperation, string sUserID)
        {
            Location _oLocation = new Location();
            try
            {
                Connection.Open();
                Command.CommandText = LocationDA.IUD(oLocation, oDBOperation, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oLocation = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oLocation = new Location();
                _oLocation.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oLocation;
        }
        public List<Location> Gets(string sSQL, string sUserID)
        {
            Location _oLocation = new Location();
            List<Location> _oLocations = new List<Location>();
            try
            {
                Connection.Open();
                Command.CommandText = LocationDA.Gets(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oLocations = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oLocation = new Location();
                _oLocations = new List<Location>();
                _oLocation.ErrorMessage = e.Message.Split('~')[1];
                _oLocations.Add(_oLocation);
            }
            return _oLocations;
        }
        public Location Get(int nID, string sUserID)
        {
            Location _oLocation = new Location();
            try
            {
                Connection.Open();
                Command.CommandText = LocationDA.Get(nID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oLocation = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oLocation = new Location();
                _oLocation.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oLocation;
        }
        public string Delete(int nLocationID, string sUserID)
        {
            try
            {
                Connection.Open();
                Command.CommandText = LocationDA.Delete(nLocationID, sUserID);
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