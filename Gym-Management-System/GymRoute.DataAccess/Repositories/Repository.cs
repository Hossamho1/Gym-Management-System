using GymRoute.DataAccess.Data.Contexts;
using GymRoute.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace GymRoute.DataAccess.Repositories
{
    public class Repository<IEntity>(GymDbContext dbContext) : IGenericRepository<IEntity> where IEntity : BaseEntity
    {
        private readonly GymDbContext _dbContext = dbContext;
        private readonly DbSet<IEntity> _dbSet = dbContext.Set<IEntity>();

     

        public async Task<IReadOnlyList<IEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
         
        public async Task<IEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        =>   await _dbSet.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

        public async Task<IEntity?> GetByIdIncludingDeletedAsync(int id, CancellationToken cancellationToken = default)
        => await _dbSet.IgnoreQueryFilters().FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        public async Task<bool> ExistAsync(Expression<Func<IEntity, bool>> predicate, CancellationToken cancellationToken = default)
        => await _dbSet.AnyAsync(predicate, cancellationToken);
 

        public async Task<bool> ExistAsync(int id, CancellationToken cancellationToken = default)
        => await _dbSet.AnyAsync(t => t.Id == id, cancellationToken);

        public async Task<IReadOnlyList<IEntity>> FindAsync(Expression<Func<IEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbSet.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task AddAsync(IEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity , cancellationToken);
        }



        
        

        public Task SoftDeleteAsync(IEntity entity, CancellationToken cancellationToken = default)
        {
           entity.IsDeleted = true;
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(IEntity entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);

        }
    }
}
