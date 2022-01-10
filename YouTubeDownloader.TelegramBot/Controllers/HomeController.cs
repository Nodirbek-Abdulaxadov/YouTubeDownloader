using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using YouTubeDownloader.TelegramBot.Service;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using VideoLibrary;
using System.IO;
using Telegram.Bot.Types.Enums;

namespace YouTubeDownloader.TelegramBot.Controllers
{
    public class HomeController : Controller
    {
        private TelegramBotClient client = new TelegramBotClient("2116797944:AAGP2DCaZa4rXqCJoo7OwiuUr5OlBCYs10E");

        long userId = 0;

        [Obsolete]
        public IActionResult Index()
        {
            client.OnMessage += Xabar_Kelganda;

            client.OnCallbackQuery += CallBack;

            client.StartReceiving();            

            return Ok("ishlayapti");
        }
        VideoService videoService;
        private async void CallBack(object sender, CallbackQueryEventArgs e)
        {
            await client.SendTextMessageAsync(
                           chatId: this.userId,
                           text: "Yuklanmoqda..."
                           );
            if (e.CallbackQuery.Data == "144")
            {
                YouTubeVideo video = videoService.videoList.FirstOrDefault(v => v.Resolution == 144);
                using (var stream = video.Stream())
                {
                    await client.SendVideoAsync(
                        chatId: this.userId,
                        caption: video.FullName + "\n\n@dotnet2021_bot",
                        video: stream,
                        supportsStreaming: true, // videoni yuklash davomida ochish
                        disableNotification: true
                    );
                }
            }
            if (e.CallbackQuery.Data == "244")
            {
                YouTubeVideo video = videoService.videoList.FirstOrDefault(v => v.Resolution == 244);
                using (var stream = video.Stream())
                {
                    await client.SendVideoAsync(
                        chatId: this.userId,
                        caption: video.FullName + "\n\n@dotnet2021_bot",
                        video: stream,
                        supportsStreaming: true, // videoni yuklash davomida ochish
                        disableNotification: true
                    );
                }
            }
            if (e.CallbackQuery.Data == "360")
            {
                YouTubeVideo video = videoService.videoList.FirstOrDefault(v => v.Resolution == 360);
                using (var stream = video.Stream())
                {
                    await client.SendVideoAsync(
                        chatId: this.userId,
                        caption: video.FullName + "\n\n@dotnet2021_bot",
                        video: stream,
                        supportsStreaming: true, // videoni yuklash davomida ochish
                        disableNotification: true
                    );
                }
            }
            if (e.CallbackQuery.Data == "480")
            {
                YouTubeVideo video = videoService.videoList.FirstOrDefault(v => v.Resolution == 480);           
                using (var stream = video.Stream())
                {
                    await client.SendVideoAsync(
                        chatId: this.userId,
                        caption: video.FullName + "\n\n@dotnet2021_bot",
                        video: stream,
                        supportsStreaming: true, // videoni yuklash davomida ochish
                        disableNotification: true
                    );
                }
            }
            if (e.CallbackQuery.Data == "720")
            {
                YouTubeVideo video = videoService.videoList.FirstOrDefault(v => v.Resolution == 720);
                using (var stream = video.Stream())
                {
                    await client.SendVideoAsync(
                        chatId: this.userId,
                        caption: video.FullName + "\n\n@dotnet2021_bot",
                        video: stream,
                        supportsStreaming: true, // videoni yuklash davomida ochish
                        disableNotification: true
                    );
                }
            }
            if (e.CallbackQuery.Data == "1080")
            {
                YouTubeVideo video = videoService.videoList.FirstOrDefault(v => v.Resolution == 1080);
                using (var stream = video.Stream())
                {
                    await client.SendVideoAsync(
                        chatId: this.userId,
                        caption: video.FullName + "\n\n@dotnet2021_bot",
                        video: stream,
                        supportsStreaming: true, // videoni yuklash davomida ochish
                        disableNotification: true
                    );
                }
            }
            if (e.CallbackQuery.Data == "1440")
            {
                YouTubeVideo video = videoService.videoList.FirstOrDefault(v => v.Resolution == 1440);
                using (var stream = video.Stream())
                {
                    await client.SendVideoAsync(
                        chatId: this.userId,
                        caption: video.FullName + "\n\n@dotnet2021_bot",
                        video: stream,
                        supportsStreaming: true, // videoni yuklash davomida ochish
                        disableNotification: true
                    );
                }
            }
            if (e.CallbackQuery.Data == "2160")
            {
                YouTubeVideo video = videoService.videoList.FirstOrDefault(v => v.Resolution == 2160);
                using (var stream = video.Stream())
                {
                    await client.SendVideoAsync(
                        chatId: this.userId,
                        caption: video.FullName + "\n\n@dotnet2021_bot",
                        video: stream,
                        supportsStreaming: true, // videoni yuklash davomida ochish
                        disableNotification: true
                    );
                }
            }
            if (e.CallbackQuery.Data == "4320")
            {
                YouTubeVideo video = videoService.videoList.FirstOrDefault(v => v.Resolution == 4320);
                using (var stream = video.Stream())
                {
                    await client.SendVideoAsync(
                        chatId: this.userId,
                        caption: video.FullName + "\n\n@dotnet2021_bot",
                        video: stream,
                        supportsStreaming: true,
                        disableNotification: true
                    );
                }
            }
        }
        private async void Xabar_Kelganda(object sender, MessageEventArgs e)
        {
            // foydalanuvchi idsi
            userId = e.Message.Chat.Id;
            // kelgan xabar idsi
            int msgId = e.Message.MessageId;
            // kelgan xabar
            string xabar = e.Message.Text.ToLower();

            #region Start blog
            if (xabar == "/start")
            {
                await client.SendTextMessageAsync(
                           chatId: e.Message.Chat.Id,
                           text: "Assalomu alaykum!"
                           );
            }
            else if (!string.IsNullOrEmpty(xabar) && xabar.Contains("http"))
            {
                try
                {
                    string url = e.Message.Text;
                    videoService = new VideoService();
                    var list = videoService.GetVideoFormats(url);
                    List<List<InlineKeyboardButton>> buttons = new List<List<InlineKeyboardButton>>();
                    foreach (var item in list)
                    {
                        List<InlineKeyboardButton> row = new List<InlineKeyboardButton>()
                        {
                            InlineKeyboardButton.WithCallbackData(text: item.ToString(), callbackData: item.ToString())
                        };
                        buttons.Add(row);
                    }

                    var markup = new InlineKeyboardMarkup(buttons);
                    string name = videoService.videoList.FirstOrDefault(p => p.FileExtension == ".mp4").FullName;
                    await client.SendTextMessageAsync(
                            chatId: e.Message.Chat.Id,
                            text: $"\n<a href=\'{url}\'>{name}</a>",
                            parseMode: ParseMode.Html,
                            replyMarkup: markup
                            );
                }
                catch (Exception)
                {
                    await client.SendTextMessageAsync(
                           chatId: e.Message.Chat.Id,
                           text: "Invalid Video-url!"
                           );
                }
            }
            #endregion
        }
    }
}
