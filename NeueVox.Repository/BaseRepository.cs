using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NeueVox.Model.NeuevoxModel.Context;

namespace NeueVox.Repository;

public interface IBaseRepository<T> where T : class
{
  Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
    Task<T?> GetByIdAsync(Guid id,params Expression<Func<T, object>>[] includes);
    Task<T> CreateAsync(T entity);
    Task<T?> UpdateAsync(T entity,Guid id);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
}

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly NeueVoxContext DbContext;
    protected readonly DbSet<T> DbSet;
    public BaseRepository(NeueVoxContext dbContext)
    {
        DbContext = dbContext;
        DbSet = DbContext.Set<T>();
    }
    public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
    {
      IQueryable<T> query = DbSet.AsNoTracking();
        foreach (var include in includes)
        {
          query = query.Include(include);
        }

        return await query.ToListAsync();
    }


    public async Task<T?> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes)
    {
      IQueryable<T> query = DbSet.AsNoTracking();
      foreach (var include in includes)
      {
        query = query.Include(include);
      }
      var keyName = DbContext.Model.FindEntityType(typeof(T))
        ?.FindPrimaryKey()
        ?.Properties
        .Select(x => x.Name)
        .Single();

      if (keyName == null) return null;

      return await query.FirstOrDefaultAsync(e => EF.Property<Guid>(e, keyName) == id);
    }

    public async Task<T> CreateAsync(T entity)
    {
        await DbSet.AddAsync(entity);

        await DbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<T?> UpdateAsync(T entity, Guid id)
    {
        var existingEntity = await DbSet.FindAsync(id);
        if (existingEntity == null) return null;

        DbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
        await DbContext.SaveChangesAsync();
        return existingEntity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await DbSet.FindAsync(id);
        if (entity == null) return false;
        DbSet.Remove(entity);
        await DbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
    {
      return await DbSet.AnyAsync(predicate);
    }
}
