using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Wanderling.Application.Dtos;
using Wanderling.Application.Interfaces;
using Wanderling.Domain.Entities;
using Wanderling.Infrastructure.Entities;

namespace Wanderling.Infrastructure.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;
        private readonly UserManager<UserEntity> _userManager;

        public UserAccountService(UserManager<UserEntity> userManager, IConfiguration config, ITokenService tokenService)
        {
            _userManager = userManager;
            _config = config;
            _tokenService = tokenService;
        }

        public async Task<Result<AuthenticationDto>> RegisterUserAsync(RegisterDto dto)
        {
            var existingUser = _userManager.FindByEmailAsync(dto.Email);

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

            var userDomain = UserDomain.Create(dto.UserName, dto.Email, userEntity.PasswordHash, "User");

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
                Token = ""
            };

            return Result.Ok(authDto);
        }
    }
}
