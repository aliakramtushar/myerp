using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Script.Serialization;
using System.Collections;

namespace MERP.Engine.GlobalClass
{
    public static class GlobalSession
    {
        public static string UserID = "UserID";
        public static string UserName = "UserName";
        public static string GroupID = "GroupID";
        public static string BaseAddress = "BaseAddress";
        public static string UserType = "UserType";
        public static string MUser = "MUser";
        public static string MenuID = "MenuID";
        public static string BranchID = "BranchID";
        public static string BranchTypeID = "BranchTypeID";
        public static string UserMenuIDs = "UserMenuIDs";

        public static void SessionIsAlive(HttpSessionStateBase Session, HttpResponseBase Response, bool IsFromHomeControllerDashboard = false)
        {
            if (Session.Contents.Count == 0)
            {
                if (IsFromHomeControllerDashboard)
                {
                    Response.Redirect("/Login/ViewLogin");
                }
                else
                {
                    Response.Redirect("/Login/TimeOut");
                }
            }
            else
            {
                Session[GlobalSession.UserID] = Session[GlobalSession.UserName];
                Session[GlobalSession.UserName] = Session[GlobalSession.UserName];
                Session[GlobalSession.MUser] = Session[GlobalSession.MUser];
                Session[GlobalSession.GroupID] = Session[GlobalSession.GroupID];
                Session[GlobalSession.BranchID] = Session[GlobalSession.BranchID];
                Session[GlobalSession.BranchTypeID] = Session[GlobalSession.BranchTypeID];
                Session[GlobalSession.UserMenuIDs] = Session[GlobalSession.UserMenuIDs];
            }
        }
    }
}