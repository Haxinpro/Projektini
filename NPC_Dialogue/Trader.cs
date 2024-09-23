using System;
using System.Collections.Generic;
using System.Net;

namespace Game
{
    public class Trader : Npc
    {
        private Dictionary<string, Ware> Wares { get; set; }
        private ReputationManager reputationManager;
        private bool hasGreeted;

        public Trader(string name, int age, string gender, int strength, int reputation, string title, ReputationManager reputationManager)
            : base(name, age, gender, strength, reputation, title)
        {
            Wares = new Dictionary<string, Ware>();
            this.reputationManager = reputationManager;
            this.hasGreeted = false;
        }

        public void ShowTraderDialogues(NpcDialogueManager dialogueManager)
        {
            if (!hasGreeted)
            {
                Console.WriteLine($"Greetings, traveler! I am {Name}, the {Title}.");
                hasGreeted = true;
            }

            dialogueManager.DisplayNpcMenu(Name, this, reputationManager);
        }

        public void SetWares(Dictionary<string, Ware> wares)
        {
            Wares = wares;
        }

        public void ListWares()
        {
            int currentReputation = reputationManager.GetReputation(Name);
            bool extraItemsAdded = false;
            double discount = 0;

            // Tarkistetaan, onko kauppias Sarah
            if (Name == "Sarah")
            {
                // Lisää kaksi uutta tavaraa, jos maine on yli 100
                if (currentReputation > 100 && !extraItemsAdded)
                {
                    Wares["Special Potion"] = new Ware("Special Potion", 50, 3);
                    Wares["Legendary Bow"] = new Ware("Legendary Bow", 500, 1);
                    extraItemsAdded = true;
                }

                // Lisää 10% alennus, jos maine on yli 150
                if (currentReputation > 150)
                {
                    discount = 0.10; // 10% alennus
                }
            }

            // Brommille
            if (Name == "Bromm")
            {
                // Lisää uusi tavara, jos maine on yli 80
                if (currentReputation > 80 && !extraItemsAdded)
                {
                    Wares["Exclusive Ale"] = new Ware("Exclusive Ale", 100, 5);
                    Wares["Meat"] = new Ware("Meat", 22, 10);
                    extraItemsAdded = true;
                }

                // Lisää 5% alennus, jos maine on yli 120
                if (currentReputation > 120)
                {
                    discount = 0.05; // 5% alennus
                }
            }

            // Gregorille
            if (Name == "Gregor")
            {
                // Lisää uusi tavara, jos maine on yli 50
                if (currentReputation > 50 && !extraItemsAdded)
                {
                    Wares["Rare Shield"] = new Ware("Rare Shield", 20, 10);
                    extraItemsAdded = true;
                }

                // Lisää 15% alennus, jos maine on yli 200
                if (currentReputation > 200)
                {
                    discount = 0.15; // 15% alennus
                }
            }

            // Lista myytävissä olevista tavaroista
            Console.WriteLine("\nHere are the items I have for sale:");

            int i = 1;
            foreach (var ware in Wares.Values)
            {
                double finalPrice = ware.Price - (ware.Price * discount);
                Console.WriteLine($"{i}. {ware.Name} - Price: {finalPrice} gold, Quantity: {ware.Quantity}");
                i++;
            }

            // Lisää paluu painikkeen loppuun.
            Console.WriteLine($"{i}. Back");

            Console.WriteLine("\nWhich item would you like to buy?");
            string? choice = Console.ReadLine();

            if (int.TryParse(choice, out int selected) && selected > 0 && selected <= Wares.Count)
            {
                BuyWare(selected - 1, discount);
            }
            else if (choice == i.ToString())
            {
                Console.WriteLine("Returning to previous menu...");
            }
            else
            {
                Console.WriteLine("Invalid choice, please try again.");
            }
        }


        private void BuyWare(int index, double discount)
        {
            var ware = new List<Ware>(Wares.Values)[index];

            Console.WriteLine($"How many {ware.Name}(s) would you like to buy?");
            string? quantityInput = Console.ReadLine();

            if (int.TryParse(quantityInput, out int quantity) && quantity > 0 && quantity <= ware.Quantity)
            {
                int totalPrice = (int)((quantity * ware.Price) * (1 - discount));

                Console.WriteLine($"The total cost for {quantity} {ware.Name}(s) is {totalPrice} gold. Do you want to proceed? (yes/no)");

                string? confirm = Console.ReadLine();
                if (confirm?.ToLower() == "yes")
                {
                    Console.WriteLine($"You have bought {quantity} {ware.Name}(s) for {totalPrice} gold.");
                    ware.Quantity -= quantity;
                }
                else
                {
                    Console.WriteLine("Purchase canceled.");
                }
            }
            else
            {
                Console.WriteLine("Invalid quantity or not enough stock available.");
            }
        }

        // Gregor NPC:n tiedot, myytävät tavarat ja maine
        public static Trader CreateGregor(ReputationManager reputationManager)
        {
            Trader gregor = new Trader(
                name: "Gregor",
                age: 26,
                gender: "male",
                strength: 150,
                reputation: 250,
                title: "Trader",
                reputationManager: reputationManager);

            gregor.SetWares(new Dictionary<string, Ware>()
            {
                { "Potion", new Ware("Potion", 10, 5) },
                { "Sword", new Ware("Sword", 100, 2) },
                { "Shield", new Ware("Shield", 50, 3) }
            });

            reputationManager.SetReputation("Gregor", -10); // Asetetaan oletusmaine Gregorille

            return gregor;
        }

        // Sarah NPC:n tiedot, myytävät tavarat ja maine
        public static Trader CreateSarah(ReputationManager reputationManager)
        {
            Trader sarah = new Trader(
                name: "Sarah",
                age: 32,
                gender: "female",
                strength: 120,
                reputation: 200,
                title: "Trader",
                reputationManager: reputationManager);

            sarah.SetWares(new Dictionary<string, Ware>()
            {
                { "Magic Potion", new Ware("Magic Potion", 20, 10) },
                { "Enchanted Sword", new Ware("Enchanted Sword", 200, 1) },
                { "Mystic Shield", new Ware("Mystic Shield", 150, 2) }
            });

            reputationManager.SetReputation("Sarah", 150); // Asetetaan oletusmaine Sarahille

            return sarah;
        }

        // Bromm NPC:n tiedot, myytävät tavarat ja maine
        public static Trader CreateBromm(ReputationManager reputationManager)
        {
            Trader bromm = new Trader(
                name: "Bromm",
                age: 49,
                gender: "male",
                strength: 150,
                reputation: 250,
                title: "Tavern keeper",
                reputationManager: reputationManager);

            bromm.SetWares(new Dictionary<string, Ware>()
            {
                { "Ale", new Ware("Ale", 10, 50) },
                { "Water", new Ware("Water", 5, 100) },
                { "Dinner", new Ware("Dinner", 30, 5) }
            });

            reputationManager.SetReputation("Bromm", 158); // Asetetaan oletusmaine Brommille

            return bromm;
        }
    }
}
