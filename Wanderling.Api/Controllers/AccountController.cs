using Microsoft.AspNetCore.Mvc;
using Wanderling.Application.Dtos;
using Wanderling.Infrastructure.Services;

namespace Wanderling.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserAccountService _accountService;

        public AccountController(UserAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register-user")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDto dto)
        {
            var result = await _accountService.RegisterUserAsync(dto);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Errors);
        }
    }
}
