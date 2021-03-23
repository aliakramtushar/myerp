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
    public class ReportMasterService : CommonGateway, IReportMaster
    {
        #region Maping
        private ReportMaster MapObject(NullHandler oReader)
        {
            ReportMaster oReportMaster = new ReportMaster();
            oReportMaster.ReportMasterID = oReader.GetInt32("ReportMasterID");
            oReportMaster.ReportName = oReader.GetString("ReportName");
            oReportMaster.Query = oReader.GetString("Query");
            oReportMaster.IsSP = oReader.GetBoolean("IsSP");
            oReportMaster.IsExcel = oReader.GetBoolean("IsExcel");
            oReportMaster.IsPDF = oReader.GetBoolean("IsPDF");
            oReportMaster.IsInActive = oReader.GetBoolean("IsInActive");
            oReportMaster.RptFileName = oReader.GetString("RptFileName");
            oReportMaster.Remarks = oReader.GetString("Remarks");
            return oReportMaster;
        }
        private ReportMaster MakeObject(NullHandler oReader)
        {
            ReportMaster oReportMaster = new ReportMaster();
            oReportMaster = MapObject(oReader);
            return oReportMaster;
        }
        private List<ReportMaster> MakeObjects(IDataReader oReader)
        {
            List<ReportMaster> oReportMasters = new List<ReportMaster>();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                ReportMaster oReportMaster = MapObject(oHandler);
                oReportMasters.Add(oReportMaster);
            }
            return oReportMasters;
        }
        #endregion


        #region Function Implementation
        public ReportMaster IUD(ReportMaster oReportMaster, EnumDBOperation oDBOperation, string sUserID)
        {
            ReportMaster _oReportMaster = new ReportMaster();
            try
            {
                Connection.Open();
                Command.CommandText = ReportMasterDA.IUD(oReportMaster, oDBOperation, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oReportMaster = MakeObject(oReader);
                }
                reader.Close();
                #region Child Update
                if (oDBOperation == EnumDBOperation.Insert || oDBOperation == EnumDBOperation.Update)
                {
                    ReportDetail oReportDetail = new ReportDetail();
                    List<ReportDetail> oReportDetails = new List<ReportDetail>();
                    ReportDetailService oReportDetailService = new ReportDetailService();
                    string sReportDetailIDs = "";
                    for (int i = 0; i < oReportMaster.ReportDetails.Count(); i++)
                    {
                        oReportMaster.ReportDetails[i].ReportMasterID = _oReportMaster.ReportMasterID;
                        if (oReportMaster.ReportDetails[i].ReportDetailID == 0)
                        {
                            oReportDetail = oReportDetailService.IUD(oReportMaster.ReportDetails[i], EnumDBOperation.Insert, sUserID);
                        }
                        else
                        {
                            oReportDetail = oReportDetailService.IUD(oReportMaster.ReportDetails[i], EnumDBOperation.Update, sUserID);
                        }
                        oReportDetails.Add(oReportDetail);
                        sReportDetailIDs = sReportDetailIDs + oReportDetail.ReportDetailID + ",";
                    }
                    //if (oReportMaster.ReportDetails.Count > 0)
                    //{
                    oReportDetailService.DeleteByIDs(_oReportMaster.ReportMasterID, sReportDetailIDs.Remove(sReportDetailIDs.Length - 1, 1), sUserID);
                    _oReportMaster.ReportDetails = oReportDetails;
                    //}
                    //else
                    //{
                    //    oReportDetailService.DeleteByParentID(_oReportMaster.ReportMasterID, sUserID);
                    //}
                }
                #endregion
                Connection.Close();
            }
            catch (Exception e)
            {
                _oReportMaster = new ReportMaster();
                _oReportMaster.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oReportMaster;
        }
        public List<ReportMaster> Gets(string sSQL, string sUserID)
        {
            ReportMaster _oReportMaster = new ReportMaster();
            List<ReportMaster> _oReportMasters = new List<ReportMaster>();
            try
            {
                Connection.Open();
                Command.CommandText = ReportMasterDA.Gets(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oReportMasters = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oReportMaster = new ReportMaster();
                _oReportMasters = new List<ReportMaster>();
                _oReportMaster.ErrorMessage = e.Message.Split('~')[1];
                _oReportMasters.Add(_oReportMaster);
            }
            return _oReportMasters;
        }
        public List<ReportMaster> GetsAccessedReports(string sUserID)
        {
            ReportMaster _oReportMaster = new ReportMaster();
            List<ReportMaster> _oReportMasters = new List<ReportMaster>();
            try
            {
                Connection.Open();
                Command.CommandText = ReportMasterDA.GetsAccessedReports(sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oReportMasters = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oReportMaster = new ReportMaster();
                _oReportMasters = new List<ReportMaster>();
                _oReportMaster.ErrorMessage = e.Message.Split('~')[1];
                _oReportMasters.Add(_oReportMaster);
            }
            return _oReportMasters;
        }
        public List<ReportMaster> GetsActiveReports(string sUserID)
        {
            ReportMaster _oReportMaster = new ReportMaster();
            List<ReportMaster> _oReportMasters = new List<ReportMaster>();
            try
            {
                Connection.Open();
                Command.CommandText = ReportMasterDA.GetsActiveReports(sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oReportMasters = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oReportMaster = new ReportMaster();
                _oReportMasters = new List<ReportMaster>();
                _oReportMaster.ErrorMessage = e.Message.Split('~')[1];
                _oReportMasters.Add(_oReportMaster);
            }
            return _oReportMasters;
        }
        public ReportMaster Get(int nID, string sUserID)
        {
            ReportMaster _oReportMaster = new ReportMaster();
            try
            {
                Connection.Open();
                Command.CommandText = ReportMasterDA.Get(nID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oReportMaster = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oReportMaster = new ReportMaster();
                _oReportMaster.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oReportMaster;
        }
        public string Delete(int nReportMasterID, string sUserID)
        {
            try
            {
                Connection.Open();
                Command.CommandText = ReportMasterDA.Delete(nReportMasterID, sUserID);
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