using System;
using System.Threading;
using System.IO;
using System.Linq;
using System.Collections.Generic;
class Program {
    
    static void Main() {
        // Clears all previous text
        Console.Clear();

        // File path for CSV
        string filePath = "data.csv";

        // Checks if data.csv exists, if not, create it
        if (!File.Exists(filePath)) {

            // Creates a new CSV file with headers
            List<string[]> initialData = new List<string[]> {
                new string[] {"Username", "Checkpoint", "HP", "Credits", "Strength", "DamageMinimizer"},
            };
            WriteToCSV(filePath, initialData);
        }

        Console.WriteLine("<Enter your USERNAME>");
        Console.Write("[YOU] ");
        string username = Console.ReadLine();

        // Checks if the username is empty
        while (string.IsNullOrEmpty(username)) {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("<Enter your username again>");
            Console.ResetColor();
            Console.Write("[YOU] ");
            username = Console.ReadLine();
        }

        // Checks if the entered username exists in the CSV file
        List<string[]> existingData = ReadFromCSV(filePath);
        bool usernameExists;
        string confirm = "";
        do {
            usernameExists = existingData.Any(row => row[0] == username);

            if (usernameExists) {
                while (confirm != "yes" && confirm != "y" && confirm != "no" && confirm != "n") {
                    Console.Clear();
                    Console.WriteLine($"<Username '{username}' already exists. Are you {username}? (yes/no)");
                    Console.Write("[YOU] ");
                    confirm = Console.ReadLine().ToLower();
                }

                if (confirm == "yes" || confirm == "y") {
                    break;

                } else if (confirm == "no" || confirm == "n") {
                    Console.Clear();
                    Console.WriteLine("<Enter your username again>");
                    Console.Write("[YOU] ");
                    username = Console.ReadLine();
                    existingData = ReadFromCSV(filePath);
                }
            }
        } while (usernameExists);

        string checkpoint = "";
        string HealthPoints = "";
        string credits = "";
        string strength = "";
        string DM = "";
        
        if (confirm == "yes" || confirm == "y") {
            // If the user is the same person, no need to enter checkpoint
            string[] currentRow = existingData.FirstOrDefault(row => row[0] == username);
            if (currentRow != null) {
                checkpoint = currentRow[1];
                HealthPoints = currentRow[2];
                credits = currentRow[3];
                strength = currentRow[4];
                DM = currentRow[5];
                
            } else {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("User not found in the CSV file.");
                Console.ResetColor();
            }
        } else {
            // If the user is different, then enter checkpoint
            string[] checkpoints = {"0", "GTFO", "RAHG", "TRMX", "ASYR", "PHVM"};

            Console.WriteLine("\n<Enter your checkpoint>");
            Console.Write("[YOU] ");
            checkpoint = Console.ReadLine().ToUpper();

            // This checks the valuable input for checkpoints
            Checkpoint reader = new Checkpoint(username, checkpoints, filePath);

            // Checks for valid input
            while (!reader.LookThroughCheckpoints(checkpoint)) {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("<Enter your checkpoint again>");
                Console.ResetColor();
                Console.Write("[YOU] ");
                checkpoint = Console.ReadLine().ToUpper();
                reader = new Checkpoint(username, checkpoints, filePath);
            }
        }

        if (checkpoint.Contains("0") || string.IsNullOrEmpty(checkpoint) || checkpoint.Contains(" ")) {
            checkpoint = "0";
        }

        Console.Clear();  // Clears after the input

        // The part in which useful user info is stored
        User mc = new User(username, checkpoint, filePath);     // Congrats, you're the main character
        Shop shop = new Shop(mc, checkpoint, filePath);
        Menu menu = new Menu(mc, checkpoint, filePath);

        // Conditions which take place after getting a certain checkpoint
        PlayMain play = new PlayMain(mc, checkpoint, filePath);

        // Headstart data registration
        mc.HP = int.Parse(HealthPoints);
        mc.Credits = int.Parse(credits);
        mc.Strength = int.Parse(strength);
        mc.DamageMinimizer = int.Parse(DM);
        
        if (checkpoint == "GTFO") {
            Douglas douglas = new Douglas(mc, true, checkpoint, filePath);
            mc.HP = 20;
            mc.Credits = 50;
            mc.Strength = 20;
            mc.DamageMinimizer = 0;
        }

        // Shows Menu
        menu.ShowMenu(username);

        // Writes user data to CSV
        List<string[]> userData = new List<string[]> {
            new string[] {username, checkpoint, mc.HP.ToString(), mc.Credits.ToString(), mc.Strength.ToString(), mc.DamageMinimizer.ToString()}
        };

        WriteToCSV(filePath, userData);
    }

    static void WriteToCSV(string filePath, List<string[]> data) {

        using (StreamWriter writer = new StreamWriter(filePath)) {

            foreach (string[] row in data) {
                writer.WriteLine(string.Join(",", row));;
            }
        }
    }

    static List<string[]> ReadFromCSV(string filePath) {

        List<string[]> data = new List<string[]>();

        using (StreamReader reader = new StreamReader(filePath)) {
            string line;

            while ((line = reader.ReadLine()) != null) {
                string[] row = line.Split(',');
                data.Add(row);
            }
        }
        return data;
    }
}