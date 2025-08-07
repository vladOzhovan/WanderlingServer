using FluentResults;
using Wanderling.Application.Dtos;

namespace Wanderling.Application.Interfaces
{
    public interface IUserAccountService
    {
        Task<Result<AuthenticationDto>> RegisterUserAsync(RegisterDto dto);
        Task<Result<AuthenticationDto>> LoginAsync(LoginDto dto);
    }
}
