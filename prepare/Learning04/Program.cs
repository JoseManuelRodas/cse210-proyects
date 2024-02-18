using System;
using System.Collections.Generic;
class ProgramWeek4
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Quit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    BreathingActivity breathingActivity = new BreathingActivity();
                    breathingActivity.StartActivity();
                    breathingActivity.EndActivity();
                    break;
                case "2":
                    ReflectionActivity reflectionActivity = new ReflectionActivity();
                    reflectionActivity.StartActivity();
                    reflectionActivity.EndActivity();
                    break;
                case "3":
                    ListingActivity listingActivity = new ListingActivity();
                    listingActivity.StartActivity();
                    listingActivity.EndActivity();
                    break;
                case "4":
                    Console.WriteLine("Exiting program...");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}

class Activity
{
    protected string name;
    protected string description;
    protected int duration;

    public virtual void StartActivity()
    {
        Console.WriteLine($"\nStarting {name} activity:");
        Console.WriteLine(description);
        Console.Write("Enter duration in seconds: ");
        duration = int.Parse(Console.ReadLine());
        Console.WriteLine("Get ready to begin...");
        ShowSpinner("");
    }
    static void ShowSpinner(string message)
    {
        Console.CursorVisible = false;
        string[] spinner = { "/", "-", "\\", "|" };
        DateTime endTime = DateTime.Now.AddSeconds(3);
        while (DateTime.Now < endTime)
        {
            foreach (string symbol in spinner)
            {
                Console.Write($"\r{message} {symbol}");
                System.Threading.Thread.Sleep(250); 
            }
        }
        Console.CursorVisible = true;
    }
    protected void Countdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.WriteLine($"Starting in {i}...");
            Thread.Sleep(1000);
        }
    }

    public virtual void EndActivity()
    {
        Console.WriteLine("\nGood job! You've completed the activity.");
        Console.WriteLine($"You've completed {name}.");
        ShowSpinner("");
    }
}

class BreathingActivity : Activity
{
    public BreathingActivity()
    {
        name = "Breathing";
        description = "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.";
    }

    public override void StartActivity()
    {
        base.StartActivity();
        
        while (duration > 0)
        {
            Console.WriteLine("\nInhale");
            CountdownWithSeconds(4);

            if (duration <= 0)
                break;

            Console.WriteLine("\nExhale");
            CountdownWithSeconds(4);
        }
    }

    private void CountdownWithSeconds(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.WriteLine($" {i} seconds");
            Thread.Sleep(1000);
            duration--;
        }
    }
}


class ReflectionActivity : Activity
{
    private string[] prompts = {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private string[] questions = {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity()
    {
        name = "Reflection";
        description = "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.";
    }

    public override void StartActivity()
    {
        base.StartActivity();

        Console.WriteLine("\nConsider the following prompt:");
        Console.WriteLine("-----------------------------");
        Random random = new Random();
        Console.WriteLine(prompts[random.Next(prompts.Length)]);
        Console.WriteLine("-----------------------------");
        Console.WriteLine("When you have something in mind, press enter to continue...");
        Console.ReadLine(); 

        Console.WriteLine("Starting in 3...");
        Thread.Sleep(1000);
        Console.WriteLine("Starting in 2...");
        Thread.Sleep(1000);
        Console.WriteLine("Starting in 1...");
        Thread.Sleep(1000);


        int totalTimeCompleted = 0;
        for (int i = 0; i < questions.Length; i++)
        {
            Console.WriteLine($"\n{questions[i]}");
            Console.WriteLine($"Time left: {(duration >= 0 ? duration : 0)} seconds");
            Thread.Sleep(6000); 
            totalTimeCompleted += 6;
            duration -= 6; 
            if (duration <= 0)
            {
                break; 
            }
        }

        Console.WriteLine("\nWell done!");
        Console.WriteLine($"You have completed {totalTimeCompleted} seconds of reflecting activity.");
    }
}



class ListingActivity : Activity
{
    private string[] prompts = {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity()
    {
        name = "Listing";
        description = "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.";
    }

    public override void StartActivity()
    {
        base.StartActivity();

        Console.WriteLine("\nList as many responses you can to the following prompt:");
        Console.WriteLine("-----------------------------");
        Random random = new Random();
        Console.WriteLine(prompts[random.Next(prompts.Length)]);
        Console.WriteLine("-----------------------------");
        Console.WriteLine("When you have something in mind, press enter to continue");
        Console.ReadLine(); 
        Console.WriteLine("Get ready to begin...");
        Countdown(3);


        List<string> listItems = new List<string>();
        DateTime startTime = DateTime.Now;
        while ((DateTime.Now - startTime).TotalSeconds < duration)
        {
            Console.Write("- ");
            string item = Console.ReadLine();
            if (!string.IsNullOrEmpty(item))
            {
                listItems.Add(item);
            }
        }
        Console.WriteLine($"\nYou listed {listItems.Count} items.");
    }
}

