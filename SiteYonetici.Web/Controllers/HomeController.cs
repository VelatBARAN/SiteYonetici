using SiteYonetici.BusinessLayer;
using SiteYonetici.Web.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SiteYonetici.Web.Controllers
{
    public class HomeController : Controller
    {
        [Auth]
        public ActionResult Index()
        {
            SafeManager test = new SafeManager();
            test.List();
            return View();
        }
    }
}