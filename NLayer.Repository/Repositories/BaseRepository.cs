using Microsoft.EntityFrameworkCore;
using NLayer.Core.Repositories;
using NLayer.Repository.Context;
using NLayer.Core.Entities;
using System.Linq.Expressions;

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

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> filter)
        {
            var query = _dbSet.Where(filter );
            return query;
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken)
        {
            var checkEntityExist = await _dbSet.AnyAsync(filter, cancellationToken);
            return checkEntityExist;
        }
        #endregion

        #region Update
        public void Update(TEntity entity)
        {
            entity.UpdatedAt = DateTime.Now;
            _dbSet.Update(entity);
        }
        #endregion
        
        #region Delete
        public void Delete(TEntity entity)
        {
            entity.UpdatedAt= DateTime.Now;
            entity.IsDeleted = true;
            _dbSet.Update(entity);
        }
        #endregion
    }
}
