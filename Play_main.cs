using System;
using System.Threading;

class PlayMain {
    private User user;
    public bool Boss1Defeated;
    public bool Boss2Defeated;
    public bool Boss3Defeated;
    public bool Boss4Defeated;
    public bool Boss5Defeated;
    public string checkpoint;
    private string filePath;
    
    public PlayMain(User user, string Checkpoint, string filePath) {
        this.user = user;
        this.Boss1Defeated = false;
        this.Boss2Defeated = false;
        this.Boss3Defeated = false;
        this.Boss4Defeated = false;
        this.Boss5Defeated = false;
        this.checkpoint = Checkpoint;
        this.filePath = filePath;
    }
    
    public void Play_main() {
        Console.Clear();

        // Objects and Constructors
        Menu menu = new Menu(this.user, this.checkpoint, filePath);
        User mc = new User(this.user.Username, this.checkpoint, filePath);
        
        string player_HP = $"{this.user.HP} HP";
        string player_CREDITS = $"{this.user.Credits} CREDITS";
        
        Console.WriteLine("PLEASE STAND BY: WE ARE CHECKING YOUR CHECKPOINT");
        Thread.Sleep(4000);
        
        // Show map depending on the CHECKPOINT
        switch (checkpoint) {
            case "0":
                Console.Write("Checkpoint set to ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("DEFAULT..");
                Thread.Sleep(2000);
                Console.ResetColor();
                Console.WriteLine("Loading game..");
                Thread.Sleep(2000);
                Console.Clear();
                ShowMap();
                
                // string player_HP = $"{this.user.HP} HP";
                // string player_CREDITS = $"{this.user.Credits} CREDITS";
                
                Console.WriteLine($"{this.user.Username}:   {player_HP},  {player_CREDITS}");
                Console.Write("[YOU] ");
                string startAnswer = Console.ReadLine().ToLower();
                
                // Checks the valid reply before the game starts
                while (startAnswer != "f" && startAnswer != "e" && startAnswer != "fight" && startAnswer != "exit") {
                    Console.Clear();
                    ShowMap();
                    
                    Console.WriteLine($"{this.user.Username}:   {player_HP},  {player_CREDITS}");
                    Console.Write("[YOU] ");
                    startAnswer = Console.ReadLine().ToLower();
                }
                
                // Valid value -> Exit case
                if (startAnswer == "exit" || startAnswer == "e") {
                    Console.Clear();
                    Console.WriteLine("Exiting the map..");
                    Thread.Sleep(2000);
                    Console.Clear();
                    menu.ShowMenu(user.Username);
                
                // The conditions if you want to FIGHT
                } else if (startAnswer == "fight" || startAnswer == "f") {
                    
                    // First boss: DOUGLAS
                    if (Boss1Defeated != true) {
                        Douglas douglas = new Douglas(mc, false, checkpoint, this.filePath);
                        douglas.Bossfight_1();
                    } 
                }
                
                // NO CHECKPOINT - case ender
                break;
            
            // If you have a checkpoint
            default:
                Console.Write("Using checkpoint: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{checkpoint}..");
                Thread.Sleep(2000);
                Console.ResetColor();
                Console.Clear();
                
                LookThroughCheckpoints(checkpoint);
                ShowMap();
                
                Console.WriteLine($"Checkpoint: {this.checkpoint}");
                Console.WriteLine($"{this.user.Username}:   {player_HP},  {player_CREDITS}");
                Console.Write("[YOU] ");
                startAnswer = Console.ReadLine().ToLower();
                
                // Checks the valid reply before the game starts
                if (checkpoint != "PHVM") {
                    while (startAnswer != "f" && startAnswer != "e" && startAnswer != "fight" && startAnswer != "exit") {
                        Console.Clear();
                        ShowMap();
                        
                        Console.WriteLine($"{this.user.Username}:   {player_HP},  {player_CREDITS}");
                        Console.Write("[YOU] ");
                        startAnswer = Console.ReadLine().ToLower();
                    }
                } else if (checkpoint == "PHVM") {
                    while (startAnswer != "end" && startAnswer != "e" && startAnswer != "see end credits" && startAnswer != "exit") {
                        Console.Clear();
                        ShowMap();
                        
                        Console.WriteLine($"{this.user.Username}:   {player_HP},  {player_CREDITS}");
                        Console.Write("[YOU] ");
                        startAnswer = Console.ReadLine().ToLower();
                    }
                }
                
                // Valid value -> Exit case
                switch (startAnswer) {
                    case "exit":
                    case "e":
                        Console.Clear();
                        Console.WriteLine("Exiting the map..");
                        Thread.Sleep(2000);
                        Console.Clear();
                        menu.ShowMenu(user.Username);
                        break;
                        
                    case "fight":
                    case "f":
                        // Second boss: JEREMY
                        if (Boss1Defeated && checkpoint == "GTFO") {
                            Jeremy jeremy = new Jeremy(mc, false, checkpoint, this.filePath);
                            jeremy.Bossfight_2();
                        
                        // Third boss: FREDERICK
                        } else if (Boss2Defeated && checkpoint == "RAHG") {
                            Frederick frederick = new Frederick(mc, false, checkpoint, this.filePath);
                            frederick.Bossfight_3();
                        
                        // Fourth boss: ASENATH
                        } else if (Boss3Defeated && checkpoint == "TRMX") {
                            Asenath asenath = new Asenath(mc, false, checkpoint, this.filePath);
                            asenath.Bossfight_4();
                        
                        // Fifth boss: JOHN?
                        } else if (Boss4Defeated && checkpoint == "ASYR") {
                            Baldwin baldwin = new Baldwin(mc, false, checkpoint, this.filePath);
                            baldwin.Bossfight_5();
                        }
                        
                        break;
                        
                    case "see end credits":
                    case "end":
                        EndCredits end = new EndCredits(mc, checkpoint, filePath);
                        end.ENDCREDITS(mc.Username);
                        
                        break;
                }
                
                // WITH CHECKPOINT - case ender
                break;
        }
    }
    
    // Very crucial for showing a map and fighting a certain boss later
    public void LookThroughCheckpoints(string checkpoint) {
        string[] checkpoints = {"0", "GTFO", "RAHG", "TRMX", "ASYR", "PHVM"};
        
        switch (checkpoint) {
            case "GTFO":
                Boss1Defeated = true;
                break;
            
            case "RAHG":
                Boss1Defeated = true;
                Boss2Defeated = true;
                break;
            
            case "TRMX":
                Boss1Defeated = true;
                Boss2Defeated = true;
                Boss3Defeated = true;
                break;
                
            case "ASYR":
                Boss1Defeated = true;
                Boss2Defeated = true;
                Boss3Defeated = true;
                Boss4Defeated = true;
                break;
            
            case "PHVM":
                Boss1Defeated = true;
                Boss2Defeated = true;
                Boss3Defeated = true;
                Boss4Defeated = true;
                Boss5Defeated = true;
                break;
        }
    }
    
    // Now 
    public void ShowMap() {
        // if no boss is defeated
        if (Boss1Defeated == false) {
            Console.WriteLine(Map1());
        
        // if Douglas is defeated
        } else if (Boss1Defeated && checkpoint == "GTFO") {
            Console.WriteLine(Map2());
        
        // if Jeremy is defeated
        } else if (Boss2Defeated && checkpoint == "RAHG") {
            Console.WriteLine(Map3());
        
        // if ... is defeated
        } else if (Boss3Defeated && checkpoint == "TRMX") {
            Console.WriteLine(Map4());
        
        // if ... is defeated
        } else if (Boss4Defeated && checkpoint == "ASYR") {
            Console.WriteLine(Map5());
        
        // if ... is defeated
        } else if (Boss5Defeated && checkpoint == "PHVM") {
            Console.WriteLine(Map6());
            
        }
    }
    
    // https://www.piliapp.com/symbol/line/
    // If NO BOSS is defeated
    public string Map1() {
        string startQuestion = "\n                              < FIGHT THE BOSS [F] >          < EXIT [E] >";
        return $@"
╔═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗
║                                                                                        __   __    __    __   __   ║
║                                                                                       │  |_|  |__|  |__|  |_|  |  ║
║                                                                                       │                        │  ║
║                                    OOO                     OOO                        │                        │  ║
║                                   OOOOO ╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼ OOOOO                       │                        │  ║
║                                    OOO                     OOO                      _-│                        │  ║
║                                     ╽                       ╽                    _-‾  │                        │  ║
║                                     ╽                       ╽                _――‾     │                        │  ║
║                                     ╽                       ╽              _-‾        │                        │  ║
║                                     ╽                       ╽          _――‾          -│________________________│  ║
║   /‾‾‾‾‾‾‾‾\                        ╽                       ╽        _-‾          _=‾             OOO             ║
║   |‾‾‾‾‾‾‾‾|                        ╽                    ┌─────┐  _=‾          _-‾               OOOOO            ║
║   |        |                        ╽                _―――│ ≡≡≡ │‾‾   ┈       =‾       /\          OOO        ^    ║
║   |    _   |                        ╽              _-‾   │ ≡≡≡ │   ┈┈   ┈ _-‾        /  \          ╽        /_\   ║
║   |   | |  |                       OOO          _=‾   ┈┈ │ ≡≡≡ │ ┈┈    _=‾          /    \         ╽              ║
║   |___|_|__| ╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼ OOOOO       _=‾  ┈┈    │ ≡≡≡ │   _――‾            /      \        ╽   /\         ║
║                                    OOO     __―‾  ┈   ┈┈  │ ≡≡≡ │―‾‾               /        \       ╽  /__\        ║
║                                        _――‾    ┈┈    _―‾‾│ ≡≡≡ │        /\       /          \      ╽              ║
║_______________________――――――――‾‾‾‾‾‾‾‾‾  ┈┈     ┈  =‾    │ ≡≡≡ │       /  \     /____________\     ╽              ║
║ ┈┈        ┈┈┈┈        ┈┈┈┈       ┈┈    ┈     ┈ __-‾      └─────┘      /    \                       ╽      /\      ║
║      ┈┈       ┈┈    ┈       ┈┈      ┈┈┈   _=‾‾‾             ╽        /______\                      ╽     /  \     ║
║__________________________―――――――――‾‾‾‾‾‾‾‾                  ╽                                      ╽    /    \    ║
║                                                             ╽                                      ╽   /______\   ║
║                                                            OOO                                     ╽              ║
║                                                           OOOOO ╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼               ║
║                                                            OOO                                                    ║
║                                                                                                                   ║
╚═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝
{startQuestion}
";
    }
    // if BOSS 1 is defeated
    public string Map2() {
        string startQuestion = "\n                              < FIGHT THE BOSS [F] >          < EXIT [E] >";
        return $@"
╔═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗
║                                                                                        __   __    __    __   __   ║
║                                                                                       │  |_|  |__|  |__|  |_|  |  ║
║                                                                                       │                        │  ║
║                                    OOO                     OOO                        │                        │  ║
║                                   OOOOO ╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼ OOOOO                       │                        │  ║
║                                    OOO                     OOO                      _-│                        │  ║
║                                     ╽                       ╽                    _-‾  │                        │  ║
║                                     ╽                       ╽                _――‾     │                        │  ║
║                                     ╽                       ╽              _-‾        │                        │  ║
║                                     ╽                       ╽          _――‾          -│________________________│  ║
║   /‾‾‾‾‾‾‾‾\                        ╽                       ╽        _-‾          _=‾             OOO             ║
║   |‾‾‾‾‾‾‾‾|                        ╽                    ┌─────┐  _=‾          _-‾               OOOOO            ║
║   |        |                        ╽                _―――│ ≡≡≡ │‾‾   ┈       =‾       /\          OOO        ^    ║
║   |    _   |                        ╽              _-‾   │ ≡≡≡ │   ┈┈   ┈ _-‾        /  \          ╽        /_\   ║
║   |   | |  |                       OOO          _=‾   ┈┈ │ ≡≡≡ │ ┈┈    _=‾          /    \         ╽              ║
║   |___|_|__| ╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼ O   O       _=‾  ┈┈    │ ≡≡≡ │   _――‾            /      \        ╽   /\         ║
║                                    OOO     __―‾  ┈   ┈┈  │ ≡≡≡ │―‾‾               /        \       ╽  /__\        ║
║                                        _――‾    ┈┈    _―‾‾│ ≡≡≡ │        /\       /          \      ╽              ║
║_______________________――――――――‾‾‾‾‾‾‾‾‾  ┈┈     ┈  =‾    │ ≡≡≡ │       /  \     /____________\     ╽              ║
║ ┈┈        ┈┈┈┈        ┈┈┈┈       ┈┈    ┈     ┈ __-‾      └─────┘      /    \                       ╽      /\      ║
║      ┈┈       ┈┈    ┈       ┈┈      ┈┈┈   _=‾‾‾             ╽        /______\                      ╽     /  \     ║
║__________________________―――――――――‾‾‾‾‾‾‾‾                  ╽                                      ╽    /    \    ║
║                                                             ╽                                      ╽   /______\   ║
║                                                            OOO                                     ╽              ║
║                                                           OOOOO ╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼               ║
║                                                            OOO                                                    ║
║                                                                                                                   ║
╚═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝
{startQuestion}
";
    }
    // if BOSS 1-2 are defeated
    public string Map3() {
        string startQuestion = "\n                              < FIGHT THE BOSS [F] >          < EXIT [E] >";
        return $@"
╔═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗
║                                                                                        __   __    __    __   __   ║
║                                                                                       │  |_|  |__|  |__|  |_|  |  ║
║                                                                                       │                        │  ║
║                                    OOO                     OOO                        │                        │  ║
║                                   O   O ╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼ OOOOO                       │                        │  ║
║                                    OOO                     OOO                      _-│                        │  ║
║                                     ╽                       ╽                    _-‾  │                        │  ║
║                                     ╽                       ╽                _――‾     │                        │  ║
║                                     ╽                       ╽              _-‾        │                        │  ║
║                                     ╽                       ╽          _――‾          -│________________________│  ║
║   /‾‾‾‾‾‾‾‾\                        ╽                       ╽        _-‾          _=‾             OOO             ║
║   |‾‾‾‾‾‾‾‾|                        ╽                    ┌─────┐  _=‾          _-‾               OOOOO            ║
║   |        |                        ╽                _―――│ ≡≡≡ │‾‾   ┈       =‾       /\          OOO        ^    ║
║   |    _   |                        ╽              _-‾   │ ≡≡≡ │   ┈┈   ┈ _-‾        /  \          ╽        /_\   ║
║   |   | |  |                       OOO          _=‾   ┈┈ │ ≡≡≡ │ ┈┈    _=‾          /    \         ╽              ║
║   |___|_|__| ╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼ O   O       _=‾  ┈┈    │ ≡≡≡ │   _――‾            /      \        ╽   /\         ║
║                                    OOO     __―‾  ┈   ┈┈  │ ≡≡≡ │―‾‾               /        \       ╽  /__\        ║
║                                        _――‾    ┈┈    _―‾‾│ ≡≡≡ │        /\       /          \      ╽              ║
║_______________________――――――――‾‾‾‾‾‾‾‾‾  ┈┈     ┈  =‾    │ ≡≡≡ │       /  \     /____________\     ╽              ║
║ ┈┈        ┈┈┈┈        ┈┈┈┈       ┈┈    ┈     ┈ __-‾      └─────┘      /    \                       ╽      /\      ║
║      ┈┈       ┈┈    ┈       ┈┈      ┈┈┈   _=‾‾‾             ╽        /______\                      ╽     /  \     ║
║__________________________―――――――――‾‾‾‾‾‾‾‾                  ╽                                      ╽    /    \    ║
║                                                             ╽                                      ╽   /______\   ║
║                                                            OOO                                     ╽              ║
║                                                           OOOOO ╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼               ║
║                                                            OOO                                                    ║
║                                                                                                                   ║
╚═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝
{startQuestion}
";
    }
    // if BOSS 1-3 are defeated
    public string Map4() {
        string startQuestion = "\n                              < FIGHT THE BOSS [F] >          < EXIT [E] >";
        return $@"
╔═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗
║                                                                                        __   __    __    __   __   ║
║                                                                                       │  |_|  |__|  |__|  |_|  |  ║
║                                                                                       │                        │  ║
║                                    OOO                     OOO                        │                        │  ║
║                                   O   O ╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼ O   O                       │                        │  ║
║                                    OOO                     OOO                      _-│                        │  ║
║                                     ╽                       ╽                    _-‾  │                        │  ║
║                                     ╽                       ╽                _――‾     │                        │  ║
║                                     ╽                       ╽              _-‾        │                        │  ║
║                                     ╽                       ╽          _――‾          -│________________________│  ║
║   /‾‾‾‾‾‾‾‾\                        ╽                       ╽        _-‾          _=‾             OOO             ║
║   |‾‾‾‾‾‾‾‾|                        ╽                    ┌─────┐  _=‾          _-‾               OOOOO            ║
║   |        |                        ╽                _―――│ ≡≡≡ │‾‾   ┈       =‾       /\          OOO        ^    ║
║   |    _   |                        ╽              _-‾   │ ≡≡≡ │   ┈┈   ┈ _-‾        /  \          ╽        /_\   ║
║   |   | |  |                       OOO          _=‾   ┈┈ │ ≡≡≡ │ ┈┈    _=‾          /    \         ╽              ║
║   |___|_|__| ╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼ O   O       _=‾  ┈┈    │ ≡≡≡ │   _――‾            /      \        ╽   /\         ║
║                                    OOO     __―‾  ┈   ┈┈  │ ≡≡≡ │―‾‾               /        \       ╽  /__\        ║
║                                        _――‾    ┈┈    _―‾‾│ ≡≡≡ │        /\       /          \      ╽              ║
║_______________________――――――――‾‾‾‾‾‾‾‾‾  ┈┈     ┈  =‾    │ ≡≡≡ │       /  \     /____________\     ╽              ║
║ ┈┈        ┈┈┈┈        ┈┈┈┈       ┈┈    ┈     ┈ __-‾      └─────┘      /    \                       ╽      /\      ║
║      ┈┈       ┈┈    ┈       ┈┈      ┈┈┈   _=‾‾‾             ╽        /______\                      ╽     /  \     ║
║__________________________―――――――――‾‾‾‾‾‾‾‾                  ╽                                      ╽    /    \    ║
║                                                             ╽                                      ╽   /______\   ║
║                                                            OOO                                     ╽              ║
║                                                           OOOOO ╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼               ║
║                                                            OOO                                                    ║
║                                                                                                                   ║
╚═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝
{startQuestion}
";
    }
    // if BOSS 1-4 are defeated
    public string Map5() {
        string startQuestion = "\n                              < FIGHT THE BOSS [F] >          < EXIT [E] >";
        return $@"
╔═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗
║                                                                                        __   __    __    __   __   ║
║                                                                                       │  |_|  |__|  |__|  |_|  |  ║
║                                                                                       │                        │  ║
║                                    OOO                     OOO                        │                        │  ║
║                                   O   O ╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼ O   O                       │                        │  ║
║                                    OOO                     OOO                      _-│                        │  ║
║                                     ╽                       ╽                    _-‾  │                        │  ║
║                                     ╽                       ╽                _――‾     │                        │  ║
║                                     ╽                       ╽              _-‾        │                        │  ║
║                                     ╽                       ╽          _――‾          -│________________________│  ║
║   /‾‾‾‾‾‾‾‾\                        ╽                       ╽        _-‾          _=‾             OOO             ║
║   |‾‾‾‾‾‾‾‾|                        ╽                    ┌─────┐  _=‾          _-‾               OOOOO            ║
║   |        |                        ╽                _―――│ ≡≡≡ │‾‾   ┈       =‾       /\          OOO        ^    ║
║   |    _   |                        ╽              _-‾   │ ≡≡≡ │   ┈┈   ┈ _-‾        /  \          ╽        /_\   ║
║   |   | |  |                       OOO          _=‾   ┈┈ │ ≡≡≡ │ ┈┈    _=‾          /    \         ╽              ║
║   |___|_|__| ╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼ O   O       _=‾  ┈┈    │ ≡≡≡ │   _――‾            /      \        ╽   /\         ║
║                                    OOO     __―‾  ┈   ┈┈  │ ≡≡≡ │―‾‾               /        \       ╽  /__\        ║
║                                        _――‾    ┈┈    _―‾‾│ ≡≡≡ │        /\       /          \      ╽              ║
║_______________________――――――――‾‾‾‾‾‾‾‾‾  ┈┈     ┈  =‾    │ ≡≡≡ │       /  \     /____________\     ╽              ║
║ ┈┈        ┈┈┈┈        ┈┈┈┈       ┈┈    ┈     ┈ __-‾      └─────┘      /    \                       ╽      /\      ║
║      ┈┈       ┈┈    ┈       ┈┈      ┈┈┈   _=‾‾‾             ╽        /______\                      ╽     /  \     ║
║__________________________―――――――――‾‾‾‾‾‾‾‾                  ╽                                      ╽    /    \    ║
║                                                             ╽                                      ╽   /______\   ║
║                                                            OOO                                     ╽              ║
║                                                           O   O ╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼               ║
║                                                            OOO                                                    ║
║                                                                                                                   ║
╚═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝
{startQuestion}
";
    }
    // if BOSS 1-5 are defeated
    public string Map6() {
        string startQuestion = "\n                              < SEE END CREDITS [END] >          < EXIT [E] >";
        return $@"
╔═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗
║                                                                                        __   __    __    __   __   ║
║                                                                                       │  |_|  |__|  |__|  |_|  |  ║
║                                                                                       │                        │  ║
║                                    OOO                     OOO                        │                        │  ║
║                                   O   O ╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼ O   O                       │                        │  ║
║                                    OOO                     OOO                      _-│                        │  ║
║                                     ╽                       ╽                    _-‾  │                        │  ║
║                                     ╽                       ╽                _――‾     │                        │  ║
║                                     ╽                       ╽              _-‾        │                        │  ║
║                                     ╽                       ╽          _――‾          -│________________________│  ║
║   /‾‾‾‾‾‾‾‾\                        ╽                       ╽        _-‾          _=‾             OOO             ║
║   |‾‾‾‾‾‾‾‾|                        ╽                    ┌─────┐  _=‾          _-‾               O   O            ║
║   |        |                        ╽                _―――│ ≡≡≡ │‾‾   ┈       =‾       /\          OOO        ^    ║
║   |    _   |                        ╽              _-‾   │ ≡≡≡ │   ┈┈   ┈ _-‾        /  \          ╽        /_\   ║
║   |   | |  |                       OOO          _=‾   ┈┈ │ ≡≡≡ │ ┈┈    _=‾          /    \         ╽              ║
║   |___|_|__| ╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼ O   O       _=‾  ┈┈    │ ≡≡≡ │   _――‾            /      \        ╽   /\         ║
║                                    OOO     __―‾  ┈   ┈┈  │ ≡≡≡ │―‾‾               /        \       ╽  /__\        ║
║                                        _――‾    ┈┈    _―‾‾│ ≡≡≡ │        /\       /          \      ╽              ║
║_______________________――――――――‾‾‾‾‾‾‾‾‾  ┈┈     ┈  =‾    │ ≡≡≡ │       /  \     /____________\     ╽              ║
║ ┈┈        ┈┈┈┈        ┈┈┈┈       ┈┈    ┈     ┈ __-‾      └─────┘      /    \                       ╽      /\      ║
║      ┈┈       ┈┈    ┈       ┈┈      ┈┈┈   _=‾‾‾             ╽        /______\                      ╽     /  \     ║
║__________________________―――――――――‾‾‾‾‾‾‾‾                  ╽                                      ╽    /    \    ║
║                                                             ╽                                      ╽   /______\   ║
║                                                            OOO                                     ╽              ║
║                                                           O   O ╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼╼               ║
║                                                            OOO                                                    ║
║                                                                                                                   ║
╚═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝
{startQuestion}
";
    }
}