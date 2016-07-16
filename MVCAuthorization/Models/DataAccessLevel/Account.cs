using System.ComponentModel.DataAnnotations;

namespace MVCAuthorization.Models
{
    public partial class Account
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        [Required]
        [Display(Name = "Sex")]
        public string Sex { get; set; }

        [Required]
        [Display(Name = "Country")]
        public Country Country { get; set; }
    }
}