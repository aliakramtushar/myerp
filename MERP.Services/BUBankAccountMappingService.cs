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
    public class BUBankAccountMappingService : CommonGateway, IBUBankAccountMapping
    {
        #region Maping
        private BUBankAccountMapping MapObject(NullHandler oReader)
        {
            BUBankAccountMapping oBUBankAccountMapping = new BUBankAccountMapping();
            oBUBankAccountMapping.BUBankAccountMappingID = oReader.GetInt32("BUBankAccountMappingID");
            oBUBankAccountMapping.BusinessUnitID = oReader.GetInt32("BusinessUnitID");
            oBUBankAccountMapping.BankAccountMappingID = oReader.GetInt32("BankAccountMappingID");
            oBUBankAccountMapping.BusinessUnitName = oReader.GetString("BusinessUnitName");
            oBUBankAccountMapping.BankName = oReader.GetString("BankName");
            oBUBankAccountMapping.BankAccountName = oReader.GetString("BankAccountName");
            oBUBankAccountMapping.BankAccountNo = oReader.GetString("BankAccountNo");
            oBUBankAccountMapping.IsActive = oReader.GetBoolean("IsActive");

            return oBUBankAccountMapping;
        }
        private BUBankAccountMapping MakeObject(NullHandler oReader)
        {
            BUBankAccountMapping oBUBankAccountMapping = new BUBankAccountMapping();
            oBUBankAccountMapping = MapObject(oReader);
            return oBUBankAccountMapping;
        }
        private List<BUBankAccountMapping> MakeObjects(IDataReader oReader)
        {
            List<BUBankAccountMapping> oBUBankAccountMappings = new List<BUBankAccountMapping>();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                BUBankAccountMapping oBUBankAccountMapping = MapObject(oHandler);
                oBUBankAccountMappings.Add(oBUBankAccountMapping);
            }
            return oBUBankAccountMappings;
        }
        #endregion


        #region Function Implementation
        public BUBankAccountMapping IUD(BUBankAccountMapping oBUBankAccountMapping, EnumDBOperation oDBOperation, string sUserID)
        {
            BUBankAccountMapping _oBUBankAccountMapping = new BUBankAccountMapping();
            try
            {
                Connection.Open();
                Command.CommandText = BUBankAccountMappingDA.IUD(oBUBankAccountMapping, oDBOperation, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oBUBankAccountMapping = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oBUBankAccountMapping = new BUBankAccountMapping();
                _oBUBankAccountMapping.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oBUBankAccountMapping;
        }
        public List<BUBankAccountMapping> Gets(string sSQL, string sUserID)
        {
            BUBankAccountMapping _oBUBankAccountMapping = new BUBankAccountMapping();
            List<BUBankAccountMapping> _oBUBankAccountMappings = new List<BUBankAccountMapping>();
            try
            {
                Connection.Open();
                Command.CommandText = BUBankAccountMappingDA.Gets(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oBUBankAccountMappings = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oBUBankAccountMapping = new BUBankAccountMapping();
                _oBUBankAccountMappings = new List<BUBankAccountMapping>();
                _oBUBankAccountMapping.ErrorMessage = e.Message.Split('~')[1];
                _oBUBankAccountMappings.Add(_oBUBankAccountMapping);
            }
            return _oBUBankAccountMappings;
        }
        public List<BUBankAccountMapping> GetsByBankAccountMappingID(int nBankAccountMappingID, string sUserID)
        {
            BUBankAccountMapping _oBUBankAccountMapping = new BUBankAccountMapping();
            List<BUBankAccountMapping> _oBUBankAccountMappings = new List<BUBankAccountMapping>();
            try
            {
                Connection.Open();
                Command.CommandText = BUBankAccountMappingDA.GetsByBankAccountMappingID(nBankAccountMappingID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oBUBankAccountMappings = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oBUBankAccountMapping = new BUBankAccountMapping();
                _oBUBankAccountMappings = new List<BUBankAccountMapping>();
                _oBUBankAccountMapping.ErrorMessage = e.Message.Split('~')[1];
                _oBUBankAccountMappings.Add(_oBUBankAccountMapping);
            }
            return _oBUBankAccountMappings;
        }
        public BUBankAccountMapping Get(int nID, string sUserID)
        {
            BUBankAccountMapping _oBUBankAccountMapping = new BUBankAccountMapping();
            try
            {
                Connection.Open();
                Command.CommandText = BUBankAccountMappingDA.Get(nID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oBUBankAccountMapping = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oBUBankAccountMapping = new BUBankAccountMapping();
                _oBUBankAccountMapping.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oBUBankAccountMapping;
        }
        public string Delete(int nBUBankAccountMappingID, string sUserID)
        {
            try
            {
                Connection.Open();
                Command.CommandText = BUBankAccountMappingDA.Delete(nBUBankAccountMappingID, sUserID);
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