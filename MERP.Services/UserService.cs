using MERP.DataAccess;
using MERP.Engine.GlobalClass;
using MERP.Engine.GlobalGateway;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MERP.Services
{
    public class UserService : CommonGateway, IUser
    {
        #region Maping
        private User MapObject(SqlDataReader oReader)
        {
            User oUser = new User();
            oUser.UserID = (int)oReader["UserID"];
            oUser.UserCode = oReader["UserCode"].ToString();
            oUser.UserName = oReader["UserName"].ToString();
            oUser.GroupID = (int)oReader["GroupID"];
            oUser.UserRoleId = (int)oReader["UserRoleId"];
            oUser.Password = oReader["Password"].ToString();
            oUser.EmployeeCode = oReader["EmployeeCode"].ToString();
            oUser.FullName = oReader["FullName"].ToString();
            oUser.Email = oReader["Email"].ToString();
            oUser.DomainID = (int)oReader["DomainID"];
            oUser.MobileNo = oReader["MobileNo"].ToString();
            //oUser.LastPasswordChangeDate = (DateTime)oReader["LastPasswordChangeDate"];
            oUser.BranchTypeId = (int)oReader["BranchTypeId"];
            oUser.BranchId = (int)oReader["BranchId"];
            oUser.SecurityQuestion = oReader["SecurityQuestion"].ToString();
            oUser.QuestionAns = oReader["QuestionAns"].ToString();
            oUser.IsBlocked = (int)oReader["IsBlocked"];
            oUser.BlockReason = oReader["BlockReason"].ToString();
            //oUser.BlockDate = (DateTime)oReader["BlockDate"];
            oUser.IsInactive = (int)oReader["IsInactive"];
            oUser.IsB2C = (int)oReader["IsB2C"];
            oUser.IsC2B = (int)oReader["IsC2B"];
            //oUser.DateAdded = (DateTime)oReader["DateAdded"];
            oUser.AddedBy = oReader["AddedBy"].ToString();
            //oUser.DateUpdated = (DateTime)oReader["DateUpdated"];
            oUser.UpdatedBy = oReader["UpdatedBy"].ToString();
            oUser.GroupName = oReader["GroupName"].ToString();
            oUser.UserRoleName = oReader["UserRoleName"].ToString();
            oUser.BranchName = oReader["BranchName"].ToString();
            oUser.BranchTypeName = oReader["BranchTypeName"].ToString();
            oUser.GradeID = oReader["GradeID"].ToString();
            oUser.EmployeeSupervisorCode = oReader["EmployeeSupervisorCode"].ToString();
            return oUser;
        }
        private User MakeObject(SqlDataReader oReader)
        {
            oReader.Read();
            User oUser = new User();
            oUser = MapObject(oReader);
            return oUser;
        }
        private List<User> MakeObjects(SqlDataReader oReader)
        {
            List<User> oUsers = new List<User>();
            while (oReader.Read())
            {
                User oUser = MapObject(oReader);
                oUsers.Add(oUser);
            }
            return oUsers;
        }
        #endregion
        #region Function Implementation
        public User IUD(User oUser, string sUserID)
        {
            Connection.Open();
            if (oUser.UserID == 0)
            {
                Command.CommandText = UserDA.IUD(oUser, EnumDBOperation.Insert, sUserID);
            }
            else
            {
                Command.CommandText = UserDA.IUD(oUser, EnumDBOperation.Update, sUserID);
            }
            SqlDataReader reader = Command.ExecuteReader();
            User _oUser = new User();
            if (reader.HasRows)
            {
                _oUser = MakeObject(reader);
            }
            reader.Close();
            Connection.Close();
            return _oUser;
        }
        public List<User> Gets(string sSQL, string sUserID)
        {
            Connection.Open();
            Command.CommandText = UserDA.Gets(sSQL, sUserID);

            SqlDataReader reader = Command.ExecuteReader();
            User _oUser = new User();
            List<User> _oUsers = new List<User>();
            if (reader.HasRows)
            {
                _oUsers = MakeObjects(reader);
            }
            reader.Close();
            Connection.Close();
            return _oUsers;
        }
        public List<User> GetsActiveUsers(string sUserID)
        {
            Connection.Open();
            Command.CommandText = UserDA.GetsActiveUsers(sUserID);

            SqlDataReader reader = Command.ExecuteReader();
            User _oUser = new User();
            List<User> _oUsers = new List<User>();
            if (reader.HasRows)
            {
                _oUsers = MakeObjects(reader);
            }
            reader.Close();
            Connection.Close();
            return _oUsers;
        }

        
        public User Get(int nID, string sUserID)
        {
            Connection.Open();
            Command.CommandText = UserDA.Get(nID, sUserID);

            SqlDataReader reader = Command.ExecuteReader();
            User _oUser = new User();
            if (reader.HasRows)
            {
                _oUser = MakeObject(reader);
            }
            reader.Close();
            Connection.Close();
            return _oUser;
        }
        public User Login(User oUser)
        {
            Connection.Open();
            Command.CommandText = UserDA.Login(oUser);

            SqlDataReader reader = Command.ExecuteReader();
            User _oUser = new User();
            if (reader.HasRows)
            {
                _oUser = MakeObject(reader);
            }
            reader.Close();
            Connection.Close();
            return _oUser;
        }
        #endregion
    }
}