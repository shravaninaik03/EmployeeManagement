using Emp.DTOs;

namespace Emp.Interfaces;

public interface IRepository<TEntity> where TEntity : class , IEntity
{
    Task <IEnumerable<TEntity>> GetAllAsyc();

    
    Task <TEntity?> GetByIdAsync(int id);

    Task AddAsync(TEntity entity);

    Task Update (TEntity  entity);

    Task Delete (TEntity entity);

    Task SaveAsync();
}