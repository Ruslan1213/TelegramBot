using Introspekt_bot.Abstraction;
using Introspekt_bot.Dependency;
using Introspekt_bot.Resourse;
using Introspekt_bot.Service;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using User = Introspekt.DAL.Models.User;

namespace Introspekt_bot.Commands
{
    public class HelloCommand : Command
    {
        private readonly TelegramBotClient telegramBot;

        private readonly Message message;

        public HelloCommand(TelegramBotClient bot, Message msg)
        {
            telegramBot = bot;
            message = msg;
        }

        public override string Name
        {
            get => ComandsName.Hello;
        }

        public override void Execute()
        {
            SmileGetter smileGetterGetter = new SmileGetter();
            KeyboardButton buttonCourses = SetButtonTextAndCallBackData(ButtonText.Catalog + smileGetterGetter.GetSmile(SmileList.Catalog), ComandsName.GetCourses);
            KeyboardButton buttonInfo = SetButtonTextAndCallBackData(ButtonText.Info + smileGetterGetter.GetSmile(SmileList.Info), ComandsName.GetInfo);
            KeyboardButton buttonCoursePreper = SetButtonTextAndCallBackData(ButtonText.Basket + smileGetterGetter.GetSmile(SmileList.Heart), ComandsName.Get);
            InlineKeyboardButton buttonGetConacts = new InlineKeyboardButton { Text = ButtonText.Calling + smileGetterGetter.GetSmile(SmileList.Calling), CallbackData = ComandsName.GetAdmin };
            buttonGetConacts.Url = "t.me/psyitseasy";
            var keyboard = new ReplyKeyboardMarkup(new[] { new[] { buttonCourses }, new[] { buttonCoursePreper }, new[] { buttonInfo } });

            var userService = new InjectDependency().GetUserService();

            if (!userService.IsExist(x => x.Name == message.From.Username))
            {
                userService.Create(new User { Name = message.From.Username });
            }

            telegramBot.SendTextMessageAsync(message.Chat.Id, MessageText.Greeting, replyMarkup: keyboard);
            telegramBot.SendTextMessageAsync(message.Chat.Id, "Для связи с администрацией перейдите по ссылке", replyMarkup: new InlineKeyboardMarkup(new[] { new[] { buttonGetConacts } }));
        }

        private KeyboardButton SetButtonTextAndCallBackData(string text, string callbackData)
        {
            return new KeyboardButton(text);
        }
    }
}
