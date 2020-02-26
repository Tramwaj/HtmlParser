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
                        .Select(td => td.InnerText.Trim().Replace(System.Environment.NewLine,""))
                        .ToList())
                        .ToList();
            foreach (var item in table)
            {
                foreach (var item2 in item)
                {
                    var temp = item2.Trim().Split("\t");
                    foreach (var bub in temp)
                    {
                        if (!string.IsNullOrWhiteSpace(bub))
                        Console.WriteLine("item:" + bub.Replace("@ ",""));
                    }
                    Console.WriteLine("Pauza");
                    //Console.WriteLine("item: "+item2);
                }
                Console.WriteLine();
            }
            //foreach (var list1 in table)
            //{
            //    foreach (var list2 in list1)
            //    {
            //        var item = list2.Trim().Split("\t").Where(;
            //        item=item.Where(i=>!string.IsNullOrWhiteSpace(i))
            //    }
            //}
            var s = DateTime.Parse("Feb 1, 2020");
            Console.WriteLine(s.ToString());
        }
        
    }
}
