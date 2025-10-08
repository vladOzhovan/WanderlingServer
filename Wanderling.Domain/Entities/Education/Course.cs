namespace Wanderling.Domain.Entities.Education
{
    public class Course
    {
        public Guid Id { get; set; }
        public Guid TeacherId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int DurationInHours { get; set; }
        public Speciality Speciality { get; set; } = new();
        public Teacher Teacher { get; set; } = new();
        public List<UserSkill> Skills { get; set; } = new();
        public DateTime DateStarted { get; set; }
        public DateTime DateEnded { get; set; }
    }
}
