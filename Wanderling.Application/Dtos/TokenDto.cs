namespace Wanderling.Application.Dtos
{
    public class TokenDto
    {
        public Guid UserId { get; init; }
        public string UserName { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string Role { get; init; } = string.Empty;
    }
}
