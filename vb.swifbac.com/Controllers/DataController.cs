using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using vb.swifbac.com.Repo;
using vb.swifbac.com.Models;

namespace vb.swifbac.com.Controllers
{
    public class DataController : ApiController
    {
        [HttpGet]
        public string GetTeamStandings(int id)
        {
            DataSource source = new DataSource();
            return Newtonsoft.Json.JsonConvert.SerializeObject(source.GetTeamStandings(id));
        }

        [HttpGet]
        public string GetCurrentSchedule(int id)
        {
            DataSource source = new DataSource();
            DateTime CurrentGameDate = DateTime.Parse(System.Configuration.ConfigurationManager.AppSettings["CurrentGameDate"]);

            UpComingGames games = new UpComingGames();
            games.Games = source.GetWeekSchedule(id, CurrentGameDate);
            games.CurrentDate = CurrentGameDate.ToShortDateString();
            games.ShowScores = (DateTime.Today >= CurrentGameDate);
            return Newtonsoft.Json.JsonConvert.SerializeObject(games);
        }

        [HttpGet]
        public List<Game> GetGamesByDateForEditing(DateTime weekDate)
        {
            DataSource source = new DataSource();
            return source.GetGamesByDateForEditing(weekDate);
        }
    }
}