using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace _04_tasks
{
    internal class Demonstration
    {
        private DateTime clock = DateTime.Now;

        public void ShowClock()
        {
            Task taskByStart = new Task(()=>Console.WriteLine($"Clock with Task.Start - {clock}"));
            taskByStart.Start();
            taskByStart.Wait();

            Task.Factory.StartNew(() => Console.WriteLine($"Clock with Task.Factory.StartNew - {clock}"));

            Task taskByTaskRun = Task.Run(() => Console.WriteLine($"Clock with Task.Run() - {clock}"));
            taskByTaskRun.Wait();
        }
        private bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true; 
            for (int i = 2; i * i <= number; i++) 
                if (number % i == 0) return false;
            return true; 

        }
        public void ShowPrimeNumbers()
        {
            Task<List<int>> primeNumbers = new Task<List<int>>(() => 
            {
                List<int> result = new List<int>();
                for(int num = 0; num <= 1000; num++)
                {
                    if (IsPrime(num))
                    {
                        result.Add(num);
                    }
                }
                return result;

            });
            primeNumbers.Start();
            primeNumbers.Wait();
            for(int i=0; i< primeNumbers.Result.Count;i++)
            {
                Console.Write($"[{primeNumbers.Result[i]}]");
                if (i % 10 == 0)
                {
                    Console.WriteLine();
                }
            }
        }
        public void ShowPrimeNumbers(int startOfRange, int endOfRange)
        {
            Task<List<int>> primeNumbers = new Task<List<int>>(() =>
            {
                List<int> result = new List<int>();
                for (int num = startOfRange; num <= endOfRange; num++)
                {
                    if (IsPrime(num))
                    {
                        result.Add(num);
                    }
                }
                return result;

            });
            primeNumbers.Start();
            primeNumbers.Wait();
            for (int i = 0; i < primeNumbers.Result.Count; i++)
            {
                Console.Write($"[{primeNumbers.Result[i]}]");
                if (i % 10 == 0)
                {
                    Console.WriteLine();
                }
            }
        }
        public void ShowingValuesByTaskList(int[] arr)
        {
            Task[] tasks =new Task[4];
            tasks[0] = new Task(()=>Console.WriteLine($"Array maximum is: {arr.Max()}"));
            tasks[1] = new Task(()=>Console.WriteLine($"Array monimum is: {arr.Min()}"));
            tasks[2] = new Task(()=>Console.WriteLine($"Array Average is: {arr.Average()}"));
            tasks[3] = new Task(() => Console.WriteLine($"Array summa is: {arr.Sum()}"));

            foreach (var tsk in tasks)
            {
                tsk.Start();
                tsk.Wait();
            }
        }
        public IEnumerable<int> FindUniqueValue(int[] arr)
        {
            return arr.Distinct();
        }

        public IEnumerable<int> SortingArray(IEnumerable<int> arr)
        {
            return arr.OrderBy(n => n);
        }

        public void ArrayWorkByContinueWith(int[] arr, int searchingValue)
        {
            try
            {
                Task<IEnumerable<int>> findUniqueTask = Task.Run(() => FindUniqueValue(arr));

                Task<IEnumerable<int>> sortedTask = findUniqueTask.ContinueWith(t =>
                {
                    return SortingArray(t.Result);
                });

                sortedTask.ContinueWith(t =>
                {
                    var sortedArray = t.Result.ToArray();
                    int searchResult = Array.BinarySearch(sortedArray, searchingValue);

                    if (searchResult >= 0)
                    {
                        Console.WriteLine($"Binary search was successful, value found at index: {searchResult}");
                    }
                    else
                    {
                        Console.WriteLine("Value not found in the array.");
                    }
                }).Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    public Demonstration() { }
    }
}

