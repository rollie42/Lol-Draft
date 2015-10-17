using System.Collections.Generic;

namespace LolDraft
{
    public class ChampionData : Dictionary<string, Champion> { }

    public class ChampionResponse
    {
        public ChampionData data { get; set; }
    }

    //public class ChampionData
    //{
    //    public IDictionary<string, Champion> Champions { get; set; }
    //}

    public class Champion
    {
        public int id { get; set; }
        public string title { get; set; }
        public string name { get; set; }
        public Image image { get; set; }

        // Additional fields for convenience
        public string thumbnailUrl { get; set; }

        public bool available { get; set; }
    }

    public class Image
    {
        public int w { get; set; }
        public string full { get; set; }
        public string sprite { get; set; }
        public string group { get; set; }
        public int h { get; set; }
        public int y { get; set; }
        public int x { get; set; }
    }
}