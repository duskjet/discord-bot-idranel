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
            {"Razor"},            {"Rubick"},            {"Phantom Lancer"},            {"Legion Commander"},            {"Brewmaster"},            {"Outworld Devourer"},            {"Sniper"},            {"Lina"},            {"Sven"},            {"Visage"},            {"Undying"},            {"Tiny"},            {"Tidehunter"},            {"Puck"},            {"Ursa"},            {"Magnus"},            {"Earthshaker"},            {"Windrunner"},            {"Techies"},            {"Crystal Maiden"},            {"Batrider"},            {"Riki"},            {"Invoker"},            {"Venomancer"},            {"Timbersaw"},            {"Wraithking"},            {"Anti Mage"},            {"Ancient Apparition"},            {"Troll Warlord"},            {"Lich"},            {"Enchantress"},            {"Bristleback"},            {"Pudge"},            {"Faceless Void"},            {"Tinker"},            {"Mirana"},            {"Bounty Hunter"},            {"Treant Protector"},            {"Gyrocopter"},            {"Slardar"},            {"Lifestealer"},            {"Jakiro"},            {"Terrorblade"},            {"Dazzle"},            {"Chaos Kinght"},            {"Abaddon"},            {"Shadow Demon"},            {"Axe"},            {"Zeus"},            {"Alchemist"},            {"Elder Titan"},            {"Pugna"},            {"Vengeful Spirit"},            {"Broodmother"},            {"Sand King"},            {"Lion"},            {"Witch Doctor"},            {"Ember Spirit"},            {"Clockwerk"},            {"Phantom Assassin"},            {"Warlock"},            {"Chen"},            {"Keeper of the Light"},            {"Beastmaster"},            {"Centaur Warruner"},            {"Naga Siren"},            {"Kunkka"},            {"Phoenix"},            {"Silencer"},            {"Morphling"},            {"Slark"},            {"Meepo"},            {"Shadow Shaman"},            {"Templar Assassin"},            {"Juggernaut"},            {"Natures Prophet"},            {"Necrolyte"},            {"Earth Spirit"},            {"Doom"},            {"Shadow Fiend"},            {"Omniknight"},            {"Skywrath Mage"},            {"Weaver"},            {"Wisp"},            {"Medusa"},            {"Nightstalker"},            {"Ogre Magi"},            {"Tusk"},            {"Spectre"},            {"Nyx Assassin"},            {"Drow Ranger"},            {"Clinkz"},            {"Disruptor"},            {"Bane"},            {"Enigma"},            {"Dragon Knight"},            {"Viper"},            {"Queen of Pain"},            {"Luna"},            {"Huskar"},            {"Death Prophet"},            {"Storm Spirit"},            {"Spirit Breaker"},            {"Dark Seer"},            {"Bloodseeker"},            {"Lone Druid"},            {"Lycan"},            {"Leshrac"},
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
