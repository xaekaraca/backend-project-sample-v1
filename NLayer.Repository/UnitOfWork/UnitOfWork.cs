using NLayer.Repository.Context;
using NLayer.Repository.Repositories.User;

namespace NLayer.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BaseContext _context;

        private IUserRepository _userRepository;
        
        
        public UnitOfWork(BaseContext context)
        {
            _context = new BaseContext();
        }

        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);

        public async Task CommitAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        public void Commit() 
        {
            _context.SaveChanges();
        }
    }
}
