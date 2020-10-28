using System.ComponentModel.DataAnnotations;

namespace Web.Application.Models.Accounts
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}