using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace Linkedin.Models
{
    [Table("Image")]
    public class Image
    {
        [ForeignKey("ApplicationUser")]
        public string ImageId { get; set; }

        public string Path { get; set; }

        public string Title { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}