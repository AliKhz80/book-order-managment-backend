using CatalogService.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CatalogService.Application.Extentions;

public class CurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser
{
    public string IPAddress => httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
    public string UserName => "";
}
