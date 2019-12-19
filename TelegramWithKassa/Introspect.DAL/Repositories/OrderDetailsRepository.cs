using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Introspekt.DAL;
using Introspekt.DAL.Models;

namespace Introspect.DAL.Repositories
{
    public class OrderDetailsRepository : IRepository<OrderDetails>
    {
        private readonly CourseStoreContext _context;

        public OrderDetailsRepository(CourseStoreContext context)
        {
            _context = context;
        }

        public void Create(OrderDetails entity)
        {
            _context.OrderDetails.Add(entity);
        }

        public void Update(OrderDetails entity)
        {
            _context.OrderDetails.Update(entity);
        }


        public void Delete(OrderDetails entity)
        {
            _context.OrderDetails.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<OrderDetails> GetAll()
        {
            return _context.OrderDetails.ToList();
        }

        public OrderDetails Get(int id)
        {
            return _context.OrderDetails.FirstOrDefault(x => x.Id == id);
        }

        public bool IsExist(Expression<Func<OrderDetails, bool>> expression)
        {
            return _context.OrderDetails.Any(expression);
        }
    }
}
