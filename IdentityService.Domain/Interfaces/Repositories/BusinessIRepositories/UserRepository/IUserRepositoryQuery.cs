using IdentityService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Domain.Interfaces.Repositories.BusinessIRepositories.UserRepository
{
    public interface IUserRepositoryQuery : IRepositoryQuery<User , long>
    {
    }
}
