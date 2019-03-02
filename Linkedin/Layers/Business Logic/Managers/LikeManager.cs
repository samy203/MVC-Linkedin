namespace Linkedin.Layers.BL.Managers
{
    using Linkedin.Layers.Repository;
    using Linkedin.Models;

    public class LikeManager : Repository<Like, ApplicationDbContext>
    {
        public LikeManager(ApplicationDbContext context) : base(context)
        { }
    }
}