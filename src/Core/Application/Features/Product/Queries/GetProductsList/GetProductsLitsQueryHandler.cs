using Application.Models;
using Domain.Aggregates.Products.Entities;
using MediatR;

namespace Application.Features.Product.Queries.GetProductsList;

public class GetProductsLitsQueryHandler : IRequestHandler<GetProductsLitsQuery, PaginatedProductDto>
{
    private readonly IProductRepository _productRepository;

    public GetProductsLitsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<PaginatedProductDto> Handle(GetProductsLitsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllAsync(request.PageSize, request.PageNumber);

        var productsDto = products.entities.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            Sku = p.Sku
        }).ToList();

        return new PaginatedProductDto
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = products.totalCount,
            Products = productsDto
        };
    }
}
