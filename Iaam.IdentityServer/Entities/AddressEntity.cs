namespace Iaam.IdentityServer.Entities
{
    public class AddressEntity : Entity
    {
#nullable disable
        public string Address { get; set; }
#nullable restore
        public string? AdditionalAddress { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
    }
}