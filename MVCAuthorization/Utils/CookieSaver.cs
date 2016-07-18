using System;
using System.Web;
using MVCAuthorization.Models;

namespace MVCAuthorization.Utils
{
	public static class CookieSaver
	{
		private static void DeleteCookie(HttpContextBase httpContext, string cookieName)
		{
			var cookie = new HttpCookie(cookieName);
			cookie.Expires = DateTime.Now.Date.AddDays(-1);
			httpContext.Response.Cookies.Add(cookie);
		}

		public static void SaveAccountMainDataCookies(HttpContextBase httpContext, AccountMainViewModel accountMainData)
		{
			accountMainData.Password = PasswordProtector.Protect(accountMainData.Username, accountMainData.Password);
			httpContext.Response.Cookies
				.Add(new HttpCookie("Username", accountMainData.Username));
			httpContext.Response.Cookies
				.Add(new HttpCookie("Password", accountMainData.Password));
		}

		public static bool TryReadAccountMainDataCookies(HttpContextBase httpContext, out string username, out string password)
		{
			username = password = null;
			HttpCookie usernameCookie = httpContext.Request.Cookies["Username"];
			HttpCookie passwordCookie = httpContext.Request.Cookies["Password"];
			if (usernameCookie == null || passwordCookie == null)
				return false;
			DeleteCookie(httpContext, "Username");
			DeleteCookie(httpContext, "Password");
			username = usernameCookie.Value;
			password = PasswordProtector.Unprotect(username, passwordCookie.Value);
			return true;
		}
	}
}