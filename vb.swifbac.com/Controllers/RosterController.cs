using vb.swifbac.com.Models;
using vb.swifbac.com.Repo;
using System.Web.Mvc;

namespace vb.swifbac.com.Controllers
{
    public class RosterController : Controller
    {
        //
        // GET: /Roster/

        public ActionResult Index()
        {
            Roster roster = new Roster();
            return View(roster);
        }

        [HttpPost]
        public ActionResult Submit(Roster roster)
        {
            DataSource source = new DataSource();
            source.SaveRoster(roster);
            return View();
        }
    }
}
