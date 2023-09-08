using System.ComponentModel.DataAnnotations;

namespace Core.Interfaces
{
    public interface IEntity
    {
        public Guid Id { get; set; }
    }
}