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
    public class EmployeeEvalutionSubmissionController : Controller
    {
        EmployeeEvalutionSubmission _oEmployeeEvalutionSubmission = new EmployeeEvalutionSubmission();
        List<EmployeeEvalutionSubmission> _oEmployeeEvalutionSubmissions = new List<EmployeeEvalutionSubmission>();
        EmployeeEvalutionSubmissionService _oEmployeeEvalutionSubmissionService = new EmployeeEvalutionSubmissionService();
        public ActionResult ViewEmployeeEvalutionSubmissions()
        {
            GlobalSession.SessionIsAlive(Session, Response);
            //_oEmployeeEvalutionSubmissions = _oEmployeeEvalutionSubmissionService.Gets("", (string)Session[GlobalSession.UserID]);
            ViewBag.EmployeeEvalutionSubmissions = _oEmployeeEvalutionSubmissions;
            return View();
        }
        public ActionResult ViewEmployeeEvalutionSubmission(int nID)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            User oUser = new User();
            List<User> oUsers = new List<User>();
            UserService oUserService = new UserService();
            oUsers = oUserService.Gets("SELECT TOP(1) * FROM MOB.View_Users WHERE UserCode = '" + (string)Session[GlobalSession.UserID] + "'", (string)Session[GlobalSession.UserID]);
            oUser = oUsers[0];
            _oEmployeeEvalutionSubmission = _oEmployeeEvalutionSubmissionService.Get(nID, (string)Session[GlobalSession.UserID]);
            ViewBag.EmployeeEvalutionSubmission = _oEmployeeEvalutionSubmission;
            ViewBag.User = oUser;

            EmployeeEvalutionSubmission obj = new EmployeeEvalutionSubmission();
            List<EmployeeEvalutionSubmission> objList = new List<EmployeeEvalutionSubmission>();
            EmployeeEvalutionSubmissionService objSer = new EmployeeEvalutionSubmissionService();
            objList = objSer.Gets("select TOP(1) EmployeeEvalutionSubmissionID, QuestionID, EmployeeCode, EvaluateFor, QuestionMark, CASE WHEN(select IsSubmitted from MOB.EmployeeEvalution WHERE EmployeeUsername = '"+ (string)Session[GlobalSession.UserID] + "') = 1 THEN 1 ELSE 0 END AS RelationType FROM MOB.EmployeeEvalutionSubmission WHERE EmployeeCode = (SELECT EmployeeCode FROM Users.UserInfo where UserName = '"+ (string)Session[GlobalSession.UserID] + "')", (string)Session[GlobalSession.UserID]);
            if (objList.Count > 0)
            {
                ViewBag.IsSubmitted = objList[0].RelationType;
            }
            else
                ViewBag.IsSubmitted = 0;
            ViewBag.ActivityStatus = Enum.GetValues(typeof(EnumActivityStatus)).Cast<EnumActivityStatus>().Select(e => new EnumClass { MModelPK = ((int)e), MModelString = e.ToString() });
            return View();
        }
        [HttpPost]
        public JsonResult Save(EmployeeEvalutionSubmission obj)
        {
            try
            {
                User oUser = new User();
                List<User> oUsers = new List<User>();
                UserService oUserService = new UserService();
                oUsers = oUserService.Gets("SELECT * FROM MOB.View_Users WHERE UserName = '"+ (string)Session[GlobalSession.UserID] + "'", (string)Session[GlobalSession.UserID]);

                obj.EmployeeCode = oUsers[0].EmployeeCode;
                if (obj.EmployeeEvalutionSubmissionID == 0)
                {
                    if(obj.EvaluateFor == "self")
                    {
                        obj.EvaluateFor = obj.EmployeeCode;
                    }
                    else
                    {
                        oUsers = new List<User>();
                        oUsers = oUserService.Gets("SELECT * FROM MOB.View_Users WHERE UserId = " + obj.EvaluateFor , (string)Session[GlobalSession.UserID]);
                        obj.EvaluateFor = oUsers[0].EmployeeCode;
                    }
                    _oEmployeeEvalutionSubmission = _oEmployeeEvalutionSubmissionService.IUD(obj, EnumDBOperation.Insert, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oEmployeeEvalutionSubmission = _oEmployeeEvalutionSubmissionService.IUD(obj, EnumDBOperation.Update, (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oEmployeeEvalutionSubmission = new EmployeeEvalutionSubmission();
                _oEmployeeEvalutionSubmission.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oEmployeeEvalutionSubmission);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SubmitChecker(EmployeeEvalutionSubmission obj)
        {
            bool bIsSubmitted = true; 
            try
            {
                User oUser = new User();
                List<User> oUsers = new List<User>();
                List<User> oTempUsers2 = new List<User>();
                List<User> oTempUsers3 = new List<User>();
                List<User> oTempUsers4 = new List<User>();
                UserService oUserService = new UserService();
                oUsers = oUserService.Gets("SELECT * FROM MOB.View_Users WHERE UserName = '" + (string)Session[GlobalSession.UserID] + "'", (string)Session[GlobalSession.UserID]);

                oTempUsers2 = oUserService.Gets("EXEC [MOB].[GetParticipantsList] '" + (string)Session[GlobalSession.UserID] + "', '2'", (string)Session[GlobalSession.UserID]);
                oTempUsers3 = oUserService.Gets("EXEC [MOB].[GetParticipantsList] '" + (string)Session[GlobalSession.UserID] + "', '3'", (string)Session[GlobalSession.UserID]);
                oTempUsers4 = oUserService.Gets("EXEC [MOB].[GetParticipantsList] '" + (string)Session[GlobalSession.UserID] + "', '4'", (string)Session[GlobalSession.UserID]);


                EmployeeEvalution oEmployeeEvalution = new EmployeeEvalution();
                List<EmployeeEvalution> oEmployeeEvalutions = new List<EmployeeEvalution>() { };
                List<EmployeeEvalution> oTempEmployeeEvalutions = new List<EmployeeEvalution>() { };

                EmployeeEvalutionService oEmployeeEvalutionService = new EmployeeEvalutionService();

                oEmployeeEvalutions = oEmployeeEvalutionService.Gets("EXEC [MOB].[GetEvaluationQuestion] " + "'" + oUsers[0].UserName + "'" + "," + "'" + oUsers[0].UserName + "','1'", (string)Session[GlobalSession.UserID]);
                for(int i =0; i< oTempUsers2.Count(); i++)
                {
                    oTempEmployeeEvalutions = new List<EmployeeEvalution>() { };
                    oTempEmployeeEvalutions = oEmployeeEvalutionService.Gets("EXEC [MOB].[GetEvaluationQuestion] " + "'" + oUsers[0].UserName + "'" + "," + "'" + oTempUsers2[i].UserName + "','2'", (string)Session[GlobalSession.UserID]);
                    oTempEmployeeEvalutions.ForEach(item => oEmployeeEvalutions.Add(item));
                }
                for (int i = 0; i < oTempUsers3.Count(); i++)
                {
                    oTempEmployeeEvalutions = new List<EmployeeEvalution>() { };
                    oTempEmployeeEvalutions = oEmployeeEvalutionService.Gets("EXEC [MOB].[GetEvaluationQuestion] " + "'" + oUsers[0].UserName + "'" + "," + "'" + oTempUsers3[i].UserName + "','3'", (string)Session[GlobalSession.UserID]);
                    oTempEmployeeEvalutions.ForEach(item => oEmployeeEvalutions.Add(item));
                }
                for (int i = 0; i < oTempUsers4.Count(); i++)
                {
                    oTempEmployeeEvalutions = new List<EmployeeEvalution>() { };
                    oTempEmployeeEvalutions = oEmployeeEvalutionService.Gets("EXEC [MOB].[GetEvaluationQuestion] " + "'" + oUsers[0].UserName + "'" + "," + "'" + oTempUsers4[i].UserName + "','4'", (string)Session[GlobalSession.UserID]);
                    oTempEmployeeEvalutions.ForEach(item => oEmployeeEvalutions.Add(item));
                }
                for (int i=0; i< oEmployeeEvalutions.Count(); i++)
                {
                    if (oEmployeeEvalutions[i].QuestionMark == "")
                    {
                        _oEmployeeEvalutionSubmission = new EmployeeEvalutionSubmission();
                        _oEmployeeEvalutionSubmission.ErrorMessage = "Please Give All Marks";
                        bIsSubmitted = false;
                    }
                }
                if (bIsSubmitted)
                {
                    if (oEmployeeEvalutions.Count > 0)
                    {
                        EmployeeEvalutionSubmission oEmployeeEvalutionSubmission = new EmployeeEvalutionSubmission();
                        _oEmployeeEvalutionSubmissionService.IUD(obj, EnumDBOperation.Update, (string)Session[GlobalSession.UserID]);
                    }
                    else
                    {
                        _oEmployeeEvalutionSubmission = new EmployeeEvalutionSubmission();
                        _oEmployeeEvalutionSubmission.ErrorMessage = "You are not permitted !!";
                    }
                    
                }
            }
            catch (Exception ex)
            {
                _oEmployeeEvalutionSubmission = new EmployeeEvalutionSubmission();
                _oEmployeeEvalutionSubmission.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oEmployeeEvalutionSubmission);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(EmployeeEvalutionSubmission obj)
        {
            _oEmployeeEvalutionSubmission = new EmployeeEvalutionSubmission();
            try
            {
                if (obj.EmployeeEvalutionSubmissionID > 0)
                {
                    _oEmployeeEvalutionSubmission.ErrorMessage = _oEmployeeEvalutionSubmissionService.Delete(obj.EmployeeEvalutionSubmissionID, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oEmployeeEvalutionSubmission.ErrorMessage = FeedbackMessage.NoIdFound;
                }
            }
            catch (Exception ex)
            {
                _oEmployeeEvalutionSubmission = new EmployeeEvalutionSubmission();
                _oEmployeeEvalutionSubmission.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oEmployeeEvalutionSubmission);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetUserList(EmployeeEvalutionSubmission obj)
        {
            User oUser = new User();
            List<User> oUsers = new List<User>() { };
            UserService oUserService = new UserService();
            string sSQL = "";
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    sSQL = "EXEC [MOB].[GetParticipantsList] '" + (string)Session[GlobalSession.UserID] + "', '" + Convert.ToInt16(obj.ErrorMessage) + "'";
                    //sSQL = "EXEC [MOB].[GetParticipantsList] 'sharif.abdullah', '" + Convert.ToInt16(obj.ErrorMessage) + "'";
                    oUsers = oUserService.Gets(sSQL, (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                oUser = new User();
                oUser.ErrorMessage = ex.Message;
                oUsers.Add(oUser);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(oUsers);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public JsonResult GetQuesitions(EmployeeEvalutionSubmission obj)
        {
            EmployeeEvalution oEmployeeEvalution = new EmployeeEvalution();
            List<EmployeeEvalution> oEmployeeEvalutions = new List<EmployeeEvalution>() { };
            EmployeeEvalutionService oEmployeeEvalutionService = new EmployeeEvalutionService();
            int nCount = 0;
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    string sEmployeeCode = (string)Session[GlobalSession.UserID];
                    int nRoleType = Convert.ToInt32(obj.ErrorMessage.Split('~')[nCount++]);
                    string sEvaluateFor = obj.ErrorMessage.Split('~')[nCount++];
                    
                    if(nRoleType == 1)
                    {
                        sEvaluateFor = sEmployeeCode;
                    }
                    else
                    {
                        User oUser = new User();
                        List<User> oUsers = new List<User>();
                        UserService oUserService = new UserService();
                        oUsers = oUserService.Gets("SELECT TOP(1) * FROM MOB.View_Users WHERE UserID = '"+ sEvaluateFor + "'", (string)Session[GlobalSession.UserID]);
                        sEvaluateFor = oUsers[0].UserName;
                    }
                    string sSQL = "EXEC [MOB].[GetEvaluationQuestion] " + "'" + sEmployeeCode + "'" + "," + "'" + sEvaluateFor + "'," + nRoleType;
                    oEmployeeEvalutions = oEmployeeEvalutionService.Gets(sSQL, (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                oEmployeeEvalution = new EmployeeEvalution();
                oEmployeeEvalution.ErrorMessage = ex.Message;
                oEmployeeEvalutions.Add(oEmployeeEvalution);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(oEmployeeEvalutions);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
       
        [HttpPost]
        public JsonResult Search(EmployeeEvalutionSubmission obj)
        {
            _oEmployeeEvalutionSubmission = new EmployeeEvalutionSubmission();
            _oEmployeeEvalutionSubmissions = new List<EmployeeEvalutionSubmission>() { };
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    _oEmployeeEvalutionSubmissions = _oEmployeeEvalutionSubmissionService.Gets(this.GeneralSearchMakeSQL(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oEmployeeEvalutionSubmission = new EmployeeEvalutionSubmission();
                _oEmployeeEvalutionSubmission.ErrorMessage = ex.Message;
                _oEmployeeEvalutionSubmissions.Add(_oEmployeeEvalutionSubmission);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oEmployeeEvalutionSubmissions);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        private string GeneralSearchMakeSQL(string sVal)
        {
            return "SELECT * FROM View_EmployeeEvalutionSubmission WHERE EmployeeEvalutionSubmissionName LIKE '%" + sVal + "%' OR" +
                " Address LIKE '%" + sVal + "%' OR" +
                " Remarks LIKE '%" + sVal + "%'";
        }
    }
}