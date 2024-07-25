using BookMachine.Core.Models;

namespace BookMachine.API.Contracts.Responses.AuthorResponses
{
    public record GetAllAuthorsResponse(Guid AuthorId, string AuthorName, List<Book> Books);
}
