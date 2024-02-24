using Newtonsoft.Json;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Main Thread ID: {Thread.CurrentThread.ManagedThreadId}\n");

            // Робота з класом Thread
            Thread thread1 = new Thread(() => ThreadMethod("First Thread"));
            thread1.Start();

            Thread thread2 = new Thread(() => ThreadMethod("Second Thread"));
            thread2.Start();

            int[] array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            int sum = ThreadSumArrayParallel(array, "ThreadSumArrayParallel");
            Console.WriteLine($"Sum: {sum}\n");

            ThreadFactorialMethod(5, "ThreadFactorialMethod");

            // Робота з Async-Await
            Task.Run(async () => await AsyncMethod("First Async"));
            Task.Run(async () => await AsyncMethod("Second Async"));

            AsyncApiRequest().Wait();

            int n = 10;
            FibonacciAsync(n).Wait();

            Console.ReadLine();
        }

        static void ThreadMethod(string nameOfThread)
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"{nameOfThread} with ID: {Thread.CurrentThread.ManagedThreadId} started!!!");
                Thread.Sleep(1000);
                Console.WriteLine($"{nameOfThread} finished!!!");
            }
        }

        static int ThreadSumArrayParallel(int[] array, string nameOfThread)
        {
            int sum = 0;

            int threadCount = 4;

            int blockSize = array.Length / threadCount;

            Thread[] threads = new Thread[threadCount];

            Console.WriteLine($"{nameOfThread} with ID: {Thread.CurrentThread.ManagedThreadId} started!!!");

            object syncObject = new object();

            for (int i = 0; i < threadCount; i++)
            {
                int startIndex = i * blockSize;
                int endIndex = (i + 1) * blockSize - 1;

                threads[i] = new Thread(() =>
                {
                    int localSum = 0;

                    for (int j = startIndex; j <= endIndex; j++)
                    {
                        localSum += array[j];
                    }

                    lock (syncObject)
                    {
                        sum += localSum;
                    }
                });

                threads[i].Start();
            }

            foreach (Thread thread in threads)
            {
                thread.Join();
            }

            Console.WriteLine($"{nameOfThread} finished!!!");
            return sum;

        }

        static void ThreadFactorialMethod(int number, string nameOfThread)
        {
            Thread thread = new Thread(() =>
            {
                Console.WriteLine($"{nameOfThread} with ID: {Thread.CurrentThread.ManagedThreadId} started!!!");
                long factorial = 1;
                for (int i = 1; i <= number; i++)
                {
                    factorial *= i;
                }
                Console.WriteLine($"Factorial of {number} is {factorial}");
                Console.WriteLine($"{nameOfThread} finished!!!");
            });

            thread.Start();
        }

        static async Task AsyncMethod(string nameOfAsync)
        {
            Console.WriteLine($"{nameOfAsync} started!!!");
            await Task.Delay(1500);
            Console.WriteLine($"{nameOfAsync} finished!!!");
        }

        static async Task AsyncApiRequest()
        {
            Console.WriteLine($"Async Api Method with ID: {Thread.CurrentThread.ManagedThreadId} started!!!");

            using (HttpClient client = new HttpClient())
            {
                string apiUrl = $"https://jsonplaceholder.typicode.com/users";
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject(content);
                    Console.WriteLine($"API response content:\n {result}");
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }
            Console.WriteLine("Async Api Method finished!!!\n");
        }

        static async Task<int> FibonacciAsync(int n)
        {
            Console.WriteLine($"FibonacciAsync Method with ID: {Thread.CurrentThread.ManagedThreadId} started!!!");
            Console.WriteLine($"Calculating Fibonacci({n}) asynchronously...");

            if (n <= 0)
            {
                throw new ArgumentException("Argument must be a positive integer.", nameof(n));
            }

            if (n == 1 || n == 2)
            {
                return 1;
            }

            int n1 = await FibonacciAsync(n - 1);
            int n2 = await FibonacciAsync(n - 2);

            int result = n1 + n2;
            Console.WriteLine($"Fibonacci({n}) = {result}");

            Console.WriteLine("FibonacciAsync Method finished!!!\n");

            return result;
        }
    }
}
