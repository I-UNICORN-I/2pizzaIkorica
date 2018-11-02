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
using Telegram.Bot.Types;
using ApiAiSDK;
using ApiAiSDK.Model;

namespace TYPIZZA_I_Korca
{
    class Program
    {
        static TelegramBotClient Bot;
        static ApiAi apiAi;

        static void Main(string[] args)
        {

            //6146f73d9b1f4cd5afa2b65c9d78da03

            Bot = new TelegramBotClient("720232352:AAH_uJK6EXMelamUl-Xo-zQKUifVwLmdve8");
            AIConfiguration cofig = new AIConfiguration("6146f73d9b1f4cd5afa2b65c9d78da03", SupportedLanguage.Russian);
            apiAi = new ApiAi(cofig);


            Bot.OnMessage += BotOnMessageReceived;
            Bot.OnCallbackQuery += BOtOnCallbacQueryReceived;
            var me = Bot.GetMeAsync().Result;

            Console.WriteLine(me.FirstName);

            Bot.StartReceiving();

            Console.ReadLine();
            Bot.StopReceiving();

        }

        private static async void BOtOnCallbacQueryReceived(object sender, CallbackQueryEventArgs e)
        {
            string buttoText = e.CallbackQuery.Data;
            string name = $"{e.CallbackQuery.From.FirstName} {e.CallbackQuery.From.LastName}";
            Console.WriteLine($"{name}:натискнув кнопку {buttoText}");

            try
            {
                await Bot.AnswerCallbackQueryAsync(e.CallbackQuery.Id, $"Ви натиснули кнопку {buttoText}");
            }
            catch
            {

            }
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
/Keyboard - вивiд кнопочок";
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
                            /*    new[]
                              {
                                   InlineKeyboardButton.WithCallbackData("пункт 1"),

                                   InlineKeyboardButton.WithCallbackData("пункт 2")
                               }*/

                    });
                    await Bot.SendTextMessageAsync(message.From.Id, "оберiть пункт меню", replyMarkup: inlineKeyboard);
                    break;

                case "/Keyboard":
                    var replyKeyboard = new ReplyKeyboardMarkup(new[]
                    {
                       new[]
                        { 
                          
                            new KeyboardButton("Привiт"),
                             new KeyboardButton("Що ти вмієш?"),
                           new KeyboardButton("Рецепт швидкої вечері."),
                           new KeyboardButton("Рецепт швидкого сніданку.")
                        },
                             new[]
                            {
                                new KeyboardButton("Контакт") {RequestContact = true},
                             new KeyboardButton("Де ти") {RequestLocation = true}

                            }
                    });
                    await Bot.SendTextMessageAsync(message.Chat.Id, "Кнопка", replyMarkup: replyKeyboard);
                    break;
                default:
                    var response = apiAi.TextRequest(message.Text);
                    string answer = response.Result.Fulfillment.Speech;
                    if (answer == "")
                        answer = "____";
                      await Bot.SendTextMessageAsync(message.From.Id, answer);

                        answer = "sdfdsgdsfgdg_";
                    await Bot.SendTextMessageAsync(message.From.Id, answer);

                    break;


            }

            }
        }
}
