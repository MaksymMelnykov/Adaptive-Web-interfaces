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

            // Робота з Async-Await
            Task.Run(async () => await AsyncMethod("First Async"));
            Task.Run(async () => await AsyncMethod("Second Async"));

            // Робота з Async-Await для API
            AsyncApiRequest().Wait();

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
    }
}
