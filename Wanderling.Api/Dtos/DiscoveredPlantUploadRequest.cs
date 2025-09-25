using System.ComponentModel.DataAnnotations;

namespace Wanderling.Api.Dtos
{
    /// <summary>
    /// Represents a request to upload an image of a discovered plant.
    /// </summary>
    /// <remarks>This request is typically used to submit an image for processing or identification
    /// purposes.</remarks>
    public class DiscoveredPlantUploadRequest
    {
        [Required]
        public IFormFile Image { get; set; } = null!;
    }
}
