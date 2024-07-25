using BookMachine.Core.Models;

namespace BookMachine.API.Contracts.Responses.BookResponses
{
    public record GetBookByIdResponse(Guid BookId, string BookTitle, Author Author);
}
