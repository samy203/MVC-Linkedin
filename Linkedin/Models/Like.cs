using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Linkedin.Models
{
    [Table("Like")]
    public class Like
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey("Post")]
        public int Fk_PostID { get; set; }


        public Post Post { get; set; }


        [ForeignKey("ApplicationUser")]
        public string Fk_ApplicationUserID { get; set; }


        public ApplicationUser ApplicationUser { get; set; }

    }
}