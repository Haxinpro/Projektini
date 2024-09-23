using System;
using System.Collections.Generic;

namespace Game
{
    public class NpcDialogueManager
    {
        private Dictionary<string, Dictionary<string, string>> npcResponses;
        private Dictionary<string, Action<Trader>> npcMenus;

        public NpcDialogueManager()
        {
            npcResponses = new Dictionary<string, Dictionary<string, string>>();
            npcMenus = new Dictionary<string, Action<Trader>>();

            npcResponses["Gregor"] = new Dictionary<string, string>()
            {
                { "rumors", "I heard there are smugglers near the east coastline. \nHaven't heard anything else." },
                { "john_silver", "John Silver? He's a shady character, best to avoid him." }
            };
            npcMenus["Gregor"] = ShowGregorMenu;

            npcResponses["Sarah"] = new Dictionary<string, string>()
            {
                { "rumors", "There's talk of a treasure hidden in the northern hills." },
                { "john_silver", "John Silver? Yes, I've dealt with him before, but I wouldn't trust him." },
                { "black_beard", "That is just a rumor. I have never worked for the Black Beard nor Have I ever seen him."}
            };
            npcMenus["Sarah"] = ShowSarahMenu;

            npcResponses["Bromm"] = new Dictionary<string, string>()
            {
                { "rumors", "If you want to hear rumors, you have to earn it." },
                { "john_silver", "John Silver. That lad is one dangerous foe that I can tell. Lost me toe to him." }
            };
            npcMenus["Bromm"] = ShowBrommMenu;
        }

        public string GetResponse(string npcName, string topic)
        {
            if (npcResponses.ContainsKey(npcName) && npcResponses[npcName].ContainsKey(topic))
            {
                return npcResponses[npcName][topic];
            }
            return "I have nothing to say about that.";
        }

        public void DisplayNpcMenu(string npcName, Trader trader, ReputationManager reputationManager)
        {
            // Näytetään valikko kerran ennen kuin siirrytään käyttäjän valintojen käsittelyyn
            if (npcMenus.ContainsKey(npcName))
            {
                npcMenus[npcName](trader);  // Kutsutaan oikeaa valikkonäyttöä
            }
            HandleMenuChoice(npcName, trader, reputationManager);  // Käsitellään käyttäjän valinta
        }



        private void ShowGregorMenu(Trader trader)
        {
            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("1. Show me your wares.");
            Console.WriteLine("2. What are the rumors for today?");
            Console.WriteLine("3. Do you know the guy named John Silver?");
            Console.WriteLine("4. Check reputation with the NPC.");
            Console.WriteLine("5. Sorry, I don't have time for a chat...");
        }

        private void ShowSarahMenu(Trader trader)
        {
            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("1. Can I see your shop?");
            Console.WriteLine("2. What are the rumors for today?");
            Console.WriteLine("3. Do you know the guy named John Silver?");
            Console.WriteLine("4. Check reputation with the NPC.");
            Console.WriteLine("5. Sorry, I don't have time for a chat...");

            if (npcResponses["Sarah"].ContainsKey("black_beard"))
            {
                Console.WriteLine("6. I heard you work for the Black Beard.");
            }
        }

        private void ShowBrommMenu(Trader trader)
        {
            Console.WriteLine("\nWhat can I do for you, lad?");
            Console.WriteLine("1. Have a look at my wares.");
            Console.WriteLine("2. Got any rumors to share?");
            Console.WriteLine("3. Know anything about John Silver?");
            Console.WriteLine("4. What’s my standing with you?");
            Console.WriteLine("5. I need to be going.");
            if (npcResponses["Bromm"].ContainsKey("favorite_ale"))
            {
                Console.WriteLine("6. What's your favorite ale?");
            }
        }

        private void HandleMenuChoice(string npcName, Trader trader, ReputationManager reputationManager)
        {
            string? choice = Console.ReadLine();
            bool validChoice = true;

            switch (choice)
            {
                case "1":
                    trader.ListWares();
                    break;
                case "2":
                    Console.WriteLine();
                    Console.WriteLine(GetResponse(npcName, "rumors"));
                    Console.WriteLine();
                    break;
                case "3":
                    Console.WriteLine();
                    Console.WriteLine(GetResponse(npcName, "john_silver"));
                    Console.WriteLine();
                    break;
                case "4":
                    Console.WriteLine();
                    Console.WriteLine(reputationManager.CheckReputationLevel(trader));
                    Console.WriteLine();
                    break;
                case "5":
                    Console.WriteLine();
                    Console.WriteLine("Alright, maybe next time.");
                    Console.WriteLine();
                    validChoice = false;
                    break;
                case "6":
                    if (npcName == "Sarah" && npcResponses["Sarah"].ContainsKey("black_beard"))
                    {
                        Console.WriteLine();
                        Console.WriteLine(GetResponse(npcName, "black_beard"));
                        Console.WriteLine();
                    }
                    else if (npcName == "Bromm" && npcResponses["Bromm"].ContainsKey("favorite_ale"))
                    {
                        Console.WriteLine();
                        Console.WriteLine(GetResponse(npcName, "favorite_ale"));
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice, please try again.");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }

            if (validChoice)
            {
                DisplayNpcMenu(npcName, trader, reputationManager);
            }
        }
    }
}


/* note to self. Kartoita vaihtoehdot:
1. Tiedostopohjainen Dialogien Hallinta - JSON-dialogitiedosto + latausmekanismi.
2. Käyttämällä Strategia- tai Tehdaskuvioita
3. Dialogien ja Eventtien Käyttö

kun aletaan lisäämään suuria määriä NPC:tä eri määrällä keskusteluja ja vaihtoehtoja, täytyy toteutus hoitaa eri tavalla, joka on tehokkaampi.
*/