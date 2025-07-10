using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace booklend.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected Guid GetUserIdOrThrow()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return id is null
            ? throw new UnauthorizedAccessException("user not authenticated") : Guid.Parse(id);
        }
    }
}