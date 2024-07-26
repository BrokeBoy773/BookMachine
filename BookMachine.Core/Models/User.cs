namespace BookMachine.Core.Models
{
    public class User
    {
        public Guid UserId { get; }
        public string UserName { get; } = string.Empty;
        public string Email { get; } = string.Empty;
        public string PasswordHash { get; } = string.Empty;

        private User(Guid userId, string userName, string email, string passwordHash)
        {
            UserId = userId;

            UserName = userName;

            Email = email;

            PasswordHash = passwordHash;
        }

        public static (User User, List<string> Errors) Create(Guid userId, string userName, string email, string passwordHash)
        {
            List<string> errors = [];

            User user = new(userId, userName, email, passwordHash);

            return (user, errors);
        }
    }
}
