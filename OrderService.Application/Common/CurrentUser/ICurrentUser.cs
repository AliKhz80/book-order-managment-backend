namespace OrderService.Application.Common.CurrentUser;

public interface ICurrentUser
{
    public string IPAddress { get; }
    public string UserName { get; }
}
