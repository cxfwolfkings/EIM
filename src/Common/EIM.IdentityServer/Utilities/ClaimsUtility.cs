using EIM.IdentityServer.Models;
using IdentityModel;
using System.Collections.Generic;
using System.Security.Claims;

namespace EIM.IdentityServer.Utilities
{
    public static class ClaimsUtility
    {
        public static IEnumerable<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Subject, user.Id.ToString()),
                new Claim(JwtClaimTypes.PreferredUserName, user.Username),
            };

            return claims;
        }
    }
}
