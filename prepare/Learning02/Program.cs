using System;
using System.Collections.Generic;
using System.IO;

class Entry
{
    public string Prompt {get;}
    public string Response {get;}
    public DateTime Date {get;}

    public Entry(string prompt, string response)
    {
        Prompt = prompt;
        Response = response;
        Date = DateTime.Now;
    }
    public void Display()
    {
        Console.WriteLine($"Date: {Date.ToShortDateString()}");
        Console.WriteLine($"Prompt: {Prompt}");
        Console.WriteLine($"Response: {Response}\n");
    }
}
class Journal
{
    public List<Entry> Entries { get; } = new List<Entry>();

    public void WriteNewEntry()
    {
        string[] prompts = {
            "What did I learn from a difficult situation or challenge I faced today?",
            "What act of kindness did I witness or perform today?",
            "What was the funniest or most enjoyable moment of the day?",
            "What activity or hobby made me feel happiest today?",
            "What was the most important lesson I learned today?",
            "Who made me smile or inspired me today?",
            "What was my biggest source of stress or worry today and how did I deal with it?",
            "What aspect of nature surprised me or made me reflect today?",
            "What action can I take to make tomorrow better?"
        };

        Random random = new Random();
        int index = random.Next(prompts.Length);
        string prompt = prompts[index];

        Console.WriteLine(prompt);
        string response = Console.ReadLine();

        Entries.Add(new Entry(prompt, response));

        Console.WriteLine("Your entry has been saved.");
    }

    public void DisplayJournal()
    {
        if (Entries.Count == 0)
            Console.WriteLine("There are no entries in the journal.");
        else
            foreach (Entry entry in Entries)
                entry.Display();
    }

    public void SaveJournalToFile()
    {
        Console.WriteLine("Enter the filename where you want to save the journal:");
        string filename = Console.ReadLine();

        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (Entry entry in Entries)
            {
                writer.WriteLine(entry.Date);
                writer.WriteLine(entry.Prompt);
                writer.WriteLine(entry.Response + "\n");
            }
        }

        Console.WriteLine($"The journal has been saved to the file {filename}.");
    }

    public void LoadJournalFromFile()
    {
        Console.WriteLine("Enter the filename from where you want to load the journal:");
        string filename = Console.ReadLine();

        if (File.Exists(filename))
        {
            Entries.Clear();

            using (StreamReader reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    DateTime date = DateTime.Parse(reader.ReadLine());
                    string prompt = reader.ReadLine();
                    string response = reader.ReadLine();

                    Entries.Add(new Entry(prompt, response));
                    reader.ReadLine();
                }
            }

            Console.WriteLine($"The journal has been loaded from the file {filename}.");
        }
        else
            Console.WriteLine($"The file {filename} does not exist.");
    }
}

class Program
{
    static Journal journal = new Journal();

    static int DisplayMenu()
    {
        Console.WriteLine("Welcome! Please choose an option:");
        Console.WriteLine("1) Write a new entry");
        Console.WriteLine("2) Display the journal");
        Console.WriteLine("3) Save the journal in a file");
        Console.WriteLine("4) Load the journal from a file");
        Console.WriteLine("5) Exit");

        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 5)
            Console.WriteLine("Invalid option. Try again.");

        return choice;
    }

    static void Main(string[] args)
    {
        int choice;

        do
        {
            choice = DisplayMenu();

            switch (choice)
            {
                case 1:
                    journal.WriteNewEntry();
                    break;
                case 2:
                    journal.DisplayJournal();
                    break;
                case 3:
                    journal.SaveJournalToFile();
                    break;
                case 4:
                    journal.LoadJournalFromFile();
                    break;
                case 5:
                    Console.WriteLine("Thank you! for using the journal program.");
                    break;
            }

            Console.WriteLine();

        } while (choice != 5);
    }
}
