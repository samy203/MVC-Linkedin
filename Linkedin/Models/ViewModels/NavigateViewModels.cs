using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Linkedin.Models.ViewModels
{
    public class NavigateViewModel
    {
        public ApplicationUser ApplicationUser { get; set; }

        public string ID { get; set; }

    }

    public class FeedsViewModel : NavigateViewModel
    {
        public List<Friend> Friends { get; set; }


        public List<ApplicationUser> Users { get; set; }


        public List<Post> Posts { get; set; }


        public List<ApplicationUser> FriendsData { get; set; }


        public string FriendID { get; set; }


        public List<string> FriendsId { get; set; }


        public Post CurrentPost { get; set; }


        public string PostContent { get; set; }


        public string CommentContent { get; set; }

    }

    public class PersonViewModel : NavigateViewModel
    {
        public Skill Skill { get; set; }
        public List<Skill> Skills { get; set; }
        public Experience Experience { get; set; }
        public int ExperienceID { get; set; }
        public int SkillID { get; set; }
        public bool IsExp { get; set; }

        public string RequiredUSerID { get; set; }

        public ApplicationUser TargetUser { get; set; }
    }
}