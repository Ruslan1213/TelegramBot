using System;
using System.Linq.Expressions;
using Introspekt.DAL.Models;

namespace Introspekt_bot.Abstraction
{
    public interface IUserService
    {
        void Create(User user);

        bool IsExist(Expression<Func<User, bool>> expression);
    }
}
