using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Linkedin.Models
{
    [Table("Message")]
    public class Message
    {
        [Key]
        public string MessageID { get; set; }

        public string Content { get; set; }


        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        [ForeignKey("SendingUser")]
        public string Fk_SendingUserID { get; set; }


        public ApplicationUser SendingUser { get; set; }


        [ForeignKey("RecievingUser")]
        public string Fk_RecievingUserID { get; set; }


        public ApplicationUser RecievingUser { get; set; }


    }
}