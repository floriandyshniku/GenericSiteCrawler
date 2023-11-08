using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Program
    {
        public static HashSet<string> searchedLinks = new HashSet<string>();

        public static async Task<HashSet<string>> GetHtmlPageLinks(string url)
        {
            var links = new HashSet<string>();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetStringAsync(url);

                    var htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(response);

                    var linkNodes = htmlDoc.DocumentNode.SelectNodes("//a[@href]");
                    if (linkNodes != null)
                    {
                        foreach (var linkNode in linkNodes)
                        {
                            string href = linkNode.GetAttributeValue("href", string.Empty);
                            
                            if (!searchedLinks.Contains(href))
                            {
                                links.Add(href); searchedLinks.Add(href);
                            }
                            
                        }

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message, "u ekzekutua");
            }

            return links;
        }

        public static async Task TraverseTree(string url, int level)
        {

            Console.WriteLine(new String('-', level) + url + " Level=" + level);

            var children = await GetHtmlPageLinks(url);

            foreach (var child in children)
            {
                await TraverseTree(child, level + 1);
            }
        }

        static async Task Main(string[] args)
        {
            string fullLink = "http://127.0.0.1:5500/";
            await TraverseTree(fullLink, 0);

            Console.ReadLine();
        }
    }
}
