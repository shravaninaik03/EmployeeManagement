using Emp.Interfaces;
using Emp.Data;
using Microsoft.EntityFrameworkCore;

namespace Emp.Repositories;

public class Repository<TEntity>  : IRepository<TEntity>
where TEntity:  class, IEntity
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<TEntity> _dbSet;
    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }
    public async Task <IEnumerable<TEntity>> GetAllAsyc()
    {
        return await _dbSet.ToListAsync();
    }
    public async Task <TEntity?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }
    public async Task AddAsync(TEntity entity)
    {
         await _dbSet.AddAsync(entity);
    }
     public async Task Update(TEntity entity)
      {
        _dbSet.Update(entity);
        await Task.CompletedTask;
    }
    public async Task Delete(TEntity entity)
    {
    _dbSet.Remove(entity);
    await Task.CompletedTask;
    }
    public async Task SaveAsync()
    {
    await _context.SaveChangesAsync();

    }
}