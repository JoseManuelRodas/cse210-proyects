using System;
using System.Collections.Generic;
using System.IO;

abstract class Goal
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Points { get; set; }
    public bool Completed { get; set; }
    public abstract void RecordEvent();
}

class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int points)
    {
        Name = name;
        Description = description;
        Points = points;
        Completed = false;
    }

    public override void RecordEvent()
    {
        Completed = true;
    }
}

class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
    {
        Name = name;
        Description = description;
        Points = points;
        Completed = false;
    }

    public override void RecordEvent()
    {
        Completed = true;
    }
}

class ChecklistGoal : Goal
{
    public int Times { get; set; }
    public int Bonus { get; set; }
    private int count;

    public ChecklistGoal(string name, string description, int points, int times, int bonus)
    {
        Name = name;
        Description = description;
        Points = points;
        Times = times;
        Bonus = bonus;
        count = 0;
        Completed = false;
    }

    public override void RecordEvent()
    {
        count++;
        if (count >= Times)
        {
            Completed = true;
            Points += Bonus; 
        }
    }
}

class User
{
    public string Name { get; set; }
    public int TotalPoints { get; set; }
    public List<Goal> Goals { get; set; }

    public User(string name)
    {
        Name = name;
        TotalPoints = 0;
        Goals = new List<Goal>();
    }

    public void Display()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Total Points: {TotalPoints}");
        Console.WriteLine("Goals:");
        foreach (var goal in Goals)
        {
            Console.WriteLine($"[{(goal.Completed ? "X" : " ")}] {goal.Name} - Points: {goal.Points} - {goal.Description}");
        }
    }

    public void RecordEvent()
    {
        Console.WriteLine("----Did you complete a goal? Congratulations, Record it! ----");
        Console.WriteLine("Select a goal to mark as completed:");
        for (int i = 0; i < Goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. [{(Goals[i].Completed ? "X" : " ")}] {Goals[i].Name} - Points: {Goals[i].Points} - {Goals[i].Description}");
        }

        Console.Write("Enter the number of the goal: ");
        if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= Goals.Count)
        {
            Goals[choice - 1].RecordEvent();
            TotalPoints += Goals[choice - 1].Points; 
            Console.WriteLine("Event recorded successfully!");
        }
        else
        {
            Console.WriteLine("Invalid choice! No event recorded.");
        }
    }

    public void SaveGoals(string filename)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine($"{Name},{TotalPoints}");
                foreach (var goal in Goals)
                {
                    string type = goal.GetType().Name;
                    writer.WriteLine($"{type},{goal.Name},{goal.Description},{goal.Points},{goal.Completed}");
                }
            }
            Console.WriteLine("Goals saved successfully!");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error saving goals: {e.Message}");
        }
    }

    public void LoadGoals(string filename)
    {
        try
        {
            Goals.Clear();
            using (StreamReader reader = new StreamReader(filename))
            {
                string[] userLine = reader.ReadLine().Split(',');
                Name = userLine[0];
                TotalPoints = int.Parse(userLine[1]);
                while (!reader.EndOfStream)
                {
                    string[] goalLine = reader.ReadLine().Split(',');
                    string type = goalLine[0];
                    string name = goalLine[1];
                    string description = goalLine[2];
                    int points = int.Parse(goalLine[3]);
                    bool completed = bool.Parse(goalLine[4]);

                    Goal goal;
                    switch (type)
                    {
                        case nameof(SimpleGoal):
                            goal = new SimpleGoal(name, description, points);
                            break;
                        case nameof(EternalGoal):
                            goal = new EternalGoal(name, description, points);
                            break;
                        case nameof(ChecklistGoal):
                            int times = int.Parse(goalLine[5]);
                            int bonus = int.Parse(goalLine[6]);
                            goal = new ChecklistGoal(name, description, points, times, bonus);
                            break;
                        default:
                            throw new Exception("Invalid goal type");
                    }
                    goal.Completed = completed;
                    Goals.Add(goal);
                }
            }
            Console.WriteLine("Goals loaded successfully!");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error loading goals: {e.Message}");
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        User user = new User("John Doe");
        DisplayMainMenu(user);
    }

    static void DisplayMainMenu(User user)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"---- Eternal Quest Program ----");
            Console.WriteLine($"Total Points: {user.TotalPoints}");
            Console.WriteLine("1. Create a new Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    CreateGoal(user);
                    break;
                case "2":
                    Console.Clear();
                    user.Display();
                    Console.WriteLine("Press Enter to continue");
                    Console.ReadLine();
                    break;
                case "3":
                    Console.Clear();
                    Console.Write("Enter the filename to save the goals: ");
                    string saveFile = Console.ReadLine();
                    user.SaveGoals(saveFile);
                    Console.WriteLine("Press Enter to continue");
                    Console.ReadLine();
                    break;
                case "4":
                    Console.Clear();
                    Console.Write("Enter the filename to load the goals: ");
                    string loadFile = Console.ReadLine();
                    user.LoadGoals(loadFile);
                    Console.WriteLine("Press Enter to continue");
                    Console.ReadLine();
                    break;
                case "5":
                    Console.Clear();
                    user.RecordEvent();
                    Console.WriteLine("Press Enter to continue");
                    Console.ReadLine();
                    break;
                case "6":
                    Console.Clear();
                    Console.WriteLine("Thank you for using the Eternal Quest Program. Goodbye!");
                    return;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid choice. Please try again.");
                    Console.WriteLine("Press Enter to continue");
                    Console.ReadLine();
                    break;
            }
        }
    }

    static void CreateGoal(User user)
    {
        Console.WriteLine("---- Create a new Goal ----");
        Console.WriteLine("Select the type of the goal:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("Enter your choice: ");
        if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice < 4)
        {
            Console.Write("Enter the name of the goal: ");
            string name = Console.ReadLine();
            Console.Write("Enter the description of the goal: ");
            string description = Console.ReadLine();
            Console.Write("Enter the points of the goal: ");
            if (int.TryParse(Console.ReadLine(), out int points) && points > 0)
            {
                Goal goal;
                switch (choice)
                {
                    case 1:
                        goal = new SimpleGoal(name, description, points);
                        break;
                    case 2:
                        goal = new EternalGoal(name, description, points);
                        break;
                    case 3:
                        Console.Write("Enter the number of times to complete the goal: ");
                        if (int.TryParse(Console.ReadLine(), out int times) && times > 0)
                        {
                            Console.Write("Enter the bonus points for completing the goal: ");
                            if (int.TryParse(Console.ReadLine(), out int bonus) && bonus > 0)
                            {
                                goal = new ChecklistGoal(name, description, points, times, bonus);
                            }
                            else
                            {
                                Console.WriteLine("Invalid bonus points. No goal created.");
                                return;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid number of times. No goal created.");
                            return;
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid goal type. No goal created.");
                        return;
                }
                user.Goals.Add(goal);
                Console.WriteLine("Goal created successfully!");
            }
            else
            {
                Console.WriteLine("Invalid points. No goal created.");
            }
        }
        else
        {
            Console.WriteLine("Invalid choice. No goal created.");
        }
    }
}
