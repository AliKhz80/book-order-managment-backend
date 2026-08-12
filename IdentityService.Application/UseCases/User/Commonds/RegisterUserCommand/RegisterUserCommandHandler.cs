using BuildingBlocks.CQRS;
using MediatR;
using Auth;
using IdentityService.Domain.Interfaces;
using IdentityService.Domain.Interfaces.Repositories.BusinessIRepositories.UserRepository;
using IdentityService.Application.Common.Security;
using IdentityService.Application.UseCases.User.ViewModels;
using System.Security.Claims;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityService.Application.UseCases.User.Commonds.RegisterUserCommand
{
    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommandRequest, UserResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public RegisterUserCommandHandler(
            IUnitOfWork unitOfWork,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<UserResponseDto> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
        {
            // Check if user already exists
            var existingUser = await _unitOfWork.UserRepositoryQuery.GetByUserNameAsync(request.UserName, cancellationToken);
            if (existingUser != null)
            {
                throw new InvalidOperationException($"Username '{request.UserName}' is already taken.");
            }

            // Map and create user using static UserMapper
            var user = request.ToEntity();
            user.Password = PasswordHasher.Hash(request.Password);

            await _unitOfWork.UserRepositoryCommond.AddAsync(user, cancellationToken);
            await _unitOfWork.CommitAsync();

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
