using GenericSiteCrawler.data;
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
       

        static async Task Main(string[] args)
        {
           
            Console.WriteLine("Please enter domain (ex:www.w3schools.com/): ");
            string domain = Console.ReadLine();
            (string, string) result = FileUtils.GetPath(  domain);
            await Traverse.TraverseTree(result.Item1, 0, result.Item2, domain);
            Console.ReadLine();
        }
    }
}
