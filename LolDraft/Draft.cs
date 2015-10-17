using System;
using System.Collections.Generic;
using System.Linq;

namespace LolDraft
{
    public class Draft
    {
        public static Turn[] TurnSequence = {   Turn.BlueBan, Turn.RedBan, Turn.BlueBan, Turn.RedBan, Turn.BlueBan, Turn.RedBan,
                                                Turn.BluePick,
                                                Turn.RedPick, Turn.RedPick,
                                                Turn.BluePick, Turn.BluePick,
                                                Turn.RedPick, Turn.RedPick,
                                                Turn.BluePick, Turn.BluePick,
                                                Turn.RedPick,
                                                Turn.Done};
        public Guid AdminKey { get; set; }
        public Guid PublicKey { get; set; }
        public Team RedTeam { get; set; }
        public Team BlueTeam { get; set; }
        public List<Champion> Selected
        {
            get
            {
                return BlueTeam.Bans.Concat(BlueTeam.Picks).Concat(RedTeam.Bans).Concat(RedTeam.Picks).ToList();
            }
        }

        public Turn Turn
        {
            get
            {
                return TurnSequence[Selected.Count];
            }
        }

        public Turn PreviousTurn
        {
            get
            {
                if (Selected.Count == 0)
                {
                    return Turn;
                }

                return TurnSequence[Selected.Count - 1];
            }
        }

        public Draft()
        {
            AdminKey = Guid.NewGuid();
            PublicKey = Guid.NewGuid();
            RedTeam = new Team();
            BlueTeam = new Team();
        }

        public bool CanView(Guid guid)
        {
            var guids = new List<Guid> { PublicKey, AdminKey, BlueTeam.TeamAdminGuid, RedTeam.TeamAdminGuid };
            return guids.Contains(guid);
        }

        public bool CanAdmin(Guid guid)
        {
            return guid == AdminKey;
        }

        public bool CanAdminTeam(Guid guid, Team team)
        {
            return guid == AdminKey || guid == team.TeamAdminGuid;
        }
    }

    public enum Turn
    {
        RedBan,
        RedPick,
        BlueBan,
        BluePick,
        Done
    }
}