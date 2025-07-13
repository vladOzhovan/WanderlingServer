namespace Wanderling.Domain.Entities.Collections.Funguses
{
    public abstract class Fungus
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime DiscoveryDate { get; set; }
        
        public abstract void Born();
        public abstract void Grow();
        public abstract void Die();
        public abstract void Reproduce();
    }
}
