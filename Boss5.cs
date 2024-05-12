using System;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// BOSS #5 - Meet Baldwin
class Baldwin {
    
    // for the main character
    private User user;
    private string checkpoint;
    private int AttackCount;
    private string filePath;
    
    // temporary data in case you lose
    private int temp_HP;
    private int temp_Credits;
    private int temp_Strength;
    
    // for the boss
    private int BOSS_HP;
    private int BOSS_strength;
    public bool isDefeated;
    public int DamageMinimizer;
    
    public Baldwin(User user, bool isDefeated, string CHECKPOINT, string filePath) {
        this.user = user;
        this.checkpoint = CHECKPOINT;
        this.filePath = filePath;
        DamageMinimizer += this.user.DamageMinimizer;
        
        this.BOSS_HP = 500;
        this.BOSS_strength = 50 - DamageMinimizer;
        this.isDefeated = isDefeated;
        this.AttackCount = 0;
        
        this.temp_HP = this.user.HP;
        this.temp_Credits = this.user.Credits;
        this.temp_Strength = this.user.Strength;
    }
    
    public void Bossfight_5() {

        // Objects and Constructors
        Menu menu = new Menu(this.user, this.checkpoint, this.filePath);
        
        // Reads user's HP and Credits from the CSV file
        List<string[]> userData = ReadFromCSV(filePath);
        string[] currentUserData = userData.FirstOrDefault(row => row[0] == user.Username);
        if (currentUserData != null && currentUserData.Length >= 4) {
            int parsedHP;
            int parsedCredits;
            int parsedStrength;
            if (int.TryParse(currentUserData[2], out parsedHP)) {
                user.HP = parsedHP;
                temp_HP = user.HP;
            }
            if (int.TryParse(currentUserData[3], out parsedCredits)) {
                user.Credits = parsedCredits;
                temp_Credits = user.Credits;
            }
            if (int.TryParse(currentUserData[4], out parsedStrength)) {
                user.Strength = parsedStrength;
                temp_Strength = user.Strength;
            }
        }
    
        Console.Clear();
        string BossStage1 = "";
        
        // While Baldwin ISN'T defeated
        while (BOSS_HP > 0 && this.isDefeated == false) {
            Console.Clear();
            
            BossStage1 = $@"
        ╔══════════════════════════════════════════════════════════════════════════╗
        ║   Credits: {user.Credits,-4}                                                          ║
        ║   HP: {user.HP,-3}/100                        HP: {BOSS_HP,-3}/500                         ║
        ║                                                                          ║
        ║         ____                                                             ║
        ║        |    |                                                            ║
        ║      __|    |__                                                          ║
        ║     [          ]                           /‾‾‾‾‾‾‾\                     ║
        ║      /‾‾‾‾‾‾‾‾\                          /‾‾<>‾‾‾‾\ \                    ║
        ║     |   0   0  |                        |  T  T    | |                   ║
        ║     |     ?    |                        |    <     | |                   ║
        ║     |  .___    |                        |   ―――.   | |                   ║
        ║      \________/                          \________/| |                   ║
        ║          |                                   ||    \_/                   ║
        ║    /‾‾‾‾‾‾‾‾‾‾‾\                        /‾‾‾\||/‾‾‾‾\                    ║
        ║   / /|       |\ \                      / /|  ‾‾   |\ \                   ║
        ║   || |       | ||                      || |       | ||                   ║
        ║   || |       | ||                      || |       | ||                   ║
        ║   () |       | ()                      () |       | ()                   ║
        ║==========================================================================║
        ║ Name: Gary                        | Name: Baldwin                        ║
        ║ {user.HP,-3}/100 HP                        | {BOSS_HP,-3}/500 HP                           ║
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
            if (choice != "attack" && choice != "a" && choice != "power attack" && choice != "b") {
                Console.Clear();
                
                Console.WriteLine(BossStage1);
                Console.WriteLine("Pick a choice to beat the boss!");
                Console.Write("[YOU] ");
                choice = Console.ReadLine().ToLower();
            }
            
            switch (choice) {
                case "a":
                    A_choice();
                    break;
                    
                case "b":
                    if (this.AttackCount < 3) {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("You need to attack atleast 3 times to unlock this!");
                        Console.ResetColor();
                        Thread.Sleep(2000);
                        
                    } else if (this.AttackCount >= 3) {
                        B_choice();
                    }
                    
                    break;
            }
            
            if (BOSS_HP <= 0 && user.HP > 0) {
                this.isDefeated = true;
                AfterWin();
                EndCredits end = new EndCredits(this.user, this.checkpoint, this.filePath);
                end.ENDCREDITS(this.user.Username);
                
            } else if (user.HP <= 0) {
                AfterLoss();
            }
        }
    }
    
    private void A_choice() {
        BOSS_HP -= user.Strength;
        user.HP -= BOSS_strength;
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\nYou attacked BALDWIN!! His HP dropped by {user.Strength}");
        Thread.Sleep(1500);
        
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"BALDWIN gives you -{BOSS_strength} damage");
        Thread.Sleep(2000);
        
        Console.ResetColor();
        this.AttackCount++;
    }
    
    private void B_choice() {
        user.Strength = user.Strength * 2;
        
        BOSS_HP -= user.Strength;
        user.HP -= BOSS_strength;
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\nYou attacked BALDWIN!! His HP dropped by {user.Strength}");
        Thread.Sleep(1500);
        
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"BALDWIN gives you -{BOSS_strength} damage");
        Thread.Sleep(2000);
        
        user.Strength = user.Strength / 2;
        this.AttackCount -= 3;
        
        Console.ResetColor();
    }
    
    private void AfterWin() {
        // The message after beating the boss
        Console.Clear();
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("You defeated BALDWIN");
        Thread.Sleep(1000);
        Console.WriteLine("You have earned 200 credits!!");
        Thread.Sleep(2000);
        Console.WriteLine("Good luck on your next journey!");
        Console.ResetColor();
        Thread.Sleep(1500);
        
        // After beating the boss
        this.user.Credits += 250;
        
        // File path for CSV
        string filePath = "data.csv";
        
        // This will work if only Douglas is defeated
        if (this.checkpoint == "ASYR") {
            this.checkpoint = "PHVM";
        }
        
        // Updates user's HP and Credits in the CSV data
        List<string[]> userData = ReadFromCSV(filePath);
        userData.RemoveAll(row => row[0] == user.Username); // Removes previous data

        // Adds updated user data
        List<string[]> new_userData = new List<string[]> {
            new string[] { this.user.Username, this.user.Checkpoint, this.user.HP.ToString(), this.user.Credits.ToString(), this.user.Strength.ToString(), this.user.DamageMinimizer.ToString() }
        };

        // Temporary variables to hold HP and Credits values
        int tempHP = user.HP;
        int tempCredits = user.Credits;
        int tempStrength = user.Strength;

        // Write updated data back to CSV
        userData.RemoveAll(row => row[0] == user.Username); // Remove previous data
        WriteToCSV(filePath, new_userData);
    
        // Update user's HP and Credits after writing to CSV
        user.Credits += 200;
    
        PlayMain showmap = new PlayMain(this.user, this.checkpoint, filePath);
        showmap.Boss1Defeated = true;
        showmap.checkpoint = this.checkpoint;
        
        showmap.Play_main();
    }
    
    private void AfterLoss() {
        // The message after losing to the boss
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("You were defeated..");
        Thread.Sleep(1000);
        Console.WriteLine("BALDWIN remains in his post..");
        Thread.Sleep(2000);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("25 CREDITS compensated for losing");
        Thread.Sleep(2000);
        Console.WriteLine("Better luck next time!");
        Thread.Sleep(1500);
        Console.ResetColor();

        // After losing to the boss
        this.user.HP = temp_HP;
        this.user.Credits += 25;
        BOSS_HP = 500;
        AttackCount = 0;

        PlayMain showmap = new PlayMain(this.user, this.checkpoint, filePath);
        showmap.Play_main();
    }
    
    void WriteToCSV(string filePath, List<string[]> data) {
        using (StreamWriter writer = new StreamWriter(filePath)) {
            foreach (string[] row in data) {
                writer.WriteLine(string.Join(",", row));
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