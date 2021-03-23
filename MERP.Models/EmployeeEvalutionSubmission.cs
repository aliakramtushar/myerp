using MERP.Engine;
using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class EmployeeEvalutionSubmission
    {
        #region EmployeeEvalutionSubmission Defult
        public EmployeeEvalutionSubmission()
        {
            EmployeeEvalutionSubmissionID = 0;
            QuestionID = 0;
            EmployeeCode = "";
            EvaluateFor = "";
            QuestionMark = "";
            RelationType = 0;
            ErrorMessage = "";
        }
        #endregion

        #region Properties
        public int EmployeeEvalutionSubmissionID { get; set; }
        public int QuestionID { get; set; }
        public string EmployeeCode { get; set; }
        public string EvaluateFor { get; set; }
        public string QuestionMark { get; set; }
        public int RelationType { get; set; }
        public string ErrorMessage { get; set; }
        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.QuestionID; }
        }
        public string MModelString
        {
            get { return this.EmployeeCode; }
        }
        #endregion

        #region Functions
        public EmployeeEvalutionSubmission Get(int nId, string sUserID)
        {
            return EmployeeEvalutionSubmission.Service.Get(nId, sUserID);
        }
        public EmployeeEvalutionSubmission IUD(EmployeeEvalutionSubmission oEmployeeEvalutionSubmission, EnumDBOperation oDBOperation, string sUserID)
        {
            return EmployeeEvalutionSubmission.Service.IUD(oEmployeeEvalutionSubmission, oDBOperation, sUserID);
        }
        public static List<EmployeeEvalutionSubmission> Gets(string sSQL, string sUserID)
        {
            return EmployeeEvalutionSubmission.Service.Gets(sSQL, sUserID);
        }

        #endregion

        #region ServiceFactory
        internal static IEmployeeEvalutionSubmission Service
        {
            get { return (IEmployeeEvalutionSubmission)Services.Factory.CreateService(typeof(IEmployeeEvalutionSubmission)); }
        }
        #endregion
    }
    #region IEmployeeEvalutionSubmission interface
    public interface IEmployeeEvalutionSubmission
    {
        EmployeeEvalutionSubmission Get(int id, string sUserID);
        List<EmployeeEvalutionSubmission> Gets(string sSQL, string sUserID);
        EmployeeEvalutionSubmission IUD(EmployeeEvalutionSubmission oEmployeeEvalutionSubmission, EnumDBOperation oDBOperation, string sUserID);
        string Delete(int id, string sUserID);
    }
    #endregion
}