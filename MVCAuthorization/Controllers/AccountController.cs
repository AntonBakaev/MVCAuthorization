using System.Web.Mvc;
using MVCAuthorization.Models;
using MVCAuthorization.Utils;

namespace MVCAuthorization.Controllers
{
    public class AccountController : Controller
	{
		#region Private fields
		private IAccountManager accountManager;
		#endregion

		#region Constructors
		public AccountController(IAccountManager accountManager)
	    {
		    this.accountManager = accountManager;
	    }
		#endregion

		#region Action methods
		[HttpGet]
		public ActionResult AccountLogin(int accountId)
		{
			var account = accountManager.GetAccount(accountId);
			if (account == null)
				return RedirectToAction("RegForm", "Home");
			CookieManager.SaveLoginCookie(HttpContext, account.Username, accountId);
			ViewBag.Username = account.Username;
			return View(account);
		}

		[HttpGet]
		public ActionResult AccountLogout(string username)
		{
			CookieManager.DeleteCookie(HttpContext, username + CookieManager.LoginCookieValue);
			return RedirectToAction("RegForm", "Home");
		}
		#endregion
    }
}