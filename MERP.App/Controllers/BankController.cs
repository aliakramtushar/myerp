using CrystalDecisions.CrystalReports.Engine;
using MERP.Engine.GlobalClass;
using MERP.Models;
using MERP.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MERP.App.Controllers
{
    public class BankController : Controller
    {
        Bank _oBank = new Bank();
        List<Bank> _oBanks = new List<Bank>();
        BankService _oBankService = new BankService();
        public ActionResult ViewBanks()
        {
            _oBanks = _oBankService.Gets("", (string)Session[GlobalSession.UserID]);
            ViewBag.Banks = _oBanks;
            return View();
        }
        public ActionResult ViewBank(int nID)
        {
            _oBank = _oBankService.Get(nID, (string)Session[GlobalSession.UserID]);
            ViewBag.Bank = _oBank;
            ViewBag.ActivityStatus = Enum.GetValues(typeof(EnumActivityStatus)).Cast<EnumActivityStatus>().Select(e => new EnumClass { MModelPK = ((int)e), MModelString = e.ToString() });
            return View();
        }
        [HttpPost]
        public JsonResult Save(Bank obj)
        {
            try
            {
                if (obj.BankID == 0)
                {
                    _oBank = _oBankService.IUD(obj, EnumDBOperation.Insert, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oBank = _oBankService.IUD(obj, EnumDBOperation.Update, (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oBank = new Bank();
                _oBank.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oBank);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(Bank obj)
        {
            _oBank = new Bank();
            try
            {
                if (obj.BankID > 0)
                {
                    _oBank.ErrorMessage = _oBankService.Delete(obj.BankID, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oBank.ErrorMessage = FeedbackMessage.NoIdFound;
                }
            }
            catch (Exception ex)
            {
                _oBank = new Bank();
                _oBank.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oBank);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult PrintForCheck(string str)
        //{
        //    _oBank = new Bank();

        //    _oBanks = _oBankService.Gets("", (string)Session[GlobalSession.UserID]);
        //    _oBank = _oBanks[1];
        //    _oBanks = new List<Bank>();
        //    _oBanks.Add(_oBank);

        //    rptBankList orptBankList = new rptBankList();
        //    orptBankList.Load();
        //    orptBankList.SetDataSource(_oBanks);
        //    Stream s = orptBankList.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //    return File(s, "application/pdf");
        //}

        [HttpPost]
        public JsonResult Search(Bank obj)
        {
            _oBank = new Bank();
            _oBanks = new List<Bank>() { };
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    _oBanks = _oBankService.Gets(this.GeneralSearchMakeSQL(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oBank = new Bank();
                _oBank.ErrorMessage = ex.Message;
                _oBanks.Add(_oBank);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oBanks);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        private string GeneralSearchMakeSQL(string sVal)
        {
            return "SELECT * FROM View_Bank WHERE BankName LIKE '%" + sVal + "%' OR" +
                " Address LIKE '%" + sVal + "%' OR" +
                " Remarks LIKE '%" + sVal + "%'";
        }
    }
}