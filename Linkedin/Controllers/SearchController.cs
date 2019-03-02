using Linkedin.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Linkedin.Controllers
{
    public class SearchController : ParentController
    {
        [HttpPost]
        public ActionResult Index(NavigateViewModel model)
        {
            SearchViewModel s = new SearchViewModel();
            s.SearchInput = model.SearchIndex;
            var user = u.context.Users.Where(e => e.Id == model.ID).FirstOrDefault();
            s.ApplicationUser = user;
            s.SearchedUsers = u.context.Users.Where(f => f.FirstName.Contains(s.SearchInput) || f.LastName.Contains(s.SearchInput)).ToList();
            return View(s);
        }
    }
}