// EternalQuest Program - W06 Project
// Description: This program tracks personal goals using gamification. It supports simple, eternal, and checklist goals.
// Creativity: Added Levels and Achievements when reaching score thresholds.

using System;
using System.Collections.Generic;
using System.IO;

abstract class Goal
{
    protected string _name;
    protected string _description;
    protected int _points;

    public Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public abstract void RecordEvent(ref int score);
    public abstract bool IsComplete();
    public abstract string GetDetailsString();
    public abstract string GetStringRepresentation();
}

class SimpleGoal : Goal
{
    private bool _isComplete = false;

    public SimpleGoal(string name, string description, int points) : base(name, description, points) { }

    public override void RecordEvent(ref int score)
    {
        if (!_isComplete)
        {
            _isComplete = true;
            score += _points;
        }
    }

    public override bool IsComplete() => _isComplete;

    public override string GetDetailsString() => $"[{(_isComplete ? "X" : " ")}] {_name} ({_description})";

    public override string GetStringRepresentation() => $"SimpleGoal:{_name},{_description},{_points},{_isComplete}";
}

class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points) : base(name, description, points) { }

    public override void RecordEvent(ref int score)
    {
        score += _points;
    }

    public override bool IsComplete() => false;

    public override string GetDetailsString() => $"[∞] {_name} ({_description})";

    public override string GetStringRepresentation() => $"EternalGoal:{_name},{_description},{_points}";
}

class ChecklistGoal : Goal
{
    private int _targetCount;
    private int _completedCount;
    private int _bonusPoints;

    public ChecklistGoal(string name, string description, int points, int targetCount, int bonusPoints)
        : base(name, description, points)
    {
        _targetCount = targetCount;
        _bonusPoints = bonusPoints;
    }

    public override void RecordEvent(ref int score)
    {
        if (_completedCount < _targetCount)
        {
            _completedCount++;
            score += _points;
            if (_completedCount == _targetCount)
            {
                score += _bonusPoints;
            }
        }
    }

    public override bool IsComplete() => _completedCount >= _targetCount;

    public override string GetDetailsString() => $"[{(_completedCount >= _targetCount ? "X" : " ")}] {_name} ({_description}) -- Completed {_completedCount}/{_targetCount}";

    public override string GetStringRepresentation() => $"ChecklistGoal:{_name},{_description},{_points},{_bonusPoints},{_targetCount},{_completedCount}";
}

class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score = 0;

    public void CreateGoal()
    {
        Console.WriteLine("Select goal type: 1) Simple 2) Eternal 3) Checklist");
        string input = Console.ReadLine();
        Console.Write("Name: ");
        string name = Console.ReadLine();
        Console.Write("Description: ");
        string desc = Console.ReadLine();
        Console.Write("Points: ");
        int points = int.Parse(Console.ReadLine());

        switch (input)
        {
            case "1":
                _goals.Add(new SimpleGoal(name, desc, points));
                break;
            case "2":
                _goals.Add(new EternalGoal(name, desc, points));
                break;
            case "3":
                Console.Write("Target count: ");
                int count = int.Parse(Console.ReadLine());
                Console.Write("Bonus points: ");
                int bonus = int.Parse(Console.ReadLine());
                _goals.Add(new ChecklistGoal(name, desc, points, count, bonus));
                break;
        }
    }

    public void RecordEvent()
    {
        Console.WriteLine("Which goal did you accomplish?");
        for (int i = 0; i < _goals.Count; i++)
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");

        int choice = int.Parse(Console.ReadLine()) - 1;
        _goals[choice].RecordEvent(ref _score);
        Console.WriteLine("Event recorded!");
        DisplayLevel();
    }

    public void DisplayGoals()
    {
        foreach (Goal goal in _goals)
            Console.WriteLine(goal.GetDetailsString());
    }

    public void DisplayScore()
    {
        Console.WriteLine($"Score: {_score}");
    }

    private void DisplayLevel()
    {
        string level = _score switch
        {
            >= 1000 => "🏆 Master",
            >= 500 => "🎖️ Intermediate",
            >= 100 => "🔰 Beginner",
            _ => "🌱 Newcomer"
        };
        Console.WriteLine($"You are now: {level}");
    }

    public void SaveGoals(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            writer.WriteLine(_score);
            foreach (Goal g in _goals)
                writer.WriteLine(g.GetStringRepresentation());
        }
    }

    public void LoadGoals(string filename)
    {
        string[] lines = File.ReadAllLines(filename);
        _score = int.Parse(lines[0]);
        _goals.Clear();

        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(":");
            string type = parts[0];
            string[] data = parts[1].Split(",");

            if (type == "SimpleGoal")
                _goals.Add(new SimpleGoal(data[0], data[1], int.Parse(data[2])) { });
            else if (type == "EternalGoal")
                _goals.Add(new EternalGoal(data[0], data[1], int.Parse(data[2])));
            else if (type == "ChecklistGoal")
                _goals.Add(new ChecklistGoal(data[0], data[1], int.Parse(data[2]), int.Parse(data[4]), int.Parse(data[3])));
        }
    }
}

class Program
{
    static void Main()
    {
        GoalManager manager = new GoalManager();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\nEternal Quest Menu:");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Record Event");
            Console.WriteLine("4. Show Score");
            Console.WriteLine("5. Save Goals");
            Console.WriteLine("6. Load Goals");
            Console.WriteLine("7. Quit");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1": manager.CreateGoal(); break;
                case "2": manager.DisplayGoals(); break;
                case "3": manager.RecordEvent(); break;
                case "4": manager.DisplayScore(); break;
                case "5": manager.SaveGoals("goals.txt"); break;
                case "6": manager.LoadGoals("goals.txt"); break;
                case "7": running = false; break;
            }
        }
    }
}
 