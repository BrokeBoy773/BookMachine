namespace BookMachine.API.Contracts.Responses.BookResponses
{
    public record GetBookByFilterResponse(Guid BookId, string BookTitle, Guid AuthorId, string AuthorName);
}
