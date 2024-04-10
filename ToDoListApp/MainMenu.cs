using System;
using System.Collections.Generic;
using System.IO;

class Task
{
    public string Name { get; set; }
    public DateTime DueDate { get; set; }
    public string Status { get; set; }

    public Task(string name, DateTime dueDate, string status)
    {
        Name = name;
        DueDate = dueDate;
        Status = status;
    }
}

class MainMenu
{
    static List<Task> tasks = new List<Task>();

    static void Main(string[] args)
    {
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("------ To-Do List Menu ------");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. Change Task Status");
            Console.WriteLine("3. Remove Task From List");
            Console.WriteLine("4. Display Tasks");
            Console.WriteLine("5. Save List to File");
            Console.WriteLine("6. Load List from File");
            Console.WriteLine("7. Exit");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddTask();
                    break;
                case "2":
                    ChangeTaskStatus();
                    break;
                case "3":
                    RemoveTask();
                    break;
                case "4":
                    DisplayTasks();
                    break;
                case "5":
                    SaveToFile();
                    break;
                case "6":
                    LoadFromFile();
                    break;
                case "7":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 7.");
                    break;
            }

            Console.WriteLine();
        }
    }

    static void AddTask()
    {
        Console.Write("Enter the task name: ");
        string name = Console.ReadLine();
        DateTime dueDate = DateTime.MinValue; 
        bool validDate = false;

        while (!validDate)
        {
            Console.Write("Enter the due date (yyyy-mm-dd): ");
            string dueDateString = Console.ReadLine();

            try
            {
                dueDate = DateTime.Parse(dueDateString);
                validDate = true;
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid date format. Please enter the due date in yyyy-mm-dd format.");
            }
        }

        Console.Write("Enter the status (Not Started, In Progress, Complete): ");
        string status = Console.ReadLine();
        tasks.Add(new Task(name, dueDate, status));
        Console.WriteLine("Task added successfully.");
    }


    static void RemoveTask()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("The list is empty");
            return;
        }

        Console.WriteLine("Tasks:");
        for (int i = 0; i < tasks.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {tasks[i].Name}");
        }

        Console.Write("Enter the number of the task to remove: ");
        int index;
        if (int.TryParse(Console.ReadLine(), out index) && index > 0 && index <= tasks.Count)
        {
            tasks.RemoveAt(index - 1);
            Console.WriteLine("Task removed successfully.");
        }
        else
        {
            Console.WriteLine("Invalid input.");
        }
    }

    static void DisplayTasks()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("List is empty");
        }
        else
        {
            Console.WriteLine("Tasks:");
            for (int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {tasks[i].Name} - Due: {tasks[i].DueDate.ToShortDateString()} - Status: {tasks[i].Status}");
            }
        }
    }

    static void SaveToFile()
    {
        Console.Write("Enter the file name to save: ");
        string fileName = Console.ReadLine();

        try
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (Task task in tasks)
                {
                    writer.WriteLine($"{task.Name},{task.DueDate},{task.Status}");
                }
            }

            Console.WriteLine("Tasks saved to file successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving tasks to file: {ex.Message}");
        }
    }

    static void LoadFromFile()
    {
        Console.Write("Enter the file name to load: ");
        string fileName = Console.ReadLine();

        if (File.Exists(fileName))
        {
            try
            {
                tasks.Clear();
                using (StreamReader reader = new StreamReader(fileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        string name = parts[0];
                        DateTime dueDate = DateTime.Parse(parts[1]);
                        string status = parts[2];
                        tasks.Add(new Task(name, dueDate, status));
                    }
                }

                Console.WriteLine("Tasks loaded from file successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading tasks from file: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }

    static void ChangeTaskStatus()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks to change status.");
            return;
        }

        Console.WriteLine("Tasks:");
        for (int i = 0; i < tasks.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {tasks[i].Name} - Status: {tasks[i].Status}");
        }

        Console.Write("Enter the number of the task to change status: ");
        int index;
        if (int.TryParse(Console.ReadLine(), out index) && index > 0 && index <= tasks.Count)
        {
            Console.Write("Enter the new status (Not Started, In Progress, Complete): ");
            string newStatus = Console.ReadLine();
            tasks[index - 1].Status = newStatus;
            Console.WriteLine("Task status changed successfully.");
        }
        else
        {
            Console.WriteLine("Invalid input.");
        }
    }
}
