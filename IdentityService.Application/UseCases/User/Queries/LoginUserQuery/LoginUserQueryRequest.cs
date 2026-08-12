using BuildingBlocks.CQRS;
using IdentityService.Application.UseCases.User.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.UseCases.User.Queries.LoginUserQuery
{
    public class LoginUserQueryRequest : IQuery<UserResponseDto>
    {
        [Required(ErrorMessage = "UserName is required.")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = string.Empty;
    }
}
