using System;
using System.Web.Mvc;
using MVCAuthorization.Utils;
using MVCAuthorization.ViewModels;

namespace MVCAuthorization.Controllers
{
	public class HomeController : Controller
	{
        #region Private methods
        private void SaveCookies(AccountMainViewModel accountMainData)
        {
            CookieManager.SaveAccountMainDataCookies(this.HttpContext, accountMainData);
        }

        private void SaveSession(AccountMainViewModel accountMainData)
        {
            Session["Username"] = accountMainData.Username;
            Session["Password"] = PasswordProtector.Protect(accountMainData.Username, accountMainData.Password);
        }

        private void SaveTempData(AccountMainViewModel accountMainData)
        {
            TempData["Username"] = accountMainData.Username;
            TempData["Password"] = PasswordProtector.Protect(accountMainData.Username, accountMainData.Password);
        }

        private string GetSavedUsername()
        {
            string username, password;
            if (TempData["Username"] != null)
                return TempData["Username"].ToString();
            else if (Session["Username"] != null)
                return Session["Username"].ToString();
            else if (CookieManager.TryReadAccountMainDataCookies(this.HttpContext, out username, out password))
                return username;
            else
                return String.Empty;
        }
        #endregion

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
				return RedirectToAction("AccountLogin", "Account", new { id = CookieManager.GetLoggedUserId(Request) });
			if (cookies == true)				
		        return View(new AccountMainViewModel() { Username = GetSavedUsername(), Password = String.Empty });
            return View();
        }

		[HttpPost]
		public ActionResult RegForm(AccountMainViewModel accountMainData, string Next, string Next2, string Next3)
		{
			if (CookieManager.IsUserLoggedIn(Request))
				return RedirectToAction("AccountLogin", "Account", new { id = CookieManager.GetLoggedUserId(Request) });
			if (!ModelState.IsValid)
				return View();

            if (Next != null)
                SaveCookies(accountMainData);
            else if (Next2 != null)
                SaveSession(accountMainData);
            else if (Next3 != null)
                SaveTempData(accountMainData);

            return RedirectToAction("RegFormAdditional", "Registration");
		}
		#endregion
	}
}