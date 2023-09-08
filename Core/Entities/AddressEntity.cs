using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class AddressEntity : Entity
    {
#nullable disable
        [Required]
        public string Address { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string ZipCode { get; set; }
#nullable restore
        public string? State { get; set; }
        public string? AdditionalAddress { get; set; }
    }
}