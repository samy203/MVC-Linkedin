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
    [Authorize]
    public class PersonController : ParentController
    {

        public ActionResult Index(PersonViewModel model)
        {

            var user = u.context.Users.Where(e => e.Id == model.ID).FirstOrDefault();
            model.ApplicationUser = user;
            model.ApplicationUser.Skills = skillMang.GetAllBind().Where(s => s.Fk_ApplicationUserID == user.Id).ToList();
            model.ApplicationUser.Experiences = expMang.GetAllBind().Where(s => s.Fk_ApplicationUserID == user.Id).ToList();


            if (Request.IsAjaxRequest())
            {
                if (model.IsExp)
                    return PartialView("_PartialContainerExp", model);
                else
                    return PartialView("_PartialContainerSkill", model);
            }
            else
            {
                return View("PersonalEdit", model);
            }


        }


        public ActionResult Delete(PersonViewModel model)
        {
            var user = u.context.Users.Find(model.ID);
            model.ApplicationUser = user;
            if (model.IsExp == true)
            {
                model.Experience = expMang.GetAllBind().Find(e => e.Id == model.ExperienceID);
                expMang.Remove(model.Experience);
                var expList = expMang.GetAllBind().Where(s => s.Fk_ApplicationUserID == model.ID).ToList();
                model.ApplicationUser.Experiences = expList;
                return PartialView("_PartialContainerExp", model);
            }
            else
            {
                model.Skill = skillMang.GetAllBind().Find(e => e.Id == model.SkillID);
                skillMang.Remove(model.Skill);
                var skillList = skillMang.GetAllBind().Where(s => s.Fk_ApplicationUserID == model.ID).ToList();
                model.ApplicationUser.Skills = skillList;
                return PartialView("_PartialContainerSkill", model);
            }
        }



        public ActionResult Save(PersonViewModel model)
        {
            var user = u.context.Users.Where(e => e.Id == model.ID).FirstOrDefault();
            model.ApplicationUser = user;
            model.ApplicationUser.Skills = skillMang.GetAllBind().Where(s => s.Fk_ApplicationUserID == user.Id).ToList();
            model.ApplicationUser.Experiences = expMang.GetAllBind().Where(s => s.Fk_ApplicationUserID == user.Id).ToList();

            if (ModelState.IsValid)
            {
                if (model.IsExp)
                {
                    model.Experience.Id = model.ExperienceID;
                    model.Experience.Fk_ApplicationUserID = model.ID;
                    expMang.Update(model.Experience);
                    model.ExperienceID = 0;
                    model.SkillID = 0;
                    return PartialView("_PartialContainerExp", model);
                }
                else
                {
                    model.Skill.Id = model.SkillID;
                    model.Skill.Fk_ApplicationUserID = model.ID;
                    skillMang.Update(model.Skill);
                    model.ExperienceID = 0;
                    model.SkillID = 0;
                    return PartialView("_PartialContainerSkill", model);
                }

            }
            else
            {
                if (model.IsExp)
                    return PartialView("_PartialContainerExp", model);
                else
                    return PartialView("_PartialContainerSkill", model);
            }
        }




        public ActionResult Edit(PersonViewModel model)
        {
            if (model.IsExp)
            {
                var user = u.context.Users.Where(e => e.Id == model.ID).FirstOrDefault();
                model.ApplicationUser = user;
                model.ApplicationUser.Skills = skillMang.GetAllBind().Where(s => s.Fk_ApplicationUserID == user.Id).ToList();
                model.ApplicationUser.Experiences = expMang.GetAllBind().Where(s => s.Fk_ApplicationUserID == user.Id).ToList();
                model.Experience = expMang.GetAllBind().Find(e => e.Id == model.ExperienceID);
                return PartialView("_PartialContainerExp", model);
            }
            else
            {
                var user = u.context.Users.Where(e => e.Id == model.ID).FirstOrDefault();
                model.ApplicationUser = user;
                model.ApplicationUser.Skills = skillMang.GetAllBind().Where(s => s.Fk_ApplicationUserID == user.Id).ToList();
                model.ApplicationUser.Experiences = expMang.GetAllBind().Where(s => s.Fk_ApplicationUserID == user.Id).ToList();
                model.Skill = skillMang.GetAllBind().Find(e => e.Id == model.SkillID);
                return PartialView("_PartialContainerSkill", model);
            }



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
                var user = u.context.Users.Find(model.ID);
                model.Experience.Fk_ApplicationUserID = user.Id;
                model.ApplicationUser = user;
                expMang.Add(model.Experience);

                var expList = expMang.GetAllBind().Where(s => s.Fk_ApplicationUserID == model.ID).ToList();
                model.ApplicationUser.Experiences = expList;
                return PartialView("_PartialContainerExp", model);
            }

            return View(model);
        }



        public ActionResult DisplayUser(PersonViewModel model)
        {
            model.TargetUser = u.context.Users.Where(c => c.Id == model.RequiredUSerID).FirstOrDefault();
            model.ApplicationUser = u.context.Users.Where(e => e.Id == model.ID).FirstOrDefault();
            return View("Personal", model);
        }

    }
}