using Rocket.API;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Events;
using Rocket.Unturned.Permissions;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace YoungAccounts
{
    public class YoungAccounts : RocketPlugin<ConfigurationYoungAccounts>
    {
        public static YoungAccounts Instance { get; private set; }
        public WebUtil wUtil = new WebUtil();

        protected override void Load()
        {
            base.Load();
            Instance = this;
            Logger.Log("Loading YoungAccounts, made by Mr.Kwabs!\n", ConsoleColor.Yellow);
            Logger.Log($"Steam API Key: {Configuration.Instance.SteamAPIKey}", ConsoleColor.Yellow);
            Logger.Log($"Account Age Limit: {Configuration.Instance.AccountAgeLimit} seconds\n", ConsoleColor.Yellow);
            UnturnedPermissions.OnJoinRequested += OnJoinRequest;
            Logger.Log("Successfully loaded YoungAccounts, made by Mr.Kwabs!", ConsoleColor.Yellow);
        }

        protected override void Unload()
        {
            Logger.Log("Unloading YoungAccounts, made by Mr.Kwabs!", ConsoleColor.Red);
            base.Unload();
            Instance = null;
            UnturnedPermissions.OnJoinRequested -= OnJoinRequest;
        }

        private void OnJoinRequest(CSteamID cSteamID, ref ESteamRejection? RejectionReason)
        {
            if (CurrentUnix() > long.Parse(wUtil.GetCreationDate(cSteamID.ToString()) + Configuration.Instance.AccountAgeLimit))
            {
                return;
            }
            else
            {
                RejectionReason = ESteamRejection.PLUGIN;
                Logger.Log($"{cSteamID} has been kicked as their Account doesn't meet the requirement!", ConsoleColor.Red);
            }
        }
        private static long CurrentUnix() { return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds; }
    }
}
