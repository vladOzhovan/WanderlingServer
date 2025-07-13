namespace Wanderling.Domain.Interfaces
{
    public interface IOrganism
    {
        Guid Id { get; }
        Guid? UserId { get; }
        string Name { get; }
        string Description { get; }
        string Reproduction { get; }
        public bool IsDiscovered { get; }
        DateTime CreatedAt { get; }
        DateTime DiscoverededAt { get; }

        void Born();
        void Grow();
        void Die();
        void Discover(Guid userId);
    }
}
