﻿namespace Wanderling.Application.Dtos
{
    public class AuthenticationDto
    {
        public Guid UserId {  get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
