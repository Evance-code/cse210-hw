using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class Entry
{
    public string Date { get; set; }
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Mood { get; set; }  // New field for tracking mood
    public string Location { get; set; }  // New field for tracking location

    public Entry(string date, string prompt, string response, string mood, string location)
    {
        Date = date;
        Prompt = prompt;
        Response = response;
        Mood = mood;
        Location = location;
    }

    public override string ToString()
    {
        return $"Date: {Date}\nPrompt: {Prompt}\nResponse: {Response}\nMood: {Mood}\nLocation: {Location}\n";
    }

    public string ToFileString()
    {
        return $"{Date}|{Prompt}|{Response}|{Mood}|{Location}";
    }
}

class Journal
{
    private List<Entry> entries = new List<Entry>();
    private List<string> prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };

    private Random random = new Random();  // Use a single random instance

    public void AddEntry()
    {
        string prompt = prompts[random.Next(prompts.Count)];
        Console.WriteLine(prompt);
        string response = Console.ReadLine();
        Console.Write("What was your mood today? ");
        string mood = Console.ReadLine();  // Capture mood
        Console.Write("Where were you today? ");
        string location = Console.ReadLine();  // Capture location
        string date = DateTime.Now.ToString("yyyy-MM-dd");
        entries.Add(new Entry(date, prompt, response, mood, location));
    }

    public void DisplayJournal()
    {
        foreach (var entry in entries)
        {
            Console.WriteLine(entry);
        }
    }

    public void SaveJournal(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (var entry in entries)
            {
                writer.WriteLine(entry.ToFileString());
            }
        }
    }

    public void LoadJournal(string filename)
    {
        if (File.Exists(filename))
        {
            entries.Clear();
            string[] lines = File.ReadAllLines(filename);
            foreach (var line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length == 5)
                {
                    entries.Add(new Entry(parts[0], parts[1], parts[2], parts[3], parts[4]));
                }
            }
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }

    public void ExportToJson(string filename)
    {
        string json = JsonSerializer.Serialize(entries, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filename, json);
        Console.WriteLine("Journal exported to JSON.");
    }

    public void ExportToCsv(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            writer.WriteLine("Date,Prompt,Response,Mood,Location");
            foreach (var entry in entries)
            {
                string date = entry.Date.Replace(",", "");
                string prompt = entry.Prompt.Replace(",", "").Replace("\"", "\"\"");
                string response = entry.Response.Replace(",", "").Replace("\"", "\"\"");
                string mood = entry.Mood.Replace(",", "").Replace("\"", "\"\"");
                string location = entry.Location.Replace(",", "").Replace("\"", "\"\"");
                writer.WriteLine($"\"{date}\",\"{prompt}\",\"{response}\",\"{mood}\",\"{location}\"");
            }
        }
        Console.WriteLine("Journal exported to CSV.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        while (true)
        {
            Console.WriteLine("\nJournal Menu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Export to JSON");
            Console.WriteLine("6. Export to CSV");
            Console.WriteLine("7. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    journal.AddEntry();
                    break;
                case "2":
                    journal.DisplayJournal();
                    break;
                case "3":
                    Console.Write("Enter filename to save: ");
                    string saveFilename = Console.ReadLine();
                    journal.SaveJournal(saveFilename);
                    break;
                case "4":
                    Console.Write("Enter filename to load: ");
                    string loadFilename = Console.ReadLine();
                    journal.LoadJournal(loadFilename);
                    break;
                case "5":
                    Console.Write("Enter filename to export as JSON: ");
                    string jsonFilename = Console.ReadLine();
                    journal.ExportToJson(jsonFilename);
                    break;
                case "6":
                    Console.Write("Enter filename to export as CSV: ");
                    string csvFilename = Console.ReadLine();
                    journal.ExportToCsv(csvFilename);
                    break;
                case "7":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
