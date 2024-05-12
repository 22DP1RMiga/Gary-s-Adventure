using System;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// BOSS #1 - Meet Douglas
class Douglas
{

    // for the main character
    private User user;
    private string checkpoint;
    private int AttackCount;
    private string filePath;
    private int LossCount;

    // for the boss
    private int BOSS_HP;
    private int BOSS_strength;
    public bool isDefeated;

    public Douglas(User user, bool isDefeated, string CHECKPOINT, string filePath)
    {
        this.user = user;
        this.checkpoint = CHECKPOINT;
        this.filePath = filePath;
        this.LossCount = this.user.LossCount;

        this.BOSS_HP = 100;
        this.BOSS_strength = 20;
        this.isDefeated = isDefeated;
        this.AttackCount = 0;
    }

    public void Bossfight_1()
    {

        // Objects and Constructors
        Menu menu = new Menu(this.user, this.checkpoint, filePath);

        Console.Clear();
        string BossStage1 = "";

        // While Douglas ISN'T defeated
        while (BOSS_HP > 0 && this.isDefeated == false)
        {
            Console.Clear();

            BossStage1 = $@"
        ╔══════════════════════════════════════════════════════════════════════════╗
        ║   Credits: {user.Credits,-4}                                                          ║
        ║   HP: {user.HP,-3}/100                        HP: {BOSS_HP,-3}/100                         ║
        ║                                                                          ║
        ║         ____                                                             ║
        ║        |    |                                                            ║
        ║      __|    |__                                                          ║
        ║     [          ]                          /‾‾‾‾‾‾\                       ║
        ║      /‾‾‾‾‾‾‾‾\                          /‾‾‾‾‾‾‾‾\                      ║
        ║     |   0   0  |                        |  0   0   |                     ║
        ║     |     ?    |                        |    ?     |                     ║
        ║     |  .___    |                        |   ―――.   |                     ║
        ║      \________/                          \________/                      ║
        ║          |                                   |                           ║
        ║    /‾‾‾‾‾‾‾‾‾‾‾\                       /‾‾‾‾‾‾‾‾‾‾‾\                     ║
        ║   / /|       |\ \                     / /|       |\ \                    ║
        ║   || |       | ||                     || |       | ||                    ║
        ║   || |       | ||                     || |       | ||                    ║
        ║   () |       | ()                     () |       | ()                    ║
        ║==========================================================================║
        ║ Name: Gary                        | Name: Douglas                        ║
        ║ {user.HP,-3}/100 HP                        | {BOSS_HP,-3}/100 HP                           ║
        ║                                   |                                      ║
        ║ Strength:                         | Strength:                            ║
        ║ {user.Strength,-2} per damage                     | {BOSS_strength,-2} per damage                        ║
        ║                                   |                                      ║
        ╚══════════════════════════════════════════════════════════════════════════╝
                        <Attack - A>        <Power Attack - B>
                                                {AttackCount,-1}/3 remaining
        
            ";
            Console.WriteLine(BossStage1);

            Console.WriteLine("Pick a choice to beat the boss!");
            Console.Write("[YOU] ");
            string choice = Console.ReadLine().ToLower();

            // Checks for valid input
            if (choice != "attack" && choice != "a" && choice != "power attack" && choice != "b")
            {
                Console.Clear();

                Console.WriteLine(BossStage1);
                Console.WriteLine("Pick a choice to beat the boss!");
                Console.Write("[YOU] ");
                choice = Console.ReadLine().ToLower();
            }

            switch (choice)
            {
                case "a":
                    A_choice();
                    break;

                case "b":
                    if (this.AttackCount < 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("You need to attack atleast 3 times to unlock this!");
                        Console.ResetColor();
                        Thread.Sleep(2000);

                    }
                    else if (this.AttackCount >= 3)
                    {
                        B_choice();
                    }

                    break;
            }

            if (BOSS_HP <= 0 && user.HP > 0)
            {
                this.isDefeated = true;
                AfterWin();

            }
            else if (user.HP <= 0)
            {
                AfterLoss();
            }
        }
    }

    private void A_choice()
    {
        BOSS_HP -= user.Strength;
        user.HP -= BOSS_strength;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\nYou attacked DOUGLAS!! His HP dropped by {user.Strength}");
        Thread.Sleep(1500);

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"DOUGLAS gives you -{BOSS_strength} damage");
        Thread.Sleep(2000);

        Console.ResetColor();
        this.AttackCount++;
    }

    private void B_choice()
    {
        user.Strength = user.Strength * 2;

        BOSS_HP -= user.Strength;
        user.HP -= BOSS_strength;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\nYou attacked DOUGLAS!! His HP dropped by {user.Strength}");
        Thread.Sleep(1500);

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"DOUGLAS gives you -{BOSS_strength} damage");
        Thread.Sleep(2000);

        user.Strength = user.Strength / 2;
        this.AttackCount -= 3;

        Console.ResetColor();
    }

    private void AfterWin()
    {
        // The message after beating the boss
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("You defeated DOUGLAS");
        Thread.Sleep(1000);
        Console.WriteLine("You have earned 50 credits!!");
        Thread.Sleep(2000);
        Console.WriteLine("Good luck on your next journey!");
        Console.ResetColor();
        Thread.Sleep(1500);

        // After beating the boss
        // Updates user's Credits before writing to CSV
        this.user.Credits += 50;

        // File path for CSV
        string filePath = "data.csv";

        // This will work if only Douglas is defeated
        if (this.checkpoint == "0")
        {
            this.checkpoint = "GTFO";
        }

        // Updates user's HP and Credits in the CSV data
        List<string[]> userData = ReadFromCSV(filePath);
        userData.RemoveAll(row => row[0] == user.Username); // Removes previous data

        // Adds updated user data
        List<string[]> new_userData = new List<string[]> {
            new string[] { this.user.Username, this.user.Checkpoint, this.user.HP.ToString(), this.user.Credits.ToString(), this.user.Strength.ToString(), this.user.DamageMinimizer.ToString() }
        };

        // Writes updated data back to CSV
        UpdateInCSV();
        WriteToCSV(user.Username);

        PlayMain showmap = new PlayMain(this.user, this.checkpoint, filePath);
        showmap.Boss1Defeated = true;
        showmap.checkpoint = this.checkpoint;

        showmap.Play_main();
    }

    private void AfterLoss()
    {
        // The message after losing to the boss
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("You were defeated..");
        Thread.Sleep(1000);
        Console.WriteLine("DOUGLAS remains in his post..");
        Thread.Sleep(2000);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Better luck next time!");
        Thread.Sleep(1500);
        Console.ResetColor();

        // After losing to the boss
        this.user.HP = 100;
        BOSS_HP = 100;
        AttackCount = 0;
        this.LossCount += 1;
        UpdateInCSV();

        PlayMain showmap = new PlayMain(this.user, this.checkpoint, filePath);
        showmap.Play_main();
    }

    // Writes user data in CSV
    private void WriteToCSV(string username) {
        // Check if the username already exists in the CSV file
        bool userExists = File.ReadLines(filePath).Any(line => line.Split(',')[0] == username);

        // If the user does not exist, write the data to the CSV file
        if (!userExists) {
            using (StreamWriter writer = new StreamWriter(filePath, true)) {
                writer.WriteLine($"{username},{checkpoint},{user.HP},{user.Credits},{user.Strength},{user.DamageMinimizer},{user.LossCount}");
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
    private void UpdateInCSV() {
        // Read existing user data from CSV
        List<string[]> userData = ReadFromCSV(filePath);

        // Find the index of the row that corresponds to the current user
        int index = userData.FindIndex(row => row[0] == user.Username);

        // If the user data exists in the CSV file
        if (index != -1) {
            // Update the existing user data
            userData[index][2] = user.HP.ToString(); // HP
            userData[index][3] = user.Credits.ToString(); // Credits
            userData[index][4] = user.Strength.ToString(); // Strength
            userData[index][5] = user.DamageMinimizer.ToString(); // DamageMinimizer
            userData[index][6] = user.LossCount.ToString(); // LossCount
        } else {
            // Add new user data if not found (this should not happen if the user data is properly initialized)
            userData.Add(new string[] { user.Username, user.Checkpoint, user.HP.ToString(), user.Credits.ToString(), user.Strength.ToString(), user.DamageMinimizer.ToString(), user.LossCount.ToString() });
        }

        // Write updated data back to CSV
        using (StreamWriter writer = new StreamWriter(filePath)) {
            foreach (string[] row in userData) {
                writer.WriteLine(string.Join(",", row));
            }
        }
    }
}