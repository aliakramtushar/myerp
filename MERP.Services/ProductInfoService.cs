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
    public class ProductInfoService : CommonGateway, IProductInfo
    {
        #region Maping
        private ProductInfo MapObject(NullHandler oReader)
        {
            ProductInfo oProductInfo = new ProductInfo();
            oProductInfo.ProductID = oReader.GetInt32("ProductID");
            oProductInfo.ProductCode = oReader.GetString("ProductCode");
            oProductInfo.ProductName = oReader.GetString("ProductName");
            oProductInfo.ShortName = oReader.GetString("ShortName");
            oProductInfo.ImagePath = oReader.GetString("ImagePath");
            oProductInfo.SerialNo = oReader.GetString("SerialNo");
            oProductInfo.TagNo = oReader.GetString("TagNo");
            oProductInfo.ModelNo = oReader.GetString("ModelNo");
            oProductInfo.ProductTypeID = oReader.GetInt32("ProductTypeID");
            oProductInfo.ProductTypeName = oReader.GetString("ProductTypeName");
            oProductInfo.ProductCategoryID = oReader.GetInt32("ProductCategoryID");
            oProductInfo.ProductCategoryName = oReader.GetString("ProductCategoryName");
            oProductInfo.ProductSubCategoryID = oReader.GetInt32("ProductSubCategoryID");
            oProductInfo.ProductSubCategoryName = oReader.GetString("ProductSubCategoryName");
            oProductInfo.SegmentID = oReader.GetInt32("SegmentID");
            oProductInfo.SegmentName = oReader.GetString("SegmentName");
            oProductInfo.ProductStatus = (EnumActivityStatus)oReader.GetInt16("ProductStatus");
            oProductInfo.CostCenterID = oReader.GetInt32("CostCenterID");
            oProductInfo.CostCenterName = oReader.GetString("CostCenterName");
            oProductInfo.DepartmentID = oReader.GetInt32("DepartmentID");
            oProductInfo.DepartmentName = oReader.GetString("DepartmentName");
            oProductInfo.BrandID = oReader.GetInt32("BrandID");
            oProductInfo.BrandName = oReader.GetString("BrandName");
            oProductInfo.Description = oReader.GetString("Description");
            oProductInfo.LotID = oReader.GetInt32("LotID");
            oProductInfo.SupplierPersonID = oReader.GetInt32("SupplierPersonID");
            oProductInfo.SupplierPersonName = oReader.GetString("SupplierPersonName");
            oProductInfo.LaunchingDate = oReader.GetDateTime("LaunchingDate");
            oProductInfo.WarrantyInMonth = oReader.GetInt32("WarrantyInMonth");
            oProductInfo.ServiceLifeInMonth = oReader.GetInt32("ServiceLifeInMonth");
            oProductInfo.DepreciationRate = oReader.GetDouble("DepreciationRate");
            oProductInfo.RefCode = oReader.GetString("RefCode");
            oProductInfo.PurchaseDate = oReader.GetDateTime("PurchaseDate");
            oProductInfo.Remarks = oReader.GetString("Remarks");
            return oProductInfo;
        }
        private ProductInfo MakeObject(NullHandler oReader)
        {
            ProductInfo oProductInfo = new ProductInfo();
            oProductInfo = MapObject(oReader);
            return oProductInfo;
        }
        private List<ProductInfo> MakeObjects(IDataReader oReader)
        {
            List<ProductInfo> oProductInfos = new List<ProductInfo>();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                ProductInfo oProductInfo = MapObject(oHandler);
                oProductInfos.Add(oProductInfo);
            }
            return oProductInfos;
        }
        #endregion


        #region Function Implementation
        public ProductInfo IUD(ProductInfo oProductInfo, EnumDBOperation oDBOperation, string sUserID)
        {
            ProductInfo _oProductInfo = new ProductInfo();
            try
            {
                Connection.Open();
                Command.CommandText = ProductInfoDA.IUD(oProductInfo, oDBOperation, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oProductInfo = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oProductInfo = new ProductInfo();
                _oProductInfo.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oProductInfo;
        }
        public List<ProductInfo> Gets(string sSQL, string sUserID)
        {
            ProductInfo _oProductInfo = new ProductInfo();
            List<ProductInfo> _oProductInfos = new List<ProductInfo>();
            try
            {
                Connection.Open();
                Command.CommandText = ProductInfoDA.Gets(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oProductInfos = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oProductInfo = new ProductInfo();
                _oProductInfos = new List<ProductInfo>();
                _oProductInfo.ErrorMessage = e.Message;
                _oProductInfos.Add(_oProductInfo);
            }
            return _oProductInfos;
        }
        public ProductInfo Get(int nID, string sUserID)
        {
            ProductInfo _oProductInfo = new ProductInfo();
            try
            {
                Connection.Open();
                Command.CommandText = ProductInfoDA.Get(nID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oProductInfo = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oProductInfo = new ProductInfo();
                _oProductInfo.ErrorMessage = e.Message;
            }
            return _oProductInfo;
        }
        public string Delete(int nProductInfoID, string sUserID)
        {
            try
            {
                Connection.Open();
                Command.CommandText = ProductInfoDA.Delete(nProductInfoID, sUserID);
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