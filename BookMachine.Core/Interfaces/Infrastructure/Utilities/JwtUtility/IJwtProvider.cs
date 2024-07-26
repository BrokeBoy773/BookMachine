using BookMachine.Core.Models;

namespace BookMachine.Core.Interfaces.Infrastructure.Utilities.JwtUtility
{
    public interface IJwtProvider
    {
        public string GenerateToken(User user);
    }
}
