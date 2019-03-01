using Linkedin.Controllers;
using Linkedin.Layers.BL.Managers;
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

                var friendShipList = u.GetManager<FriendManager>().GetAllBind().ToList();

                var modelFriendList = friendShipList.Where(fr => fr.Fk_ApplicationUserID == model.ID);

                List<string> friends = new List<string>();

                foreach (var friend in modelFriendList)
                {
                    friends.Add(friend.FriendUserID);
                }

                userList.RemoveAll(u => friends.Contains(u.Id) || u.Id == model.ID);

                model.Users = new List<ApplicationUser>();

                for (int i = 0; i < 10; i++)
                {
                    if ((u.context.Users != null))
                    {
                        if (userList.Count > i)
                        {
                            model.Users.Add(userList[i]);
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                if (model.FriendsId == null)
                    model.FriendsId = new List<string>();

            }

            return View(model);
        }


        [HttpPost]
        public ActionResult Add(FeedsViewModel model)
        {
            var user = u.context.Users.Where(e => e.Id == model.ID).FirstOrDefault();

            var friendUser = u.context.Users.Where(e => e.Id == model.FriendID).FirstOrDefault();

            Friend f = new Friend();

            f.Fk_ApplicationUserID = user.Id;

            f.FriendUserID = friendUser.Id;

            u.GetManager<FriendManager>().Add(f);

            f = new Friend();
            f.FriendUserID = user.Id;
            f.Fk_ApplicationUserID = friendUser.Id;

            u.GetManager<FriendManager>().Add(f);

            var friendShipList = u.GetManager<FriendManager>().GetAllBind().ToList();

            var modelFriendList = friendShipList.Where(fr => fr.Fk_ApplicationUserID == model.ID);

            List<string> friends = new List<string>();

            foreach (var friend in modelFriendList)
            {
                friends.Add(friend.FriendUserID);
            }

            model.FriendsId = friends;

            model.ApplicationUser = user;

            var userList = u.context.Users.ToList();

            userList.RemoveAll(l => l.Id == model.ID);

            model.Users = userList;

            return PartialView("_PartialFriendContainer", model);
        }

    }
}
