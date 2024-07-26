namespace BookMachine.API.Contracts.Requests.UserRequests
{
    public record UserLoginRequest(string Email, string Password);
}
