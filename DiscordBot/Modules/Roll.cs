using Discord;
using Discord.Modules;
using DiscordBot.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBot.Modules
{
    public class RollModule : IModule
    {
        public void Install(ModuleManager manager)
        {
            manager.MessageReceived += async (source, e) =>
            {
                await e.Roll();
            };
        }
    }

    public static class RollExtensions
    {
        private static Random random = new Random();


        #region Hero List
        private static List<string> heroList = new List<string>()
        {
            {"Razor"},
            {"Pit Lord" },
        };
        #endregion

        public static async Task Roll(this MessageEventArgs args)
        {
            var isPrivate = args.Channel.IsPrivate;
            var isKeyword = args.Message.RawText.ToUpperInvariant().Contains("!ROLL");
            var dotoKeyword = args.Message.RawText.ToUpperInvariant().Contains("!RANDOM");
            var isMentioned = args.Message.MentionedUsers.Any(user => user.Id.Equals(args.Channel.Client.CurrentUser.Id));
            string dotoUser = args.Message.User.Name.ToString();
            
            if (isKeyword | isMentioned && isKeyword)
            {
                await args.Message.Channel.RollNumber();
            }

            if (dotoKeyword | isPrivate && dotoKeyword)
            {
                await args.Message.Channel.DotoRandom(dotoUser);
            }
        }

        public static async Task<Message> DotoRandom(this Channel channel, string dotoUser)
        {
            
            var index = random.Next(0, heroList.Count);
            var msg = dotoUser + " has randomed " + "**"+heroList[index]+"**";

            return await channel.SendMessage(msg);
        }

        public static async Task<Message> RollNumber(this Channel channel)
        {
            
            var index = random.Next(0, 101);
            var msg = index.ToString();

            return await channel.SendMessage(msg);
        }
    }
}