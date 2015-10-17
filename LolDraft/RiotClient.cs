using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LolDraft
{
    public class RiotClient
    {
        private HttpClient _httpClient = new HttpClient();
        private const string imageBase = "http://ddragon.leagueoflegends.com/cdn/5.19.1/img/champion/";
        private const string apiBase = "https://na.api.pvp.net/api/lol/";
        private string apiKey = "d0c71672-c49b-4e79-80ed-65433c9f71f6";

        public async Task<List<Champion>> GetChampionsAsync()
        {
            string url = apiBase + "static-data/na/v1.2/champion?champData=image&api_key=" + apiKey;

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var champsStr = await response.Content.ReadAsStringAsync();
            var respObj = JsonConvert.DeserializeObject<ChampionResponse>(champsStr);

            foreach (var champ in respObj.data.Values)
            {
                champ.thumbnailUrl = GetImageUrl(champ);
                champ.available = true;
            }

            return respObj.data.Values.ToList();
        }

        public string GetImageUrl(Champion champ)
        {
            return imageBase + champ.image.full;
        }

        public static async Task TestAsync()
        {
            var client = new RiotClient();
            await client.GetChampionsAsync();
        }
    }
}