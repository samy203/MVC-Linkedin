using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Linkedin.Models
{
    [Table("Comment")]
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ApplicationUser")]
        public string Fk_ApplicationUserID { get; set; }


        public ApplicationUser ApplicationUser { get; set; }


        [ForeignKey("Post")]
        public int Fk_PostID { get; set; }


        [Required]
        public string Content { get; set; }


        public Post Post { get; set; }


        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

    }
}