using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class AddressEntity : Entity
    {
#nullable disable
        [Required]
        [Column(TypeName = "nvarchar(256)")]
        public string Address { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(128)")]
        public string Country { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(128)")]
        public string City { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(32)")]
        public string ZipCode { get; set; }
#nullable restore

        [Column(TypeName = "nvarchar(128)")]
        public string? State { get; set; }

        [Column(TypeName = "nvarchar(256)")]
        public string? AdditionalAddress { get; set; }
    }
}