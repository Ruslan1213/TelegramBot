using System.Collections.Generic;
using Introspekt.DAL.Models;

namespace Introspekt_bot.Abstraction
{
    public interface IOrderService
    {
        User GetUserByChatId(long chatId);

        void Create(Order order);

        void Create(long chatId, int courseId, string name);

        void Update(Order order);

        void Delete(Order order);

        IEnumerable<Order> GetAll();

        Order Get(int id);

        Order GetByChatId(long chatId);
    }
}
