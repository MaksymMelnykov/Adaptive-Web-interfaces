using System.Data;

namespace Lab1
{
    class Program
    {
        static void ShowMenu()
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Print the number of words in the text \"Lorem ipsum\"");
            Console.WriteLine("2. Perform a mathematical operation");
            Console.WriteLine("3. Exit");
            Console.Write("Input your choice: ");
        }

        //Опис поля: статичне поле, яке містить текст "Lorem ipsum..." з файлу
        static string loremText = File.ReadAllText("Lorem.txt");

        static int CountWords(string text)
        {
            string[] words = text.Split(new char[] { ' ', ',', '.', ';', ':', '?', '!' }, StringSplitOptions.RemoveEmptyEntries);

            return words.Length;
        }

        //Опис методу: статичний метод, який дозволяє користувачеві вводити математичні вирази,
        //виконувати їх обчислення та виводити результат на консоль.
        static void MathOperation()
        {
            //Введення математичного виразу, введений вираз зчитується з консолі
            Console.Write($"Enter the mathematical expression you want to execute: ");
            
            //Змінна expression для зберігання виразу.
            string expression = Console.ReadLine();

            try
            {
                //Створення об'єкту DataTable, який використовується для обчислення виразу
                DataTable data = new DataTable();

                //Обчислення математичного виразу за допомогою методу Compute
                var result = data.Compute(expression, "");

                //Виведення результату на консоль
                Console.WriteLine($"The result of the expression {expression}: {result}\n");
            }
            catch (Exception e)
            {
                //Обробка виключення, яке може виникнути при невірному введенні виразу
                Console.WriteLine($"The expression {expression} cannot be executed. Error: {e.Message}\n");
            }
        }

        static void Main(string[] args)
        {
            int choice = 0;

            while (choice != 3)
            {
                ShowMenu();

                try
                {
                    choice = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input format. Please enter an integer between 1 and 3\n");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Console.WriteLine($"Number of words in the text \"Lorem ipsum\": {CountWords(loremText)}\n");
                        break;

                    case 2:
                        MathOperation();
                        break;

                    case 3:
                        Console.WriteLine("Good luck, bye!\n");
                        break;

                    default:
                        Console.WriteLine("Invalid input format. Please enter an integer between 1 and 3\n");
                        break;
                }
            }
        }
    }
}