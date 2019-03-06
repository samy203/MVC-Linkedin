using Linkedin.Controllers;
using Linkedin.Layers.BL.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading;

namespace Linkedin.Models.ViewModels
{
    [System.Web.Mvc.Authorize]
    public class FeedController : ParentController
    {
        public ActionResult Index(FeedsViewModel model)
        {
            if (model.ApplicationUser == null)
            {
                ApplicationUser user;
                List<ApplicationUser> userList;
                List<Friend> friendShipList;
                using (var context = new ApplicationDbContext())
                {
                    user = context.Users.Where(e => e.Id == model.ID).FirstOrDefault();
                    userList = context.Users.ToList();
                }

                model.ApplicationUser = user;

                model.ApplicationUser.Image = u.context.Images.Where(i => i.ImageId == model.ApplicationUser.Id).FirstOrDefault();

                var userList = u.context.Users.Include(f=>f.Image).ToList();

                using (var context = new ApplicationDbContext())
                {
                    friendShipList = context.Friends.ToList();
                }



                var modelFriendList = friendShipList.Where(fr => fr.Fk_ApplicationUserID == model.ID);

                List<string> friends = new List<string>();

                foreach (var friend in modelFriendList)
                {
                    friends.Add(friend.FriendUserID);
                }

                userList.RemoveAll(u => friends.Contains(u.Id) || u.Id == model.ID);

                model.Users = new List<ApplicationUser>();
                using (var context = new ApplicationDbContext())
                {
                    for (int i = 0; i < 10; i++)
                    {

                        if ((context.Users != null))
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
                }
                if (model.FriendsId == null)
                    model.FriendsId = new List<string>();

                model.Posts = NavigatePosts(model.ID);

            }

            return View(model);
        }


        private List<Post> NavigatePosts(string userID)
        {
            ApplicationUser user;
            List<string> idList = new List<string>();
            List<Post> posts;

            using (var context = new ApplicationDbContext())
            {
                user = context.Users.Include(u => u.Friends).Include(u => u.Posts).Where(u => u.Id == userID).ToList().FirstOrDefault();
            }

            idList.Add(user.Id);

            foreach (var friend in user.Friends)
            {
                idList.Add(friend.FriendUserID);
            }

            using (var context = new ApplicationDbContext())
            {
                posts = context.Posts.Include(p => p.Comments).Include(p => p.ApplicationUser).Where(p => idList.Contains(p.Fk_ApplicationUserID)).ToList();
            }

            posts.Sort((post1, post2) => post1.Date.Value.CompareTo(post2.Date.Value));

            posts.Reverse();

            List<Post> finalPostList = new List<Post>();

            for (int i = 0; i < 10; i++)
            {
                if ((posts != null))
                {
                    if (posts.Count > i)
                    {
                        finalPostList.Add(posts[i]);
                    }
                }
                else
                {
                    break;
                }
            }

            return finalPostList;
        }


        [HttpPost]
        public ActionResult AddFriend(FeedsViewModel model)
        {
            ApplicationUser user;
            ApplicationUser friendUser;
            List<Friend> modelFriendList;
            List<ApplicationUser> userList;
            using (var context = new ApplicationDbContext())
            {
                user = context.Users.Where(e => e.Id == model.ID).FirstOrDefault();
                userList = context.Users.ToList();
                friendUser = context.Users.Where(e => e.Id == model.FriendID).FirstOrDefault();
            }

            Friend f = new Friend();
            f.Fk_ApplicationUserID = user.Id;
            f.FriendUserID = friendUser.Id;

            using (var context = new ApplicationDbContext())
            {
                context.Friends.Add(f);
                context.SaveChanges();
            }

            f = new Friend();
            f.FriendUserID = user.Id;
            f.Fk_ApplicationUserID = friendUser.Id;
            using (var context = new ApplicationDbContext())
            {
                context.Friends.Add(f);
                context.SaveChanges();
                user = context.Users.Include(u => u.Friends).Include(u => u.Posts).Where(u => u.Id == model.ID).ToList().FirstOrDefault();
                modelFriendList = context.Friends
                    .Where(fr => fr.Fk_ApplicationUserID == model.ID).ToList();
            }



            List<string> friends = new List<string>();

            foreach (var friend in modelFriendList)
            {
                friends.Add(friend.FriendUserID);
            }

            model.FriendsId = friends;

            model.ApplicationUser = user;

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
            using (var context = new ApplicationDbContext())
            {
                context.Posts.Add(post);
                context.SaveChanges();
            }
            model.Posts = NavigatePosts(model.ID);
            return RedirectToAction("Index", model);
        }


        [HttpPost]
        public ActionResult AddComment(FeedsViewModel model)
        {
            using (var context = new ApplicationDbContext())
            {
                model.CurrentPost = context.Posts.Include(p => p.Comments).Where(p => p.Id == model.CurrentPostId).ToList().FirstOrDefault();
            }

            Comment comment = new Comment();
            comment.Date = DateTime.Now;
            comment.Fk_PostID = model.CurrentPostId;
            comment.Fk_ApplicationUserID = model.ID;
            comment.ApplicationUser = u.context.Users.Include(u => u.Image).Where(u => u.Id == model.ID).FirstOrDefault();
            comment.Content = model.CommentContent;

            using (var context = new ApplicationDbContext())
            {
                context.Coments.Add(comment);
                context.SaveChanges();
            }

            model.Posts = NavigatePosts(model.ID);

            return PartialView("_PartialPostContainer", model);
        }


        [HttpPost]
        public ActionResult RefreshPost(FeedsViewModel model)
        {
            model.Posts = NavigatePosts(model.ID);
            DateTime candidatePostTime = Convert.ToDateTime(model.Posts[0].Date.ToString());
            
            DateTime time = Convert.ToDateTime(model.RecentDateString);
            List<Post> postList = new List<Post>();

            if (model.Posts.Count == 0)
            {
                return PartialView();
            }

            if (candidatePostTime > time)
            {
                int i = 0;

                while (candidatePostTime > time)
                {
                    postList.Add(model.Posts[i]);
                    candidatePostTime = Convert.ToDateTime(model.Posts[++i].Date.ToString());
                }

                model.Posts = postList;
                model.RecentDateString = candidatePostTime.ToString();
                return PartialView("_PartialPost", model);
            }

            else
            {
                return PartialView();
            }

        }


        [HttpPost]
        public ActionResult RefreshComment(FeedsViewModel model)
        {
            Thread.Sleep(600);
            model.RecentDate = DateTime.Now;
            using (var context = new ApplicationDbContext())
            {
                model.ApplicationUser = context.Users.Find(model.ID);
                model.Comments = context.Coments.Include(c => c.ApplicationUser).Where(c => c.Fk_PostID == model.CurrentPostId).ToList();
            }
            return PartialView("_PartialComment", model);
        }
    }
}
