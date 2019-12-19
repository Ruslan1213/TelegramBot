using System.Collections.Generic;
using Introspekt_bot.Abstraction;
using Introspekt_bot.Dependency;
using Introspekt_bot.Resourse;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Introspekt_bot.Commands
{
    public class GetCoursesCommand : Command
    {
        private readonly TelegramBotClient telegramBot;

        private readonly Message message;

        public GetCoursesCommand(TelegramBotClient bot, Message msg)
        {
            telegramBot = bot;
            message = msg;
        }

        public override string Name => ButtonText.Catalog;

        public override void Execute()
        {
            var corses = InjectDependency.GetInstance().GetCourseService().GetAll();
            List<List<InlineKeyboardButton>> list = new List<List<InlineKeyboardButton>>();

            foreach (var course in corses)
            {
                list.Add(new List<InlineKeyboardButton> 
                             {
                                 SetButtonTextAndCallBackData(course.Name, ComandsName.GetOneCourse + course.Id)
                             });
            }

            var keyboard = new InlineKeyboardMarkup(list);
            telegramBot.SendTextMessageAsync(message.Chat.Id, MessageText.ViewCourses, replyMarkup: keyboard);
        }

        private InlineKeyboardButton SetButtonTextAndCallBackData(string text, string callbackData)
        {
            return new InlineKeyboardButton { Text = text, CallbackData = callbackData };
        }
    }
}
