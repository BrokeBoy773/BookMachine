namespace BookMachine.Core.Interfaces.Application.Services
{
    public interface IUserService
    {
        public Task UserRegistrationAsync(string userName, string email, string password);

        public Task<string> UserLoginAsync(string email, string password);
    }
}
