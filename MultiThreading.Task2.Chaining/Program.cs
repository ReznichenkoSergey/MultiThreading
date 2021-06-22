/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
using System.Threading.Tasks;

namespace MultiThreading.Task2.Chaining
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. MultiThreading V1 ");
            Console.WriteLine("2.	Write a program, which creates a chain of four Tasks.");
            Console.WriteLine("First Task – creates an array of 10 random integer.");
            Console.WriteLine("Second Task – multiplies this array with another random integer.");
            Console.WriteLine("Third Task – sorts this array by ascending.");
            Console.WriteLine("Fourth Task – calculates the average value. All this tasks should print the values to console");
            Console.WriteLine();

            var printer = new TaskResultPrinter();
            var generator = new NumGenerator();

            var task1 = Task.Run(() =>
            {
                var array = new int[10];
                for(int i = 0; i < array.Length; i++)
                {
                    array[i] = generator.GetRandomValue();
                }

                printer.PrintResult(array, "First Task");

                return array;
            });
            var task2 = task1.ContinueWith((task) =>
            {
                var array = task.Result;
                Array.Resize(ref array, array.Length + 1);
                array[array.Length-1] = generator.GetRandomValue();

                printer.PrintResult(array, "Second Task");

                return array;
            });
            var task3 = task2.ContinueWith((task) =>
            {
                var array = task.Result;
                Array.Sort(array);

                printer.PrintResult(array, "Third Task");

                return array;
            });
            var task4 = task3.ContinueWith((task) =>
            {
                var itemsSum = 0;
                var array = task.Result;
                double result = 0;
                if (array?.Length > 0)
                {
                    for(int i = 0; i < array.Length; i++)
                    {
                        itemsSum += array[i];
                    }
                    result = itemsSum / (double)array.Length;
                }

                printer.PrintResult(result, "Fourth Task");

                return result;
            });

            task4.Wait();

            Console.ReadLine();
        }
    }
}
