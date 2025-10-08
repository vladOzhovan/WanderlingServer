namespace Wanderling.Domain.Entities.Education
{
    public class University
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<Course> Courses { get; set; } = new();
        public List<Teacher> Teachers { get; set; } = new();
    }
}
