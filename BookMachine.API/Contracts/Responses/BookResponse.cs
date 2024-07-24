using BookMachine.Core.Models;

namespace BookMachine.API.Contracts.Responses
{
    public record BookResponse(Guid BookId, string BookTitle, Author Author);
}
