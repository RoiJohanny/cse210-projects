using System;
using System.Collections.Generic;
using System;
using System.IO;

namespace JournalApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Journal journal = new Journal();

            while (true)
            {
                Console.WriteLine("\nSelect an option:");
                Console.WriteLine("1. Write a new entry");
                Console.WriteLine("2. Display the journal");
                Console.WriteLine("3. Save the journal to a file");
                Console.WriteLine("4. Load the journal from a file");
                Console.WriteLine("5. Exit");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        journal.WriteEntry();
                        break;
                    case 2:
                        journal.DisplayEntries();
                        break;
                    case 3:
                        journal.SaveJournal();
                        break;
                    case 4:
                        journal.LoadJournal();
                        break;
                    case 5:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }
    }

    class Journal
    {
        private string fileName = "journal.csv";
        private string[] prompts = {
            "Who was the most interesting person you interacted with today?",
            "What was the best part of your day?",
            "How did you see the hand of the Lord in your life today?",
            "What was the strongest emotion you felt today?",
            "If you could change one thing about today, what would it be?",
            "Did i help anyone today?"
        };
        private StreamWriter writer;

        public Journal()
        {
            writer = new StreamWriter(fileName, true);
            writer.AutoFlush = true;
        }

        public void WriteEntry()
        {
            Random rnd = new Random();
            string prompt = prompts[rnd.Next(prompts.Length)];

            Console.WriteLine("\n" + prompt);
            string response = Console.ReadLine();
            string date = DateTime.Now.ToShortDateString();

            writer.WriteLine(prompt + "|" + response + "|" + date);

            Console.WriteLine("Entry saved!");
        }

        public void DisplayEntries()
        {
            Console.WriteLine("\nJournal entries:");

            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] fields = line.Split("|");
                    Console.WriteLine(fields[2] + ": " + fields[0]);
                    Console.WriteLine(fields[1] + "\n");
                }
            }
        }

        public void SaveJournal()
        {
            Console.Write("Enter filename to save journal: ");
            string fileName = Console.ReadLine();

            File.Copy(this.fileName, fileName, true);

            Console.WriteLine("Journal saved to file: " + fileName);
        }

        public void LoadJournal()
        {
            Console.Write("Enter filename to load journal: ");
            string fileName = Console.ReadLine();

            if (File.Exists(fileName))
            {
                File.Copy(fileName, this.fileName, true);
                Console.WriteLine("Journal loaded from file: " + fileName);
            }
            else
            {
                Console.WriteLine("File not found");
            }
        }
    }
}
