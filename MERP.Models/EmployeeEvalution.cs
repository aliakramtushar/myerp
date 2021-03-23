using MERP.Engine;
using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class EmployeeEvalution
    {
        #region EmployeeEvalution Defult
        public EmployeeEvalution()
        {

            ErrorMessage = "";
            EvaluateFor = "";
        }
        #endregion

        #region Properties
        public string EmployeeCode { get; set; }
        public int QuestionID { get; set; }
        public string QuestionType { get; set; }
        public string QuestionName { get; set; }
        public string GradeID { get; set; }
        public string SpecialEmpType { get; set; }
        public string QuestionMark { get; set; }
        public string EmployeeSupervisorCode { get; set; }
        public string EvaluateFor { get; set; }
        public string ErrorMessage { get; set; }

        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.QuestionID; }
        }
        public string MModelString
        {
            get { return this.QuestionName; }
        }
        #endregion

        #region Functions
        public EmployeeEvalution Get(int nId, string sUserID)
        {
            return EmployeeEvalution.Service.Get(nId, sUserID);
        }
        public EmployeeEvalution IUD(EmployeeEvalution oEmployeeEvalution, EnumDBOperation oDBOperation, string sUserID)
        {
            return EmployeeEvalution.Service.IUD(oEmployeeEvalution, oDBOperation, sUserID);
        }
        public static List<EmployeeEvalution> Gets(string sSQL, string sUserID)
        {
            return EmployeeEvalution.Service.Gets(sSQL, sUserID);
        }
        public string Delete(int id, string sUserID)
        {
            return EmployeeEvalution.Service.Delete(id, sUserID);
        }
        #endregion

        #region ServiceFactory
        internal static IEmployeeEvalution Service
        {
            get { return (IEmployeeEvalution)Services.Factory.CreateService(typeof(IEmployeeEvalution)); }
        }
        #endregion
    }
    #region IEmployeeEvalution interface
    public interface IEmployeeEvalution
    {
        EmployeeEvalution Get(int id, string sUserID);
        List<EmployeeEvalution> Gets(string sSQL, string sUserID);
        EmployeeEvalution IUD(EmployeeEvalution oEmployeeEvalution, EnumDBOperation oDBOperation, string sUserID);
        string Delete(int id, string sUserID);
    }
    #endregion
}