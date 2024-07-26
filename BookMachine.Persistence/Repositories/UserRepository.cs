using BookMachine.Core.Interfaces.Persistence.Repositories;
using BookMachine.Core.Models;
using BookMachine.Persistence.Entities;
using BookMachine.Persistence.Mappings;
using Microsoft.EntityFrameworkCore;

namespace BookMachine.Persistence.Repositories
{
    public class UserRepository(BookMachineDbContext context) : IUserRepository
    {
        private readonly BookMachineDbContext _context = context;

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            UserEntity? userEntity = await _context.UserEntities
                .AsNoTracking().
                FirstOrDefaultAsync(u => u.Email == email);

            User? user = UserMapping.FromUserEntityToUser(userEntity!);

            return user;
        }

        public async Task<Guid> CreateUserAsync(User user)
        {
            UserEntity userEntity = UserMapping.FromUserToUserEntity(user);

            await _context.UserEntities.AddAsync(userEntity);
            await _context.SaveChangesAsync();

            return userEntity.UserId;
        }
    }
}
