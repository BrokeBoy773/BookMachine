namespace BookMachine.API.Contracts.Requests.BookRequests
{
    public record UpdateBookRequest(string BookTitle, Guid AuthorId);
}
