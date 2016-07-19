using System;
using System.Text;
using System.Web.Security;

namespace MVCAuthorization.Utils
{
    public static class PasswordProtector
    {
        private const string purpose = "Protecting password owner is: {0}";

        public static string Protect(string username, string password)
        {
            byte[] protectedBytes = MachineKey.Protect(
                                                        Encoding.UTF8.GetBytes(password),
                                                        string.Format(purpose, username)
                                                      );
            return Convert.ToBase64String(protectedBytes);
        }

        public static string Unprotect(string username, string protectedPassword)
        {
            byte[] unprotectedBytes = MachineKey.Unprotect(
                                                            Convert.FromBase64String(protectedPassword),
                                                            string.Format(purpose, username)
                                                          );
            return Encoding.UTF8.GetString(unprotectedBytes);
        }
    }
}