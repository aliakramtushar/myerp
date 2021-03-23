using MERP.Engine.GlobalClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MERP.App.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Dashboard()
        {
            GlobalSession.SessionIsAlive(Session, Response, true);
            return View();
        }
    }
}