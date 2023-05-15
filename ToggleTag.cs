using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Linq;
using CommandSystem;
using CommandSystem.Commands.RemoteAdmin;
using Newtonsoft.Json.Linq;
using PlayerRoles;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PluginAPI.Events;
using PluginAPI.Helpers;
using RemoteAdmin;

namespace ToggleTag
{

    public class ToggleTag
    {
        public static HashSet<string> tagsToggled;

        private readonly string defaultConfig =
        "{\n"                 +
        "    \"tags\": []\n"  +
        "}";

        public const string VERSION = "1.2.0";

        [PluginEntryPoint("SCPDiscord", VERSION, "Enables persistant toggling of tags.", "Karl Essinger")]
        public void OnEnable()
        {
            Log.Info("Loading saved players '" + GetConfigPath() + "'...");
            if (!File.Exists(GetConfigPath()))
            {
                File.WriteAllText(GetConfigPath(), defaultConfig);
            }
            JToken jsonObject = JToken.Parse(File.ReadAllText(GetConfigPath()));

            tagsToggled = new HashSet<string>(jsonObject.SelectToken("tags")?.Values<string>());

            EventManager.RegisterEvents(this, this);
            Log.Info("ToggleTag " + VERSION + " enabled!");
        }

        public static string GetConfigPath()
        {
            return Paths.GlobalPlugins.Plugins + "/toggletag.json";
        }

        public static void SaveTagsToFile()
        {
            // Save the state to file
            StringBuilder builder = new StringBuilder();
            builder.Append("{\n");
            builder.Append("    \"tags\":\n");
            builder.Append("    [\n");
            foreach (string line in tagsToggled)
            {
                builder.Append("        \"" + line + "\"," + "\n");
            }
            builder.Append("    ]\n");
            builder.Append("}");
            File.WriteAllText(GetConfigPath(), builder.ToString());
        }

        [PluginEvent(ServerEventType.PlayerJoined)]
        public void OnPlayerJoin(Player player)
        {
            if(tagsToggled.Contains(player.UserId))
            {
                player.ReferenceHub.characterClassManager.CmdRequestHideTag();
            }
            else
            {
                player.ReferenceHub.characterClassManager.CmdRequestShowTag(false);
            }
        }

        [PluginEvent(ServerEventType.RemoteAdminCommandExecuted)]
        public void OnAdminQuery(ICommandSender sender, string command, string[] args, bool result, string response)
        {
            if (!(sender is PlayerCommandSender playerSender) || Player.Get(playerSender.ReferenceHub) == null) return;

            Player player = Player.Get(playerSender.ReferenceHub);
            switch (command)
            {
                case "hidetag":
                    tagsToggled.Add(player.UserId);
                    SaveTagsToFile();
                    return;
                case "showtag":
                    tagsToggled.Remove(player.UserId);
                    SaveTagsToFile();
                    return;
                default:
                    return;
            }
        }
    }

    [CommandHandler(typeof (GameConsoleCommandHandler))]
    class HideTagCommand
    {
        public string Command => "console_hidetag";
        public string[] Aliases => new string[] {};
        public string Description => "Hide the tag of a player, callable from the console.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count > 0)
            {
                bool wasVisible = ToggleTag.tagsToggled.Add(arguments.At(0));

                Player player = Player.GetPlayers().FirstOrDefault(x => x.UserId == arguments.At(0));

                if (player == null)
                {
                    response = "Could not find a player by that user ID.";
                    return false;
                }

                player?.ReferenceHub.characterClassManager.CmdRequestHideTag();
                if (wasVisible)
                {
                    response = "Tag hidden of " + player.DisplayNickname + ".";
                    ToggleTag.SaveTagsToFile();
                }
                else
                {
                    response = "Tag was already hidden.";
                }

                return true;
            }
            response = "Not enough arguments provided. 'console_hidetag <userid>'";
            return false;
        }
    }

    [CommandHandler(typeof (GameConsoleCommandHandler))]
    class ShowTagCommand
    {
        public string Command => "console_showtag";
        public string[] Aliases => new string[] {};
        public string Description => "Show the tag of a player, callable from the console.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count > 0)
            {
                bool wasHidden = ToggleTag.tagsToggled.Remove(arguments.At(0));

                Player player = Player.GetPlayers().FirstOrDefault(x => x.UserId == arguments.At(0));

                if (player == null)
                {
                    response = "Could not find a player by that user ID.";
                    return false;
                }

                player.ReferenceHub.characterClassManager.CmdRequestShowTag(false);
                if (wasHidden)
                {
                    response = "Tag revealed of " + player.DisplayNickname + ".";
                    ToggleTag.SaveTagsToFile();
                }
                else
                {
                    response = "Tag was already revealed.";
                }

                return true;
            }
            response = "Not enough arguments provided. 'console_showtag <userid>'";
            return false;
        }
    }
}
