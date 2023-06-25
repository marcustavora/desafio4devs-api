using Microsoft.AspNetCore.Identity;

namespace Desafio4Devs.Domain.Entities
{
    public class AppIdentityUser : IdentityUser
    {
        public string? Name { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}