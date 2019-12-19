using System;
using Introspekt.DAL.Models;
using Introspekt_bot.Abstraction;
using Introspekt_bot.Dependency;
using Introspekt_bot.Resourse;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Introspekt_bot.Commands
{
    public class BuyCourseCommand : Command
    {
        private readonly TelegramBotClient telegramBot;

        private readonly Message message;

        private readonly CallbackQuery querys;

        public BuyCourseCommand(TelegramBotClient bot, Message msg, CallbackQuery query)
        {
            telegramBot = bot;
            message = msg;
            querys = query;
        }

        public override string Name
        {
            get => ComandsName.AddToBasket;
        }

        public override void Execute()
        {
            string command = querys.Data;
            var str = command.Substring(command.IndexOf(":", StringComparison.Ordinal) + 1);
            var orderService = InjectDependency.GetInstance().GetOrderService();
            string messages = string.Empty;

            try
            {
                if (int.TryParse(str, out int courseId))
                {
                    orderService.Create(message.Chat.Id, courseId, querys.From.Username);
                }
            }
            catch (ArgumentException e)
            {
                messages = e.Message;
                telegramBot.SendTextMessageAsync(message.Chat.Id, messages);

                return;
            }

            messages = "Курс успешно добавлен в корзину";
            telegramBot.SendTextMessageAsync(message.Chat.Id, messages);
        }
    }
}
