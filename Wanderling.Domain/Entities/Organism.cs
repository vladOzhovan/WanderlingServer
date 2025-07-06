using Wanderling.Domain.Interfaces;

namespace Wanderling.Domain.Entities
{
    public abstract class Organism : IOrganism
    {
        private readonly IReproductionStrategy _strategy;

        protected Organism(IReproductionStrategy strategy)
        {
            _strategy = strategy;
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime DiscoveryDate { get; set; }

        public abstract void Born();
        public abstract void Grow();
        public abstract void Die();

        public void Reproduce()
        {
            _strategy.Reproduce(this);
        }
    }
}
