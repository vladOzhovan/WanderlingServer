namespace Wanderling.Domain.Interfaces
{
    public interface IOrganism
    {
        Guid Id { get; }
        string Name { get; }
        bool IsDiscovered { get; }
        DateTime CreatedAt { get; }
        DateTime DiscoveryDate { get; }

        void Born();
        void Grow();
        void Die();
        void Discover();
    }
}
