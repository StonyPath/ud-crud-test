using Application.Models;
using MediatR;

namespace Application.Features.Product.Queries.GetProductsList;

public record GetProductsLitsQuery(int PageNumber, int PageSize) : IRequest<PaginatedProductDto> { }
