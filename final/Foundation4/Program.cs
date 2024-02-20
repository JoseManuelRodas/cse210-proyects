using System;

public abstract class Activity
{
    public DateTime Date { get; set; }
    public int Minutes { get; set; }

    public virtual double GetDistance()
    {
        return 0;
    }

    public virtual double GetSpeed()
    {
        return 0;
    }

    public virtual double GetPace()
    {
        return 0;
    }

    public virtual string GetSummary()
    {
        double distance = GetDistance();
        double speed = GetSpeed();
        double pace = GetPace();
        string summary = $"{Date.ToShortDateString()} {this.GetType().Name} ({Minutes} min) - Distance {distance:F1} km, Speed {speed:F1} kph, Pace {pace:F2} min per km";
        return summary;
    }
}

public class Running : Activity
{
    public double Distance { get; set; }

    public override double GetDistance()
    {
        return Distance;
    }

    public override double GetSpeed()
    {
        double speed = (Distance / Minutes) * 60;
        return speed;
    }

    public override double GetPace()
    {
        double pace = Minutes / Distance;
        return pace;
    }
}

public class Cycling : Activity
{
    public double Speed { get; set; }

    public override double GetDistance()
    {
        double distance = (Speed / 60) * Minutes;
        return distance;
    }

    public override double GetSpeed()
    {
        return Speed;
    }

    public override double GetPace()
    {
        double pace = 60 / Speed;
        return pace;
    }
}

public class Swimming : Activity
{
    public int Laps { get; set; }

    public override double GetDistance()
    {
        double distance = Laps * 0.05;
        return distance;
    }

    public override double GetSpeed()
    {
        double speed = (GetDistance() / Minutes) * 60;
        return speed;
    }

    public override double GetPace()
    {
        double pace = Minutes / GetDistance();
        return pace;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Activity a1 = new Running() { Date = new DateTime(2024, 2, 14), Minutes = 30, Distance = 4.8 };
        Activity a2 = new Cycling() { Date = new DateTime(2024, 2, 17), Minutes = 45, Speed = 25 };
        Activity a3 = new Swimming() { Date = new DateTime(2024, 2, 20), Minutes = 20, Laps = 20 };

        List<Activity> activities = new List<Activity>() { a1, a2, a3 };

        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
