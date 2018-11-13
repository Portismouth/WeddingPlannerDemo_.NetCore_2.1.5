using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.ViewModels
{
    public class LoginViewModel
    {
        [Required (ErrorMessage = "Invalid Login")]
        [EmailAddress (ErrorMessage = "Invalid Login")]
        [Display (Name = "Email Address")]
        public string LoginEmail { get; set; }

        [Required (ErrorMessage = "Invalid Login")]
        [DataType (DataType.Password)]
        [Display (Name = "Password")]
        public string LoginPassword { get; set; }
    }
}