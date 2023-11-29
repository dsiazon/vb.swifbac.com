using vb.swifbac.com.Models;
using vb.swifbac.com.Repo;
using System.Web.Mvc;

namespace vb.swifbac.com.Controllers
{
    public class TeamRegisterController : Controller
    {
        //
        // GET: /Register/

        public ActionResult Index()
        {
            Registration reg = new Registration();
            return View(reg);
        }

        [HttpPost]
        public ActionResult Submit(Registration reg)
        {
            DataSource source = new DataSource();
            reg.Division = "VB-TEAM";
            source.SaveRegistration(reg);
            return View();
        }
    }
}
