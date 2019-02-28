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
            if (model.ApplicationUser == null)
            {
                var user = u.context.Users.Where(e => e.Id == model.ID).FirstOrDefault();
                model.ApplicationUser = user;
            }

            return View(model);
        }
    }
}