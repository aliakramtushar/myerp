using MERP.Engine.GlobalClass;
using MERP.Models;
using MERP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MERP.App.Controllers
{
    public class GroupReportMappingController : Controller
    {
        GroupReportMapping _oGroupReportMapping = new GroupReportMapping();
        List<GroupReportMapping> _oGroupReportMappings = new List<GroupReportMapping>();
        GroupReportMappingService _oGroupReportMappingService = new GroupReportMappingService();
        [HttpPost]
        public JsonResult GetsByReportMasterID(GroupReportMapping obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oGroupReportMapping = new GroupReportMapping();
            _oGroupReportMappings = new List<GroupReportMapping>();
            _oGroupReportMappingService = new GroupReportMappingService();
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    _oGroupReportMappings = _oGroupReportMappingService.GetsByReportMasterID(Convert.ToInt32(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oGroupReportMapping = new GroupReportMapping();
                _oGroupReportMapping.ErrorMessage = ex.Message;
                _oGroupReportMappings = new List<GroupReportMapping>();
                _oGroupReportMappings.Add(_oGroupReportMapping);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oGroupReportMappings);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetsByGroupID(GroupReportMapping obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oGroupReportMapping = new GroupReportMapping();
            _oGroupReportMappings = new List<GroupReportMapping>();
            _oGroupReportMappingService = new GroupReportMappingService();
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    _oGroupReportMappings = _oGroupReportMappingService.GetsByGroupID(Convert.ToInt32(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oGroupReportMapping = new GroupReportMapping();
                _oGroupReportMapping.ErrorMessage = ex.Message;
                _oGroupReportMappings.Add(_oGroupReportMapping);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oGroupReportMappings);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveByReportMasterID(GroupReportMapping obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oGroupReportMapping = new GroupReportMapping();
            _oGroupReportMappingService = new GroupReportMappingService();
            try
            {
                if (obj.ReportMasterID != 0)
                {
                    _oGroupReportMapping = _oGroupReportMappingService.SaveByReportMasterID(obj, (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oGroupReportMapping = new GroupReportMapping();
                _oGroupReportMapping.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oGroupReportMapping);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SaveByGroupID(GroupReportMapping obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oGroupReportMapping = new GroupReportMapping();
            _oGroupReportMappingService = new GroupReportMappingService();
            try
            {
                if (obj.GroupID != 0)
                {
                    _oGroupReportMapping = _oGroupReportMappingService.SaveByGroupID(obj, (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oGroupReportMapping = new GroupReportMapping();
                _oGroupReportMapping.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oGroupReportMapping);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
    }
}