using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class UserEntity : IdentityUser
    {
        [Column(TypeName = "nvarchar(128)")]
        public string? FirstName { get; set; }

        [Column(TypeName = "nvarchar(128)")]
        public string? LastName { get; set; }

        public AddressEntity? Address { get; set; }
    }
}