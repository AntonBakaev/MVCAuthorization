using System.Web;
using System.Web.Mvc;
using MVCAuthorization.Models;
using MVCAuthorization.Utils;

namespace MVCAuthorization.Controllers
{
    public class HomeController : Controller
    {
        private void SaveAccountMainDataCookies(AccountMainViewModel accountMainData)
        {
            accountMainData.Password = PasswordProtector.Protect(accountMainData.Username, accountMainData.Password);
            this.HttpContext.Response.Cookies
                    .Add(new HttpCookie("Username", accountMainData.Username));
            this.HttpContext.Response.Cookies
                .Add(new HttpCookie("Password", accountMainData.Password));
        }

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
            {
                SaveAccountMainDataCookies(accountMainData);
                return RedirectToAction("RegFormAdditional");
            }
            else
                return View();
        }

        [HttpGet]
        public ActionResult RegFormAdditional()
        {
            return View();
            //TODO: remove cookies
        }
    }
}