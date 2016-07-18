using System.Collections.Generic;
using MVCAuthorization.Models.DataAccessLevel;

namespace MVCAuthorization.Models
{
	public class CountryManager : ICountryManager
	{
		private ICountryRepository countryRepository;

		public CountryManager(ICountryRepository countryRepository)
		{
			this.countryRepository = countryRepository;
		}

		public IEnumerable<Country> GetCountries()
		{
			return countryRepository.GetCountries();
		}

		public Country GetCountryById(int countryId)
		{
			return countryRepository.GetCountryById(countryId);
		}
	}
}