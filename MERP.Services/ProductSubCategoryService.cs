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
    public class ProductSubCategoryService : CommonGateway, IProductSubCategory
    {
        #region Maping
        private ProductSubCategory MapObject(NullHandler oReader)
        {
            ProductSubCategory oProductSubCategory = new ProductSubCategory();
            oProductSubCategory.ProductSubCategoryID = oReader.GetInt32("ProductSubCategoryID");
            oProductSubCategory.ProductCategoryID = oReader.GetInt32("ProductCategoryID");
            oProductSubCategory.ProductSubCategoryCode = oReader.GetString("ProductSubCategoryCode");
            oProductSubCategory.ProductSubCategoryName = oReader.GetString("ProductSubCategoryName");
            oProductSubCategory.ProductCategoryName = oReader.GetString("ProductCategoryName");
            oProductSubCategory.IsInActive = oReader.GetBoolean("IsInActive");
            return oProductSubCategory;
        }
        private ProductSubCategory MakeObject(NullHandler oReader)
        {
            ProductSubCategory oProductSubCategory = new ProductSubCategory();
            oProductSubCategory = MapObject(oReader);
            return oProductSubCategory;
        }
        private List<ProductSubCategory> MakeObjects(IDataReader oReader)
        {
            List<ProductSubCategory> oProductSubCategorys = new List<ProductSubCategory>();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                ProductSubCategory oProductSubCategory = MapObject(oHandler);
                oProductSubCategorys.Add(oProductSubCategory);
            }
            return oProductSubCategorys;
        }
        #endregion


        #region Function Implementation
        public ProductSubCategory IUD(ProductSubCategory oProductSubCategory, EnumDBOperation oDBOperation, string sUserID)
        {
            ProductSubCategory _oProductSubCategory = new ProductSubCategory();
            try
            {
                Connection.Open();
                Command.CommandText = ProductSubCategoryDA.IUD(oProductSubCategory, oDBOperation, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oProductSubCategory = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oProductSubCategory = new ProductSubCategory();
                _oProductSubCategory.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oProductSubCategory;
        }
        public List<ProductSubCategory> Gets(string sSQL, string sUserID)
        {
            ProductSubCategory _oProductSubCategory = new ProductSubCategory();
            List<ProductSubCategory> _oProductSubCategorys = new List<ProductSubCategory>();
            try
            {
                Connection.Open();
                Command.CommandText = ProductSubCategoryDA.Gets(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oProductSubCategorys = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oProductSubCategory = new ProductSubCategory();
                _oProductSubCategorys = new List<ProductSubCategory>();
                _oProductSubCategory.ErrorMessage = e.Message.Split('~')[1];
                _oProductSubCategorys.Add(_oProductSubCategory);
            }
            return _oProductSubCategorys;
        }
        public ProductSubCategory Get(int nID, string sUserID)
        {
            ProductSubCategory _oProductSubCategory = new ProductSubCategory();
            try
            {
                Connection.Open();
                Command.CommandText = ProductSubCategoryDA.Get(nID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oProductSubCategory = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oProductSubCategory = new ProductSubCategory();
                _oProductSubCategory.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oProductSubCategory;
        }
        #endregion
    }
}