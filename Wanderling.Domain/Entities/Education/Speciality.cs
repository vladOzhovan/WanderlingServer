namespace Wanderling.Domain.Entities.Education
{
    public class Speciality
    {
        public Guid Id { get; set; }
        public int Level { get; set; } = 1;
        public DateTime DateAssignment { get; set; }
    }
}
