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
        public static string domain;

        public static async Task<HashSet<string>> GetHtmlPageLinks(string url)
        {
            var links = new HashSet<string>();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetStringAsync(url);

                    var htmlDoc = new HtmlDocument();
                    // get html page
                    htmlDoc.LoadHtml(response);
                    //get all a tag
                    var linkNodes = htmlDoc.DocumentNode.SelectNodes("//a[@href]");
                    if (linkNodes != null)
                    {
                        foreach (var linkNode in linkNodes)
                        {
                            string href = linkNode.GetAttributeValue("href", string.Empty);
                            if (!href.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                            {
                                Uri baseUri = new Uri(url);
                                Uri absoluteUri = new Uri(baseUri, href);
                                href = absoluteUri.AbsoluteUri;
                            }
                            Uri hrefUri;
                            if (Uri.TryCreate(href, UriKind.Absolute, out hrefUri))
                            {
                                    if (!searchedLinks.Contains(href))
                                    {
                                        links.Add(href);
                                        searchedLinks.Add(href);
                                    }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return links;
        }

        public static async Task TraverseTree(string url, int level, string  absolutePath)
        {
            
           Console.WriteLine(new String('-', level) + url + " Level=" + level);


            await WriteInFile(url, level, absolutePath);
            HashSet<string> links = new HashSet<string>();
            if (url.Contains(domain))
            {
                 links = await GetHtmlPageLinks(url);
            }
            
            
            foreach (var link in links)
            {
                 await TraverseTree(link, level + 1, absolutePath);
            }
        }

        public static async Task WriteInFile(string url, int level, string path)
        {
            

            using (StreamWriter writer = File.AppendText(path))
            {
                string str = new String('-', level);
                await writer.WriteLineAsync($"{str}{url}");
            }

        }

        static async Task Main(string[] args)
        {
            domain = "www.w3schools.com/";
            string currentDirectory = Directory.GetCurrentDirectory();
            string path = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\Data.txt"));
            Console.WriteLine(path);
            string fulllink = "https://" + domain;
            await TraverseTree(fulllink, 0, path);

            Console.ReadLine();
        }
    }
}
