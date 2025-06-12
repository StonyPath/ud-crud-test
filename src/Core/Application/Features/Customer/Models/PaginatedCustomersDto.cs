namespace Application.Features.Customer.Models;

public class PaginatedCustomersDto
{
    public List<CustomerDto> Customers { get; set; }
    public int TotalCount { get; internal set; }
    public int PageNumber { get; internal set; }
    public int PageSize { get; internal set; }
}
