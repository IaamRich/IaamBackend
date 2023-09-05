namespace Iaam.IdentityServer.Entities
{
    public class Entity
    {
        protected Guid Id { get; set; }

        public Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}