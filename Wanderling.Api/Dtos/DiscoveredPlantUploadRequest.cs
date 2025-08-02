using System.ComponentModel.DataAnnotations;

namespace Wanderling.Api.Dtos
{
    public class DiscoveredPlantUploadRequest
    {
        [Required]
        public IFormFile image { get; set; } = null!;
    }
}
