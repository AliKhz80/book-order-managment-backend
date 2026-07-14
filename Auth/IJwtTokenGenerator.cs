using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Auth
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(string userId, string email, IEnumerable<Claim>? customClaims = null);
    }
}
