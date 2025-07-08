using Domain.Aggregates.Products.ValueObjects;
using MediatR;

namespace Application.Features.Product.Commands.CreateProduct;
public record CreateProductCommand(string Name, Money Price, SKU Sku) : IRequest<ProductId> { }
