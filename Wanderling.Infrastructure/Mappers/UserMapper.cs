using Wanderling.Domain.Entities;
using Wanderling.Infrastructure.Entities;

namespace Wanderling.Infrastructure.Mappers
{
    public static class UserMapper
    {
        public static UserEntity ToEntity(this UserDomain userDomain)
        {
            return new UserEntity
            {
                Id = userDomain.Id,
                UserName = userDomain.UserName,
                Email = userDomain.Email,
                PasswordHash = userDomain.PasswordHash,
                PhoneNumber = userDomain.PhoneNumber,
                CreatedAt = userDomain.CreatedAt,

            };
        }

        public static UserDomain ToDomainUser(this UserEntity userEntity)
        {
            var userDomain = UserDomain.Create(
                userEntity?.UserName ?? string.Empty,
                userEntity?.Email ?? string.Empty,
                userEntity?.PasswordHash ?? string.Empty,
                "User"
            );

            userDomain.AssignPhone(userEntity?.PhoneNumber ?? string.Empty);
            userDomain.FirstName = userEntity?.FirstName ?? string.Empty;
            userDomain.SecondName = userEntity?.SecondName ?? string.Empty;
            userDomain.CreatedAt = userEntity?.CreatedAt ?? DateTime.MinValue;

            return userDomain;
        }
    }
}
