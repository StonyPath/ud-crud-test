using Application.Models;
using Domain.Aggregates.Products.ValueObjects;
using MediatR;

namespace Application.Features.Product.Queries.GetProductById;

public record GetProductByIdQuery(ProductId ProductId) : IRequest<ProductDto> { }
