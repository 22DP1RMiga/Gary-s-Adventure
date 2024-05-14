using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

class Leaderboard {
    
    private User user;
    private string checkpoint;
    private string filePath;
    
    public Leaderboard (User user, string checkpoint, string filePath) {
        this.user = user;
        this.checkpoint = checkpoint;
        this.filePath = filePath;
    }

    public void showLeaderboard() {
        Menu menu = new Menu(this.user, this.checkpoint, filePath);
        User user = new User(this.user.Username, this.checkpoint, this.filePath);
        List<User> users = user.ReadCSVFile(this.filePath);

        // Default leaderboard
        LossCount(users);
            Console.WriteLine("\n\n CHOOSE:    <LOSS COUNT [LC]>        <STRENGTH [ST]>        <CREDITS [C]>        <PROTECTION [PR]>       <EXIT [E]>");

        Console.Write("\n[YOU] ");
        string input = Console.ReadLine().ToLower();

        // Shows leaderboard if not exiting
        while (input != "exit" && input != "e") {
            Console.Clear();

            if (input == "loss count" || input == "lc") {
                Console.Clear();
                LossCount(users);
                
            } else if (input == "strength" || input == "st") {
                Console.Clear();
                Strength(users);

            } else if (input == "credits" || input == "c") {
                Console.Clear();
                Credits(users);
                
            } else if (input == "protection" || input == "pr") {
                Console.Clear();
                Protection(users);
                
            }

            Console.WriteLine("\n\n CHOOSE:    <LOSS COUNT [LC]>        <STRENGTH [ST]>        <CREDITS [C]>        <PROTECTION [PR]>       <EXIT [E]>");
            Console.Write("\n[YOU] ");
            input = Console.ReadLine().ToLower();
        }

        Console.Clear();
        Console.WriteLine("Exiting the leaderboard..");
        Thread.Sleep(2000);
        Console.Clear();
        menu.ShowMenu(user.Username);
    }

    // For Least Losses
    private void LossCount(List<User> users) {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("~~~~~~~~~~~~~~~~LEADERBOARD (WITH LEAST LOSSES)~~~~~~~~~~~~~~~~");
        Console.ResetColor();

        var sortedUsers = users.OrderBy(u => u.LossCount).ToList();

        int rank = 1;

        foreach (var u in sortedUsers) {
            Console.WriteLine($"{rank} - {u.Username} ({u.LossCount} losses)");
            rank++;
        }
    }

    // For Highest Strength
    private void Strength(List<User> users) {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("~~~~~~~~~~~~~~~~LEADERBOARD (WITH MOST STRENGTH)~~~~~~~~~~~~~~~~");
        Console.ResetColor();

        var sortedUsers = users.OrderByDescending(u => u.Strength).ToList();

        int rank = 1;

        foreach (var u in sortedUsers) {
            Console.WriteLine($"{rank} - {u.Username} (Strength: {u.Strength})");
            rank++;
        }
    }

    // For Most Credits
    private void Credits(List<User> users) {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("~~~~~~~~~~~~~~~~LEADERBOARD (WITH MOST CREDITS)~~~~~~~~~~~~~~~~");
        Console.ResetColor();

        var sortedUsers = users.OrderByDescending(u => u.Credits).ToList();

        int rank = 1;

        foreach (var u in sortedUsers) {
            Console.WriteLine($"{rank} - {u.Username} ({u.Credits} credits)");
            rank++;
        }
    }

    // For Highest Protection
    private void Protection(List<User> users) {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("~~~~~~~~~~~~~~~~LEADERBOARD (WITH MOST PROTECTION])~~~~~~~~~~~~~~~~");
        Console.ResetColor();

        var sortedUsers = users.OrderByDescending(u => u.DamageMinimizer).ToList();

        int rank = 1;

        foreach (var u in sortedUsers) {
            Console.WriteLine($"{rank} - {u.Username} (+{u.DamageMinimizer})");
            rank++;
        }
    }
}