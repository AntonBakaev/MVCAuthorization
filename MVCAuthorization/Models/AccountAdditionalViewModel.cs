using System.ComponentModel.DataAnnotations;

namespace MVCAuthorization.Models
{
    public class AccountAdditionalViewModel
    {
        [Required(ErrorMessage = "Sex is required.")]
        public string Sex { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public Country Country { get; set; }
    }
}
