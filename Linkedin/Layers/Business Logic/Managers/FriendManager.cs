namespace Linkedin.Layers.BL.Managers
{
    using Linkedin.Layers.Repository;
    using Linkedin.Models;

    public class FriendManager : Repository<Friend, ApplicationDbContext>
    {
        public FriendManager(ApplicationDbContext context) : base(context)
        { }
    }
}