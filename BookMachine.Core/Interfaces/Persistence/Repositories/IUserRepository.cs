using BookMachine.Core.Models;

namespace BookMachine.Core.Interfaces.Persistence.Repositories
{
    public interface IUserRepository
    {
        public Task<User?> GetUserByEmailAsync(string email);

        public Task<Guid> CreateUserAsync(User user);
    }
}
