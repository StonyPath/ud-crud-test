namespace Application.Models;

public class PaginatedBase
{
    public int PageNumber { get; internal set; }
    public int PageSize { get; internal set; }
    public int TotalCount { get; internal set; }
}