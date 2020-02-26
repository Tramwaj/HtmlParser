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

        public Game(string awayTeam, string homeTeam, DateTime date, string awayScore, string homeScore) : this(awayTeam, homeTeam, date)
        {
            AwayScore = Convert.ToInt32(awayScore);
            HomeScore = Convert.ToInt32(homeScore);
            Finished = true;
        }

        public Game(string awayTeam, string homeTeam, DateTime date)
        {
            AwayTeam = awayTeam;
            HomeTeam = homeTeam;
            Date = date;
            Finished = false;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(AwayTeam).Append(" at ").Append(HomeTeam).Append(" - ");
            if (Finished) sb.Append(AwayScore).Append(" : ").Append(HomeScore);
            else sb.Append(" TO BE PLAYED");
            return sb.ToString();
        }
    }
}
