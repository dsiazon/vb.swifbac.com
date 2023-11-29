using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using vb.swifbac.com.Repo;
using vb.swifbac.com.Models;

namespace vb.swifbac.com.Controllers
{
    public class ScoresController : Controller
    {
        //
        // GET: /Scores/

        public ActionResult Index()
        {
            DataSource source = new DataSource();
            var scores = new ScoreEntry();
            scores.LeagueId = int.Parse(System.Configuration.ConfigurationManager.AppSettings["LeagueId"]);
            scores.GameDate =  DateTime.Parse(System.Configuration.ConfigurationManager.AppSettings["CurrentGameDate"]);
            scores.Games = source.GetWeekSchedule(0, scores.GameDate);
            return View(scores);
        }


        public ActionResult Update(ScoreEntry scores)
        {
            DataSource source = new DataSource();

            if (scores.Games != null && scores.Games.Any())
            {
                foreach (var game in scores.Games)
                {
                    source.UpdateGameScore(game);
                }

                source.UpdateStandings();
            }

            

            return View(scores);
        }

        //

    }
}
