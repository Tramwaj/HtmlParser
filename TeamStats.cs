using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ParseHTML
{
    public class TeamStats
    {
        List<Game> gamesList;
        List<Game> gamesPlayed;
        List<Game> gamesToBePlayed;
        public TeamStats(List<Game> _gamesList)
        {
            gamesList = _gamesList;
            gamesPlayed = _gamesList.Where(g => g.Finished == true).ToList();
            gamesToBePlayed = _gamesList.Where(g => g.Finished == false).ToList();
        }
        public double WinningPercentage(string teamName)
        {
            //int played = gamesList.Where()
            double won = gamesPlayed.Where(t =>
           (t.AwayTeam == teamName && t.AwayScore > t.HomeScore)
           ||
           (t.HomeTeam == teamName && t.HomeScore > t.AwayScore))
               .Count();

            double played =
            gamesPlayed.Where(t => t.AwayTeam == teamName || t.HomeTeam == teamName)
            .Count();
            //Console.WriteLine("Won: {0}, Played: {1}", won, played);
            return won / played;
        }
        //public double 
    }
}
