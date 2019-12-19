using System;
using System.Collections.Generic;
using System.Globalization;
using Introspect.DAL.Repositories;
using Introspekt.DAL.Models;
using Introspekt_bot.Abstraction;

namespace Introspekt_bot.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        
        private readonly IUserRepository userRepository;

        public OrderService(
            IOrderRepository orderRepository,
            IUserRepository userRepository)
        {
            this.orderRepository = orderRepository;
            this.userRepository = userRepository;
        }

        public User GetUserByChatId(long chatId)
        {
            return orderRepository.GetUserByChatId(chatId);
        }

        public void Create(Order order)
        {
            orderRepository.Create(order);
        }

        public void Create(long chatId, int courseId, string name)
        {
            var user = userRepository.GetByName(name);

            if (user.Order == null)
            {
                user.Order = new Order
                {
                    ChatId = chatId,
                    Amount = 0,
                    Status = "new",
                    Date = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                    UserId = user.Id,
                    Email = string.Empty,
                };
                orderRepository.Create(user.Order);
                var order = orderRepository.GetOrderByChatId(chatId);
                order.OrderDetails = new List<OrderDetails> { new OrderDetails { CourseId = courseId, OrderId = order.Id } };
                orderRepository.Update(order);

                return;
            }

            if (user.Order.OrderDetails == null)
            {
                user.Order.OrderDetails =
                    new List<OrderDetails> { new OrderDetails { CourseId = courseId, OrderId = user.Order.Id } };
                orderRepository.Update(user.Order);

                return;
            }

            var orderDetails = new OrderDetails { CourseId = courseId, OrderId = user.Order.Id };

            if (IsExistOrderDetailsInList(orderDetails, user.Order.OrderDetails))
            {
                throw new ArgumentException("Вы уже добавили в корзину данный курс ");
            }

            user.Order.OrderDetails.Add(orderDetails);
            orderRepository.Update(user.Order);
        }

        public void Update(Order order)
        {
            orderRepository.Update(order);
        }

        public void Delete(Order order)
        {
            orderRepository.Delete(order);
        }

        public IEnumerable<Order> GetAll()
        {
            return orderRepository.GetAll();
        }

        public Order Get(int id)
        {
            return orderRepository.Get(id);
        }

        public Order GetByChatId(long chatId)
        {
            return orderRepository.GetOrderByChatId(chatId);
        }

        private bool IsExistOrderDetailsInList(OrderDetails orderDetails, IEnumerable<OrderDetails> listOrderDetails)
        {
            foreach (var details in listOrderDetails)
            {
                if (details.CourseId == orderDetails.CourseId 
                    && details.OrderId == orderDetails.OrderId)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
