using System.Linq.Expressions;

namespace Domain.SeedWork;

public interface IBaseRepository<TEntity>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="take">PageSize</param>
    /// <param name="skip">PageNumber </param>
    /// <returns></returns>
    Task<(IReadOnlyList<TEntity> entities, int totalCount)> GetAllAsync(int take, int skip, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null,
                                          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                          string includeString = null,
                                          bool disableTracking = true,
                                          CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null,
                                          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                          List<Expression<Func<TEntity, object>>> includes = null,
                                          bool disableTracking = true,
                                          CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default);
    Task<bool> AddAsync(TEntity entity);
    /// <summary>
    /// before use this method, check if item already exist or not
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<bool> UpdateAsync(TEntity entity);
    /// <summary>
    /// this method has builtIn checking if item already exist
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> DeleteByIdAsync<TId>(TId id);
    /// <summary>
    /// before use this method, check if item already exist or not
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(TEntity entity);
    Task SaveChangesAsync();
}
