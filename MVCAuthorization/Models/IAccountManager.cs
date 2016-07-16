using System.Collections.Generic;
using MVCAuthorization.Models.DataAccessLevel;

namespace MVCAuthorization.Models
{
    public interface IAccountManager
    {
        IEnumerable<Account> GetAccounts();
        Account GetAccount(int accountId);
        void AddAccount(Account account);
        void UpdateAccount(Account account);
        void DeleteAccount(int accountId);
    }
}
