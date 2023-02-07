using NLayer.Repository.Context;

namespace NLayer.Repository.Repositories.User
{
    public class UserRepository :BaseRepository<Entity.User>, IUserRepository
    {
        public UserRepository(BaseContext context) : base(context) { }
    }
}
