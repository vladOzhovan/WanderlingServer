using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Wanderling.Application.Dtos;
using Wanderling.Application.Interfaces;
using Wanderling.Infrastructure.Entities;

namespace Wanderling.Infrastructure.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;

        public UserAccountService(
            UserManager<UserEntity> userManager,
            SignInManager<UserEntity> signinManager,
            IConfiguration config, 
            ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signinManager;
            _config = config;
            _tokenService = tokenService;
        }

        public async Task<Result<AuthenticationDto>> RegisterUserAsync(RegisterDto dto)
        {
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);

            if (existingUser != null)
                return Result.Fail<AuthenticationDto>("User already exists");

            var userEntity = new UserEntity
            {
                Id = Guid.NewGuid(),
                UserName = dto.UserName,
                Email = dto.Email,
                CreatedAt = DateTime.UtcNow
            };

            var creationResult = await _userManager.CreateAsync(userEntity, dto.Password);

            if (!creationResult.Succeeded)
                return Result.Fail<AuthenticationDto>(creationResult.Errors.Select(e => e.Description));

            var roleResult = await _userManager.AddToRoleAsync(userEntity, "User");

            if (!roleResult.Succeeded)
                return Result.Fail<AuthenticationDto>(roleResult.Errors.Select(e => e.Description));

            var tokenDto = new TokenDto
            {
                UserId = userEntity.Id,
                UserName = userEntity.UserName,
                Email = userEntity.Email,
                Role = "User"
            };

            var token = _tokenService.GenerateToken(tokenDto);

            var authDto = new AuthenticationDto
            {
                UserId = userEntity.Id,
                UserName = userEntity.UserName,
                Email = userEntity.Email,
                Role = "User",
                Token = token
            };

            return Result.Ok(authDto);
        }

        public async Task<Result<AuthenticationDto>> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
                return Result.Fail<AuthenticationDto>("Invalid email or password");

            var passwordResult = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);

            if (!passwordResult.Succeeded)
                return Result.Fail<AuthenticationDto>("Invalid email or password");

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "User";

            var tokenDto = new TokenDto
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Role = role
            };

            var token = _tokenService.GenerateToken(tokenDto);

            var autDto = new AuthenticationDto
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Role = role,
                Token = token
            };

            return Result.Ok(autDto);
        }
    }
}
