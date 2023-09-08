using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class UserEntity : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public AddressEntity? Address { get; set; }
    }
}