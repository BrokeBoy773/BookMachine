namespace BookMachine.API.Contracts.Responses.BookResponses
{
    public record GetAllBooksResponse(Guid BookId, string BookTitle, Guid AuthorId, string AuthorName);
}
