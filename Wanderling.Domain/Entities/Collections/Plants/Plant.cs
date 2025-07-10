using Wanderling.Domain.Interfaces;

namespace Wanderling.Domain.Entities.Collections.Plants
{
    public abstract class Plant : IOrganism
    {
        private readonly IReproduction _reproduction;
        protected Plant(string name, IReproduction reproduction)
        {
            Name = name;
            _reproduction = reproduction;
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; }
        public string Name { get; }
        public bool IsDiscovered { get; private set; }
        public DateTime CreatedAt { get; }
        public DateTime DiscoveryDate { get; private set; }

        public abstract void Born();
        public abstract void Grow();
        public abstract void Die();
        
        public void Discover()
        {
            if(!IsDiscovered)
            {
                IsDiscovered = true;
                DiscoveryDate = DateTime.UtcNow;
            }
        }

        public void Reproduce()
        {
            _reproduction.Reproduce(this);
        }

        //public Dictionary<string, object> Properties { get; } = new();
    }
}
