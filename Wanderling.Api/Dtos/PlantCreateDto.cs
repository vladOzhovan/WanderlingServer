namespace Wanderling.Api.Dtos
{
    /// <summary>
    /// Represents the data transfer object used to create a new plant.
    /// </summary>
    /// <remarks>This DTO is used to encapsulate the necessary information for creating a plant,  including
    /// its species name, type, and reproduction method.</remarks>
    public class PlantCreateDto
    {
        public string SpeciesName { get; set; } = string.Empty;
        public string TypeName { get; set; } = string.Empty;
        public string ReproductionType { get; set; } = string.Empty;
    }
}
