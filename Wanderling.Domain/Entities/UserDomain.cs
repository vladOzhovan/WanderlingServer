namespace Wanderling.Domain.Entities
{
    public class UserDomain
    {
        public UserDomain(string userName, string email, string passwordHash, string role)
        {
            Id = Guid.NewGuid();
            UserName = userName;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
            CreatedAt = DateTime.UtcNow;
        }
        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string Role { get; private set; }
        public string PhoneNumber { get; private set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? FullName
        {
            get
            {
                var first = FirstName ?? string.Empty;
                var second = SecondName ?? string.Empty;
                return $"{first} {second}".Trim();
            }
        }

        public static UserDomain Create(string userName, string email, string passwordHash, string role) =>
            new UserDomain(userName, email, passwordHash, role);

        public void AssignPhone(string phoneNumber)
        {
            if (!string.IsNullOrWhiteSpace(phoneNumber))
                PhoneNumber = phoneNumber;
        }
    }
}
