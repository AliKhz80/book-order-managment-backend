using Microsoft.AspNetCore.Http;
using OrderService.Application.Common.CurrentUser;

namespace OrderService.Presentation.Extentions;

public class CurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser
{
    public string IPAddress => httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
    public string UserName => httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "System";
}
