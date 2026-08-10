using CatalogService.Application.Common.CurrentUser;
using CatalogService.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CatalogService.Presentation.Extentions;

public class CurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser
{
    public string IPAddress => httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
    public string UserName => httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "System";

}
