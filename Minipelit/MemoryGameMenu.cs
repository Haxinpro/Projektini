using System;

namespace GameCollection
{
    public static class MemoryGameMenu
    {
        public static void ShowMenu()
        {
            bool backToMainMenu = false;

            while (!backToMainMenu)
            {
                Console.Clear(); // Tyhjennetään konsoli
                Console.WriteLine("Muistipeli");
                Console.WriteLine("1. Aloita peli");
                Console.WriteLine("2. Säännöt");
                Console.WriteLine("3. Palaa takaisin");

                string? choice = Console.ReadLine(); // Lukee käyttäjän valinnan

                switch (choice)
                {
                    case "1":
                        StartGame(); // Aloitetaan peli valitsemalla vaikeustaso
                        break;
                    case "2":
                        ShowRules(); // Näytetään säännöt
                        break;
                    case "3":
                        backToMainMenu = true; // Paluu päävalikkoon
                        break;
                    default:
                        Console.WriteLine("Virheellinen valinta, yritä uudelleen.");
                        Console.ReadKey(); // Odotetaan, että käyttäjä painaa jotain näppäintä
                        break;
                }
            }
        }

        private static void StartGame()
        {
            Console.Clear();
            Console.WriteLine("Valitse vaikeustaso:");
            Console.WriteLine("1. Helppo (4x4)");
            Console.WriteLine("2. Keskivaikea (6x6)");
            Console.WriteLine("3. Vaikea (8x8)");

            string? difficultyChoice = Console.ReadLine();

            MemoryGameDifficulty difficulty = difficultyChoice switch
            {
                "1" => MemoryGameDifficulty.Easy,
                "2" => MemoryGameDifficulty.Medium,
                "3" => MemoryGameDifficulty.Hard,
                _ => MemoryGameDifficulty.Easy
            };

            MemoryGame.StartGame(difficulty);
        }

        private static void ShowRules()
        {
            Console.Clear();
            Console.WriteLine("Muistipelin säännöt:");
            Console.WriteLine("1. Valitse kaksi korttia kerrallaan yhdellä kierroksella.");
            Console.WriteLine("2. Jos kortit ovat pari, ne jäävät näkyviin.");
            Console.WriteLine("3. Jatka kunnes kaikki parit on löydetty.");
            Console.WriteLine("4. Valitse ensin vaakasuorassa oleva rivi ja sen jälkeen erikseen pystysuorassa oleva rivi numeron perusteella");
            Console.WriteLine("Kirjoita 'exit' missä tahansa vaiheessa palataksesi valikkoon.");
            Console.WriteLine("Paina mitä tahansa näppäintä palataksesi valikkoon.");
            Console.ReadKey();
        }
    }
}
