using System.Web.Mvc;

namespace Bookcase.WebClient.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
           
            return View();

        }
    }
}