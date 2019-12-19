using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Introspect.DAL.Repositories;
using Introspekt.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Introspekt.DAL.Repositories
{
    public class RepositoryUser : IUserRepository
    {
        private readonly CourseStoreContext _context;

        public RepositoryUser(CourseStoreContext context)
        {
            _context = context;
        }

        public void Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public bool IsExist(Expression<Func<User, bool>> expression)
        {
            return _context.Users.Any(expression);
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.Include(x => x.Order).ThenInclude(x => x.OrderDetails).ThenInclude(x => x.Course).ToList();
        }

        public User GetByName(string name)
        {
            return _context.Users.Include(x => x.Order).ThenInclude(x => x.OrderDetails).ThenInclude(x => x.Course).FirstOrDefault(x => x.Name == name);
        }

        public User Get(int id)
        {
            return _context.Users.Include(x => x.Order).ThenInclude(x => x.OrderDetails).ThenInclude(x => x.Course).FirstOrDefault(x => x.Id == id);
        }
    }
}
