using MERP.Engine.GlobalClass;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.DataAccess
{
    public class EmployeeEvalutionSubmissionDA
    {
        public static string IUD(EmployeeEvalutionSubmission oEmployeeEvalutionSubmission, EnumDBOperation oDBOperation, string sUserID)
        {
            return GlobalHelpers.ExcecuteQurey("EXEC MOB.SP_IUD_EmployeeEvalutionSubmission",
                oEmployeeEvalutionSubmission.EmployeeEvalutionSubmissionID, oEmployeeEvalutionSubmission.QuestionID, oEmployeeEvalutionSubmission.EmployeeCode, oEmployeeEvalutionSubmission.EvaluateFor, 
                oEmployeeEvalutionSubmission.QuestionMark, oEmployeeEvalutionSubmission.RelationType,  sUserID, (int)oDBOperation);
        }
        public static string Gets(string sSQL, string sUserID)
        {
            if (sSQL == "") return "SELECT * FROM MOB.EmployeeEvalutionSubmission";
            else return sSQL;
        }
        public static string Get(int nID, string sUserID)
        {
            return "SELECT * FROM MOB.EmployeeEvalutionSubmission WHERE QuestionID =" + nID;
        }
        public static string Delete(int nEmployeeEvalutionSubmissionID, string sUserID)
        {
            //return "DELETE FROM EmployeeEvalutionSubmission WHERE EmployeeEvalutionSubmissionID = " + nEmployeeEvalutionSubmissionID;
            return "";
        }
    }
}