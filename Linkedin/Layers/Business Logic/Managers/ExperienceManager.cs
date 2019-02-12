namespace Linkedin.Layers.BL.Managers
{
    using Linkedin.Layers.Repository;
    using Linkedin.Models;

    public class ExperienceManager : Repository<Experience, ApplicationDbContext>
    {
        public ExperienceManager(ApplicationDbContext context) : base(context)
        { }
    }
}