using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using MVCAuthorization.Models;
using MVCAuthorization.Utils;

namespace MVCAuthorization.Controllers
{
	public class HomeController : Controller
	{
		#region Private fields
		private readonly IAccountManager accountManager;
		private readonly ICountryManager countryManager;
		#endregion

		#region Constructors
		public HomeController(IAccountManager accountManager, ICountryManager countryManager)
		{
			this.accountManager = accountManager;
			this.countryManager = countryManager;
		}
		#endregion

		#region Private methods
		private IEnumerable<SelectListItem> GetCountryNames()
		{
			IEnumerable<SelectListItem> names = countryManager.GetCountries()
															  .Select(c => new SelectListItem()
															  {
																  Value = c.Id.ToString(),
																  Text = c.Name
															  });
			return new SelectList(names, "Value", "Text");
		}

		private int AddAccount(string username, string password, string sex, int countryId)
		{
			return accountManager.AddAccount(new Account()
			{
				Username = username,
				Password = SHA1Encoder.Encode(password),
				Sex = sex,
				CountryId = countryId
			});
		}
		#endregion

		#region First page (RegForm)
		[HttpGet]
		public ActionResult Index()
		{
			return RedirectToAction("RegForm");
		}

		[HttpGet]
		public ActionResult RegForm(bool? cookies)
		{
			if (CookieManager.IsUserLoggedIn(Request))
				return RedirectToAction("AccountLogin", new { accountId = CookieManager.GetLoggedUserId(Request) });
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
				return RedirectToAction("AccountLogin", new { accountId = CookieManager.GetLoggedUserId(Request) });
			if (!ModelState.IsValid)
				return View();
			CookieManager.SaveAccountMainDataCookies(this.HttpContext, accountMainData);
			return RedirectToAction("RegFormAdditional");
		}
		#endregion

		#region Second page (RegFormAdditional)
		[HttpGet]
		public ActionResult RegFormAdditional()
		{
			if (CookieManager.IsUserLoggedIn(Request))
				return RedirectToAction("AccountLogin", new { accountId = CookieManager.GetLoggedUserId(Request) });
			return View(new AccountAdditionalViewModel() { CountryNames = GetCountryNames() });
		}

		[HttpPost]
		public ActionResult RegFormAdditional(AccountAdditionalViewModel accountMainData, string back)
		{
			if (CookieManager.IsUserLoggedIn(Request))
				return RedirectToAction("AccountLogin", new { accountId = CookieManager.GetLoggedUserId(Request) });
			if (!back.IsNullOrWhiteSpace())
				return RedirectToAction("RegForm", new { cookies = true });
			if (!ModelState.IsValid)
				return View(new AccountAdditionalViewModel() { CountryNames = GetCountryNames() });
			string username;
			string password;
			if (!CookieManager.TryReadAccountMainDataCookies(this.HttpContext, out username, out password))
				return RedirectToAction("RegForm");
			int accountId = AddAccount(username, password, accountMainData.Sex, accountMainData.SelectedCountryId);
			return RedirectToAction("AccountLogin", new { accountId = accountId });
		}
		#endregion

		#region Third page (AccountLogin)

		[HttpGet]
		public ActionResult AccountLogin(int accountId)
		{
			var account = accountManager.GetAccount(accountId);
			if (account == null)
				return RedirectToAction("RegForm");
			CookieManager.SaveLoginCookie(HttpContext, account.Username, accountId);
			ViewBag.Username = account.Username;
			return View();
		}

		[HttpGet]
		public ActionResult AccountLogout(string username)
		{
			CookieManager.DeleteCookie(HttpContext, username + CookieManager.LoginCookieValue);
			return RedirectToAction("RegForm");
		}
		#endregion
	}
}