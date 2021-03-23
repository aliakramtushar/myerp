using MERP.Engine.GlobalClass;
using MERP.Models;
using MERP.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MERP.App.Controllers
{
    public class StudentInfoController : Controller
    {
        StudentInfo _oStudentInfo = new StudentInfo();
        List<StudentInfo> _oStudentInfos = new List<StudentInfo>();
        StudentInfoService _oStudentInfoService = new StudentInfoService();
        public ActionResult ViewStudentInfos()
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oStudentInfos = _oStudentInfoService.Gets("", (string)Session[GlobalSession.UserID]);
            ViewBag.StudentInfos = _oStudentInfos;
            return View();
        }
        public ActionResult ViewStudentInfo(int nID)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oStudentInfo = new StudentInfo();
            if (nID > 0)
            {
                _oStudentInfo = _oStudentInfoService.Get(nID, (string)Session[GlobalSession.UserID]);
            }
            else
            {
                _oStudentInfo.ErrorMessage = FeedbackMessage.NoDataFound;
            }
            ViewBag.StudentTypes = Enum.GetValues(typeof(EnumEmployeeType)).Cast<EnumEmployeeType>().Select(e => new EnumClass { MModelPK = ((int)e), MModelString = e.ToString() });
            ViewBag.ReferenceTypes = Enum.GetValues(typeof(EnumRelation)).Cast<EnumRelation>().Select(e => new EnumClass { MModelPK = ((int)e), MModelString = e.ToString() });
            ViewBag.GenderTypes = Enum.GetValues(typeof(EnumGender)).Cast<EnumGender>().Select(e => new EnumClass { MModelPK = ((int)e), MModelString = e.ToString() });
            ViewBag.Religions = Enum.GetValues(typeof(EnumReligion)).Cast<EnumReligion>().Select(e => new EnumClass { MModelPK = ((int)e), MModelString = e.ToString() });
            ViewBag.ActivityStatus = Enum.GetValues(typeof(EnumActivityStatus)).Cast<EnumActivityStatus>().Select(e => new EnumClass { MModelPK = ((int)e), MModelString = e.ToString() });
            ViewBag.StudentInfo = _oStudentInfo;
            return View();
        }
        [HttpPost]
        public JsonResult Save(StudentInfo obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            try
            {
                if (obj.StudentID == 0)
                {
                    _oStudentInfo = _oStudentInfoService.IUD(obj, EnumDBOperation.Insert, (string)Session[GlobalSession.UserID]); ;
                }
                else
                {
                    _oStudentInfo = _oStudentInfoService.IUD(obj, EnumDBOperation.Update, (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oStudentInfo = new StudentInfo();
                _oStudentInfo.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oStudentInfo);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UploadImage(int nStudentID)
        {
            var file = Request.Files[0];
            if (file != null && nStudentID!=0)
            {
                StudentInfo oStudentInfo = new StudentInfo();
                StudentInfoService oStudentInfoService = new StudentInfoService();
                oStudentInfo = oStudentInfoService.Get(nStudentID, (string)Session[GlobalSession.UserID]);

                if (file.ContentType != "image/jpeg")
                {
                    return Json(new { ErrorMessage = "Invalid file type, Please Select only JPG/JPEG Format !!" }, JsonRequestBehavior.AllowGet);
                }
                if (file.ContentLength > 100000)
                {
                    return Json(new { ErrorMessage = "Maximum file size exceeded. Please Select a file less than 100kb" }, JsonRequestBehavior.AllowGet);
                }
                if (oStudentInfo.StudentID > 0 && file.ContentType == "image/jpeg")
                {
                    string sFilePath = "/Content/img/StudentInfos/";
                    string sFullFilePath = sFilePath + oStudentInfo.StudentCode + "." + file.ContentType.Split('/')[1];
                    file.SaveAs(Server.MapPath(sFullFilePath));
                    oStudentInfo.StudentPhotoPath = sFullFilePath;
                    oStudentInfo = oStudentInfoService.IUD(oStudentInfo, EnumDBOperation.Update, (string)Session[GlobalSession.UserID]);
                    return Json(new { ErrorMessage = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { ErrorMessage = "No File" }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(StudentInfo obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oStudentInfo = new StudentInfo();
            try
            {
                if (obj.StudentID > 0)
                {
                    this.DeleteImage(obj.StudentID);
                    _oStudentInfo = _oStudentInfoService.IUD(obj, EnumDBOperation.Delete, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oStudentInfo.ErrorMessage = FeedbackMessage.NoIdFound;
                }
            }
            catch (Exception ex)
            {
                _oStudentInfo = new StudentInfo();
                _oStudentInfo.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oStudentInfo);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        public string DeleteImage(int nStudentID)
        {
            if (nStudentID <= 0)
            {
                return "No Student Found";
            }
            else
            {
                StudentInfo oStudentInfo = new StudentInfo();
                StudentInfoService oStudentInfoService = new StudentInfoService();
                oStudentInfo = oStudentInfoService.Get(nStudentID, (string)Session[GlobalSession.UserID]);
                if (oStudentInfo.StudentID == 0) return "No Student Found";
                if (oStudentInfo.StudentPhotoPath == "") return "No photo found for " + oStudentInfo.StudentFullName;
                try
                {
                    //string[] files = Directory.GetFiles(oStudentInfo.StudentPhotoPath);
                    //FileInfo fl = new FileInfo(@ "~\Content\img\StudentInfos\ST-0000012021.jpeg");
                    //fl.Delete();
                    //foreach (string file in files)
                    //{
                    //    var temp = File.Delete(oStudentInfo.StudentPhotoPath);
                    //}
                    var dir = new DirectoryInfo(Directory.GetCurrentDirectory() + oStudentInfo.StudentPhotoPath);
                    var dir1 = new DirectoryInfo(@"C:\Program Files (x86)\IIS Express\Content\img\StudentInfos\ST-0000012021.jpeg");
                    string asd = Directory.GetCurrentDirectory();
                    //dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
                    //dir.Delete(true);
                    return "";
                }
                catch (System.IO.IOException ex)
                {
                    return ex.Message;
                }
            }
        }
        [HttpPost]
        public JsonResult Search(StudentInfo obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oStudentInfo = new StudentInfo();
            _oStudentInfos = new List<StudentInfo>() { };
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    _oStudentInfos = _oStudentInfoService.Gets(this.GeneralSearchMakeSQL(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oStudentInfo = new StudentInfo();
                _oStudentInfo.ErrorMessage = ex.Message;
                _oStudentInfos.Add(_oStudentInfo);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oStudentInfos);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        private string GeneralSearchMakeSQL(string sVal)
        {
            return "SELECT * FROM PSDP.View_StudentInfo WHERE "
                                + " StudentInfoName LIKE '%" + sVal + "%' OR"
                                + " Remarks LIKE '%" + sVal + "%'";

        }
    }
}