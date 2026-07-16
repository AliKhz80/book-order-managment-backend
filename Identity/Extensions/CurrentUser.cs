using IdentityService.Application.Common.CurrentUser;
using Microsoft.AspNetCore.Http;

namespace Identity.Extensions
{
    public class CurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser
    {
        public string IPAddress => httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
        public string UserName => httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "System";
    }
}
