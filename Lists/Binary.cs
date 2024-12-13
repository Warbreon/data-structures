using static Lists.BinaryTree;

namespace Lists
{
    public class Binary
    {
        private const string FilePath = "C:\\Users\\Vlad\\Documents\\Code\\data-structures\\Lists\\data2.txt";

        public static void Execute()
        {
            BinaryTree tree = new();

            if (File.Exists(FilePath))
            {
                var lines = File.ReadAllLines(FilePath);
                foreach (var line in lines)
                {
                    if (int.TryParse(line, out int value))
                        tree.Add(value);
                }
                Console.WriteLine("Data loaded into the tree");
            }
            else
            {
                Console.WriteLine("File not found");
            }

            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Add an element");
                Console.WriteLine("2. Search for an element");
                Console.WriteLine("3. Remove an element");
                Console.WriteLine("4. In-order traversal");
                Console.WriteLine("5. Pre-order traversal");
                Console.WriteLine("6. Post-order traversal");
                Console.WriteLine("7. Exit");
                Console.Write("Choose an option: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddElement(tree);
                        break;

                    case "2":
                        SearchElement(tree);
                        break;

                    case "3":
                        RemoveElement(tree);
                        break;

                    case "4":
                        Console.WriteLine("\nIn-order traversal:");
                        InOrderTraversal(tree.Root);
                        Console.WriteLine();
                        break;

                    case "5":
                        Console.WriteLine("\nPre-order traversal:");
                        PreOrderTraversal(tree.Root);
                        Console.WriteLine();
                        break;

                    case "6":
                        Console.WriteLine("\nPost-order traversal:");
                        PostOrderTraversal(tree.Root);
                        Console.WriteLine();
                        break;

                    case "7":
                        Console.WriteLine("Exiting...");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private static void AddElement(BinaryTree tree)
        {
            Console.Write("Enter an integer to add: ");
            if (int.TryParse(Console.ReadLine(), out int value))
            {
                tree.Add(value);
                Console.WriteLine($"Element {value} added.");
                UpdateFile(tree);
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter an integer.");
            }
        }

        private static void SearchElement(BinaryTree tree)
        {
            Console.Write("Enter an integer to search: ");
            if (int.TryParse(Console.ReadLine(), out int value))
            {
                if (tree.Search(value))
                {
                    Console.WriteLine($"Element {value} found in the tree.");
                }
                else
                {
                    Console.WriteLine($"Element {value} not found in the tree.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter an integer.");
            }
        }

        private static void RemoveElement(BinaryTree tree)
        {
            Console.Write("Enter an integer to remove: ");
            if (int.TryParse(Console.ReadLine(), out int value))
            {
                tree.Remove(value);
                Console.WriteLine($"Element {value} removed.");
                UpdateFile(tree);
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter an integer.");
            }
        }

        private static void UpdateFile(BinaryTree tree)
        {
            using (StreamWriter writer = new(FilePath))
            {
                WriteInOrderToFile(tree.Root, writer);
            }
            Console.WriteLine("File updated.");
        }

        private static void WriteInOrderToFile(BinaryTree.Node? node, StreamWriter writer)
        {
            if (node != null)
            {
                WriteInOrderToFile(node.Left, writer);
                writer.WriteLine(node.Value);
                WriteInOrderToFile(node.Right, writer);
            }
        }
    }

    public class BinaryTree
    {
        public class Node
        {
            public int Value;
            public Node? Left;
            public Node? Right;

            public Node(int value)
            {
                Value = value;
                Left = null;
                Right = null;
            }
        }

        public Node? Root;

        public BinaryTree()
        {
            Root = null;
        }

        public void Add(int value)
        {
            Root = AddRecursively(Root, value);
        }

        private static Node AddRecursively(Node? node, int value)
        {
            if (node == null)
                return new Node(value);

            if (value < node.Value)
                node.Left = AddRecursively(node.Left, value);
            else if (value > node.Value)
                node.Right = AddRecursively(node.Right, value);

            return node;
        }

        public bool Search(int value)
        {
            return SearchRecursively(Root, value);
        }

        private static bool SearchRecursively(Node? node, int value)
        {
            if (node == null)
                return false;

            if (value == node.Value)
                return true;

            return value < node.Value
                ? SearchRecursively(node.Left, value)
                : SearchRecursively(node.Right, value);
        }

        public void Remove(int value)
        {
            Root = RemoveRecursively(Root, value);
        }

        private static Node? RemoveRecursively(Node? node, int value)
        {
            if (node == null)
                return null;

            if (value < node.Value)
            {
                node.Left = RemoveRecursively(node.Left, value);
            }
            else if (value > node.Value)
            {
                node.Right = RemoveRecursively(node.Right, value);
            }
            else
            {
                if (node.Left == null)
                    return node.Right;
                if (node.Right == null)
                    return node.Left;

                node.Value = FindMinValue(node.Right);
                node.Right = RemoveRecursively(node.Right, node.Value);
            }

            return node;
        }

        private static int FindMinValue(Node node)
        {
            int minValue = node.Value;
            while (node.Left != null)
            {
                minValue = node.Left.Value;
                node = node.Left;
            }
            return minValue;
        }

        public static void InOrderTraversal(Node? node)
        {
            if (node != null)
            {
                InOrderTraversal(node.Left);
                Console.Write($"{node.Value} ");
                InOrderTraversal(node.Right);
            }
        }

        public static void PreOrderTraversal(Node? node)
        {
            if (node != null)
            {
                Console.Write($"{node.Value} ");
                PreOrderTraversal(node.Left);
                PreOrderTraversal(node.Right);
            }
        }

        public static void PostOrderTraversal(Node? node)
        {
            if (node != null)
            {
                PostOrderTraversal(node.Left);
                PostOrderTraversal(node.Right);
                Console.Write($"{node.Value} ");
            }
        }

    }
}
