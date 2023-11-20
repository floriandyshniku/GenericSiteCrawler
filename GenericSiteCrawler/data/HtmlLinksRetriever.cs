using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GenericSiteCrawler.data
{
    public class HtmlLinksRetriever
    {
        public static HashSet<string> searchedLinks = new HashSet<string>();

        public static async Task<HashSet<string>> GetHtmlPageLinks(string url)
        {
            try
            {
                var links = new HashSet<string>();
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetStringAsync(url);

                    var htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(response);

                    var linkNodes = htmlDoc.DocumentNode.SelectNodes("//a[@href]");
                    if (linkNodes != null)
                    {
                         links = linkNodes
                        .Select(href => href.GetAttributeValue("href", string.Empty))
                        .Where(href => !href.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                        .Select(href => {
                            Uri baseUri = new Uri(url);
                            Uri absoluteUri = new Uri(baseUri, href);
                            return absoluteUri.AbsoluteUri;
                        }).Where(href => {
                            Uri hrefUri;
                            return Uri.TryCreate(href, UriKind.Absolute, out hrefUri) && !searchedLinks.Contains(href);
                        }).ToHashSet();
                        searchedLinks.UnionWith(links);
                        return links;
                    }
                    else{
                        return null;
                    }
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
