using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MVCAuthorization.Models;
using MVCAuthorization.Utils;

namespace MVCAuthorization.Controllers
{
	public class HomeController : Controller
	{
		private readonly IAccountManager accountManager;
		private readonly ICountryManager countryManager;

		public HomeController(IAccountManager accountManager, ICountryManager countryManager)
		{
			this.accountManager = accountManager;
			this.countryManager = countryManager;
		}

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

		private void AddAccount(string username, string password, string sex, int countryId)
		{
			accountManager.AddAccount(new Account()
										{
											Username = username,
											Password = SHA1Encoder.Encode(password),
											Sex = sex,
											CountryId = countryId
										});
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
				CookieSaver.SaveAccountMainDataCookies(this.HttpContext, accountMainData);
				return RedirectToAction("RegFormAdditional");
			}
			return View();
		}

		[HttpGet]
		public ActionResult RegFormAdditional()
		{
			return View(new AccountAdditionalViewModel() { CountryNames = GetCountryNames() });
		}

		[HttpPost]
		public ActionResult RegFormAdditional(AccountAdditionalViewModel accountMainData)
		{
			if (ModelState.IsValid)
			{
				string username;
				string password;
				if (!CookieSaver.TryReadAccountMainDataCookies(this.HttpContext, out username, out password))
					return RedirectToAction("RegForm");
				AddAccount(username, password, accountMainData.Sex, accountMainData.SelectedCountryId);
				return RedirectToAction("UserLogin");
			}
			return View(new AccountAdditionalViewModel() { CountryNames = GetCountryNames() });
		}

		[HttpGet]
		public ActionResult UserLogin()
		{
			return View();
		}
	}
}