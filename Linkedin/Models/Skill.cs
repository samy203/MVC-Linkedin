using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Linkedin.Models
{
    [Table("Skill")]
    public class Skill
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [ForeignKey("ApplicationUser")]
        public string Fk_ApplicationUserID { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}