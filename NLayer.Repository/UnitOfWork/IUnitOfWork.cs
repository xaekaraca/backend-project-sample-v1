using NLayer.Repository.Repositories.User;

namespace NLayer.Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        Task CommitAsync(CancellationToken cancellationToken = default);
        void Commit();
    }

}
