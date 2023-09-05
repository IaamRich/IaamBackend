using Microsoft.AspNetCore.Identity;

namespace Iaam.IdentityServer.Entities
{
    public class UserEntity : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Guid? AddressId { get; set; }
        public AddressEntity? Address { get; set; }
    }
}