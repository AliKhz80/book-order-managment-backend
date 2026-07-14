using IdentityService.Domain.Entities;
using IdentityService.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Domain.Interfaces.BusinessIRepositories
{
    public interface IUserRepository : IRepository<User , long>
    {
    }
}
