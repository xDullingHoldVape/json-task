using Newtonsoft.Json;
using Newtonsoft.Json.Linq;         // for JArray
using System;
using System.Collections.Generic;
using System.IO;
using JsonExample.Models;

class Program
{
    // Resolve paths relative to the executable so the app works from any working directory
    private static readonly string BaseDir =
        Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
        ?? Directory.GetCurrentDirectory();

    private static string DataPath(string filename) =>
        Path.Combine(BaseDir, "data", filename);

    static void Main(string[] args)
    {

        ReadSingleUser();

    }

    // Reading every user in manually created JSON File
    static void ReadSingleUser()
    {
        PrintSectionHeader("Reads a single user from users.json");

        string path = DataPath("users.json");

        // Read raw JSON text from file
        string json = File.ReadAllText(path);

        // users.json is an array; take only the first element
        var allUsers = JsonConvert.DeserializeObject<List<User>>(json)!;
        User firstUser = allUsers[0];

        Console.WriteLine("Deserialised first user from users.json:");
        firstUser.DisplayInfo();
    }

    


    // To make headers more attractive(design)

    static void PrintHeader(string title)
    {
        Console.WriteLine(new string('═', 60));
        Console.WriteLine($"  {title}");
        Console.WriteLine(new string('═', 60));
        Console.WriteLine();
    }

    static void PrintSectionHeader(string title)
    {
        Console.WriteLine();
        Console.WriteLine(new string('─', 60));
        Console.WriteLine($"  {title}");
        Console.WriteLine(new string('─', 60));
    }
}
