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
    public class CompanyInfoService : CommonGateway, ICompanyInfo
    {
        #region Maping
        private CompanyInfo MapObject(NullHandler oReader)
        {
            CompanyInfo oCompanyInfo = new CompanyInfo();
            oCompanyInfo.CompanyID = oReader.GetInt32("CompanyID");
            oCompanyInfo.CompanyName = oReader.GetString("CompanyName");
            oCompanyInfo.IsInActive = oReader.GetBoolean("IsInActive");
            oCompanyInfo.Remarks = oReader.GetString("Remarks");
            return oCompanyInfo;
        }
        private CompanyInfo MakeObject(NullHandler oReader)
        {
            CompanyInfo oCompanyInfo = new CompanyInfo();
            oCompanyInfo = MapObject(oReader);
            return oCompanyInfo;
        }
        private List<CompanyInfo> MakeObjects(IDataReader oReader)
        {
            List<CompanyInfo> oCompanyInfos = new List<CompanyInfo>();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                CompanyInfo oCompanyInfo = MapObject(oHandler);
                oCompanyInfos.Add(oCompanyInfo);
            }
            return oCompanyInfos;
        }
        #endregion


        #region Function Implementation
        public CompanyInfo IUD(CompanyInfo oCompanyInfo, EnumDBOperation oDBOperation, string sUserID)
        {
            CompanyInfo _oCompanyInfo = new CompanyInfo();
            try
            {
                Connection.Open();
                Command.CommandText = CompanyInfoDA.IUD(oCompanyInfo, oDBOperation, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oCompanyInfo = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oCompanyInfo = new CompanyInfo();
                _oCompanyInfo.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oCompanyInfo;
        }
        public List<CompanyInfo> Gets(string sSQL, string sUserID)
        {
            CompanyInfo _oCompanyInfo = new CompanyInfo();
            List<CompanyInfo> _oCompanyInfos = new List<CompanyInfo>();
            try
            {
                Connection.Open();
                Command.CommandText = CompanyInfoDA.Gets(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oCompanyInfos = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oCompanyInfo = new CompanyInfo();
                _oCompanyInfos = new List<CompanyInfo>();
                _oCompanyInfo.ErrorMessage = e.Message;
                _oCompanyInfos.Add(_oCompanyInfo);
            }
            return _oCompanyInfos;
        }
        public List<CompanyInfo> GetsActiveCompanys(string sUserID)
        {
            CompanyInfo _oCompanyInfo = new CompanyInfo();
            List<CompanyInfo> _oCompanyInfos = new List<CompanyInfo>();
            try
            {
                Connection.Open();
                Command.CommandText = CompanyInfoDA.GetsActiveCompanys(sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oCompanyInfos = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oCompanyInfo = new CompanyInfo();
                _oCompanyInfos = new List<CompanyInfo>();
                _oCompanyInfo.ErrorMessage = e.Message;
                _oCompanyInfos.Add(_oCompanyInfo);
            }
            return _oCompanyInfos;
        }
        
        public CompanyInfo Get(int nID, string sUserID)
        {
            CompanyInfo _oCompanyInfo = new CompanyInfo();
            try
            {
                Connection.Open();
                Command.CommandText = CompanyInfoDA.Get(nID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oCompanyInfo = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oCompanyInfo = new CompanyInfo();
                _oCompanyInfo.ErrorMessage = e.Message;
            }
            return _oCompanyInfo;
        }
        #endregion
    }
}