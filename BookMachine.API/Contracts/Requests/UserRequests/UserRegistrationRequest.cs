namespace BookMachine.API.Contracts.Requests.UserRequests
{
    public record UserRegistrationRequest(string UserName, string Email, string Password);
}
