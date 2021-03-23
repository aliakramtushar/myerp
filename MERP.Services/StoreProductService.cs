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
    public class StoreProductService : CommonGateway, IStoreProduct
    {
        #region Maping
        private StoreProduct MapObject(NullHandler oReader)
        {
            StoreProduct oStoreProduct = new StoreProduct();
            oStoreProduct.StoreProductID = oReader.GetInt32("StoreProductID");
            oStoreProduct.StoreID = oReader.GetInt32("StoreID");
            oStoreProduct.ProductID = oReader.GetInt32("ProductID");
            oStoreProduct.GoodQty = oReader.GetInt32("GoodQty");
            oStoreProduct.FaultyQty = oReader.GetInt32("FaultyQty");
            oStoreProduct.ScrapQty = oReader.GetInt32("ScrapQty");
            oStoreProduct.TransitGoodQty = oReader.GetInt32("TransitGoodQty");
            oStoreProduct.TransitFaultyQty = oReader.GetInt32("TransitFaultyQty");
            oStoreProduct.TransitScrapQty = oReader.GetInt32("TransitScrapQty");
            oStoreProduct.Remarks = oReader.GetString("Remarks");
            oStoreProduct.StoreName = oReader.GetString("StoreName");
            oStoreProduct.ProductName = oReader.GetString("ProductName");
            oStoreProduct.ProductCode = oReader.GetString("ProductCode");
            oStoreProduct.ShortName = oReader.GetString("ShortName");
            return oStoreProduct;
        }
        private StoreProduct MakeObject(NullHandler oReader)
        {
            StoreProduct oStoreProduct = new StoreProduct();
            oStoreProduct = MapObject(oReader);
            return oStoreProduct;
        }
        private List<StoreProduct> MakeObjects(IDataReader oReader)
        {
            List<StoreProduct> oStoreProducts = new List<StoreProduct>();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                StoreProduct oStoreProduct = MapObject(oHandler);
                oStoreProducts.Add(oStoreProduct);
            }
            return oStoreProducts;
        }
        #endregion


        #region Function Implementation
        public StoreProduct IUD(StoreProduct oStoreProduct, EnumDBOperation oDBOperation, string sUserID)
        {
            StoreProduct _oStoreProduct = new StoreProduct();
            try
            {
                Connection.Open();
                Command.CommandText = StoreProductDA.IUD(oStoreProduct, oDBOperation, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oStoreProduct = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oStoreProduct = new StoreProduct();
                _oStoreProduct.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oStoreProduct;
        }
        public List<StoreProduct> Gets(string sSQL, string sUserID)
        {
            StoreProduct _oStoreProduct = new StoreProduct();
            List<StoreProduct> _oStoreProducts = new List<StoreProduct>();
            try
            {
                Connection.Open();
                Command.CommandText = StoreProductDA.Gets(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oStoreProducts = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oStoreProduct = new StoreProduct();
                _oStoreProducts = new List<StoreProduct>();
                _oStoreProduct.ErrorMessage = e.Message;
                _oStoreProducts.Add(_oStoreProduct);
            }
            return _oStoreProducts;
        }
        public List<StoreProduct> GetsByStore(int nStoreID, string sUserID)
        {
            StoreProduct _oStoreProduct = new StoreProduct();
            List<StoreProduct> _oStoreProducts = new List<StoreProduct>();
            try
            {
                Connection.Open();
                Command.CommandText = StoreProductDA.GetsByStore(nStoreID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oStoreProducts = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oStoreProduct = new StoreProduct();
                _oStoreProducts = new List<StoreProduct>();
                _oStoreProduct.ErrorMessage = e.Message;
                _oStoreProducts.Add(_oStoreProduct);
            }
            return _oStoreProducts;
        }
        public StoreProduct Get(int nID, string sUserID)
        {
            StoreProduct _oStoreProduct = new StoreProduct();
            try
            {
                Connection.Open();
                Command.CommandText = StoreProductDA.Get(nID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oStoreProduct = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oStoreProduct = new StoreProduct();
                _oStoreProduct.ErrorMessage = e.Message;
            }
            return _oStoreProduct;
        }
        public StoreProduct GetByStoreAndProductID(StoreProduct oStoreProduct, string sUserID)
        {
            StoreProduct _oStoreProduct = new StoreProduct();
            try
            {
                Connection.Open();
                Command.CommandText = StoreProductDA.GetByStoreAndProductID(oStoreProduct, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oStoreProduct = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oStoreProduct = new StoreProduct();
                _oStoreProduct.ErrorMessage = e.Message;
            }
            return _oStoreProduct;
        }
        #endregion
    }
}