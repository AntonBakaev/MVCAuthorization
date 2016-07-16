using System.Collections.Generic;

namespace MVCAuthorization.Models.DataAccessLevel
{
    public class CountryRepository : ICountryRepository
    {
        private readonly AccountsDBEntities accountDB = new AccountsDBEntities();

        public IEnumerable<Country> GetCountries()
        {
            return accountDB.Countries;
        }

        public Country GetCountryById(int countryId)
        {
            return accountDB.Countries.Find(countryId);
        }
    }
}
