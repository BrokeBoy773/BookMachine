namespace BookMachine.API.Contracts.Requests.BookRequests
{
    public record GetBookByFilterRequest(string? Search, string? SortItem, string? SortOrder);
}
