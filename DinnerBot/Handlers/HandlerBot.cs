using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace DinnerBot.Handlers
{
    public class HandlerBot
    {
        private readonly ILogger _logger;

        private readonly ITelegramBotClient _botClient;
        public HandlerBot(ILogger<HandlerBot> logger, ITelegramBotClient botClient)
        {
            _logger = logger;
            _botClient = botClient;
        }

        public void Start()
        {
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };
            _botClient.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
        }
        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            _logger.LogDebug("{@update}", update);

            try
            {
                if (update.Type == UpdateType.Message)
                {
                    var message = update.Message;
                    if (message.Text.ToLower() == "/start")
                    {
                        await botClient.SendTextMessageAsync(message.Chat, "Добро пожаловать на борт, добрый путник!");
                        return;
                    }

                    if (message.Text.ToLower() == "/menu")
                    {
                        var b = new InlineKeyboardButton("asd").CallbackData = "1";

                        InlineKeyboardButton[] row1 = new InlineKeyboardButton[] { b };

                        InlineKeyboardButton[][] buttons = new InlineKeyboardButton[][] { row1 };

                        var a = new InlineKeyboardMarkup(buttons);
                        await botClient.SendTextMessageAsync(message.Chat, "menu", ParseMode.Markdown, 
                            replyMarkup: a);
                    }
                    else
                    {

                        await botClient.SendTextMessageAsync(message.Chat, "Привет-привет!!");

                    }
                }

                if (update.Type == UpdateType.CallbackQuery)
                {
                    var message = update.CallbackQuery.Message;
                    await botClient.EditMessageTextAsync(message.Chat, message.MessageId, "Привет-привет!!");
                }
            }
            catch (Exception e)
            {

                _logger.LogError(e, "{@update}", update);
            }

        }

        private async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "error");
        }
    }
}