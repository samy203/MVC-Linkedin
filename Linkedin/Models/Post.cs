using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Linkedin.Models
{
    [Table("Post")]
    public class Post
    {
        [Key]
        public int Id { get; set; }

        public int Likes { get; set; }

        List<Comment> Comments { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        [Required]
        public string Content { get; set; }


        [ForeignKey("ApplicationUser")]
        public string Fk_ApplicationUserID { get; set; }


        public ApplicationUser ApplicationUser { get; set; }
    }
}