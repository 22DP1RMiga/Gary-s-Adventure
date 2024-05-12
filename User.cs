using System;

class User {
    public string Username;
    public string Checkpoint { get; set; }
    public int Credits { get; set; }
    public int HP { get; set; }
    public int Strength;
    private string filePath;
    public int DamageMinimizer;
    
    public User(string username, string checkpoint, string filePath) {
        Username = username;
        this.Checkpoint = checkpoint;
        
        // If you have NO checkpoint to enter
        if (checkpoint.Contains("0") || string.IsNullOrEmpty(checkpoint) || checkpoint.Contains(" ")) {
            checkpoint = "0";
            Credits = 0;
            HP = 100;
            Strength = 20;
            this.DamageMinimizer = 0;
        } else if (checkpoint == "GTFO") {
            checkpoint = "GTFO";
            Credits = 50;
            HP = 20;
            Strength = 20;
            this.DamageMinimizer = 0;
        }
        
        this.filePath = filePath;
    }
    
    public override string ToString() {
        return $"Hello {this.Username}! You have {this.Credits} credits. Good luck on your adventure!";
    }
}