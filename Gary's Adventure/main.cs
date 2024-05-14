using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;


class Program
{

    static void Main()
    {
        // Clears all previous text
        Console.Clear();

        // Shows start screen
        ShowStart();

        // File path for CSV
        string filePath = "data.csv";

        // Checks if data.csv exists, if not, create it
        if (!File.Exists(filePath))
        {
            // Creates a new CSV file with headers
            List<string[]> initialData = new List<string[]> {
                new string[] {"Username", "Checkpoint", "HP", "Credits", "Strength", "DamageMinimizer", "LossCount"},
            };
            WriteToCSV(filePath, initialData);
        }

        Console.WriteLine("<Enter your USERNAME>");
        Console.Write("[YOU] ");
        string username = Console.ReadLine();

        // Checks if the username is faulty
        while (string.IsNullOrEmpty(username) || Regex.IsMatch(username, @"^\d+$") || Regex.IsMatch(username, @"[^a-zA-Z0-9]"))
        {
            Console.Clear();
            ShowStart();
            Console.ForegroundColor = ConsoleColor.Red;
            
            Console.Write("<Enter your username again ");

            // Checks if the username is empty or only contains numbers
            if (string.IsNullOrEmpty(username) || Regex.IsMatch(username, @"^\d+$")) {
                Console.WriteLine("(cannot be empty or contain only numbers)>");

            // Checks if the username contains any unnecessary characters
            } else if (Regex.IsMatch(username, @"[^a-zA-Z0-9]")) {
                Console.WriteLine("(cannot contain any special characters or spaces)>");
            }
            
            Console.ResetColor();
            Console.Write("[YOU] ");
            username = Console.ReadLine();
        }

        // Checks if the entered username exists in the CSV file
        List<string[]> existingData = ReadFromCSV(filePath);
        bool usernameExists;
        string confirm = "";
        do
        {
            usernameExists = existingData.Any(row => row[0] == username);

            if (usernameExists)
            {
                while (confirm != "yes" && confirm != "y" && confirm != "no" && confirm != "n")
                {
                    Console.Clear();
                    Console.WriteLine($"<Username '{username}' already exists. Are you {username}? (yes/no)");
                    Console.Write("[YOU] ");
                    confirm = Console.ReadLine().ToLower();
                }

                if (confirm == "yes" || confirm == "y")
                {
                    break;

                }
                else if (confirm == "no" || confirm == "n")
                {
                    Console.Clear();
                    ShowStart();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("<Enter your username again>");
                    Console.ResetColor();
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

        if (confirm == "yes" || confirm == "y")
        {
            // If the user is the same person, no need to enter checkpoint
            string[] currentRow = existingData.FirstOrDefault(row => row[0] == username);
            if (currentRow != null)
            {
                checkpoint = currentRow[1];
                HealthPoints = currentRow[2];
                credits = currentRow[3];
                strength = currentRow[4];
                DM = currentRow[5];

                // Admin rights
                if (usernameExists && username == "RoltonsLV")
                {
                    Console.WriteLine("\n<Enter your checkpoint>");
                    Console.Write("[YOU] ");
                    checkpoint = Console.ReadLine().ToUpper();

                    // Existing checkpoints
                    string[] checkpoints = { "0", "GTFO", "RAHG", "TRMX", "ASYR", "PHVM" };

                    // This checks the valuable input for checkpoints
                    Checkpoint reader = new Checkpoint(username, checkpoints, filePath);

                    // Checks for valid input
                    while (!reader.LookThroughCheckpoints(checkpoint) || checkpoint.Contains(" "))
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("<Enter your checkpoint again>");
                        Console.ResetColor();
                        Console.Write("[YOU] ");
                        checkpoint = Console.ReadLine().ToUpper();
                        reader = new Checkpoint(username, checkpoints, filePath);
                    }
                }

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("User not found in the CSV file.");
                Console.ResetColor();
            }
        }
        else
        {
            // If the user is different, then enter checkpoint
            if (!usernameExists)
            {
                checkpoint = "0";
            }
        }

        // Firsthand default value check
        if (checkpoint == "0" || string.IsNullOrEmpty(checkpoint))
        {
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
        int LossCount;

        if (checkpoint == "GTFO")
        {
            Douglas douglas = new Douglas(mc, true, checkpoint, filePath);
            mc.HP = 20;
            mc.Credits = 50;
            mc.Strength = 20;
            mc.DamageMinimizer = 0;

            // Find the index of the row that corresponds to the current user
            int index = existingData.FindIndex(row => row[0] == mc.Username);

            if (index != -1)
            {
                if (int.TryParse(existingData[index][5], out int losscount))
                {
                    mc.LossCount = losscount;
                    LossCount = losscount;
                }
            }
        }

        // Updates registry
        UpdateInCSV(mc, existingData);

        // Shows Menu
        menu.ShowMenu(username);

        // Writes user data to CSV
        List<string[]> userData = new List<string[]> {
            new string[] {username, checkpoint, mc.HP.ToString(), mc.Credits.ToString(), mc.Strength.ToString(), mc.DamageMinimizer.ToString()}
        };

        WriteToCSV(filePath, userData);
    }


    static void WriteToCSV(string filePath, List<string[]> data)
    {

        using (StreamWriter writer = new StreamWriter(filePath))
        {

            foreach (string[] row in data)
            {
                writer.WriteLine(string.Join(",", row)); ;
            }
        }
    }

    static List<string[]> ReadFromCSV(string filePath)
    {

        List<string[]> data = new List<string[]>();

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] row = line.Split(',');
                data.Add(row);
            }
        }
        return data;
    }

    // Updates user data in CSV
    static void UpdateInCSV(User user, List<string[]> userData)
    {
        // File path for CSV
        string filePath = "data.csv";

        // Read existing user data from CSV
        userData = ReadFromCSV(filePath);

        // Find the index of the row that corresponds to the current user
        int index = userData.FindIndex(row => row[0] == user.Username);

        // If the user data exists in the CSV file
        if (index != -1)
        {
            // Update the existing user data
            if (userData[index].Length > 6)
            { // Check if the array has enough elements
                userData[index][1] = user.Checkpoint; // Checkpoint
                userData[index][2] = user.HP.ToString(); // HP
                userData[index][3] = user.Credits.ToString(); // Credits
                userData[index][4] = user.Strength.ToString(); // Strength
                userData[index][5] = user.DamageMinimizer.ToString(); // DamageMinimizer
                userData[index][6] = user.LossCount.ToString(); // LossCount
            }
            else
            {
                // Add new elements to the array to support indexing up to 6
                while (userData[index].Length <= 6)
                {
                    userData[index] = userData[index].Concat(new string[] { "" }).ToArray();
                }
                // Update the user data
                userData[index][1] = user.Checkpoint; // Checkpoint
                userData[index][2] = user.HP.ToString(); // HP
                userData[index][3] = user.Credits.ToString(); // Credits
                userData[index][4] = user.Strength.ToString(); // Strength
                userData[index][5] = user.DamageMinimizer.ToString(); // DamageMinimizer
                userData[index][6] = user.LossCount.ToString(); // LossCount
            }
        }
        else
        {
            // Add new user data if not found (this should not happen if the user data is properly initialized)
            userData.Add(new string[] { user.Username, user.Checkpoint, user.HP.ToString(), user.Credits.ToString(), user.Strength.ToString(), user.DamageMinimizer.ToString(), user.LossCount.ToString() });
        }

        // Write updated data back to CSV
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (string[] row in userData)
            {
                writer.WriteLine(string.Join(",", row));
            }
        }
    }

    static void ShowStart()
    {
        string StartScreen = $@"
                                                                                                                  ____
                                                                                                                 |    |
                                                                                                               __|    |__
 __      __       .__                                  __                                                     [          ] 
/  \    /  \ ____ |  |   ____  ____   _____   ____   _/  |_  ____                                              /‾‾‾‾‾‾‾‾\ 
\   \/\/   // __ \|  | _/ ___\/  _ \ /     \_/ __ \  \   __\/  _ \                                            |   0   0  |
 \        /\  ___/|  |_\  \__(  <_> )  Y Y  \  ___/   |  | (  <_> )                                           |     ?    |
  \__/\  /  \___  >____/\___  >____/|__|_|  /\___  >  |__|  \____/                                            |  .___    |
       \/       \/          \/            \/     \/                                                            \________/
  ________                     /\         _____       .___                    __                                   |    
 /  _____/_____ _______ ___.__.)/_____   /  _  \    __| _/__  __ ____   _____/  |_ __ _________   ____       /‾‾‾‾‾‾‾‾‾‾‾\
/   \  ___\__  \\_  __ <   |  |/  ___/  /  /_\  \  / __ |\  \/ // __ \ /    \   __\  |  \_  __ \_/ __ \     / /|       |\ \
\    \_\  \/ __ \|  | \/\___  |\___ \  /    |    \/ /_/ | \   /\  ___/|   |  \  | |  |  /|  | \/\  ___/     || |       | ||
 \______  (____  /__|   / ____/____  > \____|__  /\____ |  \_/  \___  >___|  /__| |____/ |__|    \___  >    || |       | ||
        \/     \/       \/         \/          \/      \/           \/     \/                        \/     () |       | ()
_______________________________________________________________________________________________________________|_______|____________
        ";

        Console.WriteLine(StartScreen);
    }
}