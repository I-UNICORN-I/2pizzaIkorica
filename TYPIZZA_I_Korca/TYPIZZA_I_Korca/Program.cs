using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.InlineKeyboardButtons;



namespace TYPIZZA_I_Korca
{
    class Program
    {
        static TelegramBotClient Bot;


        static void Main(string[] args)
        {
            Bot = new TelegramBotClient("720232352:AAH_uJK6EXMelamUl-Xo-zQKUifVwLmdve8");

            Bot.OnMessage += BotOnMessageReceived;
            Bot.OnCallbackQuery += BOtOnCallbacQueryReceived;
            var me = Bot.GetMeAsync().Result;

            Console.WriteLine(me.FirstName);

            Bot.StartReceiving();

            Console.ReadLine();
            Bot.StopReceiving();

        }

        private static void BOtOnCallbacQueryReceived(object sender, CallbackQueryEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static async void BotOnMessageReceived(object sender, MessageEventArgs e)
        {
            var message = e.Message;

            if (message ==null ||  message.Type != MessageType.TextMessage)
                return;

            string name = $"{message.From.FirstName} {message.From.LastName}";



            Console.WriteLine($"{name} отправил: '{message.Text}'");
            switch (message.Text)
            {
                case "/start":
                    string text =
@"Список команд:
/start - запуск бота
/inline - вивiд меню
/button - вивiд кнопочок";
                    await Bot.SendTextMessageAsync(message.From.Id, text);
                    break;

                case "/inline":
                    var inlineKeyboard = new InlineKeyboardMarkup(new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("Easy-Pizza","https://www.audit-it.ru/blogs/rabotenet/3955.php"),

                            InlineKeyboardButton.WithUrl ("Hard-Pizza","https://liza.ua/lifestyle/food/10-luchshih-retseptov-pitstsyi-so-vsego-mira/")
                               },
                               new[]
                               {
                                   InlineKeyboardButton.WithCallbackData("пункт 1"),

                                   InlineKeyboardButton.WithCallbackData("пункт 2")
                               }

                    });
                    await Bot.SendTextMessageAsync(message.From.Id, "оберiть пункт меню", replyMarkup: inlineKeyboard);
                    break;


            }

            }
        }
}
