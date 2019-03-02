using Linkedin.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Linkedin.Controllers
{
    public class SearchController : ParentController
    {
        public ActionResult Index(NavigateViewModel model)
        {
            SearchViewModel s = new SearchViewModel
            {
                SearchInput = model.SearchIndex
            };
            s.SearchedUsers = u.context.Users.Where(f => f.FirstName.Contains(s.SearchInput) || f.LastName.Contains(s.SearchInput)).ToList();
            return View();
        }
    }
}