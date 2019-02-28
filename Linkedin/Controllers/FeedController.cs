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

                var userList = u.context.Users.ToList();

                model.Users = new List<ApplicationUser>();

                for (int i = 0; i < 10; i++)
                {
                    if ((u.context.Users != null))
                    {
                        if (userList.Count > i)
                        {
                            if (userList[i].Id != model.ID)
                                model.Users.Add(userList[i]);
                        }
                    }
                    else
                    {
                        break;
                    }
                }



            }

            return View(model);
        }

        public ActionResult AddFriend(FeedsViewModel model)
        {

        }

    }
}
