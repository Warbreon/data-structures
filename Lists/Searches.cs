using System.Diagnostics;

namespace Lists
{
    public class Searches
    {
        private const string FilePath = "C:\\Users\\Vlad\\Documents\\Code\\data-structures\\Lists\\data4.txt";

        public static void Execute()
        {
            int[] data;
            if (File.Exists(FilePath))
            {
                var lines = File.ReadAllLines(FilePath);
                data = Array.ConvertAll(lines, int.Parse);
                Array.Sort(data);
                Console.WriteLine("Data loaded successfully from data.txt");
            }
            else
            {
                Console.WriteLine("File 'data.txt' not found.");
                return;
            }

            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Linear Search");
                Console.WriteLine("2. Jump Search");
                Console.WriteLine("3. Binary Search");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        SearchElement("Linear Search", LinearSearch, data);
                        break;

                    case "2":
                        SearchElement("Jump Search", JumpSearch, data);
                        break;

                    case "3":
                        SearchElement("Binary Search", BinarySearch, data);
                        break;

                    case "4":
                        Console.WriteLine("Exiting");
                        return;

                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }

        private static void SearchElement(string searchName, Func<int[], int, int> searchMethod, int[] data)
        {
            Console.Write("\nEnter the element to search for: ");
            if (int.TryParse(Console.ReadLine(), out int target))
            {
                MeasureAndDisplaySearchTime(searchName, searchMethod, data, target);
            }
            else
            {
                Console.WriteLine("Invalid input");
            }
        }

        private static int LinearSearch(int[] array, int target)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == target)
                    return i;
            }
            return -1;
        }

        private static int JumpSearch(int[] array, int target)
        {
            int n = array.Length;
            int step = (int)Math.Sqrt(n);
            int prev = 0;

            while (array[Math.Min(step, n) - 1] < target)
            {
                prev = step;
                step += (int)Math.Sqrt(n);
                if (prev >= n)
                    return -1;
            }

            for (int i = prev; i < Math.Min(step, n); i++)
            {
                if (array[i] == target)
                    return i;
            }

            return -1;
        }

        private static int BinarySearch(int[] array, int target)
        {
            int left = 0, right = array.Length - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;

                if (array[mid] == target)
                    return mid;

                if (array[mid] < target)
                    left = mid + 1;
                else
                    right = mid - 1;
            }

            return -1;
        }

        private static void MeasureAndDisplaySearchTime(string searchName, Func<int[], int, int> searchMethod, int[] data, int target)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            int result = searchMethod(data, target);
            stopwatch.Stop();

            Console.WriteLine($"\n{searchName}:");
            if (result != -1)
                Console.WriteLine($"Element found at index: {result}");
            else
                Console.WriteLine("Element not found");
            Console.WriteLine($"Time taken: {stopwatch.ElapsedMilliseconds} ms");
        }

    }
}
