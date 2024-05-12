using System;
using System.Collections.Generic;
using System.IO;

class User {
    public string Username { get; set; }
    public string Checkpoint { get; set; }
    public int HP { get; set; }
    public int Credits { get; set; }
    public int Strength { get; set; }
    private string filePath;
    public int DamageMinimizer { get; set; }
    public int LossCount { get; set; }

    public User(string username, string checkpoint, int hp, int credits, int strength, int damageMinimizer, int lossCount) {
        Username = username;
        Checkpoint = checkpoint;
        HP = hp;
        Credits = credits;
        Strength = strength;
        DamageMinimizer = damageMinimizer;
        LossCount = lossCount;
    }

    public User(string username, string checkpoint, string filePath) {
        Username = username;
        Checkpoint = checkpoint;
        HP = 100; // Default HP
        Credits = 0; // Default Credits
        Strength = 20; // Default Strength
        DamageMinimizer = 0; // Default DamageMinimizer
        LossCount = 0; // Default LossCount

        this.filePath = filePath;
    }

    public override string ToString() {
        return $"Hello {this.Username}! You have {this.Credits} credits. Good luck on your adventure!";
    }
    
    public List<User> ReadCSVFile(string filePath) {
        List<User> users = new List<User>();

        using (StreamReader reader = new StreamReader(filePath)) {
            // Skip the header row
            reader.ReadLine();

            string line;
            while ((line = reader.ReadLine()) != null) {
                string[] parts = line.Split(',');
                if (parts.Length >= 7) {
                    User user = new User(
                        parts[0],
                        parts[1],
                        int.Parse(parts[2]),
                        int.Parse(parts[3]),
                        int.Parse(parts[4]),
                        int.Parse(parts[5]),
                        int.Parse(parts[6])
                    );
                    users.Add(user);
                }
            }
        }
        return users;
    }
}