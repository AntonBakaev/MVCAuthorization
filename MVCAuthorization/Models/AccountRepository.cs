using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCAuthorization.Models
{
	public class AccountRepository : IAccountRepository
	{
		private readonly AccountsDBEntities accountEntities = new AccountsDBEntities();

		public IEnumerable<Account> GetAccounts()
		{
			return accountEntities.Accounts;
		}

		public Account GetAccountById(int accountId)
		{
			return accountEntities.Accounts.Find(accountId);
		}

		public void InsertAccount(Account account)
		{
			accountEntities.Accounts.Add(account);
			accountEntities.SaveChanges();
		}

		public void UpdateAccount(Account account)
		{
			var accountToUpdate = accountEntities.Accounts.Find(account.Id);
			if (accountToUpdate == null)
				return;
			accountToUpdate.Username = account.Username;
			accountToUpdate.Password = account.Password;
			accountToUpdate.Sex = account.Sex;
			accountToUpdate.CountryId = account.CountryId;

			accountEntities.Entry(account).State = EntityState.Modified;
			accountEntities.SaveChanges();
		}

		public void DeleteAccount(int accountId)
		{
			accountEntities.Accounts.Remove(accountEntities.Accounts.Find(accountId));
			accountEntities.SaveChanges();
		}
	}
}