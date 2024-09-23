using System;

namespace Game
{
    class Program
    {
        public static void Main(string[] args)
        {
            NpcDialogueManager dialogueManager = new NpcDialogueManager();
            ReputationManager reputationManager = new ReputationManager();

            bool keepRunning = true;

            while (keepRunning)
            {
                Console.WriteLine("Which trader would you like to talk to?");
                Console.WriteLine("1. Gregor");
                Console.WriteLine("2. Sarah");
                Console.WriteLine("3. Bromm");
                Console.WriteLine("4. Back");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Trader gregor = Trader.CreateGregor(reputationManager);
                        gregor.ShowTraderDialogues(dialogueManager);
                        break;
                    case "2":
                        Trader sarah = Trader.CreateSarah(reputationManager);
                        sarah.ShowTraderDialogues(dialogueManager);
                        break;
                    case "3":
                        Trader bromm = Trader.CreateBromm(reputationManager);
                        bromm.ShowTraderDialogues(dialogueManager);
                        break;
                    case "4":
                        Console.WriteLine("You decided not to talk to any trader right now.");
                        keepRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        break;
                }

                Console.WriteLine();
            }

            Console.WriteLine("Exiting the program. Goodbye!");
        }
    }
}
