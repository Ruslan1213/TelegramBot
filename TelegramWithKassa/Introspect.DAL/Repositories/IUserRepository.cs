using Introspekt.DAL.Models;

namespace Introspect.DAL.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByName(string name);
    }
}
