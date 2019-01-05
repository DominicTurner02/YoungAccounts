using System.Net;
using Newtonsoft.Json.Linq;

namespace YoungAccounts
{
    public class WebUtil
    {
        public string GetCreationDate(string SteamID)
        {
            using (WebClient WebClient = new WebClient())
            {
                return JObject.Parse(WebClient.DownloadString($"http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key={YoungAccounts.Instance.Configuration.Instance.SteamAPIKey}&steamids={SteamID}"))["response"]["players"][0]["timecreated"].ToString();
            }
        }
    }
}
