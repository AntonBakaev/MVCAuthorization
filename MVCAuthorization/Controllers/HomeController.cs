using System.Web.Mvc;
using MVCAuthorization.Models;

namespace MVCAuthorization.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("RegForm");
        }

        [HttpGet]
        public ActionResult RegForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegForm(AccountMainViewModel accountMainData)
        {
            if (ModelState.IsValid)
                return View(accountMainData);
            else
                return View();
        }
    }
}