using System;
using System.Collections.Generic;

namespace DiscordBot.Configuration
{
    public class Configuration
    {
        private static Configuration _config;

        private static Dictionary<string, Dictionary<string, Action<Configuration, string>>> commands = 
            new Dictionary<string, Dictionary<string, Action<Configuration, string>>>()
        {
            {"-voice", new Dictionary<string, Action<Configuration, string>>() {
                { "--server", (c, s) => { c.Voice.Server = s; } },
                { "--channel", (c, s) => { c.Voice.Channel = s; } }
            } }
        };

        public static Configuration Current { get { return _config; } }
        public VoiceConfiguration Voice { get; set; }

        public Configuration(string[] args)
        {
            this.Voice = new VoiceConfiguration();

            for (int i = 0; i < args.Length; i++)
            {
                var command = args[i];

                if (commands.ContainsKey(command))
                    for (int j = i + 1; j < i + commands[command].Count; j += 2)
                    {
                        var parameter = args[j];
                        if (commands[command].ContainsKey(parameter))
                        {
                            commands[command][parameter](this, args[j + 1]);
                        }
                    }
            }

            _config = this;
        }
    }

    public class VoiceConfiguration
    {
        public string Server { get; set; }
        public string Channel { get; set; }
    }
}
