namespace Linkedin.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

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