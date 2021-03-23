using MERP.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MERP.Models
{
    public class User
    {
        #region User Defult
        public User()
        {
            UserID = 0;
            UserCode = "";
            UserName = "";
            GroupID = 0;
            UserRoleId = 0;
            Password = "";
            EmployeeCode = "";
            FullName = "";
            Email = "";
            DomainID = 0;
            MobileNo = "";
            LastPasswordChangeDate = DateTime.Now;
            BranchTypeId = 0;
            BranchId = 0;
            SecurityQuestion = "";
            QuestionAns = "";
            IsBlocked = 0;
            BlockReason = "";
            BlockDate = DateTime.MinValue;
            IsInactive = 0;
            IsB2C = 0;
            IsC2B = 0;
            DateAdded = DateTime.Now;
            AddedBy = "";
            DateUpdated = DateTime.Now;
            UpdatedBy = "";
            GroupName = "";
            UserRoleName = "";
            BranchName = "";
            BranchTypeName = "";
            GradeID = "";
            EmployeeSupervisorCode = "";
            ErrorMessage = "";
        }
        #endregion

        #region Properties
        public int UserID { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public int GroupID { get; set; }
        public int UserRoleId { get; set; }
        public string Password { get; set; }
        public string EmployeeCode { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int DomainID { get; set; }
        public string MobileNo { get; set; }
        public DateTime LastPasswordChangeDate { get; set; }
        public int BranchTypeId { get; set; }
        public int BranchId { get; set; }
        public string SecurityQuestion { get; set; }
        public string QuestionAns { get; set; }
        public int IsBlocked { get; set; }
        public string BlockReason { get; set; }
        public DateTime BlockDate { get; set; }
        public int IsInactive { get; set; }
        public int IsB2C { get; set; }
        public int IsC2B { get; set; }
        public DateTime DateAdded { get; set; }
        public string AddedBy { get; set; }
        public DateTime DateUpdated { get; set; }
        public string UpdatedBy { get; set; }
        public string ErrorMessage { get; set; }
        #endregion

        #region Derived Properties
        public int MModelPK
        {
            get { return this.UserID; }
        }
        public string MModelString
        {
            get { return this.FullName; }
        }
        public string GroupName { get; set; }
        public string UserRoleName { get; set; }
        public string BranchName { get; set; }
        public string BranchTypeName { get; set; }
        public string GradeID { get; set; }
        public string EmployeeSupervisorCode { get; set; }

        #endregion

        #region Functions
        public User Get(int nId, string sUserID)
        {
            return User.Service.Get(nId, sUserID);
        }
        public User IUD(User oUser, string sUserID)
        {
            return User.Service.IUD(oUser, sUserID);
        }
        public static List<User> Gets(string sSQL, string sUserID)
        {
            return User.Service.Gets(sSQL, sUserID);
        }
        public static List<User> GetsActiveUsers(string sUserID)
        {
            return User.Service.GetsActiveUsers(sUserID);
        }
        #endregion

        #region ServiceFactory
        internal static IUser Service
        {
            get { return (IUser)Services.Factory.CreateService(typeof(IUser)); }
        }
        #endregion
    }
    #region IUser interface
    public interface IUser
    {
        User Get(int id, string sUserID);
        List<User> Gets(string sSQL, string sUserID);
        List<User> GetsActiveUsers(string sUserID);
        User IUD(User oUser, string sUserID);
        User Login(User oUser);
    }
    #endregion
}