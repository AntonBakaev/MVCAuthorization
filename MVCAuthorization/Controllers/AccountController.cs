using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MVCAuthorization.Models;
using MVCAuthorization.Utils;
using MVCAuthorization.ViewModels;

namespace MVCAuthorization.Controllers
{
	public class AccountController : Controller
	{
		#region Private fields
		private ICountryManager countryManager;
		private IAccountManager accountManager;
		#endregion

		#region Constructors
		public AccountController(ICountryManager countryManager, IAccountManager accountManager)
		{
			this.countryManager = countryManager;
			this.accountManager = accountManager;
		}
		#endregion

		#region Action methods
		[HttpGet]
		public ActionResult AccountLogin(int accountId)
		{
			int currentUserId;
			if (CookieManager.IsUserLoggedIn(Request)
				&& (currentUserId = CookieManager.GetLoggedUserId(Request)) != accountId)
			{
				return RedirectToAction("AccountLogin", new { accountId = currentUserId });
			}

			var account = accountManager.GetAccount(accountId);
			if (account == null)
				return RedirectToAction("RegForm", "Home");
			CookieManager.SaveLoginCookie(HttpContext, account.Username, accountId);
			ViewBag.Username = account.Username;

			IEnumerable<CabinetViewModel> list = countryManager.GetCountryById(account.CountryId).Accounts
				.Select(a => new CabinetViewModel() { Username = a.Username, Sex = a.Sex });
			return View(list);
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