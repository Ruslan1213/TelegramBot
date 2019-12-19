using Introspekt.DAL.Models;

namespace Introspect.DAL.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        User GetUserByChatId(long chatId);

        Order GetOrderByChatId(long chatId);

        Order GetByUserId(int userId);
    }
}
