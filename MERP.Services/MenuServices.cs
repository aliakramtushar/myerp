using MERP.DataAccess;
using MERP.Engine;
using MERP.Engine.GlobalClass;
using MERP.Engine.GlobalGateway;
using MERP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MERP.Services
{
    public class MenuService : CommonGateway, IMenu
    {
        #region Maping
        private Menu MapObject(NullHandler oReader)
        {
            Menu oMenu = new Menu();
            oMenu.MenuID = oReader.GetInt32("MenuID");
            oMenu.MenuName = oReader.GetString("MenuName");
            oMenu.ParentID = oReader.GetInt32("ParentID");
            oMenu.ControllerName = oReader.GetString("ControllerName");
            oMenu.ActionName = oReader.GetString("ActionName");
            oMenu.MenuSequence = oReader.GetInt32("MenuSequence");
            oMenu.ActivityStatus = (EnumActivityStatus)oReader.GetInt16("ActivityStatus");
            oMenu.Remarks = oReader.GetString("Remarks");
            return oMenu;
        }
        private Menu MakeObject(NullHandler oReader)
        {
            Menu oMenu = new Menu();
            oMenu = MapObject(oReader);
            return oMenu;
        }
        private List<Menu> MakeObjects(IDataReader oReader)
        {
            List<Menu> oMenus = new List<Menu>();
            NullHandler oHandler = new NullHandler(oReader);
            while (oReader.Read())
            {
                Menu oMenu = MapObject(oHandler);
                oMenus.Add(oMenu);
            }
            return oMenus;
        }
        #endregion


        #region Function Implementation
        public Menu IUD(Menu oMenu, EnumDBOperation oDBOperation, string sUserID)
        {
            Menu _oMenu = new Menu();
            try
            {
                Connection.Open();
                Command.CommandText = MenuDA.IUD(oMenu, oDBOperation, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oMenu = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oMenu = new Menu();
                _oMenu.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oMenu;
        }
        public List<Menu> Gets(string sSQL, string sUserID)
        {
            Menu _oMenu = new Menu();
            List<Menu> _oMenus = new List<Menu>();
            try
            {
                Connection.Open();
                Command.CommandText = MenuDA.Gets(sSQL, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oMenus = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oMenu = new Menu();
                _oMenus = new List<Menu>();
                _oMenu.ErrorMessage = e.Message.Split('~')[1];
                _oMenus.Add(_oMenu);
            }
            return _oMenus;
        }
        public List<Menu> GetUserAccessedMenus(string sUserID)
        {
            Menu _oMenu = new Menu();
            List<Menu> _oMenus = new List<Menu>();
            try
            {
                Connection.Open();
                Command.CommandText = MenuDA.GetUserAccessedMenus(sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oMenus = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oMenu = new Menu();
                _oMenus = new List<Menu>();
                _oMenu.ErrorMessage = e.Message.Split('~')[1];
                _oMenus.Add(_oMenu);
            }
            return _oMenus;
        }
        public List<Menu> GetMenusByGroupID(int nGroupID, string sUserID)
        {
            Menu _oMenu = new Menu();
            List<Menu> _oMenus = new List<Menu>();
            try
            {
                Connection.Open();
                Command.CommandText = MenuDA.GetMenusByGroupID(nGroupID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oMenus = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oMenu = new Menu();
                _oMenus = new List<Menu>();
                _oMenu.ErrorMessage = e.Message.Split('~')[1];
                _oMenus.Add(_oMenu);
            }
            return _oMenus;
        }
        public List<Menu> GetMenusWithoutParentsByGroupID(int nGroupID, string sUserID)
        {
            Menu _oMenu = new Menu();
            List<Menu> _oMenus = new List<Menu>();
            try
            {
                Connection.Open();
                Command.CommandText = MenuDA.GetMenusWithoutParentsByGroupID(nGroupID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oMenus = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oMenu = new Menu();
                _oMenus = new List<Menu>();
                _oMenu.ErrorMessage = e.Message.Split('~')[1];
                _oMenus.Add(_oMenu);
            }
            return _oMenus;
        }
        public List<Menu> GetMenusWithoutParentsByUserID(int nUserID, string sUserID)
        {
            Menu _oMenu = new Menu();
            List<Menu> _oMenus = new List<Menu>();
            try
            {
                Connection.Open();
                Command.CommandText = MenuDA.GetMenusWithoutParentsByUserID(nUserID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                _oMenus = MakeObjects(reader);
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oMenu = new Menu();
                _oMenus = new List<Menu>();
                _oMenu.ErrorMessage = e.Message.Split('~')[1];
                _oMenus.Add(_oMenu);
            }
            return _oMenus;
        }

        public Menu Get(int nID, string sUserID)
        {
            Menu _oMenu = new Menu();
            try
            {
                Connection.Open();
                Command.CommandText = MenuDA.Get(nID, sUserID);
                IDataReader reader = Command.ExecuteReader();
                NullHandler oReader = new NullHandler(reader);
                if (reader.Read())
                {
                    _oMenu = MakeObject(oReader);
                }
                reader.Close();
                Connection.Close();
            }
            catch (Exception e)
            {
                _oMenu = new Menu();
                _oMenu.ErrorMessage = e.Message.Split('~')[1];
            }
            return _oMenu;
        }
        public string Delete(int nMenuID, string sUserID)
        {
            try
            {
                Connection.Open();
                Command.CommandText = MenuDA.Delete(nMenuID, sUserID);
                Command.ExecuteNonQuery();
                Connection.Close();
            }
            catch (Exception e)
            {
                FeedbackMessage.Deleted = e.Message.Split('~')[0];
            }
            return FeedbackMessage.Deleted;
        }
        #endregion
    }
}