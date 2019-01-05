using Rocket.API;

namespace YoungAccounts
{
    public class ConfigurationYoungAccounts : IRocketPluginConfiguration
    {
        public string SteamAPIKey;
        public int AccountAgeLimit;

        public void LoadDefaults()
        {
            SteamAPIKey = "XXXXXXXXXXXXXXXXX";
            AccountAgeLimit = 86400;
        }
    }
}

