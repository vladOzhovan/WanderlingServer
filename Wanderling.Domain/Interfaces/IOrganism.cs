namespace Wanderling.Domain.Interfaces
{
    public interface IOrganism
    {
        Guid Id { get; }
        Guid UserId { get; }
        string Name { get; }
        string Description { get; }
        public bool IsDiscovered { get; }
        DateTime DiscoverededAt { get; }
        DateTime CreatedAt { get; }

        void Born();
        void Grow();
        void Die();
        void Discover(Guid userId);
    }
}
