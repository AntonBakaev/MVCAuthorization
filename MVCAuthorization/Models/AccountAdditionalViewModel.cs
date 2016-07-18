using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCAuthorization.Models
{
	public class AccountAdditionalViewModel
	{
		[Required(ErrorMessage = "Sex is required.")]
		public string Sex { get; set; }

		[Required(ErrorMessage = "Country is required.")]
		public int SelectedCountryId { get; set; }

		public IEnumerable<SelectListItem> CountryNames { get; set; }
	}
}
