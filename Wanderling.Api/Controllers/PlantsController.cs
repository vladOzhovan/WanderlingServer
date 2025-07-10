using Microsoft.AspNetCore.Mvc;
using Wanderling.Api.Dtos;
using Wanderling.Application.Interfaces;
using Wanderling.Application.Models;

namespace Wanderling.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlantsController : ControllerBase
    {
        public PlantsController()
        {
            
        }

        public async Task<IActionResult> Create([FromServices] IPlantCreationService service, [FromBody] PlantCreateDto dto)
        {
            var model = new PlantCreateModel
            {
                Name = dto.Name ?? string.Empty,
                TypeKey = dto.TypeName ?? string.Empty,
                ReproductionKey = dto.ReproductionType ?? string.Empty
            };

            var plant = await service.CreatePlantAsync(model);

            return Ok(plant);
        }
    }
}
