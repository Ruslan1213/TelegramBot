using Introspekt_bot.Abstraction;
using Introspekt_bot.Dependency;
using Introspekt_bot.Resourse;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Introspekt_bot.Commands
{
    public class DetailsDeleteCommand : Command
    {
        private readonly TelegramBotClient telegramBot;

        private readonly Message message;

        private readonly CallbackQuery query;

        private readonly IOrderDetailsService orderDetailsService;

        public DetailsDeleteCommand(TelegramBotClient telegramBot, Message message, CallbackQuery query)
        {
            this.telegramBot = telegramBot;
            this.message = message;
            this.query = query;
            orderDetailsService = InjectDependency.GetInstance().GetOrderDetailsService();
        }

        public override string Name
        {
            get => ComandsName.DeleteDetails;
        }

        public override void Execute()
        {
            var text = query.Data;
            string id = text.Substring(text.IndexOf(":") + 1);

            if (int.TryParse(id, out int idToDelete))
            {
                var orderDetails = orderDetailsService.Get(idToDelete);

                if (orderDetails != null)
                {
                    orderDetailsService.Delete(orderDetails);
                    telegramBot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                }
            }
        }
    }
}
