using System;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Shop {
    private string checkpoint;
    private string[] potions;
    private string[] products;
    private User user;
    private int DamageMinimizer;
    private string filePath;
    
    public Shop(User user, string Checkpoint, string filePath) {
        this.user = user;
        this.checkpoint = Checkpoint;
        this.DamageMinimizer = this.user.DamageMinimizer;
        
        this.filePath = filePath;
    }
    
    public void ShowShop() {
        // variables
        string[] products;
        string[] potions;
        string input;
        int Credits;
        int HP;
        
        // File path for CSV
        string filePath = "data.csv";
        
        string Shop1 = @"
        
                            <WELCOME TO THE LOCAL SHOP>
        
 _____________                     ________                           _____________
|             |                   /~~~~~~~~\  <--- The Cashier       |             |
|             |                   |  0  0  |                         |             |
|  [PRODUCTS] |                   |  ._    |                         |  [POTIONS]  |
|             |            _______|        |_______                  |             |
| ᴱᵃᶜʰ ᶠᵒʳ ¹⁰ |           |       \________/       |                 | ᴱᵃᶜʰ ᶠᵒʳ ²⁵ |
|   ᶜʳᵉᵈᶦᵗˢ   |           |                        |                 |   ᶜʳᵉᵈᶦᵗˢ   |
|             |           |   |                |   |                 |             |
|             |           |   |                |   |                 |             |
|             |           |   |    ___  ___    |   |                 |             |
|             |           |   |___|   ||   |___|   |                 |             |
|             |           |       |   ||   |       |                 |             |
|_____________|‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾|_____________|
(_)‾‾‾‾‾‾‾‾‾(_)                                                      (_)‾‾‾‾‾‾‾‾‾(_)  ";


        string Shop2 = @"
        
        <TO PICK WHAT TO BUY, CHOOSE THE CATEGORY - PRODUCTS [PR], POTIONS [PO]>
        
 _____________                     ________                           _____________
|             |                   /~~~~~~~~\  <How may I help you?>  |             |
|             |                   |        |                         |             |
|  [PRODUCTS] |                   |  0  0  |                         |  [POTIONS]  |
|             |            _______|  ._    |_______                  |             |
| ᴱᵃᶜʰ ᶠᵒʳ ¹⁰ |           |       \________/       |                 | ᴱᵃᶜʰ ᶠᵒʳ ²⁵ |
|   ᶜʳᵉᵈᶦᵗˢ   |           |                        |                 |   ᶜʳᵉᵈᶦᵗˢ   |
|             |           |   |                |   |                 |             |
|             |           |   |                |   |                 |             |
|             |           |   |    ___  ___    |   |                 |             |
|             |           |   |___|   ||   |___|   |                 |             |
|             |           |       |   ||   |       |                 |             |
|_____________|‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾|_____________|
(_)‾‾‾‾‾‾‾‾‾(_)                                                      (_)‾‾‾‾‾‾‾‾‾(_)
                    <IF YOU WISH TO LEAVE, TYPE EXIT [E]";


        string ShopProducts = @"
        
         <PICK A PRODUCT TO BUY>
         ______________________
        |                      |
        |    Apple [AP] - 10   |
        |                      |
        |   Banana [BA] - 10   |
        |                      |
        |   Cookie [CO] - 10   |
        |                      |
        |    Meat [MA] - 10    |
        |                      |
        |______________________|
        (_)‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾(_)

  <IF YOU WISH TO LEAVE, TYPE EXIT [E]";


        string ShopPotions = @"
        
         <PICK A POTION TO BUY>
         ______________________
        |                      |
        |   Healing [HE] - 25  |
        |                      |
        |  Strength [ST] - 25  |
        |                      |
        |    Luck [LK] - 25    |
        |                      |
        | Protection [PT] - 25 |
        |                      |
        |______________________|
        (_)‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾(_)

  <IF YOU WISH TO LEAVE, TYPE EXIT [E]";
  
        // Welcome To The Local Shop
        Console.WriteLine(Shop1);
        
        // Sleep for 2 seconds (2000 milliseconds)
        Thread.Sleep(2000);
        
        // To pick what to buy, choose the category - products [pr], potions [po]
        Console.Clear();
        Console.WriteLine(Shop2);
        
        Console.WriteLine($"\n{this.user.Credits} credits");
        Console.Write("[YOU] ");
        string choice = Console.ReadLine().ToLower();
        Menu menu = new Menu(user, checkpoint, filePath);
        
        // Before we go to the categories. If exit, then we go to back to Menu
        while (choice != "exit" && choice != "e") {
            while (choice != "products" && choice != "potions" && choice != "pr" && choice != "po") {
                Console.WriteLine("\n<Invalid choice, try again>");
                Console.WriteLine($"{this.user.Credits} credits");
                Console.Write("[YOU] ");
                choice = Console.ReadLine().ToLower();
                
                // Check if the new choice is to exit
                if (choice == "exit" || choice == "e") {
                    break; // Exit the loop if the user wants to leave
                }
            }
            
            // Check if the new choice is to exit
            if (choice == "exit" || choice == "e") {
                break; // Exit the loop if the user wants to leave
            }
            
            // Before we buy any products. If exit, then we go back to the Cashier
            bool validInput = false;
            
            do {
                switch (choice) {
                    case "products":
                    case "pr":
                        Console.Clear();
                        
                        // The products array is in the function LookThroughProducts(input)
                        
                        // Pick a product to buy
                        Console.WriteLine(ShopProducts);
                        Console.WriteLine($"\n{this.user.Credits} credits");
                        Console.Write("[YOU] ");
                        input = Console.ReadLine().ToLower();
                        
                        // The exit case
                        if (input == "exit" || input == "e") {
                            Console.Clear();
                            Console.WriteLine("Exiting products market..");
                            Thread.Sleep(2000);
                            Console.Clear();
                            
                            Console.WriteLine(Shop2);
                            Console.WriteLine($"\n{this.user.Credits} credits");
                            Console.Write("[YOU] ");
                            choice = Console.ReadLine();
                            
                        } else if (input != "exit" || input != "e") {
                            // This checks the valuable input for potions
                            LookThroughProducts(input);
                            
                        } else if (!validInput) {
                            Console.WriteLine("Invalid input. Please try again!");
                            Thread.Sleep(2000);
                            continue;
                        }
                        
                        break;
                        
                    case "potions":
                    case "po":
                        Console.Clear();
                        // The potions array is in the function LookThroughPotions(input)
                        
                        // Pick a potion to buy
                        Console.WriteLine(ShopPotions);
                        Console.WriteLine($"\n{this.user.Credits} credits");
                        Console.Write("[YOU] ");
                        input = Console.ReadLine();
                        
                        
                        // The exit case
                        if (input == "exit" || input == "e") {
                            Console.Clear();
                            Console.WriteLine("Exiting potions market..");
                            Thread.Sleep(2000);
                            Console.Clear();
                            
                            Console.WriteLine(Shop2);
                            Console.WriteLine($"\n{this.user.Credits} credits");
                            Console.Write("[YOU] ");
                            choice = Console.ReadLine();
                            
                        } else if (input != "exit" || input != "e") {
                            // This checks the valuable input for potions
                            LookThroughPotions(input);
                            
                        } else if (!validInput) {
                            Console.WriteLine("Invalid input. Please try again!");
                            Thread.Sleep(2000);
                            continue;
                        }
                        
                        break;
                        
                }
                validInput = true;
            } while (!validInput);
        }
        
        Console.Clear();
        Console.WriteLine("Exiting shop..");
        Thread.Sleep(2000);
        Console.Clear();
        menu.ShowMenu(user.Username);
    }
    
    // For products
    private void LookThroughProducts(string input) {
        
        // Products array
        products = new string[] { "apple", "banana", "cookie", "meat", "ap", "ba", "co", "ma" };
        
        bool validInput = false;
        
        // This checks the valuable input for potions
        foreach (string product in products) {
            if (input == product || input == product.Substring(0, 2)) {
                validInput = true;
                break;
            }
        }
        
        if (user.Credits >= 10 && validInput) {
            switch (input) {
                case "apple":
                case "ap":
                    if (user.HP < 100) {
                        if (user.HP < 90) {
                            user.HP += 10;
                        } else {
                            user.HP = 100;
                        }
                    }
                    break;
                    
                case "banana":
                case "ba":
                    if (user.HP < 100) {
                        if (user.HP < 80) {
                            user.HP += 20;
                        } else {
                            user.HP = 100;
                        }
                    }
                    break;
                    
                case "cookie":
                case "co":
                    if (user.HP < 100) {
                        if (user.HP < 85) {
                            user.HP += 15;
                        } else {
                            user.HP = 100;
                        }
                    }
                    break;
                    
                case "meat":
                case "ma":
                    if (user.HP < 100) {
                        if (user.HP < 80) {
                            user.HP += 20;
                        } else {
                            user.HP = 100;
                        }
                    }
                    break;
                    
            }
            
            // Updates user data in CSV
            UpdateUserDataInCSV();
            
            // Since every product costs 10 credits, this line applies to all cases
            user.Credits -= 10;
            Console.WriteLine($"\nHP: {user.HP}");
            Console.WriteLine($"Strength: {user.Strength}");
            Thread.Sleep(2000);
            Console.WriteLine("Thank you for purchasing the item!");
            
            Thread.Sleep(2000);
            Console.Clear();
            
        } else if (user.Credits < 10 && validInput) {
            Console.WriteLine("You don't have enough credits..");
            Thread.Sleep(2000);
            Console.WriteLine("Defeat the bosses to collect more CREDITS!!");
            Thread.Sleep(2000);
        }
    }
    
    // For potions
    private void LookThroughPotions(string input) {
        
        // Potions array
        potions = new string[] { "healing", "strength", "luck", "protection", "he", "st", "lk", "pt" };
        
        bool validInput = false;
        
        // This checks the valuable input for potions
        foreach (string potion in potions) {
            if (input == potion || input == potion.Substring(0, 2)) {
                validInput = true;
                break;
            }
        }
        
        if (user.Credits >= 25 && validInput) {
            int DamageMinimizer;
            
            switch (input) {
                
                case "healing":
                case "he":
                    if (user.HP < 100) {
                        if (user.HP < 50) {
                            user.HP += 50;
                        } else {
                            user.HP = 100;
                        }
                    }
                    break;
                
                case "strength":
                case "st":
                    user.Strength += 20;
                    break;
                
                case "luck":
                case "lk":
                    Random rand = new Random();
                    
                    // For HP
                    int extra_HP = rand.Next(1, 11);     // user Health Points: +1, +.. or +10
                    int extra_Strength = rand.Next(1, 11);     // user Strength: +1, +.. or +10
                    
                    // For Strength
                    user.HP += extra_HP;
                    user.Strength += extra_Strength;
                    break;
                
                case "protection":
                case "pt":
                    // Further stuff will be covered below this case
                    break;
            }
            
            // Updates user data in CSV
            UpdateUserDataInCSV();
    
            // Since every potion costs 25 credits, this line applies to all cases
            user.Credits -= 25;
            Console.WriteLine($"\nHP: {user.HP}");
            Console.WriteLine($"Strength: {user.Strength}");
            
            if (input == "protection" || input == "pt") {
                user.DamageMinimizer += 10;
                Console.WriteLine($"Protection: +{this.DamageMinimizer}");
            }
            
            Thread.Sleep(2000);
            Console.WriteLine("Thank you for purchasing the item!");
            
            Thread.Sleep(2000);
            Console.Clear();
            
        } else if (user.Credits < 25 && validInput) {
            Console.WriteLine("You don't have enough credits..");
            Thread.Sleep(2000);
            Console.WriteLine("Defeat the bosses to collect more CREDITS!!");
            Thread.Sleep(2000);
        }
    }
    
    // Writes user data in CSV
    void WriteToCSV(string filePath, List<string[]> data, int HP, int Credits, int Strength) {
        
        using (StreamWriter writer = new StreamWriter(filePath)) {
            
            foreach (string[] row in data) {
                writer.WriteLine(string.Join(",", row.Concat(new string[] { user.HP.ToString(), user.Credits.ToString(), user.Strength.ToString() })));
            }
        }
    }
    
    // Reads user data from CSV
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
    
    // Updates user data in CSV
    private void UpdateUserDataInCSV() {
        // File path for CSV
        string filePath = "data.csv";
    
        // Read existing user data from CSV
        List<string[]> userData = ReadFromCSV(filePath);
    
        // Remove previous data
        userData.RemoveAll(row => row[0] == user.Username);
    
        // Add updated user data
        userData.Add(new string[] { user.Username, user.Checkpoint, user.HP.ToString(), user.Credits.ToString(), user.Strength.ToString() });
    
        // Write updated data back to CSV
        WriteToCSV(filePath, userData, user.HP, user.Credits, user.Strength);
    }
}