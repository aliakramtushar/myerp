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
    public class ProductTypeService : CommonGateway, IProductType
    {
        #region Maping
        private ProductType MapObject(NullHandler oReader)
        {
            ProductType oProductType = new ProductType();
            oProductType.ProductTypeID = oReader.GetInt32("ProductTypeID");
            oProductType.ProductTypeCode = oReader.GetString("ProductTypeCode");
            oProductType.ProductTypeName = oReader.GetString("ProductTypeName");
            oProductType.IsInActive = oReader.GetBoolean("IsInActive");
            return oProductType;
        }
        private ProductType MakeObject(NullHandler oReader)
        {
            ProductType oProductType = new ProductType();
            oProductType = MapObject(oReader);
            return oProductType;
        }
        private List<ProductType> MakeObjects(IDataReader oReader)
        {
            List<ProductType> oProductTypes = new List<ProductType>();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                ProductType oProductType = MapObject(oHandler);
                oProductTypes.Add(oProductType);
            }
            return oProductTypes;
        }
        #endregion


        #region Function Implementation
        public ProductType IUD(ProductType oProductType, EnumDBOperation oDBOperation, string sUserID)
        {
            ProductType _oProductType = new ProductType();
            try
            {
                Connection.Open();
                Command.CommandText = ProductTypeDA.IUD(oProductType, oDBOperation, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oProductType = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oProductType = new ProductType();
                _oProductType.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oProductType;
        }
        public List<ProductType> Gets(string sSQL, string sUserID)
        {
            ProductType _oProductType = new ProductType();
            List<ProductType> _oProductTypes = new List<ProductType>();
            try
            {
                Connection.Open();
                Command.CommandText = ProductTypeDA.Gets(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oProductTypes = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oProductType = new ProductType();
                _oProductTypes = new List<ProductType>();
                _oProductType.ErrorMessage = e.Message;
                _oProductTypes.Add(_oProductType);
            }
            return _oProductTypes;
        }
        public ProductType Get(int nID, string sUserID)
        {
            ProductType _oProductType = new ProductType();
            try
            {
                Connection.Open();
                Command.CommandText = ProductTypeDA.Get(nID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oProductType = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oProductType = new ProductType();
                _oProductType.ErrorMessage = e.Message;
            }
            return _oProductType;
        }
        #endregion
    }
}