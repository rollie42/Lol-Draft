using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LolDraft.Controllers
{    
    public class DraftController : Controller
    {
        private List<Champion> _champions;
        private List<Draft> _drafts;

        public DraftController(List<Champion> champions, List<Draft> drafts)
        {
            _drafts = drafts;
            _champions = champions;
        }

        // List existing drafts
        [Route("")]
        public ActionResult Home()
        {
            return View("Home", _drafts);
        }

        // LolDraft.com/join?draft=1234
        [Route("join")]
        public ActionResult Join(Guid draftGuid)
        {
            var draft = _drafts.Find(d => d.CanView(draftGuid));
            if (draft == null)
            {
                // TODO: error
            }


            return View("Join", _champions); // Draft data will be loaded via ajax
        }
    }
}
