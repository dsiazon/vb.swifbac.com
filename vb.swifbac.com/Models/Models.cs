using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace vb.swifbac.com.Models
{
    public class League
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Description { get; set; }

    }

    public class UpComingGames
    {
        public List<Game> Games { get; set; }
        public string CurrentDate { get; set; }
        public bool ShowScores { get; set; }
    }

    public class Team
    {
        public int Id { get; set; }
        public int LeagueId { get; set; }
        public string Name { get; set; }
        public int Win { get; set; }
        public int Loss { get; set; }
        public int TotalScore { get; set; }
        public int Differential { get; set; }
    }

    public class Game
    {
        public int Id { get; set; }
        public int LeagueId { get; set; }
        public DateTime GameDate { get; set; }
        public string Time { get; set; }
        public int DivisionId { get; set; }
        public string DivisionName { get; set; }
        public int CourtId { get; set; }
        public string CourtName { get; set; }
        public int HomeTeamId { get; set; }
        public string HomeTeamName { get; set; }
        public int HomeTeamScore { get; set; }
        public int VisitorTeamId { get; set; }
        public string VisitorTeamName { get; set; }
        public int VisitorTeamScore { get; set; }
        public bool IsFinal { get; set; }
        public int SortOrder { get; set; }
    }

    public class Division
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ScoreEntry
    {
        public int LeagueId { get; set; }
        public DateTime GameDate { get; set; }
        public List<Game> Games { get; set; }
    }

    public class ScheduleModel
    {
        public List<DateTime> GameDates { get; set; }
        public Dictionary<string, string> GameTimes { get; set; }
        public Dictionary<int, string> Divisions { get; set; }
        public Dictionary<int, string> Courts { get; set; }
        public List<Game> Games { get; set; }
        public Dictionary<int, string> Teams { get; set; }
        public DateTime TargetDate { get; set; }
        public int TargetWeek { get; set; }
        public bool IsEditable { get; set; }

    }

    public class Registration
    {     
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int Age { get; set; }
        public string Division { get; set; }
        public string ContactPhone { get; set; }
        public string EmailAddress { get; set; }
        public string Jerseysize { get; set; }

        public string Comments { get; set; }

        public IEnumerable<SelectListItem> Divisions
        {
            get
            {
                return
                    new List<SelectListItem> {
            new SelectListItem { Value = "VB-Ind", Text = "Individual" }
            //new SelectListItem { Value = "KIDS BEGINNERS", Text = "BEGINNER" },
            //new SelectListItem { Value = "KIDS SKILLED", Text = "SKILLED" },
            //new SelectListItem { Value = "KIDS NOTSURE", Text = "NOT SURE" }
            //new SelectListItem { Value = "Maharlika", Text = "Maharlika - born 1986 or earlier" },
            //new SelectListItem { Value = "All Filipino Open", Text = "All Fil. Open (no age limit)" },
            //new SelectListItem { Value = "Open", Text = "Open (no age limit)" },                      
        };       

    }


        }
        public IEnumerable<SelectListItem> Jerseysizes
        {
            get
            {
                return
                    new List<SelectListItem> {
            new SelectListItem { Value = "Small", Text = "Small" },
            new SelectListItem { Value = "Medium", Text = "Medium" },
            new SelectListItem { Value = "Large", Text = "Large" },
            new SelectListItem { Value = "XL", Text = "XL" },
            new SelectListItem { Value = "XXL", Text = "XXL" },            
        };


            }


        }
    }

    public class Roster
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int Age { get; set; }
        public string Division { get; set; }
        public string ContactPhone { get; set; }
        public string EmailAddress { get; set; }
        public string Jerseysize { get; set; }
        public IEnumerable<SelectListItem> Divisions
        {
            get
            {
                return
                   new List<SelectListItem> {
            new SelectListItem { Value = "LEGENDS", Text = "LEGENDS (55+)" },
            new SelectListItem { Value = "EXECUTIVES", Text = "EXECUTIVES (48+)" },
            new SelectListItem { Value = "MASTERS", Text = "MASTERS (40+)" },
            new SelectListItem { Value = "MAHARLIKA", Text = "MAHARLIKA (30+)" },
            new SelectListItem { Value = "ALL-FIL-OPEN", Text = "ALL FILIPINO OPEN (NO AGE LIMIT)" },
            new SelectListItem { Value = "6FTU", Text = "6FEET & UNDER" }
                                };

            }
        }
        public IEnumerable<SelectListItem> Jerseysizes
        {
            get
            {
                return
                    new List<SelectListItem> {
            new SelectListItem { Value = "Small", Text = "Small" },
            new SelectListItem { Value = "Medium", Text = "Medium" },
            new SelectListItem { Value = "Large", Text = "Large" },
            new SelectListItem { Value = "XL", Text = "XL" },
            new SelectListItem { Value = "XXL", Text = "XXL" },
        };


            }


        }
    }

}