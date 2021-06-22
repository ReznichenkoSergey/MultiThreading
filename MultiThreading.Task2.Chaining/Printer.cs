using System;

namespace MultiThreading.Task2.Chaining
{
    public class TaskResultPrinter
    {
        public void PrintResult(int[] array, string taskName)
        {
            Console.WriteLine($"{taskName}:");
            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine($"{i + 1}. Item= {array[i]}");
            }
            Console.WriteLine();
        }

        public void PrintResult(double value, string taskName)
        {
            Console.WriteLine($"{taskName}:");
            Console.WriteLine($"Result= {value}:");
            Console.WriteLine();
        }
    }
}
