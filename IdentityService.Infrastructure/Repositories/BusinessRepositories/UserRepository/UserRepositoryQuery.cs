using Microsoft.EntityFrameworkCore;
using IdentityService.Domain.Entities;
using IdentityService.Domain.Interfaces.Repositories.BusinessIRepositories.UserRepository;

namespace IdentityService.Infrastructure.Repositories.BusinessRepositories.UserRepository
{
    public class UserRepositoryQuery(IdentityDbContext dbContext) : RepositoryQuery<User , long>(dbContext), IUserRepositoryQuery
    {
        public async Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default)
        {
            return await SetAsNoTracking.FirstOrDefaultAsync(u => u.UserName == userName, cancellationToken);
        }
    }
}
