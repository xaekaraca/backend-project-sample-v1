using Microsoft.EntityFrameworkCore;
using NLayer.Core.Repositories;
using NLayer.Repository.Context;
using NLayer.Core.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace NLayer.Repository.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IBaseEntity, new()
    {
        #region Definition
        protected readonly BaseContext _context;
        private readonly DbSet<TEntity> _dbSet;
        #endregion

        #region Constructor
        public BaseRepository(BaseContext context)
        {
            _context = context;
            _dbSet= _context.Set<TEntity>();
        }
        #endregion

        #region Create
        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            var newEntity= await _dbSet.AddAsync(entity, cancellationToken);
            newEntity.Entity.CreatedAt= DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return newEntity.Entity;
        }
        #endregion

        #region Read
        public async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var getByIdAsync = await _dbSet.Where(e => e.Id == id).SingleOrDefaultAsync(cancellationToken);
            return getByIdAsync;
        }
        #endregion
        public IQueryable<TEntity> GetAll (bool includeDeleted = false)
        {
            IQueryable<TEntity> queryableEntities;
            if(!includeDeleted)
            {
                queryableEntities = _dbSet.Where(e=>!e.IsDeleted).AsNoTracking().AsQueryable();
                return queryableEntities;
            }
            queryableEntities = _dbSet.AsNoTracking().AsQueryable();
            return queryableEntities;
        }
        #region Update
        
        #endregion
        
        #region Delete
        
        #endregion
    }
}
