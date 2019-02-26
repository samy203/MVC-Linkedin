using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Linkedin.Models
{
    [Table("Experience")]
    public class Experience
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int StartYear { get; set; }

        [Required]

        public int EndYear { get; set; }

        [Required]
        public string Content { get; set; }

        [ForeignKey("ApplicationUser")]
        public string Fk_ApplicationUserID { get; set; }


        public ApplicationUser ApplicationUser { get; set; }
    }
}