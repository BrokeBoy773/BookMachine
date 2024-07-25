namespace BookMachine.API.Contracts.Requests.AuthorRequests
{
    public record GetAuthorByFilterRequest(string? Search, string? SortItem, string? SortOrder);
}
