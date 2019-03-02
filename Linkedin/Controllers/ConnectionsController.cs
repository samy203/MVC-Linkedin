using Linkedin.Layers.BL.Managers;
using Linkedin.Models;
using Linkedin.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Linkedin.Controllers
{
    public class ConnectionsController : ParentController
    {
        public ActionResult Index(FeedsViewModel model)
        {
            if (model.ApplicationUser == null)
            {
                var user = u.context.Users.Where(e => e.Id == model.ID).FirstOrDefault();

                model.ApplicationUser = user;

                model.FriendsData = new List<ApplicationUser>();

                var friendlist = u.GetManager<FriendManager>().GetAllBind().Where(f => f.Fk_ApplicationUserID == user.Id).ToList();

                foreach (var friend in friendlist)
                {
                    model.FriendsData.AddRange(u.context.Users.Where(k => k.Id == friend.FriendUserID).ToList());
                }

            }

            return View(model);
        }

        public ActionResult Delete(FeedsViewModel model)
        {
            var user = u.context.Users.Where(e => e.Id == model.ID).FirstOrDefault();


            model.ApplicationUser = user;
            

            var toBeDeletedFriend = u.GetManager<FriendManager>().GetAllBind().Where(f => f.FriendUserID== model.DeletedFriendID).FirstOrDefault();


            var toBeDeletedOtherFriend = u.GetManager<FriendManager>().GetAllBind().Where(f => f.Fk_ApplicationUserID == model.DeletedFriendID).FirstOrDefault();


            u.context.Friends.Remove(toBeDeletedFriend);


            u.context.Friends.Remove(toBeDeletedOtherFriend);


            u.context.SaveChanges();


            model.FriendsData = new List<ApplicationUser>();


            var friendlist = u.GetManager<FriendManager>().GetAllBind().Where(f => f.Fk_ApplicationUserID == user.Id).ToList();

            foreach (var friend in friendlist)
            {
                model.FriendsData.AddRange(u.context.Users.Where(k => k.Id == friend.FriendUserID).ToList());
            }

            return PartialView("_PartialConnections", model);

        }

    }
}