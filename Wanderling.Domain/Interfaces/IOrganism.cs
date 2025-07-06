namespace Wanderling.Domain.Interfaces
{
    public interface IOrganism
    {
        Guid Id { get; set; }
        string Name { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime DiscoveryDate { get; set; }

        void Born();
        void Grow();
        void Die();
    }
}
