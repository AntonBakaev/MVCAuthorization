using System.Web.Mvc;
using MVCAuthorization.Models;
using MVCAuthorization.Utils;

namespace MVCAuthorization.Controllers
{
	public class HomeController : Controller
	{
		#region Action methods
		[HttpGet]
		public ActionResult Index()
		{
			return RedirectToAction("RegForm");
		}

		[HttpGet]
		public ActionResult RegForm(bool? cookies)
		{
			if (CookieManager.IsUserLoggedIn(Request))
				return RedirectToAction("AccountLogin", "Account", new { accountId = CookieManager.GetLoggedUserId(Request) });
			if (cookies != true)
				return View();
			string username;
			string password;
			if (CookieManager.TryReadAccountMainDataCookies(this.HttpContext, out username, out password))
				return View(new AccountMainViewModel() { Username = username, Password = password });
			return View();
		}

		[HttpPost]
		public ActionResult RegForm(AccountMainViewModel accountMainData)
		{
			if (CookieManager.IsUserLoggedIn(Request))
				return RedirectToAction("AccountLogin", "Account", new { accountId = CookieManager.GetLoggedUserId(Request) });
			if (!ModelState.IsValid)
				return View();
			CookieManager.SaveAccountMainDataCookies(this.HttpContext, accountMainData);
			return RedirectToAction("RegFormAdditional", "Registration");
		}
		#endregion
	}
}