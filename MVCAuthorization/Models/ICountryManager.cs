using System.Collections.Generic;

namespace MVCAuthorization.Models
{
    public interface ICountryManager
    {
        IEnumerable<Country> GetCountries();
        Country GetCountryById(int countryId);
    }
}
