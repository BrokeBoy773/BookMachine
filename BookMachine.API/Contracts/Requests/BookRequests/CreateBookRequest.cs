namespace BookMachine.API.Contracts.Requests.BookRequests
{
    public record CreateBookRequest(string BookTitle, Guid AuthorId);
}
