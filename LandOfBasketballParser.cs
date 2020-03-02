using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;

namespace ParseHTML
{
    class LandOfBasketballParser
    {
        public static List<Game> GetGames()
        {
            using WebClient webClient = new WebClient();
            string page = webClient.DownloadString("https://www.landofbasketball.com/results/2019_2020_scores_full.htm");

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(page);

            List<List<string>> table = doc.DocumentNode.SelectSingleNode("//table[@class='color-alt max-1']")
                        .Descendants("tr")
                        .Skip(1)
                        .Select(tr => tr.Elements("td")
                        .Select(td => td.InnerText.Trim().Replace(System.Environment.NewLine, ""))
                        .ToList())
                        .ToList();
            table = table.Skip(1).ToList();
            var gameList = new List<Game>();
            foreach (var list1 in table)
            {
                DateTime currentDate = DateTime.Now;
                foreach (var list2 in list1)
                {
                    var gameData = list2
                        .Trim().Split("\t")
                        .Where(s => !string.IsNullOrWhiteSpace(s))
                        .ToList();
                    if (gameData.Count() == 1)
                    {
                        currentDate = DateTime.Parse(gameData[0]);
                    }
                    if (gameData.Count() > 5)
                    {
                        var homeTown = gameData.Last().Replace("at ", ""); // Last item in a gameData is "at homeTown"
                        string homeTeam, awayTeam;
                        string homeScore, awayScore;
                        if (gameData[0].Contains(homeTown))
                        {
                            (homeTeam, awayTeam) = (gameData[0], gameData[3]);
                            (homeScore, awayScore) = (gameData[1], gameData[4]);
                        }
                        else
                        {
                            (homeTeam, awayTeam) = (gameData[3], gameData[0]);
                            (homeScore, awayScore) = (gameData[4], gameData[1]);
                        }
                        gameList.Add(new Game(awayTeam, homeTeam, currentDate, awayScore,homeScore));
                    }
                    if (gameData.Count == 3)
                    {
                        gameList.Add(new Game(gameData[0], gameData[1].Replace("@", "").Trim(), currentDate));
                    }
                }
            }
            return gameList;
        }
        // Development purposes
        // A method to test the input got from Parser 
        // (left in case of HTML changes)
        public static void DevelopmentGetGameObjects()
        {
            using WebClient webClient = new WebClient();
            string page = webClient.DownloadString("https://www.landofbasketball.com/results/2019_2020_scores_full.htm");

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(page);

            List<List<string>> table = doc.DocumentNode.SelectSingleNode("//table[@class='color-alt max-1']")
                        .Descendants("tr")
                        .Skip(1)
                        .Select(tr => tr.Elements("td")
                        .Select(td => td.InnerText.Trim().Replace(System.Environment.NewLine, ""))
                        .ToList())
                        .ToList();
            table = table.Skip(1).ToList();
            foreach (var item in table)
            {
                foreach (var item2 in item)
                {
                    var temp = item2
                        .Trim().Split("\t")
                        .Where(s => !string.IsNullOrWhiteSpace(s));
                    foreach (var bub in temp)
                    {
                        Console.WriteLine("item:" + bub.Replace("@ ", ""));
                    }
                    Console.WriteLine("Pauza");
                }
                //Console.WriteLine("GRUBA PAUZA");
            }
        }
    }
}
