using System;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Menu {
    private User mainCharacter;
    private string checkpoint;
    private string filePath;
    
    public Menu(User mainCharacter, string Checkpoint, string filePath) {
        this.mainCharacter = mainCharacter;
        this.checkpoint = Checkpoint;
        this.filePath = filePath;
    }
    
    public void ShowMenu(string username) {
        // File path for CSV
        string filePath = "data.csv";
        
        // Writes user data to CSV
        WriteUserDataToCSV(username);
        
        string Menu1 = @"
        ____
       |    |
     __|    |__     <--- Gary
    [          ]
     /‾‾‾‾‾‾‾‾\
    |   0   0  |    <WELCOME TO THE ADVENTURE, TRAVELER!!>
    |     ?    |
    |  .___    |
     \________/
         |
   /‾‾‾‾‾‾‾‾‾‾‾\
  / /|       |\ \
  || |       | ||
  || |       | ||
  () |       | ()
‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾
";

        string Menu2 = @"
        ____
       |    |
     __|    |__
    [          ]
     /‾‾‾‾‾‾‾‾\         [GARY'S ADVENTURE]
    |   0   0  |
    |     ?    |
    |  .___    |        > Play [P]
     \________/         > Shop [S]
         |              > Exit [E]
   /‾‾‾‾‾‾‾‾‾‾‾\
  / /|       |\ \
  || |       | ||
  || |       | ||
  () |       | ()
‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾
";
        
        // Objects and Constructors
        Shop shop = new Shop(mainCharacter, checkpoint, filePath);
        PlayMain play = new PlayMain(mainCharacter, checkpoint, this.filePath);
        
        // Output the first frame
        Console.WriteLine(Menu1);
        
        // Sleep for 2 seconds (2000 milliseconds)
        Thread.Sleep(2000);
        
        // Output the second frame
        Console.Clear();
        Console.WriteLine(Menu2);
        
        // Your input to proceed
        Console.WriteLine(mainCharacter.ToString());
        
        Console.WriteLine("\n<What will you do? For instance, enter Play or P>");
        Console.Write("[YOU] ");
        string input = Console.ReadLine().ToLower();
      
        // Checks if your input is valid
        while (input != "exit" && input != "shop" && input != "play" && input != "e" && input != "s" && input != "p") {
            Console.WriteLine("\n<Invalid command, try again>");
            Console.Write("[YOU] ");
            input = Console.ReadLine();
        }
        
        // THE MAIN REMOTE
        switch(input) {
            
            case "exit":
            case "e":
                Console.Clear();
                Console.WriteLine("Exiting game..");
                Thread.Sleep(2000);
                Console.Clear();
                break;
            
            case "shop":
            case "s":
                Console.Clear();
                shop.ShowShop();
                break;
            
            case "play":
            case "p":
                Console.Clear();
                play.Play_main();
                break;
        }
    }
    
    private void WriteUserDataToCSV(string username) {
        // Writes user data to CSV
        List<string[]> userData = new List<string[]> {
            new string[] {username, checkpoint}
        };
        
        using (StreamWriter writer = new StreamWriter(filePath, true)) {
            foreach (string[] row in userData) {
                writer.WriteLine(string.Join(",", row.Concat(new string[] { mainCharacter.HP.ToString(), mainCharacter.Credits.ToString(), mainCharacter.Strength.ToString() })));
            }
        }
    }
}