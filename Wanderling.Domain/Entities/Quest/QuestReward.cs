namespace Wanderling.Domain.Entities.Quest
{
    public class QuestReward
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
