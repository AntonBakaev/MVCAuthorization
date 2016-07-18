using System.Collections.Generic;
using System.Data.Entity;

namespace MVCAuthorization.Models.DataAccessLevel
{
	public class AccountRepository : IAccountRepository
	{
		private readonly AccountsDBEntities accountDB = new AccountsDBEntities();

		public IEnumerable<Account> GetAccounts()
		{
			return accountDB.Accounts;
		}

		public Account GetAccountById(int accountId)
		{
			return accountDB.Accounts.Find(accountId);
		}

		public int InsertAccount(Account account)
		{
			accountDB.Accounts.Add(account);
			accountDB.SaveChanges();
			return (int)accountDB.Entry(account).GetDatabaseValues()["Id"];
		}

		public void UpdateAccount(Account account)
		{
			var accountToUpdate = accountDB.Accounts.Find(account.Id);
			if (accountToUpdate == null)
				return;
			accountToUpdate.Username = account.Username;
			accountToUpdate.Password = account.Password;
			accountToUpdate.Sex = account.Sex;
			accountToUpdate.CountryId = account.CountryId;
			accountToUpdate.Country = account.Country;

			accountDB.Entry(accountToUpdate).State = EntityState.Modified;
			accountDB.SaveChanges();
		}

		public void DeleteAccount(int accountId)
		{
			accountDB.Accounts.Remove(accountDB.Accounts.Find(accountId));
			accountDB.SaveChanges();
		}
	}
}