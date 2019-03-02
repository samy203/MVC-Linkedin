using Linkedin.Controllers;
using Linkedin.Layers.BL.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

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

                var friendShipList = friendMang.GetAllBind().ToList();

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
                model.Posts = postMang.GetAllBindInclude(p => p.Comments).ToList();


            }

            return View(model);
        }


        [HttpPost]
        public ActionResult AddFriend(FeedsViewModel model)
        {
            var user = u.context.Users.Where(e => e.Id == model.ID).FirstOrDefault();

            var friendUser = u.context.Users.Where(e => e.Id == model.FriendID).FirstOrDefault();

            Friend f = new Friend();
            f.Fk_ApplicationUserID = user.Id;
            f.FriendUserID = friendUser.Id;

            friendMang.Add(f);

            f = new Friend();
            f.FriendUserID = user.Id;
            f.Fk_ApplicationUserID = friendUser.Id;

            friendMang.Add(f);

            var modelFriendList = friendMang.GetAllBind().ToList()
                .Where(fr => fr.Fk_ApplicationUserID == model.ID);

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


        [HttpPost]
        public ActionResult AddPost(FeedsViewModel model)
        {
            Post post = new Post();
            post.Fk_ApplicationUserID = model.ID;
            post.Content = model.PostContent;
            post.Date = DateTime.Now;
            post.Comments = new List<Comment>();
            postMang.Add(post);

            //Add post navigation
            model.Posts = postMang.GetAllBindInclude(p => p.Comments).ToList();
            //
            return RedirectToAction("Index", model);
        }


        [HttpPost]
        public ActionResult AddComment(FeedsViewModel model)
        {
            model.CurrentPost = postMang.GetAllBindInclude(p => p.Comments).Where(p => p.Id == model.CurrentPost.Id).ToList().FirstOrDefault();
            Comment comment = new Comment();
            comment.Date = DateTime.Now;
            comment.Fk_PostID = model.CurrentPost.Id;
            comment.Fk_ApplicationUserID = model.ID;
            comment.Content = model.CommentContent;
            commentMang.Add(comment);
            return PartialView("_PartialComments", model);
        }



    }
}
