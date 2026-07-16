namespace IdentityService.Application.UseCases.User.ViewModels
{
    public class UserResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
    }
}
