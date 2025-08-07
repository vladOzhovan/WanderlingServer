using Microsoft.AspNetCore.Mvc;
using Wanderling.Application.Dtos;
using Wanderling.Application.Interfaces;
using Wanderling.Infrastructure.Services;

namespace Wanderling.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserAccountService _accountService;

        public AccountController(IUserAccountService accountService)
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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _accountService.LoginAsync(dto);

            return result.IsSuccess 
                ? Ok(result.Value)
                : Unauthorized(result.Errors);
        }
    }
}
