using System;
using System.Collections.Generic;
using System.IO;
using FreeKassa.FreeKassaUrls;
using Introspekt.DAL.Models;
using Introspekt_bot.Abstraction;
using Introspekt_bot.Dependency;
using Introspekt_bot.Resourse;
using Introspekt_bot.Service;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using Order = Introspekt_bot.Models.Order;

namespace Introspekt_bot.Commands
{
    public class GetOneCourse : Command
    {
        private readonly TelegramBotClient telegramBot;

        private readonly Message message;

        private readonly CallbackQuery querys;

        public GetOneCourse(TelegramBotClient bot, Message msg, CallbackQuery query)
        {
            telegramBot = bot;
            message = msg;
            querys = query;
        }

        public override string Name
        {
            get => ComandsName.GetOneCourse;
        }

        public override void Execute()
        {
            try
            {
                string command = querys.Data;

                var str = command.Substring(command.IndexOf(":", StringComparison.Ordinal) + 1);

                if (int.TryParse(str, out var id))
                {
                    var course = InjectDependency.GetInstance().GetCourseService().Get(id);
                    string messageText = GetterCourseInfo.GetCourseInfo(course);
                    var keyboard = new InlineKeyboardMarkup(new List<List<InlineKeyboardButton>> { new List<InlineKeyboardButton> { new InlineKeyboardButton { Text = ButtonText.AddToBasket, CallbackData = ComandsName.AddToBasket + course.Id } } });
                    var filePath = new DirectoryInformer().GetCurrentDirrectory() + "Content\\" + course.Photo;
                    var stream = System.IO.File.Open(filePath, FileMode.Open);
                    var photo = new InputOnlineFile(stream, course.Name);
                    var check = telegramBot.SendPhotoAsync(chatId: message.Chat.Id, photo: photo, caption: messageText, replyMarkup: keyboard).Result;
                    stream.Dispose();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}