using MERP.Engine;
using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class StudentTrainingMapping
    {
        #region StudentTrainingMapping Defult
        public StudentTrainingMapping()
        {
            StudentTrainingMappingID = 0;
            StudentID = 0;
            TrainingID = 0;
            BranchID = 0;
            Amount = 0;
            DiscountAmount = 0;
            PaidAmount = 0;
            AdjustmentAmount = 0;
            ReferenceType = EnumReferenceType.None;
            ReferenceName = "";
            ReferenceContactNo = "";
            StartDate = DateTime.Now;
            Remarks = "";
            BlankField_1 = "";
            BlankField_2 = "";
            BlankField_3 = "";
            BlankField_4 = 0;
            BlankField_5 = DateTime.Now;
            ErrorMessage = "";
        }
        #endregion

        #region Properties
        public int StudentTrainingMappingID { get; set; }
        public int StudentID { get; set; }
        public int TrainingID { get; set; }
        public int BranchID { get; set; }
        public double Amount { get; set; }
        public double DiscountAmount { get; set; }
        public double PaidAmount { get; set; }
        public double AdjustmentAmount { get; set; }
        public EnumReferenceType ReferenceType { get; set; }
        public string ReferenceName { get; set; }
        public string ReferenceContactNo { get; set; }
        public DateTime StartDate { get; set; }
        public string Remarks { get; set; }
        public string BlankField_1 { get; set; }
        public string BlankField_2 { get; set; }
        public string BlankField_3 { get; set; }
        public int BlankField_4 { get; set; }
        public DateTime BlankField_5 { get; set; }
        public string ErrorMessage { get; set; }

        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.TrainingID; }
        }
        public string MModelString
        {
            get { return this.Remarks; }
        }
        public string ReferenceTypeSt
        {
            get { return this.ReferenceType.ToString(); }
        }
        public string StartDateSt
        {
            get { return (this.StartDate == DateTime.MinValue) ? "" : this.StartDate.ToString("dd-MM-yyyy"); }
        }
        public string AmountSt
        {
            get { return this.Amount.ToString(); }
        }
        public string DiscountAmountSt
        {
            get { return this.DiscountAmount.ToString(); }
        }
        public double GrandTotalAmount
        {
            get { return (Amount - DiscountAmount - AdjustmentAmount); }
        }
        public string GrandTotalAmountSt
        {
            get { return this.GrandTotalAmount.ToString(); }
        }
        public string PaidAmountSt
        {
            get { return this.PaidAmount.ToString(); }
        }
        public double DueAmount
        {
            get { return GrandTotalAmount - PaidAmount; }
        }
        public string DueAmountSt
        {
            get { return this.DueAmount.ToString(); }
        }
        public string AdjustmentAmountSt
        {
            get { return this.AdjustmentAmount.ToString(); }
        }
        public string BlankField_5St
        {
            get { return (this.BlankField_5 == DateTime.MinValue) ? "" : this.BlankField_5.ToString("dd-MM-yyyy"); }
        }
        #endregion

        #region Functions
        public StudentTrainingMapping Get(int nId, string sUserID)
        {
            return StudentTrainingMapping.Service.Get(nId, sUserID);
        }
        public StudentTrainingMapping IUD(StudentTrainingMapping oStudentTrainingMapping, EnumDBOperation oDBOperation, string sUserID)
        {
            return StudentTrainingMapping.Service.IUD(oStudentTrainingMapping, oDBOperation, sUserID);
        }
        public static List<StudentTrainingMapping> Gets(string sSQL, string sUserID)
        {
            return StudentTrainingMapping.Service.Gets(sSQL, sUserID);
        }
        #endregion

        #region ServiceFactory
        internal static IStudentTrainingMapping Service
        {
            get { return (IStudentTrainingMapping)Services.Factory.CreateService(typeof(IStudentTrainingMapping)); }
        }
        #endregion
    }
    #region IStudentTrainingMapping interface
    public interface IStudentTrainingMapping
    {
        StudentTrainingMapping Get(int id, string sUserID);
        List<StudentTrainingMapping> Gets(string sSQL, string sUserID);
        StudentTrainingMapping IUD(StudentTrainingMapping oStudentTrainingMapping, EnumDBOperation oDBOperation, string sUserID);
    }
    #endregion
}