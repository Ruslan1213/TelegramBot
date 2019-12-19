using System;
using System.Collections.Generic;
using Introspect.DAL.Repositories;
using Introspekt.DAL.Models;
using Introspekt_bot.Abstraction;

namespace Introspekt_bot.Service
{
    public class OrderDetailsService : IOrderDetailsService
    {
        private readonly IRepository<OrderDetails> _repository;

        public OrderDetailsService(IRepository<OrderDetails> repository)
        {
            _repository = repository;
        }

        public void Create(OrderDetails order)
        {
            _repository.Create(order);
        }

        public void Create(long chatId, int courseId, string name)
        {
            throw new NotImplementedException();
        }

        public void Update(OrderDetails order)
        {
            _repository.Update(order);
        }

        public void Delete(OrderDetails order)
        {
            _repository.Delete(order);
        }

        public IEnumerable<OrderDetails> GetAll()
        {
            return _repository.GetAll();
        }

        public OrderDetails Get(int id)
        {
            return _repository.Get(id);
        }
    }
}