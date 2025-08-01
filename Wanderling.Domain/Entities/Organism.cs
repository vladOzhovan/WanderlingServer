using Wanderling.Domain.Interfaces;

namespace Wanderling.Domain.Entities
{
    public abstract class Organism : IOrganism
    {
        private readonly IReproduction _reproduction;

        protected Organism(string speciesName, IReproduction reproduction)
        {
            Id = Guid.NewGuid();
            ScientificName = speciesName;
            _reproduction = reproduction;
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; }
        public Guid? UserId {  get; private set; }
        public string ScientificName { get; set; } = string.Empty;
        public string DisplayedName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Reproduction => _reproduction.GetType().Name;
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool IsDiscovered { get; private set; } = false;
        public DateTime? DiscoverededAt { get; private set; }


        public abstract void Born();
        public abstract void Grow();
        public abstract void Die();

        public void Discover(Guid userId)
        {
            if (!IsDiscovered)
            {
                IsDiscovered = true;
                DiscoverededAt = DateTime.UtcNow;
                UserId = userId;
            }
        }

        public void Reproduce()
        {
            _reproduction.Reproduce(this);
        }
    }
}
