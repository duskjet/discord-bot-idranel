using System;
using System.Collections.Generic;

namespace DiscordBot.Configuration
{
    public class Configuration
    {
        private static Configuration _config;

        private static Dictionary<string, Option> commands = new Dictionary<string, Option>()
        {
            { "-v", new Option((c, s) => { c.Voice.Server = s; }) },
            { "-c", new Option((c, s) => { c.Voice.Channel = s; }, new string[] { "-v" }) }
        };

        public static Configuration Current { get { return _config; } }

        public VoiceConfiguration Voice { get; set; }

        public Configuration(string[] args)
        {
            this.Voice = new VoiceConfiguration();

            for (int i = 0; i < args.Length; i++)
            {
                if (commands.ContainsKey(args[i]))
                {
                    var cmd = args[i];
                    var value = args[i + 1];

                    if (commands[cmd].Dependencies != null) ValidateDependencies(commands[cmd].Dependencies, cmd, args);

                    commands[cmd].Action(this, value);
                }
            }

            _config = this;
        }

        private void ValidateDependencies(string[] dependencies, string command, string[] args)
        {
            foreach (var dep in dependencies)
            {
                var match = Array.Find(args, s => s.Equals(dep));
                if (string.IsNullOrEmpty(match))
                {
                    throw new ArgumentNullException(dep, $"Option \"{command}\" depends upon option \"{dep}\" that is not present.");
                }
            }
        }
    }

    public class VoiceConfiguration
    {
        public string Server { get; set; }
        public string Channel { get; set; }
    }

    internal class Option
    {
        public Option(Action<Configuration, string> action, string[] dependencies = null)
        {
            this.Action = action;
            this.Dependencies = dependencies;
        }
        public string[] Dependencies { get; }
        public Action<Configuration, string> Action { get; }
    }
}
