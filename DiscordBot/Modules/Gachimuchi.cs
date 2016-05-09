using Discord;
using Discord.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBot.Modules
{
    public class GachimuchiModule : IModule
    {
        public void Install(ModuleManager manager)
        {
            manager.MessageReceived += async (source, e) =>
            {
                await e.Gachimuchi();
            };
        }
    }

    public static class GachimuchiExtensions
    {
        private static Random random = new Random();

        private static List<string> linkList = new List<string>()
        {
            { "https://youtu.be/HInx3rKx36Q?t=166" },
            { "https://youtu.be/HInx3rKx36Q?t=259" },
            { "https://youtu.be/HInx3rKx36Q?t=311" },
            { "https://youtu.be/ZdRTaYrmIIM?t=167" },
            { "https://youtu.be/ZdRTaYrmIIM?t=269" },
            { "https://youtu.be/ZdRTaYrmIIM?t=402" },
            { "https://youtu.be/e2VXvqjGXjo" },
            { "https://youtu.be/kw7IBJaN6rQ" },
            { "https://youtu.be/AnbTd2WKYdY" },
        };

        public static async Task Gachimuchi(this MessageEventArgs args)
        {
            var isMentioned = args.Message.MentionedUsers.Any(user => user.Id.Equals(args.Channel.Client.CurrentUser.Id));
            var isPrivate = args.Channel.IsPrivate;
            var isKeyword = args.Message.RawText.ToUpperInvariant().Contains("FUCK YOU");

            if ((isMentioned | isPrivate) && isKeyword)
            {
                await args.Message.Channel.SendGachimuchi();
            }
        }

        public static async Task<Message> SendGachimuchi(this Channel channel)
        {
            var index = random.Next(0, linkList.Count);
            var msg = linkList[index];

            return await channel.SendMessage(msg);
        }
    }
}
