namespace Application.Models;

public class PaginatedCustomersDto : PaginatedBase
{
    public List<CustomerDto> Customers { get; set; }
}
