using BookMachine.Core.Models;

namespace BookMachine.API.Contracts.Responses.AuthorResponses
{
    public record GetAuthorByIdResponse(Guid AuthorId, string AuthorName, List<Book> Books);
}
