using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int>();
        
        int userNumber;
        
        // Prompting user for input until 0 is entered
        do
        {
            Console.Write("Enter a number (0 to quit): ");
            string userResponse = Console.ReadLine();
            userNumber = int.Parse(userResponse);
            
            if (userNumber != 0)
            {
                numbers.Add(userNumber);
            }
        } while (userNumber != 0);

        // Compute the sum of the numbers
        int sum = CalculateSum(numbers);
        Console.WriteLine($"The sum is: {sum}");

        // Compute the average of the numbers
        float average = CalculateAverage(sum, numbers.Count);
        Console.WriteLine($"The average is: {average}");

        // Find the maximum number
        int max = FindMax(numbers);
        Console.WriteLine($"The max is: {max}");

        // Stretch Challenge: Find the smallest positive number closest to zero
        int smallestPositive = FindSmallestPositive(numbers);
        if (smallestPositive != int.MaxValue)
        {
            Console.WriteLine($"The smallest positive number closest to zero is: {smallestPositive}");
        }
        else
        {
            Console.WriteLine("No positive numbers were entered.");
        }

        // Sort the numbers and display the sorted list
        numbers.Sort();
        Console.WriteLine("Sorted numbers:");
        foreach (int number in numbers)
        {
            Console.WriteLine(number);
        }
    }

    static int CalculateSum(List<int> numbers)
    {
        int sum = 0;
        foreach (int number in numbers)
        {
            sum += number;
        }
        return sum;
    }

    static float CalculateAverage(int sum, int count)
    {
        return count > 0 ? (float)sum / count : 0;
    }

    static int FindMax(List<int> numbers)
    {
        int max = numbers[0];
        foreach (int number in numbers)
        {
            if (number > max)
            {
                max = number;
            }
        }
        return max;
    }

    // Stretch Challenge: Find the smallest positive number closest to zero
    static int FindSmallestPositive(List<int> numbers)
    {
        int smallestPositive = int.MaxValue;
        
        foreach (int number in numbers)
        {
            if (number > 0 && number < smallestPositive)
            {
                smallestPositive = number;
            }
        }
        
        return smallestPositive;
    }
}
