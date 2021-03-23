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
    public class BankService : CommonGateway, IBank
    {
        #region Maping
        private Bank MapObject(NullHandler oReader)
        {
            Bank oBank = new Bank();
            oBank.BankID = oReader.GetInt32("BankID");
            oBank.BankName = oReader.GetString("BankName");
            oBank.BankAddress = oReader.GetString("BankAddress");
            oBank.IsInActive = oReader.GetInt32("IsInActive");
            oBank.Remarks = oReader.GetString("Remarks");

            return oBank;
        }
        private Bank MakeObject(NullHandler oReader)
        {
            Bank oBank = new Bank();
            oBank = MapObject(oReader);
            return oBank;
        }
        private List<Bank> MakeObjects(IDataReader oReader)
        {
            List<Bank> oBanks = new List<Bank>();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                Bank oBank = MapObject(oHandler);
                oBanks.Add(oBank);
            }
            return oBanks;
        }
        #endregion


        #region Function Implementation
        public Bank IUD(Bank oBank, EnumDBOperation oDBOperation, string sUserID)
        {
            Bank _oBank = new Bank();
            try
            {
                Connection.Open();
                Command.CommandText = BankDA.IUD(oBank, oDBOperation, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oBank = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oBank = new Bank();
                _oBank.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oBank;
        }
        public List<Bank> Gets(string sSQL, string sUserID)
        {
            Bank _oBank = new Bank();
            List<Bank> _oBanks = new List<Bank>();
            try
            {
                Connection.Open();
                Command.CommandText = BankDA.Gets(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oBanks = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oBank = new Bank();
                _oBanks = new List<Bank>();
                _oBank.ErrorMessage = e.Message.Split('~')[1];
                _oBanks.Add(_oBank);
            }
            return _oBanks;
        }
        public Bank Get(int nID, string sUserID)
        {
            Bank _oBank = new Bank();
            try
            {
                Connection.Open();
                Command.CommandText = BankDA.Get(nID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oBank = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oBank = new Bank();
                _oBank.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oBank;
        }
        public string Delete(int nBankID, string sUserID)
        {
            try
            {
                Connection.Open();
                Command.CommandText = BankDA.Delete(nBankID, sUserID);
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