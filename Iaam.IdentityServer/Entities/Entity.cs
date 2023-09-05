using System.ComponentModel.DataAnnotations;

namespace Iaam.IdentityServer.Entities
{
    public class Entity
    {
        [Key]
        public Guid Id { get; set; } = default!;

        public Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}