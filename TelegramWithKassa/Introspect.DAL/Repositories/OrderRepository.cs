using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Introspekt.DAL;
using Introspekt.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Introspect.DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly CourseStoreContext _context;

        public OrderRepository(CourseStoreContext context)
        {
            _context = context;
        }

        public void Create(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void Update(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public int SelectMaxId()
        {
            return _context.Orders.Select(x => x.Id).Max();
        }

        public void Delete(Order order)
        {
            _context.Orders.Remove(order);
        }

        public IEnumerable<Order> GetAll()
        {
            return _context.Orders.ToList();
        }

        public Order Get(int id)
        {
            return _context.Orders.Include(x => x.OrderDetails).ThenInclude(x => x.Course).FirstOrDefault(x => x.Id == id);
        }

        public bool IsExist(Expression<Func<Order, bool>> expression)
        {
            return _context.Orders.Any(expression);
        }

        public User GetUserByChatId(long chatId)
        {
            return _context.Orders.Include(x => x.OrderDetails).ThenInclude(x => x.Course).FirstOrDefault(x => x.ChatId == chatId)?.User;
        }

        public Order GetOrderByChatId(long chatId)
        {
            return _context.Orders.Include(x => x.OrderDetails).ThenInclude(x => x.Course).FirstOrDefault(x => x.ChatId == chatId);
        }

        public Order GetByUserId(int id)
        {
            return _context.Orders.Include(x => x.OrderDetails).ThenInclude(x => x.Course).FirstOrDefault(x => x.UserId == id);
        }
    }
}
