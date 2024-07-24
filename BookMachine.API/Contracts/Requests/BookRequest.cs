namespace BookMachine.API.Contracts.Requests
{
    public record BookRequest(string BookTitle, Guid AuthorId);
}
