using Linkedin.Models.ViewModels;
using System.Web.Mvc;

namespace Linkedin.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index(SearchViewModel model)
        {
            return View();
        }
    }
}