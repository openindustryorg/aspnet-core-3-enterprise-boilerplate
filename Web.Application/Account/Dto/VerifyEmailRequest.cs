using System.ComponentModel.DataAnnotations;

namespace Web.Application.Models.Accounts
{
    public class VerifyEmailRequest
    {
        [Required]
        public string Token { get; set; }
    }
}