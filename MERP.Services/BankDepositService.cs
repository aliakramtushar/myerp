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
    public class BankDepositService : CommonGateway, IBankDeposit
    {
        #region Maping
        private BankDeposit MapObject(NullHandler oReader)
        {
            BankDeposit oBankDeposit = new BankDeposit();
            oBankDeposit.BankDepositID = oReader.GetInt32("BankDepositID");
            oBankDeposit.DepositCode = oReader.GetString("DepositCode");
            oBankDeposit.BankAccountMappingID = oReader.GetInt32("BankAccountMappingID");
            oBankDeposit.BranchID = oReader.GetInt32("BranchID");
            oBankDeposit.DepositDate = oReader.GetDateTime("DepositDate");
            oBankDeposit.TotalDepositAmount = oReader.GetDouble("TotalDepositAmount");
            oBankDeposit.RefNo = oReader.GetString("RefNo");
            oBankDeposit.PaymentMedia = (EnumPaymentMedia)oReader.GetInt16("PaymentMedia");
            oBankDeposit.Remarks = oReader.GetString("Remarks");
            oBankDeposit.Status = (EnumStatus)oReader.GetInt16("Status");
            oBankDeposit.BankID = oReader.GetInt32("BankID");
            oBankDeposit.BankName = oReader.GetString("BankName");
            oBankDeposit.BankAccountName = oReader.GetString("BankAccountName");
            oBankDeposit.BankAccountNo = oReader.GetString("BankAccountNo");
            oBankDeposit.ApprovedBy = oReader.GetString("ApprovedBy");
            oBankDeposit.BranchName = oReader.GetString("BranchName");
            oBankDeposit.ApprovedByDate = oReader.GetDateTime("ApprovedByDate");
            return oBankDeposit;
        }
        private BankDeposit MakeObject(NullHandler oReader)
        {
            BankDeposit oBankDeposit = new BankDeposit();
            oBankDeposit = MapObject(oReader);
            return oBankDeposit;
        }
        private List<BankDeposit> MakeObjects(IDataReader oReader)
        {
            List<BankDeposit> oBankDeposits = new List<BankDeposit>();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                BankDeposit oBankDeposit = MapObject(oHandler);
                oBankDeposits.Add(oBankDeposit);
            }
            return oBankDeposits;
        }
        #endregion


        #region Function Implementation

        //public BankDeposit IUD(BankDeposit oBankDeposit, EnumDBOperation oDBOperation, string sUserID)
        //{
        //    BankDeposit _oBankDeposit = new BankDeposit();
        //    try
        //    {
        //        Connection.Open();
        //        oBankDeposit.TotalDepositAmount = oBankDeposit.NWRDirectSalesDetails.Sum(x => x.CollectedAmount);
        //        Command.CommandText = BankDepositDA.IUD(oBankDeposit, oDBOperation, sUserID);
        //        IDataReader reader = Command.ExecuteReader();
        //        NullHandler oReader = new NullHandler(reader);
        //        if (reader.Read())
        //        {
        //            _oBankDeposit = MakeObject(oReader);
        //        }
        //        reader.Close();
        //        Connection.Close();
        //        #region Child Update
        //        if (oDBOperation == EnumDBOperation.Insert || oDBOperation == EnumDBOperation.Update)
        //        {
        //            NWRDirectSalesDetail oNWRDirectSalesDetail = new NWRDirectSalesDetail();
        //            List<NWRDirectSalesDetail> oNWRDirectSalesDetails = new List<NWRDirectSalesDetail>();
        //            NWRDirectSalesDetailService oNWRDirectSalesDetailService = new NWRDirectSalesDetailService();
        //            string sNWRDirectSalesDetailIDs = "";
        //            for (int i = 0; i < oBankDeposit.NWRDirectSalesDetails.Count(); i++)
        //            {
        //                oBankDeposit.NWRDirectSalesDetails[i].BankDepositID = _oBankDeposit.BankDepositID;
        //                oBankDeposit.NWRDirectSalesDetails[i].AccountMappingID = _oBankDeposit.BankAccountMappingID;
        //                oBankDeposit.NWRDirectSalesDetails[i].DepositDate = _oBankDeposit.DepositDate;
        //                oNWRDirectSalesDetail = oNWRDirectSalesDetailService.IUD(oBankDeposit.NWRDirectSalesDetails[i], EnumDBOperation.SpecialCase, sUserID);
        //                oNWRDirectSalesDetails.Add(oNWRDirectSalesDetail);
        //                sNWRDirectSalesDetailIDs = sNWRDirectSalesDetailIDs + oNWRDirectSalesDetail.RID + ",";
        //            }
        //            oNWRDirectSalesDetailService.DeleteByIDs(_oBankDeposit.BankDepositID, sNWRDirectSalesDetailIDs.Remove(sNWRDirectSalesDetailIDs.Length - 1, 1), sUserID);
        //            _oBankDeposit.NWRDirectSalesDetails = oNWRDirectSalesDetails;
        //        }
        //        #endregion

        //    }
        //    catch (Exception e)
        //    {
        //        _oBankDeposit = new BankDeposit();
        //        _oBankDeposit.ErrorMessage = e.Message.Split('~')[1];
        //    }
        //    return _oBankDeposit;
        //}
        public BankDeposit IUD(BankDeposit oBankDeposit, EnumDBOperation oDBOperation, string sUserID)
        {
            BankDeposit _oBankDeposit = new BankDeposit();
            try
            {
                Connection.Open();
                //oBankDeposit.TotalDepositAmount = oBankDeposit.NWRDirectSalesDetails.Sum(x => x.CollectedAmount);
                Command.CommandText = BankDepositDA.IUD(oBankDeposit, oDBOperation, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oBankDeposit = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oBankDeposit = new BankDeposit();
                _oBankDeposit.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oBankDeposit;
        }
        public List<BankDeposit> Gets(string sSQL, string sUserID)
        {
            BankDeposit _oBankDeposit = new BankDeposit();
            List<BankDeposit> _oBankDeposits = new List<BankDeposit>();
            try
            {
                Connection.Open();
                Command.CommandText = BankDepositDA.Gets(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oBankDeposits = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oBankDeposit = new BankDeposit();
                _oBankDeposits = new List<BankDeposit>();
                _oBankDeposit.ErrorMessage = e.Message.Split('~')[1];
                _oBankDeposits.Add(_oBankDeposit);
            }
            return _oBankDeposits;
        }
        public List<BankDeposit> Gets(int nBranchID, EnumStatus oEnumStatus, string sUserID)
        {
            BankDeposit _oBankDeposit = new BankDeposit();
            List<BankDeposit> _oBankDeposits = new List<BankDeposit>();
            try
            {
                Connection.Open();
                Command.CommandText = BankDepositDA.Gets(nBranchID, oEnumStatus, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oBankDeposits = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oBankDeposit = new BankDeposit();
                _oBankDeposits = new List<BankDeposit>();
                _oBankDeposit.ErrorMessage = e.Message.Split('~')[1];
                _oBankDeposits.Add(_oBankDeposit);
            }
            return _oBankDeposits;
        }
        public BankDeposit Get(int nID, string sUserID)
        {
            BankDeposit _oBankDeposit = new BankDeposit();
            try
            {
                Connection.Open();
                Command.CommandText = BankDepositDA.Get(nID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oBankDeposit = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oBankDeposit = new BankDeposit();
                _oBankDeposit.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oBankDeposit;
        }
        public string Delete(int nBankDepositID, string sUserID)
        {
            string sFeedbackMessage = FeedbackMessage.Deleted;
            try
            {
                BankDeposit oBankDeposit = new BankDeposit();
                oBankDeposit.BankDepositID = nBankDepositID;
                Connection.Open();
                Command.CommandText = BankDepositDA.IUD(oBankDeposit, EnumDBOperation.Delete, sUserID);
                Command.ExecuteNonQuery();
                Connection.Close();
            }
            catch (Exception e)
            {
                sFeedbackMessage = e.Message;
            }
            return sFeedbackMessage;
        }
        #endregion
    }
}