using Linkedin.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Linkedin.Models.ViewModels
{
    [Authorize]
    public class FeedController : ParentController
    {

        public ActionResult Index(FeedsViewModel model)
        {
            return View(model);
        }
    }
}