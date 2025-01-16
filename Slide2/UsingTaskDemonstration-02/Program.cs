﻿namespace UsingTaskDemonstration_02
{
    class Program
    {
        public static void Main()
        {
            // Wait for all tasks to complete.
            Task[] tasks = new Task[5];
            String taskData = "Hello";

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
                    Console.WriteLine($"    {ex.Message}");
            }

            Console.WriteLine("Status of completed tasks:");
            foreach (var t in tasks)
            {
                Console.WriteLine($"    Task #{t.Id}: {t.Status}");
            }
        }
    } //end Program

}
