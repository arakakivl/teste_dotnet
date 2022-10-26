using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TesteDotnet.Domain.Interfaces;

namespace TesteDotnet.Infrastructure.Persistence.Repositories;

public class BaseRepository<TKey, TEntity> : IBaseRepository<TKey, TEntity> where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet;
    public DbSet<TEntity> DbSet => _dbSet;

    public BaseRepository(AppDbContext context)
    {
        _dbSet = context.Set<TEntity>();
    }

    public Task<TKey> AddAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity?> GetEntityAsync(TKey key)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<TEntity>> GetEntitiesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<TEntity>> GetEntitiesAsync(Expression<Func<TEntity, bool>>? filter = null, string includeProperties = "")
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(TKey key)
    {
        throw new NotImplementedException();
    }
}