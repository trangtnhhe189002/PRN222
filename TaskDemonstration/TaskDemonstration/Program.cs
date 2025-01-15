using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
   
        public static void Main()
        {
            // Wait for all tasks to complete
            Task[] tasks = new Task[5];
            string taskData = "Hello";
            for (int i = 0; i < 5; i++)
            {
                tasks[i] = Task.Run(() => {
                    Console.WriteLine($"Task={Task.CurrentId}, obj={taskData}, " +
                                      $"ThreadId={Thread.CurrentThread.ManagedThreadId}");
                    Thread.Sleep(1000);
                });
            }

            try
            {
                Task.WaitAll(tasks);
            }
            catch (AggregateException ae)
            {
                Console.WriteLine("One or more exceptions occurred:");
                foreach (var ex in ae.Flatten().InnerExceptions)
                    Console.WriteLine($"   {ex.Message}");
            }

            Console.WriteLine("Status of completed tasks:");
            foreach (var t in tasks)
            {
                Console.WriteLine($"   Task #{t.Id}: {t.Status}");
            }
        } 
    
    //static void PrintNumber(string message)
    //{
    //    for (int i = 1; i <= 5; i++)
    //    {
    //        Console.WriteLine($"{message}:{i}");
    //        Thread.Sleep(1000);
    //    }
    //} //end PrintNumber

    //static void Main()
    //{
    //    Thread.CurrentThread.Name = "Main";

    //    Task task01 = new Task(() => PrintNumber("Task 01"));
    //    task01.Start();


    //    Task task02 = Task.Run(delegate {
    //        PrintNumber("Task 02");
    //    });


    //    Task task03 = new Task(new Action(() => {
    //        PrintNumber("Task 03");
    //    }));
    //    task03.Start();

    //    Console.WriteLine($"Thread '{Thread.CurrentThread.Name}'");
    //    Console.ReadKey();
    //} 
} 
