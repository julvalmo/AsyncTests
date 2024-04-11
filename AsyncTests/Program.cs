using System;
using System.Threading;

class Program
{
    static void Main()
    {
        // Queue tasks for execution
        for (int i = 0; i < 10; i++)
        {
            int taskNumber = i;
            ThreadPool.QueueUserWorkItem(state =>
            {
                // Simulate work with a delay
                //int delay = taskNumber % 2 == 0 ? 1000 : 5000; // Longer delay for even-numbered tasks
                int delay = new Random().Next(1000, 10001);
                Thread.Sleep(delay);

                // Log completion
                Console.WriteLine($"Task {taskNumber} completed after {delay} ms.");
            });
        }

        // Keep the main thread alive until all tasks are completed
        Console.ReadLine();
    }
}
