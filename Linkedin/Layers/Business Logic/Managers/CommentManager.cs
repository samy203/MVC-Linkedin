namespace Linkedin.Layers.BL.Managers
{
    using Linkedin.Layers.Repository;
    using Linkedin.Models;

    public class CommentManager : Repository<Comment, ApplicationDbContext>
    {
        public CommentManager(ApplicationDbContext context) : base(context)
        { }
    }
}