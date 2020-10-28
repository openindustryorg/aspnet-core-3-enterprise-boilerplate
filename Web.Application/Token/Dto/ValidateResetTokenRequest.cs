using System.ComponentModel.DataAnnotations;

namespace Web.Application.Models.Token
{
    public class ValidateResetTokenRequest
    {
        [Required]
        public string Token { get; set; }
    }
}