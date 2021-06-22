/*
*  Create a Task and attach continuations to it according to the following criteria:
   a.    Continuation task should be executed regardless of the result of the parent task.
   b.    Continuation task should be executed when the parent task finished without success.
   c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation
   d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled
   Demonstrate the work of the each case with console utility.
*/
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task6.Continuation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Create a Task and attach continuations to it according to the following criteria:");
            Console.WriteLine("a.    Continuation task should be executed regardless of the result of the parent task.");
            Console.WriteLine("b.    Continuation task should be executed when the parent task finished without success.");
            Console.WriteLine("c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation.");
            Console.WriteLine("d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled.");
            Console.WriteLine("Demonstrate the work of the each case with console utility.");
            Console.WriteLine();

            int x = 10;
            
            var taskMain = new Task<int>(() =>
            {
                Console.WriteLine($"Parent ThreadId= {Thread.CurrentThread.ManagedThreadId}");
                return 100/x;
            });
            
            var taskA = taskMain.ContinueWith(task =>
            {
                Console.WriteLine($"Task A: Parent Task status= {task.Status}, Child ThreadId= {Thread.CurrentThread.ManagedThreadId}");
            });

            var taskB = taskMain.ContinueWith(task =>
            {
                Console.WriteLine($"Task B: Exception in the parent task! Child ThreadId= {Thread.CurrentThread.ManagedThreadId}");
            }, TaskContinuationOptions.OnlyOnFaulted);
            
            var taskC = taskMain.ContinueWith(task =>
            {
                Console.WriteLine($"Task C: Parent executed with fail ... ! Child ThreadId= {Thread.CurrentThread.ManagedThreadId}");
            }, TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);
            
            var taskD = taskMain.ContinueWith(task =>
            {
                Console.WriteLine($"Task D, Child ThreadId= {Thread.CurrentThread.ManagedThreadId}");
            }, TaskContinuationOptions.LongRunning);
            
            taskMain.Start();

            Console.ReadLine();
        }
    }
}
