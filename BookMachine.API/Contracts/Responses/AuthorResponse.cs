using BookMachine.Core.Models;

namespace BookMachine.API.Contracts.Responses
{
    public record AuthorResponse(Guid AuthorId, string AuthorName, List<Book> Books);
}
