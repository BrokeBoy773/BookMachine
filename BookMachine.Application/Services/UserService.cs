using BookMachine.Core.Interfaces.Application.Services;
using BookMachine.Core.Interfaces.Infrastructure.Utilities;
using BookMachine.Core.Interfaces.Infrastructure.Utilities.JwtUtility;
using BookMachine.Core.Interfaces.Persistence.Repositories;
using BookMachine.Core.Models;

namespace BookMachine.Application.Services
{
    public class UserService(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtProvider jwtProvider) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;
        private readonly IJwtProvider _jwtProvider = jwtProvider;

        public async Task UserRegistrationAsync(string userName, string email, string password)
        {
            string hashedPassword = _passwordHasher.Generate(password);

            User user = User.Create(Guid.NewGuid(), userName, email, hashedPassword).User;

            await _userRepository.CreateUserAsync(user);
        }

        public async Task<string> UserLoginAsync(string email, string password)
        {
            User? user = await _userRepository.GetUserByEmailAsync(email);

            bool result = _passwordHasher.Verify(password, user!.PasswordHash);

            if (result == false)
            {
                return "error"; // Временное решение //
            }

            string token = _jwtProvider.GenerateToken(user);

            return token;
        }
    }
}
