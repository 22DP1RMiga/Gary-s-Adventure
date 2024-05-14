using System;
using System.Threading;

class EndCredits {
    private User mainCharacter;
    private string checkpoint;
    private string filePath;
    
    public EndCredits(User mainCharacter, string Checkpoint, string filePath) {
        this.mainCharacter = mainCharacter;
        this.checkpoint = Checkpoint;
        this.filePath = filePath;
    }
    
    public void ENDCREDITS(string username) {
        
        // maybe will add more here
        Console.Clear();
        
        string EndArt1 = @"
        ____
       |    |
     __|    |__
    [          ]
     /‾‾‾‾‾‾‾‾\
    |   0   0  |        <YOU WIN!!!!!>
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

        string EndArt2 = $@"
        ____
       |    |
     __|    |__
    [          ]
     /‾‾‾‾‾‾‾‾\
    |   0   0  |        <YOU WIN!!!!!>
    |     ?    |        <THANK YOU SO MUCH FOR PLAYING MY GAME, {username}!>
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

        Console.WriteLine(EndArt1);
        Thread.Sleep(2000);
        Console.Clear();
        Console.WriteLine(EndArt2);
        Thread.Sleep(2000);
        
        Console.WriteLine("\n~~~~PROJECT AND SCRIPT MANAGER~~~~");
        Console.WriteLine("> Ralfs Migals");
        
        Thread.Sleep(2500);
        
        Console.WriteLine("\n\n~~~~MAIN CHARACTERS~~~~");
        Console.WriteLine("> Gary");
        Console.WriteLine($"> {username}");
        
        Thread.Sleep(2500);
        
        Console.WriteLine("\n\n~~~~PLAYTESTERS~~~~");
        Console.WriteLine("> Shiina Kochiya");
        Console.WriteLine("> GeneralVN");
        Console.WriteLine("> TruongWF");
        Console.WriteLine("> Daniils Onufrijuks");
        
        Thread.Sleep(2500);
        
        Console.WriteLine("\n\n~~~~BOSSES~~~~");
        Console.WriteLine("> Douglas");
        Console.WriteLine("> Jeremy");
        Console.WriteLine("> Frederick");
        Console.WriteLine("> Asenath");
        Console.WriteLine("> Baldwin");
        
        Thread.Sleep(2500);
        
        Console.WriteLine("\n\n~~~~HONORABLE MENTIONS~~~~");
        Console.WriteLine("> RoltonsLV");
        Console.WriteLine("> RVT audience");
        Console.WriteLine("> Best IT teachers");
        
        Thread.Sleep(2500);
        
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~THANK YOU FOR STICKING OUT!~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        Console.ResetColor();
        
        Thread.Sleep(2500);
        
        Console.Write("\n<Press 'ENTER' to continue..");
        string ENTER = Console.ReadLine();
        
        Console.Clear();
        Menu menu = new Menu(this.mainCharacter, this.checkpoint, this.filePath);
        menu.ShowMenu(username);
        
        
    }
}