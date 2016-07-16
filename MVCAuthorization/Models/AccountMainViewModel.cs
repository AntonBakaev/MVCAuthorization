using System.ComponentModel.DataAnnotations;

namespace MVCAuthorization.Models
{
    public class AccountMainViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        //[Display(Name = "Username")]
        [StringLength(30, MinimumLength = 2,
            ErrorMessage = "The username must be more than 2 and less than 30 characters long.")]
        [RegularExpression(@"^[a-zA-Z]*$",
            ErrorMessage = "Username must contain only latin characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        //[Display(Name = "Password")]
        [StringLength(100, MinimumLength = 6,
            ErrorMessage = "The password must be more than 6 and less than 100 characters long.")]
        [RegularExpression(@"^(?=.*\d)(?=.*[A-Z]).*",
            ErrorMessage = "Password must contain at least one uppercase letter and one digit.")]
        public string Password { get; set; }
    }
}
