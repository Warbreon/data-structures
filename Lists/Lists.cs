namespace Lists
{
    public class LLists
    {
        private const string FilePath = "C:\\Users\\Vlad\\Documents\\Code\\data-structures\\Lists\\data.txt";
        private const string InvalidPositionError = "invalid position try again";

        public static void Execute()
        {
            LinkedList<string?> linkedList = new();

            LoadFileAndFillList(FilePath, linkedList);

            while (true)
            {
                Console.WriteLine("\nSelect something:");
                Console.WriteLine("1. Show all elements");
                Console.WriteLine("2. Add an element to the end");
                Console.WriteLine("3. Insert an element at a specific position");
                Console.WriteLine("4. Remove an element by value");
                Console.WriteLine("5. Remove an element by position");
                Console.WriteLine("6. search for an element by position");
                Console.WriteLine("7. Exit");
                Console.Write("Choose an option: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        PrintList(linkedList);
                        break;

                    case "2":
                        AddElementToTheEnd(linkedList);
                        break;

                    case "3":
                        InsertElementAtPosition(linkedList);
                        break;

                    case "4":
                        RemoveElementByValue(linkedList);
                        break;

                    case "5":
                        RemoveElementByPosition(linkedList);
                        break;

                    case "6":
                        SearchElementByPosition(linkedList);
                        break;

                    case "7":
                        Console.WriteLine("Exiting");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private static void SearchElementByPosition(LinkedList<string?> list)
        {
            Console.Write("\nEnter the position to search: ");
            if (!int.TryParse(Console.ReadLine(), out int index) || index < 0 || index >= list.Count)
            {
                Console.WriteLine(InvalidPositionError);
                return;
            }

            LinkedListNode<string?>? current = list.First;
            for (int i = 0; i < index && current != null; i++)
            {
                current = current.Next;
            }

            if (current != null)
            {
                Console.WriteLine($"Element at position {index}: {current.Value}");
            }
            else
            {
                Console.WriteLine($"No element found");
            }
        }

        private static void RemoveElementByValue(LinkedList<string?> list)
        {
            Console.Write("\nEnter an element to remove: ");
            string? removeElement = Console.ReadLine();

            if (list.Remove(removeElement))
            {
                Console.WriteLine($"Element '{removeElement}' removed from the list");
                UpdateFile(list);
            }
            else
            {
                Console.WriteLine($"Element '{removeElement}' not found in the list");
            }
        }

        private static void RemoveElementByPosition(LinkedList<string?> list)
        {
            Console.Write("\nEnter the position to remove: ");
            if (!int.TryParse(Console.ReadLine(), out int position) || position < 0 || position >= list.Count)
            {
                Console.WriteLine(InvalidPositionError);
                return;
            }

            var current = list.First;
            for (int i = 0; i < position && current != null; i++)
            {
                current = current.Next;
            }

            if (current != null)
            {
                list.Remove(current);
                Console.WriteLine($"Element at position {position} removed from the list");
                UpdateFile(list);
            }
        }

        private static void AddElementToTheEnd(LinkedList<string?> list)
        {
            Console.Write("\nEnter an element to add: ");
            string? newElement = Console.ReadLine();
            list.AddLast(newElement);
            Console.WriteLine($"Element '{newElement}' added to the list");
            UpdateFile(list);
        }

        private static void InsertElementAtPosition(LinkedList<string?> list)
        {
            Console.WriteLine("\nEnter the position to instert at: ");
            if (!int.TryParse(Console.ReadLine(), out int position) || position < 0 || position > list.Count)
            {
                Console.WriteLine(InvalidPositionError);
                return;
            }

            Console.Write("Enter the element to insert: ");
            string? newElement = Console.ReadLine();


            LinkedListNode<string?>? current = list.First;

            for (int i = 0; i < position && current != null; i++)
            {
                current = current.Next;
            }

            if (current != null)
            {
                list.AddBefore(current, newElement);
            }

            Console.WriteLine($"Element '{newElement}' inserted at position {position}");
            UpdateFile(list);
        }

        private static void PrintList(LinkedList<string?> list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("The list is empty");
            }
            else
            {
                Console.WriteLine("\nLinked list elements:");
                int index = 0;
                foreach (var item in list)
                {
                    Console.WriteLine($"{index}: {item}");
                    index++;
                }
            }
        }

        private static void LoadFileAndFillList(string filePath, LinkedList<string?> list)
        {
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    list.AddLast(line);
                }
                Console.WriteLine("Data loaded successfully from data.txt");
            }
            else
            {
                Console.WriteLine("File 'data.txt' not found");
            }
        }

        private static void UpdateFile(LinkedList<string?> list)
        {
            var updatedList = new List<string>();
            foreach (var item in list)
            {
                if (item != null) updatedList.Add(item);
            }
            File.WriteAllLines(FilePath, updatedList);
        }
    }
}