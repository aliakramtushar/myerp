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
    public class BankAccountMappingService : CommonGateway, IBankAccountMapping
    {
        #region Maping
        private BankAccountMapping MapObject(NullHandler oReader)
        {
            BankAccountMapping oBankAccountMapping = new BankAccountMapping();
            oBankAccountMapping.BankAccountMappingID = oReader.GetInt32("BankAccountMappingID");
            oBankAccountMapping.BankID = oReader.GetInt32("BankID");
            oBankAccountMapping.BankName = oReader.GetString("BankName");
            oBankAccountMapping.BankBranchName = oReader.GetString("BankBranchName");
            oBankAccountMapping.BankAccountName = oReader.GetString("BankAccountName");
            oBankAccountMapping.BankAccountNo = oReader.GetString("BankAccountNo");
            oBankAccountMapping.IsActive = oReader.GetBoolean("IsActive");
            oBankAccountMapping.Remarks = oReader.GetString("Remarks");
            return oBankAccountMapping;
        }
        private BankAccountMapping MakeObject(NullHandler oReader)
        {
            BankAccountMapping oBankAccountMapping = new BankAccountMapping();
            oBankAccountMapping = MapObject(oReader);
            return oBankAccountMapping;
        }
        private List<BankAccountMapping> MakeObjects(IDataReader oReader)
        {
            List<BankAccountMapping> oBankAccountMappings = new List<BankAccountMapping>();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                BankAccountMapping oBankAccountMapping = MapObject(oHandler);
                oBankAccountMappings.Add(oBankAccountMapping);
            }
            return oBankAccountMappings;
        }
        #endregion


        #region Function Implementation
        public BankAccountMapping IUD(BankAccountMapping oBankAccountMapping, EnumDBOperation oDBOperation, string sUserID)
        {
            BankAccountMapping _oBankAccountMapping = new BankAccountMapping();
            try
            {
                Connection.Open();
                Command.CommandText = BankAccountMappingDA.IUD(oBankAccountMapping, oDBOperation, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oBankAccountMapping = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oBankAccountMapping = new BankAccountMapping();
                _oBankAccountMapping.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oBankAccountMapping;
        }
        public List<BankAccountMapping> Gets(string sSQL, string sUserID)
        {
            BankAccountMapping _oBankAccountMapping = new BankAccountMapping();
            List<BankAccountMapping> _oBankAccountMappings = new List<BankAccountMapping>();
            try
            {
                Connection.Open();
                Command.CommandText = BankAccountMappingDA.Gets(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oBankAccountMappings = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oBankAccountMapping = new BankAccountMapping();
                _oBankAccountMappings = new List<BankAccountMapping>();
                _oBankAccountMapping.ErrorMessage = e.Message.Split('~')[1];
                _oBankAccountMappings.Add(_oBankAccountMapping);
            }
            return _oBankAccountMappings;
        }
        public List<BankAccountMapping> GetsActiveBankAccountMappings(string sUserID)
        {
            BankAccountMapping _oBankAccountMapping = new BankAccountMapping();
            List<BankAccountMapping> _oBankAccountMappings = new List<BankAccountMapping>();
            try
            {
                Connection.Open();
                Command.CommandText = BankAccountMappingDA.GetsActiveBankAccountMappings(sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oBankAccountMappings = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oBankAccountMapping = new BankAccountMapping();
                _oBankAccountMappings = new List<BankAccountMapping>();
                _oBankAccountMapping.ErrorMessage = e.Message.Split('~')[1];
                _oBankAccountMappings.Add(_oBankAccountMapping);
            }
            return _oBankAccountMappings;
        }

        
        public BankAccountMapping Get(int nID, string sUserID)
        {
            BankAccountMapping _oBankAccountMapping = new BankAccountMapping();
            try
            {
                Connection.Open();
                Command.CommandText = BankAccountMappingDA.Get(nID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oBankAccountMapping = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oBankAccountMapping = new BankAccountMapping();
                _oBankAccountMapping.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oBankAccountMapping;
        }
        public string Delete(int nBankAccountMappingID, string sUserID)
        {
            try
            {
                Connection.Open();
                Command.CommandText = BankAccountMappingDA.Delete(nBankAccountMappingID, sUserID);
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