using Linkedin.Layers.BL.Managers;
using Linkedin.Layers.Business_Logic.Managers;
using Linkedin.Models;
using Linkedin.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Linkedin.Controllers
{
    public class PersonController : Controller
    {
        public UnitOfWork u;
        public SkillManager skillMang;
        public ExperienceManager expMang;
        public ApplicationUserManager manager;


        public PersonController()
        {
            u = UnitOfWork.Instance;
            skillMang = u.GetManager<SkillManager>();
            expMang = u.GetManager<ExperienceManager>();

        }


        // GET: Feed
        [Authorize]
        public ActionResult Index(SimplifiedPersonViewModel model)
        {
            PersonViewModel vM = new PersonViewModel();
            var user = u.context.Users.Where(e => e.Email == model.Email).FirstOrDefault();
            vM.ApplicationUser = user;
            vM.ApplicationUser.Skills = skillMang.GetAllBind().Where(s => s.Fk_ApplicationUserID == user.Id).ToList();
            vM.ApplicationUser.Experiences = expMang.GetAllBind().Where(s => s.Fk_ApplicationUserID == user.Id).ToList();
            return View("PersonalEdit", vM);
        }



        public ActionResult Edit()
        {
            return null;
        }

        [HttpPost]
        public ActionResult AddAjaxSkill(PersonViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = u.context.Users.Find(model.ID);
                model.Skill.Fk_ApplicationUserID = user.Id;
                model.ApplicationUser = user;
                skillMang.Add(model.Skill);
                var skillList = skillMang.GetAllBind().Where(s => s.Fk_ApplicationUserID == model.ID).ToList();
                model.ApplicationUser.Skills = skillList;
                return PartialView("_PartialContainerSkill", model);

            }

            return View(model);
        }

        [HttpPost]
        public ActionResult AddAjaxExp(PersonViewModel model)
        {

            if (ModelState.IsValid)
            {
                var modelForIter = new PersonViewModel();
                modelForIter.ID = model.ID;
                modelForIter.Experience = model.Experience;

                var user = u.context.Users.Find(model.ID);
                var newExp = new Experience();

                newExp.Fk_ApplicationUserID = user.Id;
                newExp.Content = model.Experience.Content;
                newExp.StartYear = model.Experience.StartYear;
                newExp.EndYear = model.Experience.EndYear;
                expMang.Add(newExp);

                var expList = u.GetManager<ExperienceManager>().GetAllBind().Where(s => s.Fk_ApplicationUserID == modelForIter.ID);
                modelForIter.ApplicationUser = user;
                modelForIter.ApplicationUser.Experiences = expList.ToList();
                return PartialView("_PartialContainerExp", modelForIter);
            }

            return View(model);
        }


    }
}