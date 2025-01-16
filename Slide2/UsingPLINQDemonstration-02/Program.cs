namespace UsingPLINQDemonstration_02
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    class Program
    {
        // IsPrime returns true if the number is prime, else false.
        private static bool IsPrime(int number)
        {
            bool result = true;
            if (number < 2)
            {
                return false;
            }
            for (var divisor = 2; divisor <= Math.Sqrt(number) && result == true; divisor++)
            {
                if (number % divisor == 0)
                {
                    result = false;
                }
            }
            return result;
        } // end IsPrime

        // GetPrimeList returns prime numbers by using a sequential ForEach
        private static IList<int> GetPrimeList(IList<int> numbers) =>
            numbers.Where(IsPrime).ToList();

        // GetPrimeListWithParallel returns prime numbers by using Parallel.ForEach
        private static IList<int> GetPrimeListWithParallel(IList<int> numbers)
        {
            var primeNumbers = new ConcurrentBag<int>();
            Parallel.ForEach(numbers, number => {
                if (IsPrime(number))
                {
                    primeNumbers.Add(number);
                }
            });
            return primeNumbers.ToList();
        } // end GetPrimeListWithParallel

        static void Main()
        {
            // Set the range up to 2 million
            var limit = 2_000_000;
            var numbers = Enumerable.Range(0, limit).ToList();

            // Sequential execution
            var watch = Stopwatch.StartNew();
            var primeNumbersFromForEach = GetPrimeList(numbers);
            watch.Stop();
            Console.WriteLine($"Classical foreach loop | Total prime numbers : " +
                              $"{primeNumbersFromForEach.Count} | Time Taken : " +
                              $"{watch.ElapsedMilliseconds} ms.");

            // Parallel execution
            var watchForParallel = Stopwatch.StartNew();
            var primeNumbersFromParallelForEach = GetPrimeListWithParallel(numbers);
            watchForParallel.Stop();
            Console.WriteLine($"Parallel.ForEach loop | Total prime numbers : " +
                              $"{primeNumbersFromParallelForEach.Count} | Time Taken : " +
                              $"{watchForParallel.ElapsedMilliseconds} ms.");

            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        } // end Main
    } // end Program

}
