using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Linkedin.Models
{
    [Table("Friend")]
    public class Friend
    {
        [Key, Column(Order = 0)]
        [ForeignKey("ApplicationUser")]
        public string Fk_ApplicationUserID { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("ApplicationUserFriend")]
        public string Fk_ApplicationFriendID { get; set; }

        public ApplicationUser ApplicationUserFriend { get; set; }


    }
}