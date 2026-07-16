using MediatR;
using Auth;
using IdentityService.Domain.Interfaces.Repositories.BusinessIRepositories.UserRepository;
using IdentityService.Application.Common.Security;
using IdentityService.Application.UseCases.User.ViewModels;
using System.Security.Claims;

namespace IdentityService.Application.UseCases.User.Queries.LoginUserQuery
{
  

    public class LoginUserQueryHandler : IRequestHandler<LoginUserQueryRequest, UserResponseDto>
    {
        private readonly IUserRepositoryQuery _userRepositoryQuery;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginUserQueryHandler(
            IUserRepositoryQuery userRepositoryQuery,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepositoryQuery = userRepositoryQuery;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<UserResponseDto> Handle(LoginUserQueryRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepositoryQuery.GetByUserNameAsync(request.UserName, cancellationToken);
            if (user == null || !PasswordHasher.Verify(request.Password, user.Password))
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            // Generate JWT Token
            var claims = new[]
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim("UserName", user.UserName),
                new Claim("FullName", user.FullName)
            };

            var token = _jwtTokenGenerator.GenerateToken(claims);

            return new UserResponseDto
            {
                Token = token,
                UserName = user.UserName,
                FullName = user.FullName
            };
        }
    }
}
