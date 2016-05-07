using System;
using Discord;
using Discord.Modules;
using Discord.Audio;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBot.Modules
{
    public class VoiceModule : IModule
    {
        public async void Install(ModuleManager manager)
        {
            var config = Configuration.Configuration.Current.Voice;
            var serverName = config.Server ?? null;

            if (string.IsNullOrEmpty(serverName)) return;

            var server = manager.Client.FindServers(serverName).FirstOrDefault();

            if (server == null) throw new NullReferenceException($"There is no server with that name ({serverName})");

            if (server.VoiceChannels == null || server.VoiceChannels.Count() == 0) throw new NullReferenceException("There are no voice channels on server");

            await JoinAudio(server, config);

            manager.ServerAvailable += async (s, e) =>
            {
                if (e.Server.Name == serverName)
                {
                    await JoinAudio(e.Server, config);
                }
            };
        }

        private async Task JoinAudio(Server server, Configuration.VoiceConfiguration config)
        {
            var channel = config.Channel != null ? server.VoiceChannels.Single(c => c.Name == config.Channel) : server.VoiceChannels.First();

            await channel.JoinAudio();
        }
    }
}
