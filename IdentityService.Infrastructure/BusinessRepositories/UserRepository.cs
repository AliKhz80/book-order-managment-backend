using IdentityService.Domain.Entities;
using IdentityService.Domain.Interfaces;
using IdentityService.Domain.Interfaces.BusinessIRepositories;
using IdentityService.Infrastructure.Repositories;

namespace IdentityService.Infrastructure.BusinessRepositories
{
    public class UserRepository(IdentityDbContext dbContext , ICurrentUser currentUser) : Repository<User , long>(dbContext , currentUser), IUserRepository
    {





    }
}
