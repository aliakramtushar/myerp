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
    public class ReportComponentService : CommonGateway, IReportComponent
    {
        #region Maping
        private ReportComponent MapObject(NullHandler oReader)
        {
            ReportComponent oReportComponent = new ReportComponent();
            oReportComponent.ReportComponentID = oReader.GetInt32("ReportComponentID");
            oReportComponent.ReportComponentName = oReader.GetString("ReportComponentName");
            oReportComponent.Remarks = oReader.GetString("Remarks");
            return oReportComponent;
        }
        private ReportComponent MakeObject(NullHandler oReader)
        {
            ReportComponent oReportComponent = new ReportComponent();
            oReportComponent = MapObject(oReader);
            return oReportComponent;
        }
        private List<ReportComponent> MakeObjects(IDataReader oReader)
        {
            List<ReportComponent> oReportComponents = new List<ReportComponent>();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                ReportComponent oReportComponent = MapObject(oHandler);
                oReportComponents.Add(oReportComponent);
            }
            return oReportComponents;
        }
        #endregion


        #region Function Implementation
        public List<ReportComponent> Gets(string sSQL, string sUserID)
        {
            ReportComponent _oReportComponent = new ReportComponent();
            List<ReportComponent> _oReportComponents = new List<ReportComponent>();
            try
            {
                Connection.Open();
                Command.CommandText = ReportComponentDA.Gets(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oReportComponents = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oReportComponent = new ReportComponent();
                _oReportComponents = new List<ReportComponent>();
                _oReportComponent.ErrorMessage = e.Message.Split('~')[1];
                _oReportComponents.Add(_oReportComponent);
            }
            return _oReportComponents;
        }
        public DataTable GetDataForReport(string sSQL, string sUserID)
        {
            ReportComponent _oReportComponent = new ReportComponent();
            List<ReportComponent> _oReportComponents = new List<ReportComponent>();
            DataTable oDataTable = new DataTable();
            var list = new List<dynamic>();
            try
            {
                Connection.Open();
                Command.CommandText = ReportComponentDA.GetDataForReport(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                oDataTable.BeginLoadData();
                oDataTable.Load(reader);
                oDataTable.EndLoadData();
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oReportComponent = new ReportComponent();
                _oReportComponents = new List<ReportComponent>();
                _oReportComponent.ErrorMessage = e.Message.Split('~')[1];
                _oReportComponents.Add(_oReportComponent);
            }
            return oDataTable;
        }
        public ReportComponent Get(int nID, string sUserID)
        {
            ReportComponent _oReportComponent = new ReportComponent();
            try
            {
                Connection.Open();
                Command.CommandText = ReportComponentDA.Get(nID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oReportComponent = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oReportComponent = new ReportComponent();
                _oReportComponent.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oReportComponent;
        }
        #endregion
    }
}