using Linkedin.Models;
using Linkedin.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;

namespace Linkedin.Controllers
{
    public class SearchController : ParentController
    {
        [HttpPost]
        public ActionResult Index(NavigateViewModel model)
        {
            SearchViewModel s = new SearchViewModel();
            s.SearchInput = model.SearchIndex;
            var user = u.context.Users.Include(e => e.Friends).Where(e => e.Id == model.ID).FirstOrDefault();
            s.ApplicationUser = user;
            s.ID = model.ID;

            s.SearchedUsers = u.context.Users.Where(f => f.FirstName.Contains(s.SearchInput) || f.LastName.Contains(s.SearchInput)).ToList();
            return View(s);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult UnAuthIndex(string firstName, string lastName)
        {
            bool firstIndex = true;
            bool lastIndex = true;
            SearchViewModel s = new SearchViewModel();
            IdComparer comparer = new IdComparer();
            List<ApplicationUser> distinctList;
            List<ApplicationUser> firstNameUsers = new List<ApplicationUser>();
            List<ApplicationUser> lastNameUsers = new List<ApplicationUser>();


            if (firstName != "")
            {
                firstNameUsers = u.context.Users.Include(f => f.Experiences).Include(f => f.Skills).Where(f => f.FirstName.Contains(firstName)).ToList();
            }
            else
            {
                firstIndex = false;
            }


            if (lastName != "")
            {
                lastNameUsers = u.context.Users.Include(l => l.Experiences).Include(l => l.Skills).Where(l => l.LastName.Contains(lastName)).ToList();
            }
            else
            {
                lastIndex = false;
            }
            if (firstIndex == true && lastIndex == true)
            {
                firstNameUsers.AddRange(lastNameUsers);
                distinctList = firstNameUsers.Distinct(comparer).ToList();
                s.SearchedUsers = distinctList;
                return View(s);
            }
            if (firstIndex == true && lastIndex == false)
            {
                distinctList = firstNameUsers;
                s.SearchedUsers = distinctList;
                return View(s);
            }
            if(firstIndex==false &&lastIndex==true)
            {
                distinctList = firstNameUsers;
                s.SearchedUsers = distinctList;
                return View(s);
            }
            else
            {
                return View("~/Views/Account/Interface.cshtml");
            }




        }

    }
    public class IdComparer : IEqualityComparer<ApplicationUser>
    {
        public bool Equals(ApplicationUser x, ApplicationUser y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(ApplicationUser obj)
        {

            return obj.GetHashCode();
        }
    }

}