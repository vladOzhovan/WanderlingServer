namespace Wanderling.Domain.Entities.Education
{
    public class Teacher
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public List<Course> Courses { get; set; } = new();
    }
}
