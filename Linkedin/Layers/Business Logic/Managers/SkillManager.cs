using Linkedin.Layers.Repository;
using Linkedin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Linkedin.Layers.Business_Logic.Managers
{
    public class SkillManager : Repository<Skill, ApplicationDbContext>
    {
        public SkillManager(ApplicationDbContext context) : base(context)
        { }
    }
}