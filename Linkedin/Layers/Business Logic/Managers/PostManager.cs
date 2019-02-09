namespace Linkedin.Layers.BL.Managers
{
    using Linkedin.Layers.Repository;
    using Linkedin.Models;

    public class PostManager : Repository<Post, ApplicationDbContext>
    {
        public PostManager(ApplicationDbContext context) : base(context)
        { }
    }
}