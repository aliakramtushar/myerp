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
    public class BankDepositMasterService : CommonGateway, IBankDepositMaster
    {
        #region Maping
        private BankDepositMaster MapObject(NullHandler oReader)
        {
            BankDepositMaster oBankDepositMaster = new BankDepositMaster();
            oBankDepositMaster.BankDepositMasterID = oReader.GetInt32("BankDepositMasterID");
            oBankDepositMaster.BankAccountMappingID = oReader.GetInt32("BankAccountMappingID");
            oBankDepositMaster.BranchID = oReader.GetInt32("BranchID");
            oBankDepositMaster.DepositDate = oReader.GetDateTime("DepositDate");
            oBankDepositMaster.TotalDepositAmount = oReader.GetDouble("TotalDepositAmount");
            oBankDepositMaster.RefNo = oReader.GetString("RefNo");
            oBankDepositMaster.PaymentMedia = (EnumPaymentMedia)oReader.GetInt16("PaymentMedia");
            oBankDepositMaster.Remarks = oReader.GetString("Remarks");
            oBankDepositMaster.Status = (EnumStatus)oReader.GetInt16("Status");
            oBankDepositMaster.BankID = oReader.GetInt32("BankID");
            oBankDepositMaster.BankName = oReader.GetString("BankName");
            oBankDepositMaster.BankBranchName = oReader.GetString("BankBranchName");
            oBankDepositMaster.BankAccountName = oReader.GetString("BankAccountName");
            oBankDepositMaster.BankAccountNo = oReader.GetString("BankAccountNo");
            oBankDepositMaster.ApprovedBy = oReader.GetString("ApprovedBy");
            oBankDepositMaster.ApprovedByDate = oReader.GetDateTime("ApprovedByDate");
            return oBankDepositMaster;
        }
        private BankDepositMaster MakeObject(NullHandler oReader)
        {
            BankDepositMaster oBankDepositMaster = new BankDepositMaster();
            oBankDepositMaster = MapObject(oReader);
            return oBankDepositMaster;
        }
        private List<BankDepositMaster> MakeObjects(IDataReader oReader)
        {
            List<BankDepositMaster> oBankDepositMasters = new List<BankDepositMaster>();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                BankDepositMaster oBankDepositMaster = MapObject(oHandler);
                oBankDepositMasters.Add(oBankDepositMaster);
            }
            return oBankDepositMasters;
        }
        #endregion


        #region Function Implementation
        public BankDepositMaster IUD(BankDepositMaster oBankDepositMaster, EnumDBOperation oDBOperation, string sUserID)
        {
            BankDepositMaster _oBankDepositMaster = new BankDepositMaster();
            try
            {
                Connection.Open();
                oBankDepositMaster.TotalDepositAmount = oBankDepositMaster.BankDepositDetails.Sum(x => x.DepositAmount);
                Command.CommandText = BankDepositMasterDA.IUD(oBankDepositMaster, oDBOperation, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oBankDepositMaster = MakeObject(oReader);
                }
                reader.Close();
                #region Child Update
                if (oDBOperation == EnumDBOperation.Insert || oDBOperation == EnumDBOperation.Update)
                {
                    
                    BankDepositDetail oBankDepositDetail = new BankDepositDetail();
                    List<BankDepositDetail> oBankDepositDetails = new List<BankDepositDetail>();
                    BankDepositDetailService oBankDepositDetailService = new BankDepositDetailService();
                    string sBankDepositDetailIDs = "";
                    for (int i = 0; i < oBankDepositMaster.BankDepositDetails.Count(); i++)
                    {
                        oBankDepositMaster.BankDepositDetails[i].BankDepositMasterID = _oBankDepositMaster.BankDepositMasterID;
                        if (oBankDepositMaster.BankDepositDetails[i].BankDepositDetailID == 0)
                        {
                            oBankDepositDetail = oBankDepositDetailService.IUD(oBankDepositMaster.BankDepositDetails[i], EnumDBOperation.Insert, sUserID);
                        }
                        else
                        {
                            oBankDepositDetail = oBankDepositDetailService.IUD(oBankDepositMaster.BankDepositDetails[i], EnumDBOperation.Update, sUserID);
                        }
                        oBankDepositDetails.Add(oBankDepositDetail);
                        sBankDepositDetailIDs = sBankDepositDetailIDs + oBankDepositDetail.BankDepositDetailID + ",";
                    }
                    oBankDepositDetailService.DeleteByIDs(_oBankDepositMaster.BankDepositMasterID, sBankDepositDetailIDs.Remove(sBankDepositDetailIDs.Length - 1, 1), sUserID);
                    _oBankDepositMaster.BankDepositDetails = oBankDepositDetails;
                }
                #endregion
                Connection.Close();
            }
            catch (Exception e)
            {
                _oBankDepositMaster = new BankDepositMaster();
                _oBankDepositMaster.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oBankDepositMaster;
        }
        public List<BankDepositMaster> Gets(string sSQL, string sUserID)
        {
            BankDepositMaster _oBankDepositMaster = new BankDepositMaster();
            List<BankDepositMaster> _oBankDepositMasters = new List<BankDepositMaster>();
            try
            {
                Connection.Open();
                Command.CommandText = BankDepositMasterDA.Gets(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oBankDepositMasters = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oBankDepositMaster = new BankDepositMaster();
                _oBankDepositMasters = new List<BankDepositMaster>();
                _oBankDepositMaster.ErrorMessage = e.Message.Split('~')[1];
                _oBankDepositMasters.Add(_oBankDepositMaster);
            }
            return _oBankDepositMasters;
        }
        public BankDepositMaster Get(int nID, string sUserID)
        {
            BankDepositMaster _oBankDepositMaster = new BankDepositMaster();
            try
            {
                Connection.Open();
                Command.CommandText = BankDepositMasterDA.Get(nID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oBankDepositMaster = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oBankDepositMaster = new BankDepositMaster();
                _oBankDepositMaster.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oBankDepositMaster;
        }
        public string Delete(int nBankDepositMasterID, string sUserID)
        {
            try
            {
                BankDepositMaster oBankDepositMaster = new BankDepositMaster();
                oBankDepositMaster.BankDepositMasterID = nBankDepositMasterID;
                Connection.Open();
                Command.CommandText = BankDepositMasterDA.IUD(oBankDepositMaster, EnumDBOperation.Delete, sUserID);
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