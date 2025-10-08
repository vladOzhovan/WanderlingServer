using Wanderling.Domain.Enums;

namespace Wanderling.Domain.Entities.Education
{
    public class Grade
    {
        public Guid Id { get; set; }
        public Guid StudentCourseId { get; set; }
        public DateTime DateAwarded { get; set; }
        public int Value { get; set; }
        public string? Comments { get; set; } = string.Empty;
        public string? Examiner { get; set; } = string.Empty;
        public GradeType Type { get; set; }
    }
}
