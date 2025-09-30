using Microsoft.AspNetCore.Mvc;
using Wanderling.Domain.Entities.Inventory;

namespace Wanderling.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {
        public InventoryController()
        {
            
        }

        [HttpPost("add-item")]
        public async Task<IActionResult> AddItem()
        {   
            return Ok();
        }
    }
}
