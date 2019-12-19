using System.Collections.Generic;
using Introspekt.DAL.Models;

namespace Introspekt_bot.Abstraction
{
    public interface IOrderDetailsService
    {
        void Create(OrderDetails order);

        void Create(long chatId, int courseId, string name);

        void Update(OrderDetails order);

        void Delete(OrderDetails order);

        IEnumerable<OrderDetails> GetAll();

        OrderDetails Get(int id);
    }
}
