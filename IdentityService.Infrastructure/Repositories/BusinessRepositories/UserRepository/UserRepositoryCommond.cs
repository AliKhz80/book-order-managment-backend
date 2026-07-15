using IdentityService.Domain.Entities;
using IdentityService.Domain.Interfaces;
using IdentityService.Domain.Interfaces.Repositories.BusinessIRepositories.UserRepository;

namespace IdentityService.Infrastructure.Repositories.BusinessRepositories.UserRepository
{
    public class UserRepositoryCommond(IdentityDbContext dbContext , ICurrentUser currentUser) : RepositoryCommond<User , long>(dbContext , currentUser), IUserRepositoryCommond
    {





    }
}
