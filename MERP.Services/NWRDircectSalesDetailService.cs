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
    public class NWRDirectSalesDetailService : CommonGateway, INWRDirectSalesDetail
    {
        #region Maping
        private NWRDirectSalesDetail MapObject(NullHandler oReader)
        {
            NWRDirectSalesDetail oNWRDirectSalesDetail = new NWRDirectSalesDetail();
            oNWRDirectSalesDetail.RID = oReader.GetInt32("RID");
            oNWRDirectSalesDetail.LSOInvoiceDetailsID = oReader.GetInt32("LSOInvoiceDetailsID");
            oNWRDirectSalesDetail.BranchID = oReader.GetInt32("BranchID");
            oNWRDirectSalesDetail.ProductID = oReader.GetInt32("ProductID");
            oNWRDirectSalesDetail.ShortName = oReader.GetString("ShortName");
            oNWRDirectSalesDetail.SKUID = oReader.GetInt32("SKUID");
            oNWRDirectSalesDetail.LSOCode = oReader.GetString("LSOCode");
            oNWRDirectSalesDetail.IsWarranty = oReader.GetInt32("IsWarranty");
            oNWRDirectSalesDetail.InvoiceNo = oReader.GetString("InvoiceNo");
            oNWRDirectSalesDetail.InvoiceDate = oReader.GetDateTime("InvoiceDate");
            oNWRDirectSalesDetail.SalesCategory = oReader.GetString("SalesCategory");
            oNWRDirectSalesDetail.Unit = oReader.GetInt32("Unit");
            oNWRDirectSalesDetail.SKUPrice = oReader.GetInt32("SKUPrice");
            oNWRDirectSalesDetail.ExchangeReplaceCharge = oReader.GetInt32("ExchangeReplaceCharge");
            oNWRDirectSalesDetail.BackupHSCharge = oReader.GetInt32("BackupHSCharge");
            oNWRDirectSalesDetail.ServiceChargeAmount = oReader.GetInt32("ServiceChargeAmount");
            oNWRDirectSalesDetail.TotalAmount = oReader.GetInt32("TotalAmount");
            oNWRDirectSalesDetail.DiscountedAmount = oReader.GetInt32("DiscountedAmount");
            oNWRDirectSalesDetail.CollectedAmount = oReader.GetInt32("CollectedAmount");
            oNWRDirectSalesDetail.DiscountReasonID = oReader.GetInt32("DiscountReasonID");
            oNWRDirectSalesDetail.RepairType = oReader.GetString("RepairType");
            oNWRDirectSalesDetail.AccountName = oReader.GetString("AccountName");
            oNWRDirectSalesDetail.BusinessUnitID = oReader.GetInt32("BusinessUnitID");
            oNWRDirectSalesDetail.DateAdded = oReader.GetDateTime("DateAdded");
            oNWRDirectSalesDetail.AddedBy = oReader.GetString("AddedBy");
            oNWRDirectSalesDetail.IscashDeposited = oReader.GetInt32("IscashDeposited");
            oNWRDirectSalesDetail.DepositDate = oReader.GetDateTime("DepositDate");
            oNWRDirectSalesDetail.DepositBy = oReader.GetString("DepositBy");
            oNWRDirectSalesDetail.AccountMappingID = oReader.GetInt32("AccountMappingID");
            oNWRDirectSalesDetail.BankDepositID = oReader.GetInt32("BankDepositID");
            oNWRDirectSalesDetail.SKUName = oReader.GetString("SKUName");
            oNWRDirectSalesDetail.BranchName = oReader.GetString("BranchName");
            oNWRDirectSalesDetail.BusinessUnitName = oReader.GetString("BusinessUnitName");

            return oNWRDirectSalesDetail;
        }
        private NWRDirectSalesDetail MakeObject(NullHandler oReader)
        {
            NWRDirectSalesDetail oNWRDirectSalesDetail = new NWRDirectSalesDetail();
            oNWRDirectSalesDetail = MapObject(oReader);
            return oNWRDirectSalesDetail;
        }
        private List<NWRDirectSalesDetail> MakeObjects(IDataReader oReader)
        {
            List<NWRDirectSalesDetail> oNWRDirectSalesDetails = new List<NWRDirectSalesDetail>();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                NWRDirectSalesDetail oNWRDirectSalesDetail = MapObject(oHandler);
                oNWRDirectSalesDetails.Add(oNWRDirectSalesDetail);
            }
            return oNWRDirectSalesDetails;
        }
        #endregion


        #region Function Implementation
        public NWRDirectSalesDetail IUD(NWRDirectSalesDetail oNWRDirectSalesDetail, EnumDBOperation oDBOperation, string sUserID)
        {
            NWRDirectSalesDetail _oNWRDirectSalesDetail = new NWRDirectSalesDetail();
            try
            {
                Connection.Open();
                Command.CommandText = NWRDirectSalesDetailDA.IUD(oNWRDirectSalesDetail, oDBOperation, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oNWRDirectSalesDetail = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oNWRDirectSalesDetail = new NWRDirectSalesDetail();
                _oNWRDirectSalesDetail.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oNWRDirectSalesDetail;
        }
        public List<NWRDirectSalesDetail> Gets(string sSQL, string sUserID)
        {
            NWRDirectSalesDetail _oNWRDirectSalesDetail = new NWRDirectSalesDetail();
            List<NWRDirectSalesDetail> _oNWRDirectSalesDetails = new List<NWRDirectSalesDetail>();
            try
            {
                Connection.Open();
                Command.CommandText = NWRDirectSalesDetailDA.Gets(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oNWRDirectSalesDetails = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oNWRDirectSalesDetail = new NWRDirectSalesDetail();
                _oNWRDirectSalesDetails = new List<NWRDirectSalesDetail>();
                _oNWRDirectSalesDetail.ErrorMessage = e.Message.Split('~')[1];
                _oNWRDirectSalesDetails.Add(_oNWRDirectSalesDetail);
            }
            return _oNWRDirectSalesDetails;
        }
        public List<NWRDirectSalesDetail> GetsNonDepositedSales(int nBankAccountMappingID, DateTime dStartDate, DateTime dEndDate, int nBranchID, string sUserID)
        {
            NWRDirectSalesDetail _oNWRDirectSalesDetail = new NWRDirectSalesDetail();
            List<NWRDirectSalesDetail> _oNWRDirectSalesDetails = new List<NWRDirectSalesDetail>();
            try
            {
                Connection.Open();
                Command.CommandText = NWRDirectSalesDetailDA.GetsNonDepositedSales(nBankAccountMappingID, dStartDate, dEndDate, nBranchID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oNWRDirectSalesDetails = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oNWRDirectSalesDetail = new NWRDirectSalesDetail();
                _oNWRDirectSalesDetails = new List<NWRDirectSalesDetail>();
                _oNWRDirectSalesDetail.ErrorMessage = e.Message.Split('~')[1];
                _oNWRDirectSalesDetails.Add(_oNWRDirectSalesDetail);
            }
            return _oNWRDirectSalesDetails;
        }
        public List<NWRDirectSalesDetail> GetsByParentID(int nParentID, string sUserID)
        {
            NWRDirectSalesDetail _oNWRDirectSalesDetail = new NWRDirectSalesDetail();
            List<NWRDirectSalesDetail> _oNWRDirectSalesDetails = new List<NWRDirectSalesDetail>();
            try
            {
                Connection.Open();
                Command.CommandText = NWRDirectSalesDetailDA.GetsByParentID(nParentID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oNWRDirectSalesDetails = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oNWRDirectSalesDetail = new NWRDirectSalesDetail();
                _oNWRDirectSalesDetails = new List<NWRDirectSalesDetail>();
                _oNWRDirectSalesDetail.ErrorMessage = e.Message.Split('~')[1];
                _oNWRDirectSalesDetails.Add(_oNWRDirectSalesDetail);
            }
            return _oNWRDirectSalesDetails;
        }
        public List<NWRDirectSalesDetail> GetsByBranchID(int nBranchID, string sUserID)
        {
            NWRDirectSalesDetail _oNWRDirectSalesDetail = new NWRDirectSalesDetail();
            List<NWRDirectSalesDetail> _oNWRDirectSalesDetails = new List<NWRDirectSalesDetail>();
            try
            {
                Connection.Open();
                Command.CommandText = NWRDirectSalesDetailDA.GetsByBranchID(nBranchID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oNWRDirectSalesDetails = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oNWRDirectSalesDetail = new NWRDirectSalesDetail();
                _oNWRDirectSalesDetails = new List<NWRDirectSalesDetail>();
                _oNWRDirectSalesDetail.ErrorMessage = e.Message.Split('~')[1];
                _oNWRDirectSalesDetails.Add(_oNWRDirectSalesDetail);
            }
            return _oNWRDirectSalesDetails;
        }
        public List<NWRDirectSalesDetail> GetNWRSalesDetailsByLSO(string sLSOCode, string sUserID)
        {
            NWRDirectSalesDetail _oNWRDirectSalesDetail = new NWRDirectSalesDetail();
            List<NWRDirectSalesDetail> _oNWRDirectSalesDetails = new List<NWRDirectSalesDetail>();
            try
            {
                Connection.Open();
                Command.CommandText = NWRDirectSalesDetailDA.GetNWRSalesDetailsByLSO(sLSOCode, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oNWRDirectSalesDetails = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oNWRDirectSalesDetail = new NWRDirectSalesDetail();
                _oNWRDirectSalesDetails = new List<NWRDirectSalesDetail>();
                _oNWRDirectSalesDetail.ErrorMessage = e.Message.Split('~')[1];
                _oNWRDirectSalesDetails.Add(_oNWRDirectSalesDetail);
            }
            return _oNWRDirectSalesDetails;
        }
        public List<NWRDirectSalesDetail> GenerateNWRSalesDetailsByLSO(string sLSOCode, string sUserID)
        {
            NWRDirectSalesDetail _oNWRDirectSalesDetail = new NWRDirectSalesDetail();
            List<NWRDirectSalesDetail> _oNWRDirectSalesDetails = new List<NWRDirectSalesDetail>();
            try
            {
                Connection.Open();
                Command.CommandText = NWRDirectSalesDetailDA.GenerateNWRSalesDetailsByLSO(sLSOCode, sUserID);
                IDataReader reader = Command.ExecuteReader();
                if (reader.Read())
                {
                    _oNWRDirectSalesDetails = MakeObjects(reader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oNWRDirectSalesDetail = new NWRDirectSalesDetail();
                _oNWRDirectSalesDetails = new List<NWRDirectSalesDetail>();
                _oNWRDirectSalesDetail.ErrorMessage = e.Message.Split('~')[1];
                _oNWRDirectSalesDetails.Add(_oNWRDirectSalesDetail);
            }
            return _oNWRDirectSalesDetails;
        }
        public NWRDirectSalesDetail Get(int nID, string sUserID)
        {
            NWRDirectSalesDetail _oNWRDirectSalesDetail = new NWRDirectSalesDetail();
            try
            {
                Connection.Open();
                Command.CommandText = NWRDirectSalesDetailDA.Get(nID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oNWRDirectSalesDetail = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oNWRDirectSalesDetail = new NWRDirectSalesDetail();
                _oNWRDirectSalesDetail.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oNWRDirectSalesDetail;
        }
        public string Delete(int nNWRDirectSalesDetailID, string sUserID)
        {
            try
            {
                Connection.Open();
                Command.CommandText = NWRDirectSalesDetailDA.Delete(nNWRDirectSalesDetailID, sUserID);
                Command.ExecuteNonQuery();
                Connection.Close();
            }
            catch (Exception e)
            {
                FeedbackMessage.Deleted = e.Message.Split('~')[0];
            }
            return FeedbackMessage.Deleted;
        }
        public string DeleteByIDs(int nBankDepositID, string nNWRDirectSalesDetailIDs, string sUserID)
        {
            try
            {
                Connection.Open();
                Command.CommandText = NWRDirectSalesDetailDA.DeleteByIDs(nBankDepositID, nNWRDirectSalesDetailIDs, sUserID);
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