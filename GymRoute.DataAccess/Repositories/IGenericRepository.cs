using GymRoute.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace GymRoute.DataAccess.Repositories;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity 
{
    Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdAsync(int id,CancellationToken cancellationToken=default);

    Task<TEntity?> GetByIdIncludingDeletedAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TEntity>> FindAsync(Expression< Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    Task<bool> ExistAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default);

    Task<bool> ExistAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task SoftDeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
