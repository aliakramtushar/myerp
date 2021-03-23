using CrystalDecisions.CrystalReports.Engine;
using MERP.Engine.GlobalClass;
using MERP.Models;
using MERP.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MERP.App.Controllers
{
    public class ReportComponentController : Controller
    {
        ReportComponent _oReportComponent = new ReportComponent();
        List<ReportComponent> _oReportComponents = new List<ReportComponent>();
        ReportComponentService _oReportComponentService = new ReportComponentService();
        [HttpPost]
        public JsonResult GetsReportComponent(ReportComponent obj)
        {
            _oReportComponent = new ReportComponent();
            _oReportComponents = new List<ReportComponent>() { };
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    _oReportComponents = _oReportComponentService.Gets((obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oReportComponent = new ReportComponent();
                _oReportComponent.ErrorMessage = ex.Message;
                _oReportComponents.Add(_oReportComponent);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oReportComponents);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetDataForExcelReport(ReportMaster obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            ReportComponentService oReportComponentService = new ReportComponentService();
            DataTable oDataTable = new DataTable();
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage) && obj.IsInActive == false)
                {
                    if(obj.IsSP)        // for store proc
                    {
                        oDataTable = oReportComponentService.GetDataForReport(obj.ErrorMessage, (string)Session[GlobalSession.UserID]);
                    }
                    else                // for view
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                _oReportComponent = new ReportComponent();
                _oReportComponent.ErrorMessage = ex.Message;
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(JsonConvert.SerializeObject(oDataTable));
            return Json(sjson, JsonRequestBehavior.AllowGet);
            //var jsonResult = Json(oDataTable, JsonRequestBehavior.AllowGet);
            //jsonResult.MaxJsonLength = int.MaxValue;
            //return jsonResult;
        }
        public ActionResult GetDataForCrystalReport(string str, string rpt)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            ReportComponentService oReportComponentService = new ReportComponentService();
            DataTable oDataTable = new DataTable();
            if (!string.IsNullOrEmpty(str))
            {
                oDataTable = oReportComponentService.GetDataForReport(str, (string)Session[GlobalSession.UserID]);
            }

            var oReportDocument = new ReportDocument();
            oReportDocument.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/CrystalReports/ReportRptFiles/"+ rpt + ".rpt"));
            oReportDocument.SetDataSource(oDataTable);
            Stream s = oReportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

            oReportDocument.Close();
            oReportDocument.Dispose();
            return File(s, "application/pdf");
        }
    }
}