using System;

namespace MultiThreading.Task2.Chaining
{
    public class NumGenerator
    {
        Random random = new Random();

        public int GetRandomValue()
        {
            return random.Next(1, 10);
        }
    }
}
