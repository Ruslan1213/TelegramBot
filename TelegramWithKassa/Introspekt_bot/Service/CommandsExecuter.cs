using System;
using System.Collections.Generic;
using Introspekt_bot.Abstraction;
using Introspekt_bot.Commands;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Introspekt_bot.Service
{
    public class CommandsExecuter
    {
        private List<Command> commands;

        private CallbackQuery query;

        public CommandsExecuter(Message message, TelegramBotClient telegramBot, CallbackQuery q = null)
        {
            query = q;
            commands = new List<Command>();
            Register(message, telegramBot);
        }

        public void Execute(string commandName)
        {
            if (string.IsNullOrEmpty(commandName))
            {
                return;
            }

            var index = commandName.IndexOf(":", StringComparison.Ordinal);

            if (index != -1)
            { 
                commandName = commandName.Substring(0, index);
            }
            
            foreach (var command in commands)
            {
                if (command.Name.Contains(commandName))
                {
                    command.Execute();
                    break;
                }
            }
        }

        private void Register(Message message, TelegramBotClient telegramBot)
        {
            commands.Add(new HelloCommand(telegramBot, message));
            commands.Add(new GetCoursesCommand(telegramBot, message));
            commands.Add(new GetOneCourse(telegramBot, message, query));
            commands.Add(new GetInfoCommand(telegramBot, message, query));
            commands.Add(new BuyCourseCommand(telegramBot, message, query));
            commands.Add(new DetailsDeleteCommand(telegramBot, message, query));
            commands.Add(new BasketItemsCommand(telegramBot, message));
        }
    }
}
