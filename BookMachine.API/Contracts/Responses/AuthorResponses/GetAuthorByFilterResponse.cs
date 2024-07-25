using BookMachine.Core.Models;

namespace BookMachine.API.Contracts.Responses.AuthorResponses
{
    public record GetAuthorByFilterResponse(Guid AuthorId, string AuthorName, List<Book> Books);
}
