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
            WebClient webClient = new WebClient();
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
            //foreach (var item in table)
            //{
            //    foreach (var item2 in item)
            //    {
            //        var temp = item2
            //            .Trim().Split("\t")
            //            .Where(s => !string.IsNullOrWhiteSpace(s));
            //        foreach (var bub in temp)
            //        {
            //            Console.WriteLine("item:" + bub.Replace("@ ", ""));
            //        }
            //        Console.WriteLine("Pauza");
            //    }
            //    //Console.WriteLine("GRUBA PAUZA");
            //}
            var gameList = new List<Game>();
            foreach (var list1 in table)
            {
                DateTime currentDate=DateTime.Now;
                foreach (var list2 in list1)
                {
                    var gameData = list2
                        .Trim().Split("\t")
                        .Where(s => !string.IsNullOrWhiteSpace(s))
                        .ToList();
                    if (gameData.Count() == 1)
                    {
                        currentDate = DateTime.Parse(gameData[0]);
                        //Console.WriteLine(currentDate.ToString());
                    }
                    if (gameData.Count() > 5) 
                    {
                        var homeTown = gameData.Last().Replace("at ", "");
                        var (homeTeam, awayTeam) = gameData[0].Contains(homeTown) ?
                            (gameData[0], gameData[3]) : (gameData[3], gameData[0]);

                        gameList.Add(new Game(awayTeam,homeTeam,currentDate,gameData[1],gameData[4]));
                    }
                    if (gameData.Count == 3)
                    {
                        gameList.Add(new Game(gameData[0], gameData[1], currentDate));
                    }
                }
            }
            foreach (var game in gameList)
            {
                Console.WriteLine(game.ToString());
            }
        }

    }
}
