/*
 * 5. Write a program which creates two threads and a shared collection:
 * the first one should add 10 elements into the collection and the second should print all elements
 * in the collection after each adding.
 * Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.
 */
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task5.Threads.SharedCollection
{
    class Program
    {
        private static List<int> values = new List<int>();
        private static ReaderWriterLockSlim locker = new ReaderWriterLockSlim();

        static void Main(string[] args)
        {
            Console.WriteLine("5. Write a program which creates two threads and a shared collection:");
            Console.WriteLine("the first one should add 10 elements into the collection and the second should print all elements in the collection after each adding.");
            Console.WriteLine("Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.");
            Console.WriteLine();

            var taskWrite = Task.Factory.StartNew(WriteValues);
            var taskRead = Task.Factory.StartNew(PrintValues);

            Task.WaitAll(taskWrite, taskRead);

            Console.ReadLine();
        }

        private static void WriteValues()
        {
            for (int i = 0; i < 10; i++)
            {
                locker.EnterWriteLock();

                values.Add(i);

                locker.ExitWriteLock();
                Thread.Sleep(250);
            }
        }

        private static void PrintValues()
        {
            for (int i = 0; i < 10; i++)
            {
                locker.EnterReadLock();

                Console.WriteLine($"[{string.Join(",", values)}]");
                Thread.Sleep(300);

                locker.ExitReadLock();
            }
        }

    }
}
