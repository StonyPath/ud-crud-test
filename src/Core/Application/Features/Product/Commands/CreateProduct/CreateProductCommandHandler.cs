using Domain.Aggregates.Products.Entities;
using Domain.Aggregates.Products.ValueObjects;
using MediatR;

namespace Application.Features.Product.Commands.CreateProduct;
public record CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductId>
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductId> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = Domain.Aggregates.Products.Entities.Product.Create(request.Name,
                                                                         request.Price,
                                                                         request.Sku);
        bool productAdded = await _productRepository.AddAsync(product);

        if (productAdded) return product.Id;
        return null;
    }
}
