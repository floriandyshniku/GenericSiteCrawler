using System;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

class Program
{
    static void Main(string[] args)
    {
        string jsonFilePath = "C:\\Users\\dyshn\\source\\repos\\GenericSiteCrawler\\ConsoleApp2\\Json\\Employee.json";

        if (File.Exists(jsonFilePath))
        {
            string jsonContent = File.ReadAllText(jsonFilePath);
            JObject jsonObject = JObject.Parse(jsonContent);
            IterateJsonObject(jsonObject);
        }
        else
        {
            Console.WriteLine("The JSON file does not exist.");
        }

        Console.ReadKey();
    }

    static void IterateJsonObject(JToken token, string path = "")
    {
        foreach (var child in token.Children())
        {
            if (child is JProperty prop)
            {
                string currentPath = string.IsNullOrEmpty(path) ? prop.Name : $"{path}.{prop.Name}";

                if (prop.Value is JArray array)
                {
                    Console.WriteLine($"Array: {currentPath}");
                    foreach (var element in array)
                    {
                        IterateJsonObject(element, currentPath);
                    }
                }
                else if (prop.Value is JObject)
                {
                    Console.WriteLine($"Object: {currentPath}");
                    IterateJsonObject(prop.Value, currentPath);
                }
                else if (prop.Value is JValue value)
                {
                    Console.WriteLine($"{currentPath}: {value}");
                }
            }
        }
    }
}
