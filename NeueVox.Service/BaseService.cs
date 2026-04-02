using NeueVox.Repository;

namespace NeueVox.Service;

public interface IBaseService<T> where T : class
{
  Task<IEnumerable<T>> GetAllAsync();
  Task<T?> GetByIdAsync(Guid id);
  Task<T> CreateAsync(T entity);
  Task<T?> UpdateAsync(T entity, Guid id);
  Task<bool> DeleteAsync(Guid id);
}

public class BaseService<T> : IBaseService<T> where T : class
{
  protected readonly IBaseRepository<T> Repository;
  public BaseService(IBaseRepository<T> repository)
  {
    Repository =  repository;
  }
  public async Task<IEnumerable<T>> GetAllAsync()
  {
    return await  Repository.GetAllAsync();
  }

  public async Task<T?> GetByIdAsync(Guid id)
  {
    return await Repository.GetByIdAsync(id);
  }

  public async Task<T> CreateAsync(T entity)
  {
    return await Repository.CreateAsync(entity);
  }

  public async Task<T?> UpdateAsync(T entity, Guid id)
  {
    return await Repository.UpdateAsync(entity, id);
  }

  public async Task<bool> DeleteAsync(Guid id)
  {
    return await Repository.DeleteAsync(id);
  }
}

