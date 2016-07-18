using System.Collections.Generic;

namespace MVCAuthorization.Models
{
	public interface IAccountManager
	{
		IEnumerable<Account> GetAccounts();
		Account GetAccount(int accountId);
		int AddAccount(Account account);
		void UpdateAccount(Account account);
		void DeleteAccount(int accountId);
	}
}
