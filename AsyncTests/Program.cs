using System;
using System.Threading;

class Program
{
    static void Main()
    {
        char opcRepeat = 'y';
        do
        {
            char OPC = '1';
            Console.WriteLine("Pick an option:");
            Console.WriteLine("1. One second delay added per every thread work added");
            Console.WriteLine("2. Random (up to 10) second(s) delay per every thread work added");
            OPC = Console.ReadKey().KeyChar;
            Console.WriteLine("\n");

            switch (OPC)
            {
                case '1':
                    ThreadTestInitial(); break;
                case '2':
                    ThreadTest(); break;
                default:
                    Console.WriteLine("Not a valid option");
                    break;
            }

            Console.WriteLine("Execute Thread Test Again? [Y/N]");
            opcRepeat = Console.ReadKey().KeyChar;
            Console.WriteLine("\n");
        } while (opcRepeat == 'y' || opcRepeat == 'Y' || opcRepeat == ((char)ConsoleKey.Enter));

        Console.WriteLine("Press a key to exit...");
        // Keep the main thread alive until all tasks are completed
        Console.ReadLine();
    }

    static void ThreadTestInitial()
    {
        bool firstPassFlag = true;
        // Initialize a CountdownEvent with the number of tasks to wait for
        int taskCount = 10;
        CountdownEvent countdown = new CountdownEvent(taskCount);

        // handle ordered added delay
        const int delayExtent = 1000;
        int currentDelay = delayExtent;

        // Queue tasks for execution
        for (int i = 0; i < 10 ; i++)
        {
            int taskNumber = i;
            ThreadPool.QueueUserWorkItem(state =>
            {
                // Simulate work with a delay
                int delay = firstPassFlag ? delayExtent : currentDelay += delayExtent;
                firstPassFlag = false;
                Thread.Sleep(delay);

                // Log completion
                Console.WriteLine($"Task {taskNumber} completed after {delay} ms.");

                countdown.Signal();
            });
        }

        // Wait for all tasks to complete
        countdown.Wait();
        Console.WriteLine("All tasks completed.");
    }

    static void ThreadTest()
    {
        // Initialize a CountdownEvent with the number of tasks to wait for
        int taskCount = 10;
        CountdownEvent countdown = new CountdownEvent(taskCount);

        // Queue tasks for execution
        for (int i = 0; i < taskCount; i++)
        {
            int taskNumber = i;
            ThreadPool.QueueUserWorkItem(state =>
            {
                // Simulate work with a delay
                int delay = new Random().Next(1000, 10001);
                Thread.Sleep(delay);

                // Log completion
                Console.WriteLine($"Task {taskNumber} completed after {delay} ms.");

                // Signal that this task is complete
                countdown.Signal();
            });
        }

        // Wait for all tasks to complete
        countdown.Wait();
        Console.WriteLine("All tasks completed.");
    }
}
