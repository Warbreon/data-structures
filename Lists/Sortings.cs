using System;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lists
{
    public class Sortings
    {
        private const string FilePath = "C:\\Users\\Vlad\\Documents\\Code\\data-structures\\Lists\\data3.txt";

        public static void Execute()
        {
            int[] data;

            if (File.Exists(FilePath))
            {
                var lines = File.ReadAllLines(FilePath);
                data = Array.ConvertAll(lines, int.Parse);
                Console.WriteLine("Data loaded");
            }
            else
            {
                Console.WriteLine("File not found");
                return;
            }

            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Insertion Sort");
                Console.WriteLine("2. Bubble Sort");
                Console.WriteLine("3. Selection Sort");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        MeasureAndDisplaySortTime("Insertion Sort", InsertionSort, data);
                        break;

                    case "2":
                        MeasureAndDisplaySortTime("Bubble Sort", BubbleSort, data);
                        break;

                    case "3":
                        MeasureAndDisplaySortTime("Selection Sort", SelectionSort, data);
                        break;

                    case "4":
                        Console.WriteLine("Exiting...");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }

        }

        private static int[] InsertionSort(int[] array)
        {
            int[] sorted = (int[])array.Clone();
            for (int i = 1; i < sorted.Length; i++)
            {
                int key = sorted[i];
                int j = i - 1;

                while (j >= 0 && sorted[j] > key)
                {
                    sorted[j + 1] = sorted[j];
                    j--;
                }
                sorted[j + 1] = key;
            }
            return sorted;
        }

        private static int[] BubbleSort(int[] array)
        {
            int[] sorted = (int[])array.Clone();
            for (int i = 0; i < sorted.Length - 1; i++)
            {
                for (int j = 0; j < sorted.Length - i - 1; j++)
                {
                    if (sorted[j] > sorted[j + 1])
                    {
                        int temp = sorted[j];
                        sorted[j] = sorted[j + 1];
                        sorted[j + 1] = temp;
                    }
                }
            }
            return sorted;
        }

        private static int[] SelectionSort(int[] array)
        {
            int[] sorted = (int[])array.Clone();
            for (int i = 0; i < sorted.Length - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < sorted.Length; j++)
                {
                    if (sorted[j] < sorted[minIndex])
                    {
                        minIndex = j;
                    }
                }
                int temp = sorted[minIndex];
                sorted[minIndex] = sorted[i];
                sorted[i] = temp;
            }
            return sorted;
        }

        private static void MeasureAndDisplaySortTime(string sortName, Func<int[], int[]> sortMethod, int[] data)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            int[] sortedData = sortMethod(data);
            stopwatch.Stop();

            Console.WriteLine($"\n{sortName}:");
            Console.WriteLine($"Time taken: {stopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine("Sorted data: " + string.Join(", ", sortedData));
        }


    }
}
