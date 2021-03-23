using MERP.Engine.GlobalClass;
using MERP.Models;
using MERP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MERP.App.Controllers
{
    public class MenuController : Controller
    {
        Menu _oMenu = new Menu();
        List<Menu> _oMenus = new List<Menu>();
        TMenu _oTMenu = new TMenu();
        List<TMenu> _oTMenus = new List<TMenu>();
        MenuService _oMenuService = new MenuService();
        [HttpPost]
        public JsonResult GetAccessedMenu(User obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            //_oMenus = _oMenuService.Gets("", (string)Session[GlobalSession.UserID]);    // for development
            _oMenus = _oMenuService.GetUserAccessedMenus((string)Session[GlobalSession.UserID]);
            _oMenus = _oMenus.Where(x => x.ActivityStatus == EnumActivityStatus.Active).ToList();
            MakeMenuTree(_oMenus);
            _oTMenu = new TMenu();
            _oTMenu = GetRoot(0);
            this.AddTreeNodes(ref _oTMenu);
            //_oTMenu.Remarks = string.Join(",", _oMenus.Select(p => p.ActionName));
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oTMenu);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ViewMenus()
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oMenus = _oMenuService.Gets("", (string)Session[GlobalSession.UserID]);
            MakeMenuTree(_oMenus);
            _oTMenu = new TMenu();
            _oTMenu = GetRoot(0);
            this.AddTreeNodes(ref _oTMenu);
            ViewBag.Menus = _oTMenu;
            ViewBag.ActivityStatus = Enum.GetValues(typeof(EnumActivityStatus)).Cast<EnumActivityStatus>().Select(e => new EnumClass { MModelPK = ((int)e), MModelString = e.ToString() });
            return View();
        }
        public ActionResult ViewGroupMenuMapping(int nID)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            Group oGroup = new Group();
            GroupService oGroupService = new GroupService();
            List<Menu> oGroupMenus = new List<Menu>();
            oGroup = oGroupService.Get(nID, (string)Session[GlobalSession.UserID]);
            _oMenus = _oMenuService.Gets("", (string)Session[GlobalSession.UserID]);
            oGroupMenus = _oMenuService.GetMenusWithoutParentsByGroupID(nID, (string)Session[GlobalSession.UserID]);
            MakeMenuTree(_oMenus);
            _oTMenu = new TMenu();
            _oTMenu = GetRoot(0);
            this.AddTreeNodes(ref _oTMenu);
            ViewBag.Menus = _oTMenu;
            ViewBag.GroupMenus = oGroupMenus;
            ViewBag.Group = oGroup;
            ViewBag.ActivityStatus = Enum.GetValues(typeof(EnumActivityStatus)).Cast<EnumActivityStatus>().Select(e => new EnumClass { MModelPK = ((int)e), MModelString = e.ToString() });
            return View();
        }
        public ActionResult ViewUserMenuMapping(int nID)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            User oUser = new User();
            UserService oUserService = new UserService();
            List<Menu> oUserMenus = new List<Menu>();
            oUser = oUserService.Get(nID, (string)Session[GlobalSession.UserID]);
            _oMenus = _oMenuService.Gets("", (string)Session[GlobalSession.UserID]);
            oUserMenus = _oMenuService.GetMenusWithoutParentsByUserID(nID, (string)Session[GlobalSession.UserID]);
            MakeMenuTree(_oMenus);
            _oTMenu = new TMenu();
            _oTMenu = GetRoot(0);
            this.AddTreeNodes(ref _oTMenu);
            ViewBag.Menus = _oTMenu;
            ViewBag.UserMenus = oUserMenus;
            ViewBag.User = oUser;
            ViewBag.ActivityStatus = Enum.GetValues(typeof(EnumActivityStatus)).Cast<EnumActivityStatus>().Select(e => new EnumClass { MModelPK = ((int)e), MModelString = e.ToString() });
            return View();
        }
        [HttpPost]
        public JsonResult Save(Menu obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            try
            {
                if (obj.MenuID == 0)
                {
                    _oMenu = _oMenuService.IUD(obj, EnumDBOperation.Insert, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oMenu = _oMenuService.IUD(obj, EnumDBOperation.Update, (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oMenu = new Menu();
                _oMenu.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oMenu);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(TMenu oTMenu)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oMenu = new Menu();
            _oMenu.MenuID = oTMenu.id;
            _oMenu.ParentID = oTMenu.parentid;

            try
            {
                if (_oMenu.MenuID > 0)
                {
                    _oMenu.ErrorMessage = _oMenuService.Delete(_oMenu.MenuID, (string)Session[GlobalSession.UserID]);
                }
                else
                {
                    _oMenu.ErrorMessage = FeedbackMessage.NoIdFound;
                }
            }
            catch (Exception ex)
            {
                _oMenu = new Menu();
                _oMenu.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oMenu);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetMenuByID(Menu obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oMenu = new Menu();
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    _oMenu = _oMenuService.Get(Convert.ToInt32(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oMenu = new Menu();
                _oMenu.ErrorMessage = ex.Message;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oMenu);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Search(Menu obj)
        {
            GlobalSession.SessionIsAlive(Session, Response);
            _oMenu = new Menu();
            _oMenus = new List<Menu>() { };
            try
            {
                if (!string.IsNullOrEmpty(obj.ErrorMessage))
                {
                    _oMenus = _oMenuService.Gets(this.GeneralSearchMakeSQL(obj.ErrorMessage), (string)Session[GlobalSession.UserID]);
                }
            }
            catch (Exception ex)
            {
                _oMenu = new Menu();
                _oMenu.ErrorMessage = ex.Message;
                _oMenus.Add(_oMenu);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string sjson = serializer.Serialize(_oMenus);
            return Json(sjson, JsonRequestBehavior.AllowGet);
        }
        private string GeneralSearchMakeSQL(string sVal)
        {
            return "SELECT * FROM View_Menu WHERE MenuName LIKE '%" + sVal + "%' OR" +
                " Address LIKE '%" + sVal + "%' OR" +
                " Remarks LIKE '%" + sVal + "%'";
        }


        #region EasyUI Tree Menu Functions
        private void MakeMenuTree(List<Menu> oMenus)
        {
            foreach (Menu oItem in oMenus)
            {
                _oTMenu = new TMenu();
                _oTMenu.id = oItem.MenuID;
                _oTMenu.parentid = oItem.ParentID;
                _oTMenu.text = oItem.MenuName;
                _oTMenu.ControllerName = oItem.ControllerName;
                _oTMenu.ActionName = oItem.ActionName;
                _oTMenu.MenuSequence = oItem.MenuSequence;
                _oTMenu.ActivityStatus = oItem.ActivityStatus;
                _oTMenu.Remarks = oItem.Remarks;
                if (oItem.ControllerName == "xxx" && oItem.ActionName == "xxx")
                {
                    _oTMenu.state = "closed";
                }
                if (oItem.ParentID == 0)
                {
                    _oTMenu.state = "open";
                }
                _oTMenus.Add(_oTMenu);
            }
        }
        private Menu GetRoot()
        {
            Menu oMenu = new Menu();
            foreach (Menu oItem in _oMenus)
            {
                if (oItem.ParentID == 0)
                {
                    return oItem;
                }
            }
            return oMenu;
        }
        private IEnumerable<Menu> GetChild(int nMenuID)
        {
            List<Menu> oMenus = new List<Menu>();
            foreach (Menu oItem in _oMenus)
            {
                if (oItem.ParentID == nMenuID)
                {
                    oMenus.Add(oItem);
                }
            }
            return oMenus;
        }

        private void AddTreeNodes(ref Menu oMenu)
        {
            IEnumerable<Menu> oChildNodes;
            oChildNodes = GetChild(oMenu.MenuID);
            oMenu.ChildNodes = oChildNodes;

            foreach (Menu oItem in oChildNodes)
            {
                Menu oTemp = oItem;
                AddTreeNodes(ref oTemp);
            }
        }
        private IEnumerable<TMenu> GetChildren(int nAccountHeadID)
        {
            List<TMenu> oTMenus = new List<TMenu>();
            foreach (TMenu oItem in _oTMenus)
            {
                if (oItem.parentid == nAccountHeadID)
                {
                    oTMenus.Add(oItem);
                }
            }
            return oTMenus;
        }
        private void AddTreeNodes(ref TMenu oTMenu)
        {
            IEnumerable<TMenu> oChildNodes;
            oChildNodes = GetChildren(oTMenu.id);
            oTMenu.children = oChildNodes;

            foreach (TMenu oItem in oChildNodes)
            {
                TMenu oTemp = oItem;
                AddTreeNodes(ref oTemp);
            }
        }
        private TMenu GetRoot(int nParentID)
        {
            TMenu oTMenu = new TMenu();
            foreach (TMenu oItem in _oTMenus)
            {
                if (oItem.parentid == nParentID)
                {
                    return oItem;
                }
            }
            return _oTMenu;
        }

        #endregion


    }
}