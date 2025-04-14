using System;
using System.Collections.Generic;

// Base Activity class
public class Activity
{
    private DateTime _date;
    private int _minutes;

    public Activity(DateTime date, int minutes)
    {
        _date = date;
        _minutes = minutes;
    }

    public DateTime Date => _date;
    public int Minutes => _minutes;

    public virtual double GetDistance() => 0; // Default implementation

    public virtual double GetSpeed() => (GetDistance() / _minutes) * 60;

    public virtual double GetPace() => _minutes / GetDistance();

    public virtual string GetSummary()
    {
        return $"{_date.ToString("dd MMM yyyy")} {GetType().Name} ({_minutes} min): " +
               $"Distance {GetDistance():F1} miles, Speed {GetSpeed():F1} mph, Pace: {GetPace():F1} min per mile";
    }
}

// Running class
public class Running : Activity
{
    private double _distance; // in miles

    public Running(DateTime date, int minutes, double distance) : base(date, minutes)
    {
        _distance = distance;
    }

    public override double GetDistance() => _distance;
}

// Cycling class
public class Cycling : Activity
{
    private double _speed; // in mph

    public Cycling(DateTime date, int minutes, double speed) : base(date, minutes)
    {
        _speed = speed;
    }

    public override double GetSpeed() => _speed;
    public override double GetDistance() => (_speed * Minutes) / 60;
}

// Swimming class
public class Swimming : Activity
{
    private int _laps;
    private const double LapLengthMeters = 50;
    private const double MetersToMiles = 0.000621371;

    public Swimming(DateTime date, int minutes, int laps) : base(date, minutes)
    {
        _laps = laps;
    }

    public override double GetDistance() => (_laps * LapLengthMeters) * MetersToMiles;
}

class Program
{
    static void Main(string[] args)
    {
        // Create activities with current date
        DateTime today = DateTime.Now;
        var activities = new List<Activity>
        {
            new Running(today, 30, 3.1),       // 5K run
            new Cycling(today, 45, 15.5),      // Cycling at 15.5 mph
            new Swimming(today.AddDays(-1), 40, 30) // Yesterday's swim (30 laps)
        };

        // Display summaries
        Console.WriteLine("Exercise Tracking Summary");
        Console.WriteLine("========================");
        
        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }

        Console.WriteLine("\nNote: All distances and speeds in miles/mph");
    }
}