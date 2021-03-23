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
    public class ReportDetailService : CommonGateway, IReportDetail
    {
        #region Maping
        private ReportDetail MapObject(NullHandler oReader)
        {
            ReportDetail oReportDetail = new ReportDetail();
            oReportDetail.ReportDetailID = oReader.GetInt32("ReportDetailID");
            oReportDetail.ReportMasterID = oReader.GetInt32("ReportMasterID");
            oReportDetail.ControlType = (EnumControlType)oReader.GetInt16("ControlType");
            oReportDetail.ControlID = oReader.GetString("ControlID");
            oReportDetail.ControlName = oReader.GetString("ControlName");
            oReportDetail.FieldName = oReader.GetString("FieldName");
            oReportDetail.LabelName = oReader.GetString("LabelName");
            oReportDetail.Sequence = oReader.GetInt32("Sequence");
            return oReportDetail;
        }
        private ReportDetail MakeObject(NullHandler oReader)
        {
            ReportDetail oReportDetail = new ReportDetail();
            oReportDetail = MapObject(oReader);
            return oReportDetail;
        }
        private List<ReportDetail> MakeObjects(IDataReader oReader)
        {
            List<ReportDetail> oReportDetails = new List<ReportDetail>();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                ReportDetail oReportDetail = MapObject(oHandler);
                oReportDetails.Add(oReportDetail);
            }
            return oReportDetails;
        }
        #endregion


        #region Function Implementation
        public ReportDetail IUD(ReportDetail oReportDetail, EnumDBOperation oDBOperation, string sUserID)
        {
            ReportDetail _oReportDetail = new ReportDetail();
            try
            {
                Connection.Open();
                Command.CommandText = ReportDetailDA.IUD(oReportDetail, oDBOperation, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oReportDetail = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oReportDetail = new ReportDetail();
                _oReportDetail.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oReportDetail;
        }
        public List<ReportDetail> Gets(string sSQL, string sUserID)
        {
            ReportDetail _oReportDetail = new ReportDetail();
            List<ReportDetail> _oReportDetails = new List<ReportDetail>();
            try
            {
                Connection.Open();
                Command.CommandText = ReportDetailDA.Gets(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oReportDetails = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oReportDetail = new ReportDetail();
                _oReportDetails = new List<ReportDetail>();
                _oReportDetail.ErrorMessage = e.Message.Split('~')[1];
                _oReportDetails.Add(_oReportDetail);
            }
            return _oReportDetails;
        }
        public List<ReportDetail> GetsByParentID(int nReportMasterID, string sUserID)
        {
            ReportDetail _oReportDetail = new ReportDetail();
            List<ReportDetail> _oReportDetails = new List<ReportDetail>();
            try
            {
                Connection.Open();
                Command.CommandText = ReportDetailDA.GetsByParentID(nReportMasterID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oReportDetails = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oReportDetail = new ReportDetail();
                _oReportDetails = new List<ReportDetail>();
                _oReportDetail.ErrorMessage = e.Message.Split('~')[1];
                _oReportDetails.Add(_oReportDetail);
            }
            return _oReportDetails;
        }
        public ReportDetail Get(int nID, string sUserID)
        {
            ReportDetail _oReportDetail = new ReportDetail();
            try
            {
                Connection.Open();
                Command.CommandText = ReportDetailDA.Get(nID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oReportDetail = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oReportDetail = new ReportDetail();
                _oReportDetail.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oReportDetail;
        }
        public string Delete(int nReportDetailID, string sUserID)
        {
            try
            {
                Connection.Open();
                Command.CommandText = ReportDetailDA.Delete(nReportDetailID, sUserID);
                Command.ExecuteNonQuery();
                Connection.Close();
            }
            catch (Exception e)
            {
                FeedbackMessage.Deleted = e.Message.Split('~')[0];
            }
            return FeedbackMessage.Deleted;
        }
        public string DeleteByIDs(int nReportMasterID, string nReportDetailIDs, string sUserID)
        {
            try
            {
                Connection.Open();
                Command.CommandText = ReportDetailDA.DeleteByIDs(nReportMasterID, nReportDetailIDs, sUserID);
                Command.ExecuteNonQuery();
                Connection.Close();
            }
            catch (Exception e)
            {
                FeedbackMessage.Deleted = e.Message.Split('~')[0];
            }
            return FeedbackMessage.Deleted;
        }
        public string DeleteByParentID(int nReportMasterID, string sUserID)
        {
            try
            {
                Connection.Open();
                Command.CommandText = ReportDetailDA.DeleteByParentID(nReportMasterID, sUserID);
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