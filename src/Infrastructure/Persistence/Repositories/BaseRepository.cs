using Domain.SeedWork;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Repositories;
public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly AppDbContext _dbContext;
    protected DbSet<TEntity> _dbSet;

    public BaseRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }

    public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _dbSet.ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        => await _dbSet.Where(predicate).ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null,
                                                       Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                       string includeString = null,
                                                       bool disableTracking = true,
                                                       CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking) query = query.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

        if (predicate != null) query = query.Where(predicate);

        if (orderBy != null) return await orderBy(query).ToListAsync(cancellationToken);

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null,
                                                       Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                       List<Expression<Func<TEntity, object>>> includes = null,
                                                       bool disableTracking = true,
                                                       CancellationToken cancellation = default)
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking) query = query.AsNoTracking();

        if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

        if (predicate != null) query = query.Where(predicate);

        if (orderBy != null) return await orderBy(query).ToListAsync(cancellation);

        return await query.ToListAsync(cancellation);
    }

    public async Task<TEntity?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default)
    {
        var parameter = Expression.Parameter(typeof(TEntity), "e");
        var property = Expression.Property(parameter, "Id");
        var equals = Expression.Equal(property, Expression.Constant(id));
        var lambda = Expression.Lambda<Func<TEntity, bool>>(equals, parameter);

        return await _dbSet.FirstOrDefaultAsync(lambda, cancellationToken);
    }

    public async Task<bool> AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return await Task.FromResult(true);
    }

    public async Task<bool> UpdateAsync(TEntity entity)
    {
        _dbSet.Attach(entity);
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return await Task.FromResult(true);
    }

    public async Task<bool> DeleteAsync(TEntity entity)
    {
        if (_dbContext.Entry(entity).State == EntityState.Detached) _dbSet.Attach(entity);

        _dbSet.Remove(entity);
        await _dbContext.SaveChangesAsync();
        return await Task.FromResult(true);
    }

    public async Task<bool> DeleteByIdAsync<TId>(TId id)
    {
        var entity = await GetByIdAsync(id);

        if (entity == null) return false;

        await DeleteAsync(entity);
        return await Task.FromResult(true);
    }
}
