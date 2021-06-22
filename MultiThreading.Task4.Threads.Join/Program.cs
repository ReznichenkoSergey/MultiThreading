/*
 * 4.	Write a program which recursively creates 10 threads.
 * Each thread should be with the same body and receive a state with integer number, decrement it,
 * print and pass as a state into the newly created thread.
 * Use Thread class for this task and Join for waiting threads.
 * 
 * Implement all of the following options:
 * - a) Use Thread class for this task and Join for waiting threads.
 * - b) ThreadPool class for this task and Semaphore for waiting threads.
 */

using System;
using System.Threading;

namespace MultiThreading.Task4.Threads.Join
{
    class Program
    {
        private static Semaphore _semaphore = new Semaphore(1,10);
        private static object locker = new object();
        private static object value = 100;

        static void Main(string[] args)
        {
            Console.WriteLine("4.	Write a program which recursively creates 10 threads.");
            Console.WriteLine("Each thread should be with the same body and receive a state with integer number, decrement it, print and pass as a state into the newly created thread.");
            Console.WriteLine("Implement all of the following options:");
            Console.WriteLine();
            Console.WriteLine("- a) Use Thread class for this task and Join for waiting threads.");
            Console.WriteLine("- b) ThreadPool class for this task and Semaphore for waiting threads.");

            Console.WriteLine();

            var test = new Test();

            for (int i = 0; i < 10; i++)
            {
                var thread = new Thread(CalculateThreads);
                thread.Start(test);
                thread.Join();
            }

            Console.WriteLine();

            for (int i = 0; i < 10; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(CalculateThreadPool), test);
            }

            Console.ReadLine();
        }

        //Thread
        private static void CalculateThreads(object obj)
        {
            var value = obj as Test;
            value.Value--;
            Console.WriteLine($"Value= {value.Value}");
        }

        //ThreadPool
        private static void CalculateThreadPool(object obj)
        {
            _semaphore.WaitOne(1000);

            var value = obj as Test;
            value.Value--;
            Console.WriteLine($"Value= {value.Value}");
            
            _semaphore.Release();
        }

        public class Test
        {
            public int Value { get; set; }

            public Test(int value = 10)
            {
                Value = value;
            }
        }
    }
}
