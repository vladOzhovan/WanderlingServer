namespace Wanderling.Domain.Entities.Education
{
    public class StudentCourse
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public List<Grade> Grades { get; set; } = new();
        public double? FinalGrade => Grades.Any() ? Grades.Average(g => g.Value) : null;
    }
}
