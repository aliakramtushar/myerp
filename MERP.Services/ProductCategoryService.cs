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
    public class ProductCategoryService : CommonGateway, IProductCategory
    {
        #region Maping
        private ProductCategory MapObject(NullHandler oReader)
        {
            ProductCategory oProductCategory = new ProductCategory();
            oProductCategory.ProductCategoryID = oReader.GetInt32("ProductCategoryID");
            oProductCategory.ProductTypeID = oReader.GetInt32("ProductTypeID");
            oProductCategory.ProductCategoryCode = oReader.GetString("ProductCategoryCode");
            oProductCategory.ProductCategoryName = oReader.GetString("ProductCategoryName");
            oProductCategory.ProductTypeName = oReader.GetString("ProductTypeName");
            oProductCategory.IsInActive = oReader.GetBoolean("IsInActive");
            return oProductCategory;
        }
        private ProductCategory MakeObject(NullHandler oReader)
        {
            ProductCategory oProductCategory = new ProductCategory();
            oProductCategory = MapObject(oReader);
            return oProductCategory;
        }
        private List<ProductCategory> MakeObjects(IDataReader oReader)
        {
            List<ProductCategory> oProductCategorys = new List<ProductCategory>();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                ProductCategory oProductCategory = MapObject(oHandler);
                oProductCategorys.Add(oProductCategory);
            }
            return oProductCategorys;
        }
        #endregion


        #region Function Implementation
        public ProductCategory IUD(ProductCategory oProductCategory, EnumDBOperation oDBOperation, string sUserID)
        {
            ProductCategory _oProductCategory = new ProductCategory();
            try
            {
                Connection.Open();
                Command.CommandText = ProductCategoryDA.IUD(oProductCategory, oDBOperation, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oProductCategory = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oProductCategory = new ProductCategory();
                _oProductCategory.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oProductCategory;
        }
        public List<ProductCategory> Gets(string sSQL, string sUserID)
        {
            ProductCategory _oProductCategory = new ProductCategory();
            List<ProductCategory> _oProductCategorys = new List<ProductCategory>();
            try
            {
                Connection.Open();
                Command.CommandText = ProductCategoryDA.Gets(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oProductCategorys = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oProductCategory = new ProductCategory();
                _oProductCategorys = new List<ProductCategory>();
                _oProductCategory.ErrorMessage = e.Message.Split('~')[1];
                _oProductCategorys.Add(_oProductCategory);
            }
            return _oProductCategorys;
        }
        public ProductCategory Get(int nID, string sUserID)
        {
            ProductCategory _oProductCategory = new ProductCategory();
            try
            {
                Connection.Open();
                Command.CommandText = ProductCategoryDA.Get(nID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oProductCategory = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oProductCategory = new ProductCategory();
                _oProductCategory.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oProductCategory;
        }
        #endregion
    }
}