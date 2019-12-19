using System;
using System.Linq;
using Introspekt_bot.Abstraction;
using Introspekt_bot.Dependency;
using Introspekt_bot.MessagePagination;
using Introspekt_bot.Resourse;
using Introspekt_bot.Service;
using Introspekt_bot.ViewModels;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Introspekt_bot.Commands
{
    public class GetInfoCommand : Command
    {
        private readonly TelegramBotClient telegramBot;

        private readonly Message message;

        private readonly CallbackQuery query;

        private int pageSize = 1;

        public GetInfoCommand(TelegramBotClient bot, Message msg, CallbackQuery q)
        {
            message = msg;
            query = q;
            telegramBot = bot;
        }

        public override string Name { get => ButtonText.Info; }

        public override void Execute()
        {
            string messageText = string.Empty;
            string command = GetCommandData();
            string pageNumber = command.Substring(command.IndexOf(":", StringComparison.Ordinal) + 1);
            int page = int.TryParse(pageNumber, out page) ? int.Parse(pageNumber) : 1;
            var coursesInfo = InjectDependency.GetInstance().GetCourseService().GetAll()
                .Select(x => new CourseInfo { Name = x.Name, Description = x.Description });
            var messageInfo = new MessageInfo(page, pageSize, coursesInfo.Count());

            var model = new CoursesListViewModel()
            {
                CoursesInfo = coursesInfo
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),
                PagingInfo = messageInfo
            };

            model.CoursesInfo.ToList().ForEach(x => messageText += GetterCourseInfo.GetCourseInfo(x));
            SendMessage(command, messageText, page, messageInfo.TotalPages);
        }

        private InlineKeyboardButton SetButtonTextAndCallBackData(string text, string callbackData)
        {
            return new InlineKeyboardButton { Text = text, CallbackData = callbackData };
        }

        private InlineKeyboardMarkup MakeButtons(int page, int totalPages)
        {
            SmileGetter smileGetter = new SmileGetter();
            InlineKeyboardButton previosPage;
            InlineKeyboardButton nextPage;
            InlineKeyboardMarkup keyboard;

            if (page == 1)
            {
                nextPage = SetButtonTextAndCallBackData(ButtonText.Next + smileGetter.GetSmile(SmileList.Next), Name + (page + 1));
                keyboard = new InlineKeyboardMarkup(new[] { new[] { nextPage } });
            }
            else if (page == totalPages)
            {
                previosPage = SetButtonTextAndCallBackData(ButtonText.Previos + smileGetter.GetSmile(SmileList.Back), Name + (page - 1));
                keyboard = new InlineKeyboardMarkup(new[] { new[] { previosPage } });
            }
            else
            {
                previosPage = SetButtonTextAndCallBackData(ButtonText.Previos + smileGetter.GetSmile(SmileList.Back), Name + (page - 1));
                nextPage = SetButtonTextAndCallBackData(ButtonText.Next + smileGetter.GetSmile(SmileList.Next), Name + (page + 1));
                keyboard = new InlineKeyboardMarkup(new[] { new[] { previosPage, nextPage } });
            }

            return keyboard;
        }

        private void SendMessage(string command, string messageText, int page, int totalPages)
        {
            if (command == Name)
            {
                telegramBot.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: messageText,
                    replyMarkup: MakeButtons(page, totalPages));
            }
            else
            {
                telegramBot.EditMessageTextAsync(
                    message.Chat.Id,
                    message.MessageId,
                    messageText,
                    replyMarkup: MakeButtons(page, totalPages));
            }
        }

        private string GetCommandData()
        {
            if (query == null)
            {
                return ButtonText.Info;
            }

            return query.Data;
        }
    }
}
