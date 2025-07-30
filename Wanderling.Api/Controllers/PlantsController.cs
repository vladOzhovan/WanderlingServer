using Microsoft.AspNetCore.Mvc;
using Wanderling.Api.Dtos;
using Wanderling.Application.Interfaces;
using Wanderling.Application.Mappers;
using Wanderling.Application.Models;
using Wanderling.Domain.Entities.Collections.Plants;
using Wanderling.Infrastructure.Extensions;

namespace Wanderling.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlantsController : ControllerBase
    {
        [HttpPost("create-virtual")]
        public async Task<IActionResult> CreateVirtual([FromServices] IPlantCreationService service, [FromBody] PlantCreateDto dto)
        {
            var model = new PlantCreateModel
            {
                SpeciesKey = dto.SpeciesName ?? string.Empty,
                TypeKey = dto.TypeName ?? string.Empty,
                ReproductionKey = dto.ReproductionType ?? string.Empty
            };

            var plant = await service.CreatePlantAsync(model) as Plant;

            if (plant == null)
                return BadRequest("Failed to create plant");

            return Ok(plant.ToPlantDto());
        }

        [HttpPost("create-discovered/{userId:Guid}")]
        public async Task<IActionResult> CreateDiscovered([FromServices] IPlantCreationService service, [FromBody] PlantCreateDto dto)
        {
            var userId = User.GetUserId();



            return Ok();
        }

        //[HttpPost("create-discovered/{userId:Guid}")]
        //public async Task<IActionResult> CreateDiscovered([FromServices] IPlantCreationService service, [FromBody] PlantCreateDto dto)
        //{
        //    var userId = User.GetUserId();

        //    var model = new PlantCreateModel
        //    {
        //        SpeciesKey = dto.SpeciesName ?? string.Empty,
        //        TypeKey = dto.TypeName ?? string.Empty,
        //        ReproductionKey = dto.ReproductionType ?? string.Empty
        //    };

        //    var plant = await service.CreatePlantAsync(model) as Plant;

        //    if (plant == null)
        //        return BadRequest("Failed to create plant");

        //    plant.Discover(userId);

        //    return Ok(plant.ToPlantDto());
        //}

        [HttpPost("identify")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Identify(IFormFile image, [FromServices] IPlantRecognitionService recognitionService)
        {
            if (image == null || image.Length == 0)
                return BadRequest("Upload a plant image.");

            byte[] imageBytes;

            using(var stream = new MemoryStream())
            {
                await image.CopyToAsync(stream);
                imageBytes = stream.ToArray();
            }

            var recognitionresult = await recognitionService.IdentifyPlantAsync(imageBytes);

            return Ok(recognitionresult);
        }
    }
}
