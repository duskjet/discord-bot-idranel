using System;
using System.Linq;
using Discord;
using Discord.Modules;
using Discord.API;
using Discord.Commands;
using Discord.Audio;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using DiscordBot.Configuration;

namespace DiscordBot
{
    class Program
    {
        private static DiscordClient client;

        private static readonly string AppKey = "MTc1OTQ0NTg0NTE2MzM3NjY1.CgY0qg.Y2VMmtQpGCR0DrnYlLvFQLqSDUQ";

        static void Main(string[] args) => new Program().Start(args);
        public void Start(string [] args)
        {
            var config = new Configuration.Configuration(args);
            
            client = new DiscordClient()
                .UsingAudio(audio => { audio.Mode = AudioMode.Outgoing; })
                .UsingModules();

            client.MessageReceived += async (source, e) =>
            {
                if (e.Message.Text.Contains("voice-connect"))
                {
                    var voiceChannel = e.Server.VoiceChannels.FirstOrDefault();
                    if (voiceChannel != null)
                    {
                        await voiceChannel.JoinAudio();
                    }
                }
            };

            client.ServerAvailable += (s, e) =>
            {
                Console.WriteLine($"Server \"{e.Server.Name}\" is available");
            };

            client.ExecuteAndWait(async () =>
            {
                await client.Connect(AppKey);
                await client.WaitForServers().ConfigureAwait(false);

                client.AddModule<Modules.GachimuchiModule>(name: "Gachimuchi Ultimate Medley link generator");
                client.AddModule<Modules.VoiceModule>();
            });
        }

    }

    public static class DiscordClientExtensions
    {
        public static async Task WaitForServers(this DiscordClient client)
        {
            var delay = 3000;
            Console.WriteLine($"Waiting {delay / 1000} seconds for server discovery...");

            await Task.Delay(delay);

            if (client.Servers == null || client.Servers.Count() == 0) throw new TimeoutException($"No servers was found in {delay / 1000} seconds.");
        }
    }

}
