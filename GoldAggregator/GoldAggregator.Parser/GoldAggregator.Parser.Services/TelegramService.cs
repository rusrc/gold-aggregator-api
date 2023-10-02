using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using System;
using System.Threading.Tasks;

using Telegram.Bot;
using Telegram.Bot.Types;

namespace GoldAggregator.Parser.Services
{
    // Api:         https://core.telegram.org/bots/api
    // Quickstart:  https://telegrambots.github.io/book/1/quickstart.html
    // github:      https://github.com/TelegramBots/Telegram.Bot
    public class TelegramService
    {
        // Keep your token secure and store it safely, it can be used by anyone to control your bot
        private const string token = "5311657244:AAH1_72D-f4I7fXIjMECxMpLJ2XEgz1aGLk";
        private const string cahtBotName = "@GoldAggregatorChatBot";
        private const long chatId = -1001525607901;

        private readonly ITelegramBotClient _botClient;
        private readonly ILogger _logger;

        public TelegramService(IConfiguration configuration,
            ILogger<TelegramService> logger)
        {
            _logger = logger;
            _botClient = new TelegramBotClient(token);
        }

        public async Task Publish(string text)
        {
            await _botClient.SendTextMessageAsync(chatId, text);
        }
    }
}
