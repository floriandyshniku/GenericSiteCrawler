using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericSiteCrawler.data
{
    public class FileUtils
    {
        public static async Task WriteInFile(string url, int level, string path)
        {
            using (StreamWriter writer = File.AppendText(path))
            {
                string str = new String('-', level);
                await writer.WriteLineAsync($"{str}{url}");
            }
        }

        public static (string, string) GetPath( string domain)
        {

            string currentDirectory = Directory.GetCurrentDirectory();
           var  path = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\Data.txt"));
            Console.WriteLine(path);
            string fulllink = "https://" + domain;
            return (fulllink, path);
        }
    }
}
