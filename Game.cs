using System;
using System.Collections.Generic;
using System.Text;

namespace ParseHTML
{
    class Game
    {
        public string AwayTeam { get;}
        public string HomeTeam { get;}
        public DateTime Date { get;}
        public bool Finished { get;}
        public int AwayScore { get;}
        public int HomeScore { get;}

        public Game(string awayTeam, string homeTeam, DateTime date, int awayScore, int homeScore) : this(awayTeam, homeTeam, date)
        {
            AwayScore = awayScore;
            HomeScore = homeScore;
            Finished = true;
        }

        public Game(string awayTeam, string homeTeam, DateTime date)
        {
            AwayTeam = awayTeam;
            HomeTeam = homeTeam;
            Date = date;
            Finished = false;
        }
    }
}
