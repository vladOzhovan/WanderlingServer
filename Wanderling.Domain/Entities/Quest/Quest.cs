using Wanderling.Domain.Enums;

namespace Wanderling.Domain.Entities.Quest
{
    public class Quest
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public QuestStatus Status { get; set; }
    }
}
