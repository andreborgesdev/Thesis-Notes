using System;
using System.Collections.Generic;

namespace NetFxQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<int> q = new Queue<int>();

            for (int i = 0; i < 10; i++)
            {
                q.Enqueue(i);
            }

            while (q.Count > 0)
            {
                Console.WriteLine(q.Dequeue());
            }
        }
    }
}
