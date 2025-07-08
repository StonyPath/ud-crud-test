namespace Application.Models;

public class PaginatedOrderDto : PaginatedBase
{
    public List<OrderDto> Orders { get; set; }
}
