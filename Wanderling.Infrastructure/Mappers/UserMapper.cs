using Wanderling.Domain.Entities;
using Wanderling.Infrastructure.Entities;

namespace Wanderling.Infrastructure.Mappers
{
    public static class UserMapper
    {
        public static UserEntity ToEntity(this User user)
        {
            return new UserEntity
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                PhoneNumber = user.PhoneNumber,
                CreatedAt = user.CreatedAt,

            };
        }

        public static User ToDomainUser(this UserEntity entity)
        {
            var user = User.Create(
                entity.Id,
                entity.UserName,
                entity.Email,
                entity.PasswordHash
            );

            user.AssignPhone(entity.PhoneNumber);
            user.FirstName = entity.FirstName;
            user.SecondName = entity.SecondName;
            user.CreatedAt = entity.CreatedAt;

            return user;
        }
    }
}
