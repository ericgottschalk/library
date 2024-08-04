using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models.Member
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(255)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(36)]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }
    }
}