using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Linkedin.Models.ViewModels
{
    public class PersonViewModel
    {
        public ApplicationUser ApplicationUser { get; set; }
        public string ID { get; set; }
        public Skill Skill { get; set; }
        public List<Skill> Skills { get; set; }
        public Experience Experience { get; set; }
        public int ExperienceID { get; set; }
        public int SkillID { get; set; }
        public bool IsExp { get; set; }
    }

    public class SimplifiedPersonViewModel
    {
        public string Email { get; set; }
        public Skill Skill { get; set; }
    }

}