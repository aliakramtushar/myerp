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
    public class StoreProductHistoryService : CommonGateway, IStoreProductHistory
    {
        #region Maping
        private StoreProductHistory MapObject(NullHandler oReader)
        {
            StoreProductHistory oStoreProductHistory = new StoreProductHistory();
            oStoreProductHistory.StoreProductHistoryID = oReader.GetInt32("StoreProductHistoryID");
            oStoreProductHistory.DBOperationID = (EnumDBOperation)oReader.GetInt16("DBOperationID");
            oStoreProductHistory.StoreID = oReader.GetInt32("StoreID");
            oStoreProductHistory.ProductID = oReader.GetInt32("ProductID");
            oStoreProductHistory.SupplierID = oReader.GetInt32("SupplierID");
            oStoreProductHistory.TransitStoreID = oReader.GetInt32("TransitStoreID");
            oStoreProductHistory.Remarks = oReader.GetString("Remarks");
            oStoreProductHistory.ProductOldQty = oReader.GetInt32("ProductOldQty");
            oStoreProductHistory.ProductNewQty = oReader.GetInt32("ProductNewQty");
            oStoreProductHistory.AddedBy = oReader.GetString("AddedBy");
            oStoreProductHistory.DateAdded = oReader.GetDateTime("DateAdded");
            oStoreProductHistory.StoreName = oReader.GetString("StoreName");
            oStoreProductHistory.ProductName = oReader.GetString("ProductName");
            oStoreProductHistory.SupplierName = oReader.GetString("SupplierName");
            oStoreProductHistory.TransitStoreName = oReader.GetString("TransitStoreName");
            return oStoreProductHistory;
        }
        private StoreProductHistory MakeObject(NullHandler oReader)
        {
            StoreProductHistory oStoreProductHistory = new StoreProductHistory();
            oStoreProductHistory = MapObject(oReader);
            return oStoreProductHistory;
        }
        private List<StoreProductHistory> MakeObjects(IDataReader oReader)
        {
            List<StoreProductHistory> oStoreProductHistorys = new List<StoreProductHistory>();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                StoreProductHistory oStoreProductHistory = MapObject(oHandler);
                oStoreProductHistorys.Add(oStoreProductHistory);
            }
            return oStoreProductHistorys;
        }
        #endregion

        #region Function Implementation
        public List<StoreProductHistory> Gets(string sSQL, string sUserID)
        {
            StoreProductHistory _oStoreProductHistory = new StoreProductHistory();
            List<StoreProductHistory> _oStoreProductHistorys = new List<StoreProductHistory>();
            try
            {
                Connection.Open();
                Command.CommandText = StoreProductHistoryDA.Gets(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oStoreProductHistorys = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oStoreProductHistory = new StoreProductHistory();
                _oStoreProductHistorys = new List<StoreProductHistory>();
                _oStoreProductHistory.ErrorMessage = e.Message;
                _oStoreProductHistorys.Add(_oStoreProductHistory);
            }
            return _oStoreProductHistorys;
        }
        public StoreProductHistory Get(int nID, string sUserID)
        {
            StoreProductHistory _oStoreProductHistory = new StoreProductHistory();
            try
            {
                Connection.Open();
                Command.CommandText = StoreProductHistoryDA.Get(nID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oStoreProductHistory = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oStoreProductHistory = new StoreProductHistory();
                _oStoreProductHistory.ErrorMessage = e.Message;
            }
            return _oStoreProductHistory;
        }
        #endregion
    }
}