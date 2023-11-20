using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericSiteCrawler.data
{
    public class Traverse
    {
        public static async Task TraverseTree(string url, int level, string absolutePath, string domain)
        {
            Console.WriteLine(new String('-', level) + url + "    LevelDepth=" + level);

            await FileUtils.WriteInFile(url, level, absolutePath);
            HashSet<string> links = new HashSet<string>();
            if (url.Contains(domain))
            {
                links = await HtmlLinksRetriever.GetHtmlPageLinks(url);
            }
            foreach (var link in links)
            {
                await TraverseTree(link, level + 1, absolutePath, domain);
            }
        }
    }
}
