using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ParseHTML
{
    public class TeamStats
    {
        public string Name { get; set; }
        public List<Game> GamesPlayed { get; }
        public List<Game> GamesPlanned { get; }
        private List<string> eastTeams;
        private List<string> westTeams;
        public TeamStats(IEnumerable<Game> _gamesList)
        {
            GamesPlayed = _gamesList.Where(g => g.Finished == true)
                .ToList();
            GamesPlanned = _gamesList.Where(g => g.Finished == false)
                .ToList(); //Sacramento?
            eastTeams = new List<string> { "Boston Celtics", "Philadelphia 76ers", "Milwaukee Bucks", "Washington Wizards", "Cleveland Cavaliers", "Detroit Pistons", "Chicago Bulls", "Indiana Pacers", "Miami Heat", "Toronto Raptors", "Orlando Magic", "Brooklyn Nets", "Charlotte Hornets", "Detroit Pistons", "Atlanta Hawks" };
            westTeams = new List<string> { "Los Angeles Clippers", "Los Angeles Lakers", "San Antonio Spurs", "Denver Nuggets", "New Orleans Pelicans", "Memphis Grizzlies", "Golden State Warriors", "Oklahoma City Thunder", "Houston Rockets", "Minnesota Timberwolves", "Dallas Mavericks", "Utah Jazz", "Portland Trail Blazers", "Sacramento Kings", "Phoenix Suns" };
        }

        public double WinningPercentage(string teamName)
        {
            return GamesPlayed.Where(t =>
           (t.AwayTeam == teamName && t.AwayScore > t.HomeScore)
           ||
           (t.HomeTeam == teamName && t.HomeScore > t.AwayScore))
               .Count()*1.0
               /
               GamesPlayed.Where(t => t.AwayTeam == teamName || t.HomeTeam == teamName)
                .Count()*1.0;
        }
        public IEnumerable<Game> GamesPlayedByTeam(string teamName)
        {
            return GamesPlayed.Where(g => g.AwayTeam == teamName || g.HomeTeam == teamName);
        }
        public IEnumerable<Game> GamesPlannedByTeam(string teamName)
        {
            return GamesPlanned.Where(g => g.AwayTeam == teamName || g.HomeTeam == teamName);
        }
        public double ScheduleDifficulty(string teamName)
        {
            var futureOpponents = GamesPlannedByTeam(teamName).
                Select(t => t.AwayTeam == teamName ? t.HomeTeam : t.AwayTeam);
            return futureOpponents.Select(team => WinningPercentage(team)).Sum()
                /
                futureOpponents.Count();
        }
    }
}
