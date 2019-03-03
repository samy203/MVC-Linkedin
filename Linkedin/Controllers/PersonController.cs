using Linkedin.Layers.BL.Managers;
using Linkedin.Models.ViewModels;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using Linkedin.Models;
using System.IO;
using System.Web;
using System;

namespace Linkedin.Controllers
{
    [Authorize]
    public class PersonController : ParentController
    {
        public ActionResult Index(PersonViewModel model)
        {
            var user = u.context.Users.Where(e => e.Id == model.ID).FirstOrDefault();
            model.ApplicationUser = user;
            model.Image = u.context.Images.Where(f => f.ImageId == model.ID).FirstOrDefault();
            model.ApplicationUser.Skills = skillMang.GetAllBind().Where(s => s.Fk_ApplicationUserID == user.Id).ToList();
            model.ApplicationUser.Experiences = expMang.GetAllBind().Where(s => s.Fk_ApplicationUserID == user.Id).ToList();
            var friendList = u.GetManager<FriendManager>().GetAllBind().Where(f => f.Fk_ApplicationUserID == model.ID).ToList();
            model.ApplicationUser.Friends = friendList;

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
            model.TargetUser = u.context.Users.Where(c => c.Id == model.RequiredUserID).FirstOrDefault();
            model.ApplicationUser = u.context.Users.Include(c => c.Friends).Where(e => e.Id == model.ID).FirstOrDefault();
            model.IsFriends = false;
            if (model.TargetUser == model.ApplicationUser)
            {
                PersonViewModel user = new PersonViewModel();
                user.ID = model.TargetUser.Id;
                return RedirectToAction("Index", "Person", user);
            }
            foreach (var friendship in model.ApplicationUser.Friends)
            {
                if (friendship.FriendUserID ==model.TargetUser.Id)
                {
                    model.IsFriends = true;
                    break;
                }
            }
            return View("Personal", model);
        }

        [AllowAnonymous]
        public ActionResult unAuthDisplayUser(PersonViewModel model)
        {
            model.TargetUser = u.context.Users.Include(c => c.Experiences).Include(c => c.Skills).Where(c => c.Id == model.RequiredUserID).FirstOrDefault();
            return View("UnAuthPersonal", model);
        }

        public ActionResult AddFriendPersonal(PersonViewModel model)
        {
            var user = u.context.Users.Include(e => e.Friends).Where(e => e.Id == model.ID).FirstOrDefault();

            var targetUser = u.context.Users.Where(e => e.Id == model.RequiredUserID).FirstOrDefault();

            model.TargetUser = targetUser;
            model.IsFriends = true;



            Friend f = new Friend();
            f.Fk_ApplicationUserID = user.Id;
            f.FriendUserID = targetUser.Id;

            friendMang.Add(f);

            f = new Friend();
            f.FriendUserID = user.Id;
            f.Fk_ApplicationUserID = targetUser.Id;

            friendMang.Add(f);

            return View("Personal", model);
        }

        [HttpPost]
        public ActionResult AddImage(PersonViewModel model)
        {
            var user = u.context.Users.Where(e => e.Id == model.ID).FirstOrDefault();
            model.ApplicationUser = user;
            model.ApplicationUser.Skills = skillMang.GetAllBind().Where(s => s.Fk_ApplicationUserID == user.Id).ToList();
            model.ApplicationUser.Experiences = expMang.GetAllBind().Where(s => s.Fk_ApplicationUserID == user.Id).ToList();
            string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
            string extension = Path.GetExtension(model.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            model.Image = new Image();
            model.Image.ImageId = model.ID;
            model.Image.Path = "/Resource/" + fileName;
            fileName = Path.Combine(Server.MapPath("/Resource"),fileName);
            model.ImageFile.SaveAs(fileName);
            u.GetManager<ImageManager>().Update(model.Image);
            return View("PersonalEdit", model);
        }

    }
}