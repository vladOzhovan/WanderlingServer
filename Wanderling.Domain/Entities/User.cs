namespace Wanderling.Domain.Entities
{
    public class User
    {
        public User(Guid id, string userName, string email, string passwordHash)
        {
            Id = id;
            UserName = userName;
            Email = email;
            PasswordHash = passwordHash;
        }
        public Guid Id { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
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
        public DateTime CreatedAt { get; set; }

        public static User Create(Guid id, string userName, string email, string passwordHash) =>
            new User(id, userName, email, passwordHash);

        public void AssignPhone(string phoneNumber)
        {
            if (!string.IsNullOrWhiteSpace(phoneNumber))
                PhoneNumber = phoneNumber;
        }
    }
}
