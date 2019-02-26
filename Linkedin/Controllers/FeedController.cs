using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Linkedin.Models.ViewModels
{
    public class FeedController : Controller
    {
       
        public ActionResult Index()
        {
            FeedsViewModel vm = new FeedsViewModel();
            return View("Feeds", vm);
        }
    }
}