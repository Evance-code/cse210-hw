using System;

class Program
{
    static void Main(string[] args)
    {
        ShowGreeting();

        string userFullName = GetUserName();
        int favoriteNumber = GetUserFavoriteNumber();

        int result = ComputeSquare(favoriteNumber);

        OutputResult(userFullName, result);
    }

    static void ShowGreeting()
    {
        Console.WriteLine("Hello and welcome to the application!");
    }

    static string GetUserName()
    {
        Console.Write("Could you please tell us your name? ");
        string name = Console.ReadLine();

        return name;
    }

    static int GetUserFavoriteNumber()
    {
        Console.Write("What’s your most favorite number? ");
        int number = Convert.ToInt32(Console.ReadLine());

        return number;
    }

    static int ComputeSquare(int number)
    {
        return number * number;
    }

    static void OutputResult(string userName, int squaredResult)
    {
        Console.WriteLine($"Hey {userName}, the result of squaring your number is {squaredResult}.");
    }
}
