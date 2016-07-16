using System.Collections.Generic;

namespace MVCAuthorization.Models.DataAccessLevel
{
    public interface ICountryRepository
    {
        IEnumerable<Country> GetCountries();
        Country GetCountryById(int countryId);
    }
}
