using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ParseHTML
{
    class Program
    {
        static void Main(string[] args)
        {


            var gameList = LandOfBasketballParser.GetGames();
            //foreach (var game in gameList)
            //{
            //    Console.WriteLine(game.ToString());
            //}

            //Remaining schedule toughness (current stats needed)
            var teamStats = new TeamStats(gameList);
            Console.WriteLine(teamStats.WinningPercentage("Los Angeles Lakers"));
            
            foreach (var team in 
                gameList
                .Where(x => x.Finished==true)
                .Select(x => x.HomeTeam)                
                .Distinct())
            {
                Console.WriteLine("{0}: {1}",team,teamStats.WinningPercentage(team).ToString("0.##"));
            }

        }

    }
}
