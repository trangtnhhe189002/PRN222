﻿namespace UsingTaskDemonstration_04
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            var tasks = new List<Task<int>>();
            var source = new CancellationTokenSource();
            var token = source.Token;
            int completedIterations = 0;

            for (int n = 1; n <= 20; n++)
            {
                tasks.Add(Task.Run(() => {
                    int iterations = 0;
                    try
                    {
                        for (int ctr = 1; ctr <= 2_000_000; ctr++)
                        {
                            token.ThrowIfCancellationRequested(); // Check for cancellation
                            iterations++;
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        Console.WriteLine($"Task {Task.CurrentId} was canceled.");
                    }
                    Interlocked.Increment(ref completedIterations);
                    if (completedIterations >= 10) // Cancel remaining tasks after 10 completed
                        source.Cancel();
                    return iterations;
                }, token));
            }

            Console.WriteLine("Waiting for the first 10 tasks to complete...\n");
            try
            {
                Task.WaitAll(tasks.ToArray());
            }
            catch (AggregateException ae)
            {
                Console.WriteLine("Status of tasks:\n");
                Console.WriteLine("{0,10} {1,20} {2,14:N0}", "Task Id", "Status", "Iterations");
                foreach (var ex in ae.Flatten().InnerExceptions)
                {
                    if (ex is OperationCanceledException)
                    {
                        Console.WriteLine("A task was canceled.");
                    }
                    else
                    {
                        Console.WriteLine($"Exception: {ex.Message}");
                    }
                }
            }

            // Display the results and statuses of tasks
            foreach (var t in tasks)
            {
                Console.WriteLine("{0,10} {1,20} {2,14}",
                                  t.Id, t.Status,
                                  t.Status != TaskStatus.Canceled ? t.Result.ToString("N0") : "n/a");
            }

            Console.ReadLine();
        }
    }


}
