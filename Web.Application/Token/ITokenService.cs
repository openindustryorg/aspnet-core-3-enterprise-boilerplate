using System.Collections.Generic;
using Web.Application.Models.Accounts;
using Web.Application.Models.Token;

namespace Web.Application.Services
{
  public interface ITokenService
  {
    AuthenticateResponse RefreshToken(string token, string ipAddress);
    void RevokeToken(string token, string ipAddress);
    void ValidateResetToken(ValidateResetTokenRequest model);
  }
}