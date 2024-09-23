using System;

namespace GameCollection
{
    public static class CoinFlipGame
    {
        public static void ShowMenu()
        {
            // kun pelaaja valitsee valikosta tämän pelin, aloitetaan pelin menu looppi.
            while (true)
            {
                Console.Clear(); // tyhjentää konsolin
                Console.WriteLine("Kruuna vai Klaava");
                Console.WriteLine("1. Aloita peli");
                Console.WriteLine("2. Säännöt");
                Console.WriteLine("3. Palaa päävalikkoon");

                string? choice = Console.ReadLine();

                if (choice == "1")
                {
                    StartGame(); // aloittaa pelin
                }
                else if (choice == "2")
                {
                    ShowRules(); // näyttää säännöt
                }
                else if (choice == "3")
                {
                    break; // palaa takaisin päävalikkoon.
                }
                else
                {
                    Console.WriteLine("Virheellinen valinta. Yritä uudelleen.");
                    Console.ReadKey();
                }
            }
        }
        static void ShowRules()
        {
            // pelin säännöt.
            Console.Clear();
            Console.WriteLine("Kruuna vai Klaava -pelin säännöt:");
            Console.WriteLine("1. Pelissä sinun tulee arvata, tuleeko kolikonheitossa kruuna vai klaava.");
            Console.WriteLine("2. Valitse kruuna (1) tai klaava (2).");
            Console.WriteLine("3. Jos arvaat oikein, voit jatkaa peliä ja yritä kasvattaa oikeiden arvauksien putkea.");
            Console.WriteLine("4. Peli päättyy, kun arvaat väärin, ja saat tietää, kuinka monta kertaa arvasit oikein peräkkäin.");
            Console.WriteLine("5. Pelin päättyessä voit valita, haluatko pelata uudelleen.");
            Console.WriteLine("\nPaina mitä tahansa näppäintä palataksesi valikkoon.");
            Console.ReadKey();
        }

        // pelin tiedot alla. 
        // -----------------------------------------------------------------------------------------------------
        static void StartGame()
        {
            int streak = 0; // Pitää kirjaa peräkkäisten oikeiden arvauksien määrästä
            Random random = new Random();

            while (true)
            {
                Console.Clear(); // tyhjentää konsolin.
                Console.WriteLine("Kruuna vai Klaava.");
                Console.WriteLine("1. Kruuna");
                Console.WriteLine("2. Klaava");
                string? input = Console.ReadLine(); // käyttäjän syöte.

                string? guess = input switch
                {
                    "1" => "kruuna",
                    "2" => "klaava",
                    _ => null
                };
                // jos syötetään tyhjää tai muuta kuin 1 tai 2 niin tulostetaan virhe.
                if (guess == null)
                {
                    Console.WriteLine("Virheellinen valinta, yritä uudelleen.");
                    Console.ReadKey(); // odottaa käyttäjän syötettä jatkaakseen.
                    continue;
                }

                string result = random.Next(0, 2) == 0 ? "kruuna" : "klaava";
                Console.WriteLine();
                Console.WriteLine($"Kolikko näyttää: {result}");

                if (guess == result)
                {
                    streak++;
                    Console.WriteLine($"Oikein!");
                }
                else
                {
                    // kun pelaaja arvaa väärin, peli päättyy ja annetaan voittoputki.
                    Console.WriteLine($"Väärin! Oikea vastaus oli {result}. Peli päättyy.");
                    Console.WriteLine($"Oikeiden arvauksien putkesi oli {streak}.");
                    // kysytään pelaajalta haluaako hän pelata uudestaan.
                    Console.Write("Haluatko pelata uudelleen? (k/e): ");
                    string? replayChoice = Console.ReadLine().ToLower();
                    if (replayChoice == "k")
                    {
                        streak = 0;
                        continue; // Aloittaa pelin uudelleen
                    }
                    else
                    {
                        break; // Lopettaa pelin ja palaa valikkoon
                    }
                }
                Console.WriteLine();
                Console.WriteLine("Paina mitä tahansa näppäintä jatkaaksesi...");
                Console.ReadKey();
            }

            Console.WriteLine("Paina mitä tahansa näppäintä palataksesi päävalikkoon.");
            Console.ReadKey();
        }


    }
}
