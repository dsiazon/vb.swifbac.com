using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vb.swifbac.com.Repo;
using vb.swifbac.com.Models;
using db = vb.swifbac.com.Database;

namespace vb.swifbac.com.Controllers
{
    public class ScheduleController : Controller
    {
        //
        // GET: /Schedule/        
        public ActionResult Create(int week)
        {
            ScheduleModel model = new ScheduleModel();
            var db = new db.swifbacdbDataContext();
            DataSource source = new DataSource();

            DateTime startDate = DateTime.Parse(System.Configuration.ConfigurationManager.AppSettings["StartDate"]);
            DateTime targetDate = startDate.AddDays(7 * week);
            model.Courts = db.GetAllCourts().OrderBy(x => x.Name).ToDictionary(k => k.Id, i => i.Name);
            model.Divisions = db.GetAllDivisions().ToDictionary(k => k.Id, i => i.Name);
            model.GameTimes = source.GetGameTimes();
            model.GameDates = source.GetGameDates();
            model.Teams = source.GetAllTeams().OrderBy(x=>x.Name).ToDictionary(x => x.Id, x => x.Name);
            var currentGames = source.GetWeekSchedule(0, targetDate);
            model.IsEditable = !currentGames.Any(x=> x.IsFinal);
            List<Game> games = new List<Game>();
            var leagueId = int.Parse(System.Configuration.ConfigurationManager.AppSettings["LeagueId"]);

            for (int i = 1; i <= 20; i++)
            {
                if (currentGames.Any(x=> x.SortOrder == i))
                {
                    var gm = currentGames.First(x => x.SortOrder == i);
                    games.Add(new Game { Id = gm.Id, LeagueId = gm.LeagueId, GameDate = gm.GameDate, Time = gm.Time,
                                            DivisionId = gm.DivisionId, DivisionName = gm.DivisionName, CourtId = gm.CourtId, CourtName = gm.CourtName, HomeTeamName = gm.HomeTeamName, VisitorTeamName = gm.VisitorTeamName,  HomeTeamId = gm.HomeTeamId,
                                            HomeTeamScore = gm.HomeTeamScore, VisitorTeamId = gm.VisitorTeamId, VisitorTeamScore = gm.VisitorTeamScore,
                                            IsFinal = gm.IsFinal, SortOrder = gm.SortOrder});
                }
                else
                {
                    games.Add(new Game { Id = 0, LeagueId = leagueId, GameDate = targetDate, IsFinal = false, SortOrder = i});
                }
            }



            model.Games = games;
            ViewBag.GameTimes = model.GameTimes;
            model.TargetDate = targetDate;
            model.TargetWeek = week;
            return View(model);
        }

        public ActionResult Update(ScheduleModel sched)
        {
            DataSource source = new DataSource();

            if (sched.Games != null && sched.Games.Any())
            {
                foreach (var game in sched.Games)
                {
                    if (
                        game.DivisionId > 0 &&
                        game.LeagueId > 0 &&
                        game.CourtId > 0 &&
                        game.HomeTeamId > 0 &&
                        game.VisitorTeamId > 0 &&
                        game.SortOrder > 0 &&
                        !string.IsNullOrWhiteSpace(game.Time) &&
                        game.GameDate > DateTime.MinValue
                    )
                    {
                        source.UpsertGame(game);
                    }
                    else if ((game.Id > 0) && (
                                game.DivisionId == 0 ||
                                game.LeagueId == 0 ||
                                game.CourtId == 0 ||
                                game.HomeTeamId == 0 ||
                                game.VisitorTeamId == 0 ||
                                game.SortOrder == 0 ||
                                string.IsNullOrWhiteSpace(game.Time)))
                    {

                        source.DeleteGame(game);

                    }
                }
            }

            ViewBag.TargetWeek = sched.TargetWeek;

            return View();
        }
    }
}
