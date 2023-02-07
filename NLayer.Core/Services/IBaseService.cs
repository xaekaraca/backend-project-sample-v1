using NLayer.Core.DTOs;
using NLayer.Core.Entities;

namespace NLayer.Core.Services
{
    public interface IBaseService<TEntityView, TEntityService>
        where TEntityService : class,IBaseEntity, new()
        where TEntityView : class,IBaseModelDto,new()
    {
        #region Create
        Task<TEntityService> AddAsync(TEntityView entity, CancellationToken cancellationToken);
        #endregion

        #region Read
        Task<TEntityService> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<List<TEntityService>> GetAll(CancellationToken cancellationToken);
        #endregion
        #region Update

        Task<bool> Update(TEntityView entity, CancellationToken cancellationToken);
        #endregion

        #region Delete
        Task<bool> Delete(int id,CancellationToken cancellationToken);
        #endregion
    }
}
