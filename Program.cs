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
        NewEntries();
        DeserialiseAllUsers();

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

    // Adding new entries to the JSON list at runtime
    static void NewEntries()
    {
        PrintSectionHeader("Adding new entries to the JSON object");

        string path = DataPath("users.json");
        string json = File.ReadAllText(path);

        // Load the JSON array into a mutable JArray (dynamic JSON model)
        JArray usersArray = JArray.Parse(json);

        // Build two new entries as JObjects and append them
        var newUser1 = new JObject
        {
            ["Name"] = "Mambet Grozniy",
            ["Age"] = 22,
            ["City"] = "Miami"
        };

        var newUser2 = new JObject
        {
            ["Name"] = "Ishak Seryozni",
            ["Age"] = 33,
            ["City"] = "Berlin"
        };

        usersArray.Add(newUser1);
        usersArray.Add(newUser2);

        // Persist the updated array back to file (pretty-printed)
        string updatedJson = usersArray.ToString(Formatting.Indented);
        File.WriteAllText(path, updatedJson);

        Console.WriteLine($"Added 2 new users. users.json now contains {usersArray.Count} entries.");
        Console.WriteLine("Snippet of updated file:");
        Console.WriteLine(updatedJson);
    }

    // Deserialise ALL entries with a loop - console output

    static void DeserialiseAllUsers()
    {
        PrintSectionHeader("Deserialise ALL users and output to console");

        string path = DataPath("users.json");
        string json = File.ReadAllText(path);

        // Deserialise the whole array into a typed List<User>
        List<User> users = JsonConvert.DeserializeObject<List<User>>(json)!;

        Console.WriteLine($"Total users loaded: {users.Count}\n");

        // Loop through every user and print their info
        for (int i = 0; i < users.Count; i++)
        {
            Console.WriteLine($"── User #{i + 1} ──────────────────────");
            users[i].DisplayInfo();
        }
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
