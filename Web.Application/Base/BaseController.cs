using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Core;

namespace Web.Application.Controllers
{
  [ApiExplorerSettings(GroupName = "Core")]
  [Controller]
  public abstract class BaseController : ControllerBase
  {
    // returns the current authenticated account (null if not logged in)
    public Account Account => (Account)HttpContext.Items["Account"];
  }
}