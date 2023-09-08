using Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Entity : IEntity
    {
        [Key]
        public Guid Id { get; set; } = default!;

        public Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}