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
    public class StoreProductHistoryController : Controller
    {
        StoreProductHistory _oStoreProductHistory = new StoreProductHistory();
        List<StoreProductHistory> _oStoreProductHistorys = new List<StoreProductHistory>();
        StoreProductHistoryService _oStoreProductHistoryService = new StoreProductHistoryService();
        public ActionResult ViewStoreProductHistorys()
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oStoreProductHistorys = _oStoreProductHistoryService.Gets("SELECT * FROM AST.View_StoreProductHistory ORDER BY DateAdded DESC", (string)Session[GlobalSession.UserID]);
            ViewBag.StoreProductHistorys = _oStoreProductHistorys;
            return View();
        }
        [HttpPost]
        public JsonResult SearchProducts(StoreProductHistory obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oStoreProductHistory = new StoreProductHistory();
            _oStoreProductHistorys = new List<StoreProductHistory>() { };
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    _oStoreProductHistorys = _oStoreProductHistoryService.Gets(this.GeneralSearchMakeSQL(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oStoreProductHistory = new StoreProductHistory();
                _oStoreProductHistory.ErrorMessage = ex.Message;
                _oStoreProductHistorys.Add(_oStoreProductHistory);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oStoreProductHistorys);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        private string GeneralSearchMakeSQL(string sVal)
        {
            return "SELECT * FROM AST.ProductInfo WHERE "
                                + " StoreProductHistoryName LIKE '%" + sVal + "%' OR"
                                + " StoreProductHistoryCode LIKE '%" + sVal + "%'";

        }
    }
}