using System.Collections.Generic;

namespace MVCAuthorization.Models.DataAccessLevel
{
	public interface IAccountRepository
	{
		IEnumerable<Account> GetAccounts();
		Account GetAccountById(int accountId);
		void InsertAccount(Account account);
		void UpdateAccount(Account account);
		void DeleteAccount(int accountId);
	}
}