using Linkedin.Layers.BL.Managers;
using Linkedin.Layers.Business_Logic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Linkedin.Controllers
{
    public class ParentController : Controller
    {
        public UnitOfWork u;
        public SkillManager skillMang;
        public ExperienceManager expMang;
        public ApplicationUserManager manager;
        public PostManager postMang;
        public CommentManager commentMang;
        public FriendManager friendMang;
        public LikeManager likeMang;

        public ParentController()
        {
            u = UnitOfWork.Instance;
            skillMang = u.GetManager<SkillManager>();
            expMang = u.GetManager<ExperienceManager>();
            commentMang = u.GetManager<CommentManager>();
            postMang = u.GetManager<PostManager>();
            friendMang = u.GetManager<FriendManager>();
            likeMang = u.GetManager<LikeManager>();
        }





    }
}