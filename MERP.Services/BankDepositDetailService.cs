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
    public class BankDepositDetailService : CommonGateway, IBankDepositDetail
    {
        #region Maping
        private BankDepositDetail MapObject(NullHandler oReader)
        {
            BankDepositDetail oBankDepositDetail = new BankDepositDetail();
            oBankDepositDetail.BankDepositDetailID = oReader.GetInt32("BankDepositDetailID");
            oBankDepositDetail.BankDepositMasterID = oReader.GetInt32("BankDepositMasterID");
            oBankDepositDetail.BusinessUnitID = oReader.GetInt32("BusinessUnitID");
            oBankDepositDetail.DepositAmount = oReader.GetDouble("DepositAmount");
            oBankDepositDetail.BusinessUnitName = oReader.GetString("BusinessUnitName");
            oBankDepositDetail.Remarks = oReader.GetString("Remarks");
            return oBankDepositDetail;
        }
        private BankDepositDetail MakeObject(NullHandler oReader)
        {
            BankDepositDetail oBankDepositDetail = new BankDepositDetail();
            oBankDepositDetail = MapObject(oReader);
            return oBankDepositDetail;
        }
        private List<BankDepositDetail> MakeObjects(IDataReader oReader)
        {
            List<BankDepositDetail> oBankDepositDetails = new List<BankDepositDetail>();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                BankDepositDetail oBankDepositDetail = MapObject(oHandler);
                oBankDepositDetails.Add(oBankDepositDetail);
            }
            return oBankDepositDetails;
        }
        #endregion


        #region Function Implementation
        public BankDepositDetail IUD(BankDepositDetail oBankDepositDetail, EnumDBOperation oDBOperation, string sUserID)
        {
            BankDepositDetail _oBankDepositDetail = new BankDepositDetail();
            try
            {
                Connection.Open();
                Command.CommandText = BankDepositDetailDA.IUD(oBankDepositDetail, oDBOperation, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oBankDepositDetail = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oBankDepositDetail = new BankDepositDetail();
                _oBankDepositDetail.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oBankDepositDetail;
        }
        public List<BankDepositDetail> Gets(string sSQL, string sUserID)
        {
            BankDepositDetail _oBankDepositDetail = new BankDepositDetail();
            List<BankDepositDetail> _oBankDepositDetails = new List<BankDepositDetail>();
            try
            {
                Connection.Open();
                Command.CommandText = BankDepositDetailDA.Gets(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oBankDepositDetails = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oBankDepositDetail = new BankDepositDetail();
                _oBankDepositDetails = new List<BankDepositDetail>();
                _oBankDepositDetail.ErrorMessage = e.Message.Split('~')[1];
                _oBankDepositDetails.Add(_oBankDepositDetail);
            }
            return _oBankDepositDetails;
        }
        public BankDepositDetail Get(int nID, string sUserID)
        {
            BankDepositDetail _oBankDepositDetail = new BankDepositDetail();
            try
            {
                Connection.Open();
                Command.CommandText = BankDepositDetailDA.Get(nID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oBankDepositDetail = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oBankDepositDetail = new BankDepositDetail();
                _oBankDepositDetail.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oBankDepositDetail;
        }
        public List<BankDepositDetail> GetsByParentID(int nParentID, string sUserID)
        {
            BankDepositDetail _oBankDepositDetail = new BankDepositDetail();
            List<BankDepositDetail> _oBankDepositDetails = new List<BankDepositDetail>();
            try
            {
                Connection.Open();
                Command.CommandText = BankDepositDetailDA.GetsByParentID(nParentID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oBankDepositDetails = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oBankDepositDetail = new BankDepositDetail();
                _oBankDepositDetails = new List<BankDepositDetail>();
                _oBankDepositDetail.ErrorMessage = e.Message.Split('~')[1];
                _oBankDepositDetails.Add(_oBankDepositDetail);
            }
            return _oBankDepositDetails;
        }
        public string Delete(int nBankDepositDetailID, string sUserID)
        {
            try
            {
                Connection.Open();
                Command.CommandText = BankDepositDetailDA.Delete(nBankDepositDetailID, sUserID);
                Command.ExecuteNonQuery();
                Connection.Close();
            }
            catch (Exception e)
            {
                FeedbackMessage.Deleted = e.Message.Split('~')[0];
            }
            return FeedbackMessage.Deleted;
        }
        public string DeleteByIDs(int nBankDepositMasterID, string nBankDepositDetailIDs, string sUserID)
        {
            try
            {
                Connection.Open();
                Command.CommandText = BankDepositDetailDA.DeleteByIDs(nBankDepositMasterID, nBankDepositDetailIDs, sUserID);
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