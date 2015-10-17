using System;
using System.Collections.Generic;

namespace LolDraft
{
    public class Team
    {
        public Team()
        {
            Bans = new List<Champion>();
            Picks = new List<Champion>();
            TeamAdminGuid = Guid.NewGuid();
        }

        public Guid TeamAdminGuid { get; set; }
        public string Name { get; set; }
        public List<Champion> Bans { get; set; }
        public List<Champion> Picks { get; set; }
    }
}