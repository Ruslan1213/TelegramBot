using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Timers;
using FreeKassa.Requests;
using Introspekt.DAL.Models;
using Introspekt_bot.Abstraction;
using Introspekt_bot.Dependency;
using Introspekt_bot.Service;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.InputFiles;
using Order = Introspekt_bot.Models.Order;

namespace Introspekt_bot
{
    public class Program
    {
        private static TelegramBotClient telegramBot;

        private static string token = "992697649:AAFq3rw2yTSdf-0YZR9wVxQOz0X3JG9Ls9I";

        private static System.Timers.Timer timer;

        private static void Main()
        {
            TimerWorker();
            telegramBot = new TelegramBotClient(token);
            telegramBot.StartReceiving();
            telegramBot.OnMessage += BotOnMessageReceived;
            telegramBot.OnCallbackQuery += BotOnCallbackQueryReceived;

            Console.WriteLine(telegramBot.BotId);
            Console.ReadLine();
            telegramBot.StopReceiving();
        }

        private static void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            CommandsExecuter commandsExecuter = new CommandsExecuter(message, telegramBot);
            string commandText = message.Text.Substring(0, message.Text.Length - 2);
            commandsExecuter.Execute(commandText);
        }

        private static void BotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs callbackQueryEvent)
        {
            var callbackQuery = callbackQueryEvent.CallbackQuery;
            var message = callbackQuery.Message;
            CommandsExecuter commandsExecuter = new CommandsExecuter(message, telegramBot, callbackQuery);
            commandsExecuter.Execute(callbackQuery.Data);
        }

        private static void CheckPayed(object obj, ElapsedEventArgs e)
        {
            var orderService = InjectDependency.GetInstance().GetOrderService();
            RequestCreator request = new RequestCreator();
            string response = request.GetAnythink();

            var result = Serializer.Deserialize(response);

            List<Order> ordersFromFreeKasa = result.CollectionProperty.ToList();

            foreach (var orderFromKassa in ordersFromFreeKasa)
            {
                var order = orderService.Get(orderFromKassa.Id);

                if (order != null)
                {
                    if (order.Status == "new" && orderFromKassa.Status == "paid")
                    {
                        ChangeStatus(order, orderService, "paid");
                    }

                    if (order.Status == "new" && orderFromKassa.Status == "completed ")
                    {
                        ChangeStatus(order, orderService, "completed");
                    }
                }
            }
        }

        private static void TimerWorker()
        {
            timer = new System.Timers.Timer(60000);//300000
            timer.Elapsed += CheckPayed;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private static void ChangeStatus(Introspekt.DAL.Models.Order order, IOrderService orderService, string status)
        {
            order.Status = status;
            orderService.Update(order);
            SendFile(order.OrderDetails, order.ChatId);
        }

        private static void SendFile(IEnumerable<OrderDetails> orderDetails, long chatId)
        {
            foreach (var details in orderDetails)
            {
                var filePath = new DirectoryInformer().GetCurrentDirrectory() + "CourseFiles\\" + details.Course.Name + ".txt";
                var stream = File.Open(filePath, FileMode.Open);
                var photo = new InputOnlineFile(stream, details.Course.Name);
                var check = telegramBot.SendDocumentAsync(chatId, photo).Result;
                stream.Dispose();
            }
        }
    }
}
