using System;
using System.Collections.Generic;

namespace ConsoleToDo
{
    public class Task
    {
        public int tID { get; set; }
        public string task { get; set; }

        public bool complete = false;

        public string GetTask()
        {
            string status = "[X]";

            if (!complete)
            {
                status = "[ ]";
            }

            return String.Format("{0,-4}{1,-40}{2}", tID, task.ToString(), status);
        }

        public void CompleteToggle()
        {
            complete = !complete;
        }
    }

    class Program
    {
        List<Task> tasks = new List<Task>();

        static void Main(string[] args)
        {            
            (new Program()).Run();
        }

        public void Run()
        {
            TestTasks();

            ConsoleKey input;

            do
            {
                Console.Clear();
                PrintHeader();
                PrintTasks();
                PrintMenu();

                input = GetInput();
                switch (input)
                {
                    case ConsoleKey.N:
                        NewTask();
                        break;
                    case ConsoleKey.D:
                        DelTask();
                        break;
                    case ConsoleKey.T:
                        ToggleStatus();
                        break;
                }

            } while (input != ConsoleKey.Q);
        }

        public int ShowtIDPrompt(int promptType)
        {
            string msg = "";
            int tID;

            switch (promptType)
            {
                case 1:
                    msg = "you wish to toggle";
                    break;
                case 2:
                    msg = "you wish to delete";
                    break;
            }

            Console.Write($"Enter tID of task {msg}. Enter 0 to cancel operation: ");

            bool isInt = int.TryParse(Console.ReadLine(), out tID);

            if (isInt)
            {
                return tID;
            } else
            {
                return 0;
            }

        }
        
        public void ToggleStatus()
        {
            int tID = ShowtIDPrompt(1);

            if (tID != 0)
            {
                try
                {
                    tasks[tasks.FindIndex(s => s.tID == tID)].CompleteToggle();
                } catch (Exception e)
                {
                    Console.WriteLine("Something went wrong...");
                }
            }
        }

        public void DelTask()
        {
            int tID = ShowtIDPrompt(2);

            if (tID != 0)
            {
                try
                {
                    tasks.Remove(tasks[tasks.FindIndex(s => s.tID == tID)]);

                    FixtIDs(tID);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Something went wrong...");
                }
            }
        }

        public void FixtIDs(int tID)
        {
            foreach (Task t in tasks)
            {
                if (t.tID > tID)
                {
                    t.tID--;
                }
            }
        }

        public ConsoleKey GetInput()
        {
            return Console.ReadKey(true).Key;
        }

        public void NewTask()
        {
            Console.WriteLine("Any task over 40 characters long will be truncated.");
            Console.Write("Enter task: ");

            string task = Console.ReadLine();

            if (task.Length > 40)
            {
                task = task.Substring(0, 37) + "...";
            }

            NewTask(task);
        }

        public void NewTask(string task)
        {
            tasks.Add(new Task() { tID = tasks.Count + 1, task = task });
        }

        public void TestTasks()
        {     
                NewTask("Test1");
                NewTask("Test2");
                NewTask("Test3");
                NewTask("Test4");
                NewTask("Test5");
        }

        private void PrintHeader()
        {
            Console.WriteLine("########################");
            Console.WriteLine("The Amazing Task Master!");
            Console.WriteLine("########################");
            Console.WriteLine("");
        }

        private void PrintTasks()
        {
            Console.WriteLine(String.Format("{0,-4}{1,-40}{2}", "tID", "Task", "Sts"));
            Console.WriteLine(String.Format("{0,-4}{1,-40}{2}", "---", "----", "---"));
            foreach (Task t in tasks)
            {
                Console.WriteLine(t.GetTask());
            }
        }

        private void PrintMenu()
        {
            Console.WriteLine("");
            Console.WriteLine("[N]ew, [D]elete, [T]oggle Status, [Q]uit");
        }
    }
}