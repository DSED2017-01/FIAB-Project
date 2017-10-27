using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FishInABox.Controllers
{
    public class SystemController : Controller
    {
        // GET: System
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InComplete()
        {
            return View();
        }

        public ActionResult TankInfoLoader()
        {
            return View();
        }
    }
}