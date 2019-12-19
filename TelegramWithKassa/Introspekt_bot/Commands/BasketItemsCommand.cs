using System.Collections.Generic;
using System.Linq;
using FreeKassa.FreeKassaUrls;
using Introspekt.DAL.Models;
using Introspekt_bot.Abstraction;
using Introspekt_bot.Dependency;
using Introspekt_bot.Resourse;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Introspekt_bot.Commands
{
    public class BasketItemsCommand : Command
    {

        private readonly TelegramBotClient telegramBot;

        private readonly Message message;

        private readonly IOrderService orderService;

        public BasketItemsCommand(TelegramBotClient telegramBot, Message message)
        {
            this.telegramBot = telegramBot;
            this.message = message;
            orderService = InjectDependency.GetInstance().GetOrderService();
        }

        public override string Name
        {
            get => ButtonText.Basket;
        }

        public override void Execute()
        {
            var order = orderService.GetByChatId(message.Chat.Id);

            if (order == null)
            {
                telegramBot.SendTextMessageAsync(message.Chat.Id, "Корзина пустая");
                return;
            }

            if (!order.OrderDetails.Any())
            {
                telegramBot.SendTextMessageAsync(message.Chat.Id, "Корзина пустая");
                return;
            }

            var orderDetailsItems = order.OrderDetails;
            int summ = orderDetailsItems.Sum(x => x.Course.Price);
            telegramBot.SendTextMessageAsync(message.Chat.Id, "Корзина:");
            OrderDetailsInfo(orderDetailsItems, summ, order.Id);
        }

        private void OrderDetailsInfo(IEnumerable<OrderDetails> orderDetails, int summ, int orderId)
        {
            string messageText = string.Empty;

            foreach (var details in orderDetails)
            {
                messageText += details.Course.Name + " " + details.Course.Price + "\n";

                telegramBot.SendTextMessageAsync(message.Chat.Id, messageText, replyMarkup: new InlineKeyboardMarkup(
                    new[] { new[] { new InlineKeyboardButton { Text = "Удалить", CallbackData = "/deleteDetails:" + details.Id } } }));
            }

            telegramBot.SendTextMessageAsync(message.Chat.Id, "Оплатить заказ", replyMarkup: new InlineKeyboardMarkup(
                new[]
                    {
                        new[]
                            {
                                new InlineKeyboardButton
                                    {
                                        Text = "Оплатить", Url = new UrlToPayOrder(summ, orderId).GetUrlOredrPay()
                                    }
                            }
                    }));
        }
    }
}
