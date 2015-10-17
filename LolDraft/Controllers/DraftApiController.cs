using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace LolDraft.Controllers
{
    public class DraftApiController : ApiController
    {
        private List<Champion> _champions;
        private List<Draft> _drafts;

        public DraftApiController(List<Champion> champions, List<Draft> drafts)
        {
            _drafts = drafts;
            _champions = champions;
        }

        [Route("api/draft/create")]
        [HttpGet]
        public Draft CreateDraft()
        {
            var draft = new Draft();
            _drafts.Add(draft);
            return draft;
        }

        [Route("api/draft")]
        [HttpGet]
        public Draft GetDraft(Guid draftGuid)
        {
            var draft = _drafts.Find(d => d.CanView(draftGuid));
            if (draft == null)
            {
                // TODO: error
            }

            return draft;
        }

        [Route("api/draft/select")]
        [HttpPut]
        public Draft Select(Guid draftGuid, int championId)
        {
            var draft = _drafts.Find(d => d.CanView(draftGuid));
            if (draft == null)
            {
                // TODO: error
            }

            var champion = _champions.Find(c => c.id == championId);
            if (champion == null)
            {
                // TODO: error
            }

            if (draft.CanAdminTeam(draftGuid, draft.BlueTeam) && draft.Turn == Turn.BlueBan)
            {
                draft.BlueTeam.Bans.Add(champion);
            }
            else if (draft.CanAdminTeam(draftGuid, draft.RedTeam) && draft.Turn == Turn.RedBan)
            {
                draft.RedTeam.Bans.Add(champion);
            }
            else if (draft.CanAdminTeam(draftGuid, draft.BlueTeam) && draft.Turn == Turn.BluePick)
            {
                draft.BlueTeam.Picks.Add(champion);
            }
            else if (draft.CanAdminTeam(draftGuid, draft.RedTeam) && draft.Turn == Turn.RedPick)
            {
                draft.RedTeam.Picks.Add(champion);
            }
            else
            {
                // TODO: error
            }            

            return draft;
        }

        [Route("api/draft/undo")]
        [HttpPut]
        public Draft Undo(Guid draftGuid)
        {
            var draft = _drafts.Find(d => d.CanView(draftGuid));
            if (draft == null)
            {
                // TODO: error
            }

            if (!draft.CanAdmin(draftGuid))
            {
                // TODO: error
            }

            if (draft.Selected.Count == 0)
            {
                // TODO: error
            }

            switch(draft.PreviousTurn)
            {
                case Turn.BlueBan:
                    draft.BlueTeam.Bans.RemoveAt(draft.BlueTeam.Bans.Count - 1);
                    break;
                case Turn.BluePick:
                    draft.BlueTeam.Picks.RemoveAt(draft.BlueTeam.Picks.Count - 1);
                    break;
                case Turn.RedBan:
                    draft.RedTeam.Bans.RemoveAt(draft.RedTeam.Bans.Count - 1);
                    break;
                case Turn.RedPick:
                    draft.RedTeam.Picks.RemoveAt(draft.RedTeam.Picks.Count - 1);
                    break;
            }
            
            return draft;
        }
    }
}