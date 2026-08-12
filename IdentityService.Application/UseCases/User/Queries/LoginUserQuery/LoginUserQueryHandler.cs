using BuildingBlocks.CQRS;
using MediatR;
using Auth;
using IdentityService.Domain.Interfaces.Repositories.BusinessIRepositories.UserRepository;
using IdentityService.Application.Common.Security;
using IdentityService.Application.UseCases.User.ViewModels;
using System.Security.Claims;
using IdentityService.Domain.Interfaces;

namespace IdentityService.Application.UseCases.User.Queries.LoginUserQuery
{
  

    public class LoginUserQueryHandler : IQueryHandler<LoginUserQueryRequest, UserResponseDto>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginUserQueryHandler(
            IUnitOfWork _unitOfWork,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            unitOfWork = _unitOfWork;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<UserResponseDto> Handle(LoginUserQueryRequest request, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.UserRepositoryQuery.GetByUserNameAsync(request.UserName, cancellationToken);
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
