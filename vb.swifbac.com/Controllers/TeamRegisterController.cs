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
            TeamRegistration reg = new TeamRegistration();
            return View(reg);
        }

        [HttpPost]
        public ActionResult Submit(TeamRegistration teamReg)
        {
            DataSource source = new DataSource();
            string listPlayers(string player)
            {
                string playerName = "";
                if (player != null && player != "teamReg.Player15Name")
                {
                    playerName = player + ", ";
                }
                else if(player == "teamReg.Player15Name")
                {
                    playerName = player;
                }
                else
                {
                    playerName = "";
                }

                return playerName;
            }
            teamReg.Comments = listPlayers(teamReg.Player1Name) + listPlayers(teamReg.Player2Name) + listPlayers(teamReg.Player3Name) + listPlayers(teamReg.Player4Name) + listPlayers(teamReg.Player5Name) + listPlayers(teamReg.Player6Name) + listPlayers(teamReg.Player7Name) + listPlayers(teamReg.Player8Name) + listPlayers(teamReg.Player9Name) + listPlayers(teamReg.Player10Name) + listPlayers(teamReg.Player11Name) + listPlayers(teamReg.Player12Name) + listPlayers(teamReg.Player13Name) + listPlayers(teamReg.Player14Name) + teamReg.Player15Name;
            Registration reg = new Registration();
            reg.FirstName = teamReg.FirstName;
            reg.LastName = teamReg.LastName;
            reg.ContactPhone = teamReg.ContactPhone;
            reg.EmailAddress = teamReg.Email;
            reg.Comments = teamReg.Comments;
            source.SaveRegistration(reg);
            return View();
        }
    }
}
