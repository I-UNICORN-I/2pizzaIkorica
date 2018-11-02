using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace TYPIZZA_I_Korca
{
    class Program
    {
        static TelegramBotClient Bot;


        static void Main(string[] args)
        {
            Bot = new TelegramBotClient("720232352:AAH_uJK6EXMelamUl-Xo-zQKUifVwLmdve8");

            Bot.OnMessage += BotOnMessageReceived;

            var me = Bot.GetMeAsync().Result;

            Console.WriteLine(me.FirstName);

            Bot.StartReceiving();

            Console.ReadLine();
            Bot.StopReceiving();

        }

        private static void BotOnMessageReceived(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            var message = e.Message;

            string name = $"{message.From.FirstName} {message.From.LastName}";


            Console.WriteLine($"{name} отправил: '{message.Text}'");
        }
    }
}
