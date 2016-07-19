using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MVCAuthorization.Models;
using MVCAuthorization.Utils;

namespace MVCAuthorization.Controllers
{
	public class RegistrationController : Controller
	{
		#region Private fields
		private readonly ICountryManager countryManager;
		private readonly IAccountManager accountManager;
		#endregion

		#region Constructors
		public RegistrationController(ICountryManager countryManager, IAccountManager accountManager)
		{
			this.countryManager = countryManager;
			this.accountManager = accountManager;
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

		#region Action methods
		[HttpGet]
		public ActionResult RegFormAdditional()
		{
			if (CookieManager.IsUserLoggedIn(Request))
				return RedirectToAction("AccountLogin", "Account", new { accountId = CookieManager.GetLoggedUserId(Request) });
			return View(new AccountAdditionalViewModel() { CountryNames = GetCountryNames() });
		}

		[HttpPost]
		public ActionResult RegFormAdditional(AccountAdditionalViewModel accountMainData)
		{
			if (CookieManager.IsUserLoggedIn(Request))
				return RedirectToAction("AccountLogin", "Account", new { accountId = CookieManager.GetLoggedUserId(Request) });
			if (!ModelState.IsValid)
				return View(new AccountAdditionalViewModel() { CountryNames = GetCountryNames() });
			string username;
			string password;
			if (!CookieManager.TryReadAccountMainDataCookies(this.HttpContext, out username, out password))
				return RedirectToAction("RegForm", "Home");
			int accountId = AddAccount(username, password, accountMainData.Sex, accountMainData.SelectedCountryId);
			return RedirectToAction("AccountLogin", "Account", new { accountId = accountId });
		}
		#endregion
	}
}