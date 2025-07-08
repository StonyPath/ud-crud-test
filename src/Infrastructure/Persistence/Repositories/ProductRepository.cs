using Domain.Aggregates.Products.Entities;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories;
public class ProductRepository(AppDbContext appDbContext) : BaseRepository<Product>(appDbContext),
                                                            IProductRepository
{
}
