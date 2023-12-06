using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vb.swifbac.com.Models;
using db = vb.swifbac.com.Database;


namespace vb.swifbac.com.Repo
{
    public class DataSource
    {
        int _leagueId = int.Parse(System.Configuration.ConfigurationManager.AppSettings["LeagueId"]);
       
        public List<Team> GetTeamStandings(int divisionId)
        {
            List<Team> teams = new List<Team>();
            var db = new db.swifbacdbDataContext();
            var dbTeams  = db.GetTeamStandings(_leagueId, divisionId).ToList();

            if (dbTeams.Any())
            {
                teams = dbTeams.Select(x => new Team {Id = x.Id, Name = x.Name, Win = x.Win.Value, Loss = x.Loss.Value, TotalScore = x.TotalScore.Value, Differential = x.Diff.Value}).ToList(); 
            }

            return teams;
        }

        public List<Team> GetAllTeams()
        {
            List<Team> teams = new List<Team>();
            var db = new db.swifbacdbDataContext();
            var dbTeams = db.Teams.Where(c=> c.LeagueId == _leagueId && c.IsTBD == false);

            if (dbTeams.Any())
            {
                teams = dbTeams.Select(x => new Team { Id = x.Id, Name = x.Name}).ToList();
            }

            return teams;
        }

        public void UpdateStandings()
        {
            var db = new db.swifbacdbDataContext();
            db.UpdateStandings(_leagueId);
        }

        public void SaveRegistration(Registration reg)
        {
            var db = new db.swifbacdbDataContext();
            db.SaveRegistration(reg.LastName, reg.FirstName, reg.Age, reg.Division, reg.ContactPhone, reg.EmailAddress, reg.Jerseysize, reg.Comments);
        }


        public void SaveRoster(Roster roster)
        {
            var db = new db.swifbacdbDataContext();
            db.SaveRegistration(roster.LastName, roster.FirstName, roster.Age, roster.Division, roster.ContactPhone, roster.EmailAddress, roster.Jerseysize, "");
        }


        public List<Game> GetWeekSchedule(int divisionId, DateTime? weekDate)
        {
      
            List<Game> games = new List<Game>();
            var db = new db.swifbacdbDataContext();

            if (weekDate == null)
            {
                weekDate = DateTime.Parse(System.Configuration.ConfigurationManager.AppSettings["CurrentGameDate"]);
            }
            
            var dbGames = db.GetGameSchedules(_leagueId, weekDate).ToList();
            
            if (dbGames.Any())
            {
                games = dbGames.Select(x => new Game {Id =x.Id, DivisionId= x.DivisionId.Value, LeagueId = x.LeagueId, 
                    HomeTeamId = x.HomeTeamId.Value, VisitorTeamId = x.VisitorTeamId.Value,                      
                    GameDate = weekDate.Value, CourtName = x.CourtName, DivisionName = x.DivisionName, 
                    CourtId = x.CourtId.Value, SortOrder = x.SortOrder.Value,
                    HomeTeamName = x.HomeTeamName, HomeTeamScore = x.HomeTeamScore, Time = x.GameTime, 
                    VisitorTeamName = x.VisitorTeamName, VisitorTeamScore = x.VisitorTeamScore, IsFinal = x.IsFinal.Value }).ToList();
            }

            if (divisionId > 0)
            {
                return games.Where(x => x.DivisionId == divisionId).ToList();
            }
            else
            {
                return games.ToList();
            }           

        }

        public List<DateTime> GetGameDates()
        {
            List<DateTime> dates = new List<DateTime>();
            DateTime StartDate = DateTime.Parse(System.Configuration.ConfigurationManager.AppSettings["StartDate"]);            
            dates.Add(StartDate);
            for (int i = 1; i <= 15; i++)
            {
                dates.Add(StartDate.AddDays(7 * i));
            }

            return dates;
        }

        public bool UpdateGameScore(Game game)
        {
            var success = false;

            try {

                var db = new db.swifbacdbDataContext();
                db.UpdateGame(_leagueId, game.Id, game.HomeTeamScore, game.VisitorTeamScore, game.IsFinal);
                success = true;
            }
            catch {
                success = false;
            }

            return success;
        }

        public bool UpsertGame(Game game)
        {
            var success = false;

            try
            {

                if (game.Id == 0)
                {

                    db.Game gm = new db.Game
                    {
                        DivisionId = game.DivisionId,
                        CourtId = game.CourtId,
                        LeagueId = game.LeagueId,
                        SortOrder = game.SortOrder,
                        Time = game.Time,
                        IsFinal = game.IsFinal,
                        HomeTeamId = game.HomeTeamId,
                        VisitorTeamId = game.VisitorTeamId,
                        GameDate = game.GameDate
                    };

                    var db = new db.swifbacdbDataContext();
                    db.Games.InsertOnSubmit(gm);
                    db.SubmitChanges();
                    success = true;
                }
                else
                {
                    var db = new db.swifbacdbDataContext();
                    var gm = db.Games.FirstOrDefault(x => x.Id == game.Id);

                    if (gm != null)
                    {
                        gm.LeagueId = game.LeagueId;
                        gm.GameDate = game.GameDate;
                        gm.Time = game.Time;
                        gm.DivisionId = game.DivisionId;
                        gm.CourtId = game.CourtId;
                        gm.HomeTeamId = game.HomeTeamId;
                        gm.HomeTeamScore = game.HomeTeamScore;
                        gm.VisitorTeamId = game.VisitorTeamId;
                        gm.VisitorTeamScore = game.VisitorTeamScore;
                        gm.IsFinal = game.IsFinal;
                        gm.SortOrder = game.SortOrder;

                        db.SubmitChanges();
                        success = true;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                success = false;
            }

            return success;
        }

        public bool DeleteGame(Game game)
        {
            var success = false;

            try
            {
                var db = new db.swifbacdbDataContext();
                var gm = db.Games.FirstOrDefault(x => x.Id == game.Id);

                if (gm != null)
                {
                    db.Games.DeleteOnSubmit(gm);
                    db.SubmitChanges();
                    success = true;
                }

            }
            catch (Exception ex)
            {
                success = false;
            }
            return success;
        }
        public Dictionary<string, string> GetGameTimes()
        {
            Dictionary<string, string> times = new Dictionary<string, string>();
            times.Add("8:00AM","8:00AM");
            times.Add("8:30AM", "8:30AM");
            times.Add("9:00AM", "9:00AM");
            times.Add("9:30AM", "9:30AM");
            times.Add("10:00AM", "10:00AM");
            times.Add("10:30AM", "10:30AM");
            times.Add("11:00AM", "11:00AM");
            times.Add("11:30AM", "11:30AM");
            times.Add("12:00PM", "12:00PM");
            times.Add("12:30PM", "12:30PM");
            times.Add("1:00PM", "1:00PM");
            times.Add("1:30PM", "1:30PM");
            times.Add("2:00PM", "2:00PM");
            times.Add("2:30PM", "2:30PM");
            times.Add("3:00PM", "3:00PM");
            times.Add("3:30PM", "3:30PM");
            times.Add("4:00PM", "4:00PM");
            times.Add("4:30PM", "4:30PM");
            times.Add("5:00PM", "5:00PM");
            times.Add("5:30PM", "5:30PM");
            times.Add("6:00PM", "6:00PM");
            times.Add("6:30PM", "6:30PM");
            times.Add("7:00PM", "7:00PM");
            times.Add("7:30PM", "7:30PM");
            times.Add("8:00PM", "8:00PM");
            times.Add("8:30PM", "8:30PM");

            return times;
        }

        public List<Game> GetGamesByDateForEditing(DateTime weekDate)
        {           
            var dbGames = GetWeekSchedule(0, weekDate);
            List<Game> games = new List<Game>();
            var cnt = dbGames.Count();

            if (cnt < 20)
            {
                int addt = 20 - cnt;

                for (int i = 0; i <= addt; i++)
                {
                    games.Add(new Game {GameDate = weekDate, LeagueId = _leagueId, Id = 0 });
                }
            }
            
            return games;
        }
    }
}