using Application.Models;
using MediatR;

namespace Application.Features.Customer.Queries.GetCustomersList;

public record GetCustomersListQuery(int PageNumber, int PageSize) : IRequest<PaginatedCustomersDto>
{
}
