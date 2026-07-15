using IdentityService.Domain.Entities;
using IdentityService.Domain.Interfaces.Repositories.BusinessIRepositories.UserRepository;

namespace IdentityService.Infrastructure.Repositories.BusinessRepositories.UserRepository
{
    public class UserRepositoryQuery(IdentityDbContext dbContext) : RepositoryQuery<User , long>(dbContext), IUserRepositoryQuery
    {





    }
}
