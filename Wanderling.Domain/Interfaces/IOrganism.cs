namespace Wanderling.Domain.Interfaces
{
    public interface IOrganism
    {
        Guid Id { get; }
        Guid? UserId { get; }
        string ScientificName { get; }
        string DisplayedName { get; set; }
        string Description { get; set; }
        string Reproduction { get; }
        string ImageUrl { get; set; }
        public bool IsDiscovered { get; }
        DateTime CreatedAt { get; }
        DateTime? DiscoverededAt { get; }

        void Born();
        void Grow();
        void Die();
        void Discover(Guid userId);
    }
}
