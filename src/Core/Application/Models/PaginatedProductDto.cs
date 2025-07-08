namespace Application.Models;

public class PaginatedProductDto : PaginatedBase
{
    public List<ProductDto> Products { get; set; }
}
