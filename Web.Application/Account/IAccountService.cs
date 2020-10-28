using System.Collections.Generic;
using Web.Application.Models.Accounts;

namespace Web.Application.Services
{
  public interface IAccountService
  {
    AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress);
    void Register(RegisterRequest model, string origin);
    void VerifyEmail(string token);
    void ForgotPassword(ForgotPasswordRequest model, string origin);
    void ResetPassword(ResetPasswordRequest model);
    IEnumerable<AccountResponse> GetAll();
    AccountResponse GetById(int id);
    AccountResponse Create(CreateRequest model);
    AccountResponse Update(int id, UpdateRequest model);
    void Delete(int id);
  }
}