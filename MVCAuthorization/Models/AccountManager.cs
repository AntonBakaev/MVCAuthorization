using System.Collections.Generic;
using MVCAuthorization.Models.DataAccessLevel;

namespace MVCAuthorization.Models
{
	public class AccountManager : IAccountManager
	{
		IAccountRepository accountRepository;

		public AccountManager(IAccountRepository accountRepository)
		{
			this.accountRepository = accountRepository;
		}

		public IEnumerable<Account> GetAccounts()
		{
			return accountRepository.GetAccounts();
		}

		public Account GetAccount(int accountId)
		{
			return accountRepository.GetAccountById(accountId);
		}

		public int AddAccount(Account account)
		{
			return accountRepository.InsertAccount(account);
		}

		public void UpdateAccount(Account account)
		{
			accountRepository.UpdateAccount(account);
		}

		public void DeleteAccount(int accountId)
		{
			accountRepository.DeleteAccount(accountId);
		}
	}
}
