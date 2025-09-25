namespace Wanderling.Application.Models
{
    /// <summary>
    /// Model for creating a domain entity – Plant
    /// </summary>
    public class PlantCreateModel
    {
        public string SpeciesKey { get; set; } = string.Empty;
        public string TypeKey { get; set; } = string.Empty;
        public string ReproductionKey { get; set; } = string.Empty;
    }
}
