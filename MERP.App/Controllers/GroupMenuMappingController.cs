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
    public class GroupMenuMappingController : Controller
    {
        GroupMenuMapping _oGroupMenuMapping = new GroupMenuMapping();
        List<GroupMenuMapping> _oGroupMenuMappings = new List<GroupMenuMapping>();
        GroupMenuMappingService _oGroupMenuMappingService = new GroupMenuMappingService();

        [HttpPost]
        public JsonResult SaveGroupMenuMapping(Group obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            try
            {
                if (obj.GroupID != 0 && obj.ErrorMessage != "")
                {
                    _oGroupMenuMappings = _oGroupMenuMappingService.SaveGroupMenuMapping(obj.GroupID, obj.ErrorMessage, EnumDBOperation.SpecialCase1, (string)Session[GlobalSession.UserID]);
                    _oGroupMenuMapping.GroupMenuMappingID = _oGroupMenuMappings.Count();
                    _oGroupMenuMapping.ErrorMessage = FeedbackMessage.Updated;
                }
                else
                {
                    _oGroupMenuMapping = new GroupMenuMapping();
                    _oGroupMenuMapping.ErrorMessage = FeedbackMessage.Invalid;
                }
            }
            catch (Exception ex)
            {
                _oGroupMenuMapping = new GroupMenuMapping();
                _oGroupMenuMapping.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oGroupMenuMapping);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }


    }
}