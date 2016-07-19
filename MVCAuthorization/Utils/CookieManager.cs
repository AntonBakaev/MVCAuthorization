using System;
using System.Linq;
using System.Web;
using MVCAuthorization.ViewModels;

namespace MVCAuthorization.Utils
{
	public static class CookieManager
	{
		public const string LoginCookieValue = "_UserLoggedIn";

		public static bool IsUserLoggedIn(HttpRequestBase request)
		{
			return request.Cookies.AllKeys.Any(cookieName => cookieName.Contains(LoginCookieValue));
		}

		public static int GetLoggedUserId(HttpRequestBase request)
		{
			foreach (var cookieName in request.Cookies.AllKeys
				.Where(cookieName => cookieName.Contains(LoginCookieValue)))
				return Convert.ToInt32(request.Cookies[cookieName].Value);
			return -1;
		}

		public static void DeleteCookie(HttpContextBase httpContext, string cookieName)
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

		public static void SaveLoginCookie(HttpContextBase httpContext, string username, int accountId)
		{
			httpContext.Response.Cookies.Add(new HttpCookie(username + LoginCookieValue, accountId.ToString()));
		}
	}
}