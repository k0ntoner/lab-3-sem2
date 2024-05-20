
using System.Security.Cryptography;
using B_Tree;
using Command;




class Program()
{
    public static void Main(string[] args)
    {
        
        CommandInvoker commandInvoker = new CommandInvoker();
        Console.WriteLine("Select single-threaded or multi-threaded");
        string selection = (Console.ReadLine()).ToLower();
        switch (selection)
        {
            case "single-threaded":
                BTree bTree = new BTree();
                while (true)
                {

                    Console.WriteLine("Enter command (insert [key], delete [key], print, exit):");
                    string inputLine = Console.ReadLine();
                    string[] parts = inputLine.Split(' ');
                    if (parts.Length == 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("------------------Invalid Command------------------");
                        Console.WriteLine();
                        continue;
                    }
                    string commandName = parts[0].ToLower();

                    switch (commandName)
                    {
                        case "insert":
                            if (parts.Length < 2! || !int.TryParse(parts[1], out int InsertKey))
                            {
                                Console.WriteLine();
                                Console.WriteLine("------------------Invalid Command------------------");
                                Console.WriteLine();
                                break;
                            }
                            commandInvoker.RegisterCommand("insert", new InsertCommand(bTree, InsertKey));
                            commandInvoker.ExecuteCommand("insert");
                            break;
                        case "delete":
                            if (parts.Length < 2! || !int.TryParse(parts[1], out int DeleteKey))
                            {
                                Console.WriteLine();
                                Console.WriteLine("------------------Invalid Command------------------");
                                Console.WriteLine();
                                break;
                            }
                            commandInvoker.RegisterCommand("delete", new DeleteCommand(bTree, DeleteKey));
                            commandInvoker.ExecuteCommand("delete");
                            break;
                        case "print":

                            commandInvoker.RegisterCommand("print", new PrintCommand(bTree));
                            commandInvoker.ExecuteCommand("print");
                            break;

                    }
                }
                
            case "multi-threaded":
                BTreeMultiThreaded bTreeBTreeMultiThreaded = new BTreeMultiThreaded();
                while (true)
                {

                    Console.WriteLine("Enter command (insert [key], delete [key], print, exit):");
                    string inputLine = Console.ReadLine();
                    string[] parts = inputLine.Split(' ');
                    if (parts.Length == 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("------------------Invalid Command------------------");
                        Console.WriteLine();
                        continue;
                    }
                    string commandName = parts[0].ToLower();

                    switch (commandName)
                    {
                        case "insert":
                            if (parts.Length < 2! || !int.TryParse(parts[1], out int InsertKey))
                            {
                                Console.WriteLine();
                                Console.WriteLine("------------------Invalid Command------------------");
                                Console.WriteLine();
                                break;
                            }
                            commandInvoker.RegisterCommand("insert", new InsertCommand(bTreeBTreeMultiThreaded, InsertKey));
                            Task.Run(() => commandInvoker.ExecuteCommand("insert"));
                            break;
                        case "delete":
                            if (parts.Length < 2! || !int.TryParse(parts[1], out int DeleteKey))
                            {
                                Console.WriteLine();
                                Console.WriteLine("------------------Invalid Command------------------");
                                Console.WriteLine();
                                break;
                            }
                            commandInvoker.RegisterCommand("delete", new DeleteCommand(bTreeBTreeMultiThreaded, DeleteKey));
                            Task.Run(() => commandInvoker.ExecuteCommand("delete"));
                            break;
                        case "print":

                            commandInvoker.RegisterCommand("print", new PrintCommand(bTreeBTreeMultiThreaded));
                            Task.Run(() => commandInvoker.ExecuteCommand("print"));
                            break;

                    }
                }
        }
        
    }
}
