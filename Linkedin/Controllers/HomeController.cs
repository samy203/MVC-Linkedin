using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Linkedin.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult About()
        {
            return View("PersonalEdit");
        }

        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View("Personal");
        }
    }
}