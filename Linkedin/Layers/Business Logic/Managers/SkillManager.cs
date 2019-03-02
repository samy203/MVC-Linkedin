namespace Linkedin.Layers.Business_Logic.Managers
{
    using Linkedin.Layers.Repository;
    using Linkedin.Models;

    public class SkillManager : Repository<Skill, ApplicationDbContext>
    {
        public SkillManager(ApplicationDbContext context) : base(context)
        { }
    }
}