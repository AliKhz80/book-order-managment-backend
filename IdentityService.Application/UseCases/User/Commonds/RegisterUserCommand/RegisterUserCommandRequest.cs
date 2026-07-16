using IdentityService.Application.UseCases.User.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.UseCases.User.Commonds.RegisterUserCommand
{
    public class RegisterUserCommandRequest : IRequest<UserResponseDto>
    {
        [Required(ErrorMessage = "FullName is required.")]
        [StringLength(100, ErrorMessage = "FullName cannot exceed 100 characters.")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "UserName is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "UserName must be between 3 and 50 characters.")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Invalid phone number.")]
        public string? PhoneNumber { get; set; }
    }

}
