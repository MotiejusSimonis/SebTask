using SEBtask.Models.Entities;
using System.Security.Claims;

namespace SEBtask.Services
{
    public interface ITokenService
    {
        public string GenerateToken(Client client);
        public long GetClientPersonalId(ClaimsPrincipal user);
        public bool HasClaims(ClaimsPrincipal user);
    }
}
