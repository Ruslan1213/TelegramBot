using System;
using System.Linq.Expressions;
using Introspect.DAL.Repositories;
using Introspekt.DAL.Models;
using Introspekt_bot.Abstraction;

namespace Introspekt_bot.Service
{
    public class UserService: IUserService
    {
        private readonly IRepository<User> repository;

        public UserService(IRepository<User> repository)
        {
            this.repository = repository;
        }

        public void Create(User user)
        {
            repository.Create(user);
        }

        public bool IsExist(Expression<Func<User, bool>> expression)
        {
            return repository.IsExist(expression);
        }
    }
}
