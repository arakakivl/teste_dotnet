using System.Linq.Expressions;

namespace TesteDotnet.Domain.Interfaces;

public interface IBaseRepository<TKey, TEntity>
{
    Task<TKey> AddAsync(TEntity entity);

    Task<ICollection<TEntity>> GetEntitiesAsync();
    Task<TEntity?> GetEntityAsync(TKey key);

    Task<ICollection<TEntity>> GetEntitiesAsync(Expression<Func<TEntity, bool>>? filter = null, string includeProperties = "");

    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TKey key);
}