using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.ViewModels
{
    public class RegistrationViewModel
    {
        [Required]
        [MinLength (2, ErrorMessage = "You need more stuff")]
        [Display (Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [MinLength (2, ErrorMessage = "You need more stuff")]
        [Display (Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display (Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType (DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType (DataType.Password)]
        [Compare ("Password")]
        [Display (Name = "Passowrd Confirmation")]
        public string PasswordConfirmation { get; set; }
    }
}