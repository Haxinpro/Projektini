using System;

namespace GameCollection
{
    public static class TicTacToeMenu
    {
        public static void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Ristinolla");
                Console.WriteLine("1. Aloita peli");
                Console.WriteLine("2. Säännöt");
                Console.WriteLine("3. Palaa päävalikkoon");

                string? choice = Console.ReadLine();

                if (choice == "1")
                {
                    ChooseDifficulty();
                }
                else if (choice == "2")
                {
                    ShowRules();
                }
                else if (choice == "3")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Virheellinen valinta. Yritä uudelleen.");
                    Console.ReadKey();
                }
            }
        }

        static void ChooseDifficulty()
        {
            Console.Clear();
            Console.WriteLine("Valitse vaikeustaso:");
            Console.WriteLine("1. Helppo");
            Console.WriteLine("2. Vaikea");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    TicTacToeGame.StartGame(true); // Käynnistetään peli helpolla tasolla
                    break;
                case "2":
                    TicTacToeGame.StartGame(false); // Käynnistetään peli vaikealla tasolla
                    break;
                default:
                    Console.WriteLine("Virheellinen valinta, palaa takaisin valikkoon.");
                    Console.ReadKey();
                    break;
            }
        }

        static void ShowRules()
        {
            Console.Clear(); // Tyhjennetään konsoli ennen sääntöjen näyttämistä
            Console.WriteLine("Ristinolla-pelin säännöt:");
            Console.WriteLine("1. Pelilaudalla on 3x3 ruudukko.");
            Console.WriteLine("2. Pelaajat vuorottelevat, ja jokainen pelaaja valitsee tyhjän ruudun.");
            Console.WriteLine("3. Ensimmäinen pelaaja, joka saa kolme merkkiään peräkkäin (pystyyn, vaakaan tai vinoon), voittaa pelin.");
            Console.WriteLine("4. Jos pelilauta täyttyy ilman voittajaa, peli päättyy tasapeliin.");
            Console.WriteLine("5. Neljännen siirron jälkeen pelaajan ensimmäinen siirto poistetaan.");
            Console.WriteLine("6. Pelissä on kaksi vaikeustasoa: helppo ja vaikea.");
            Console.WriteLine("   Helpossa tasossa saat tiedon poistuvasta merkistäsi. Vaikeassa tasossa kyse on muistipelistä.");
            Console.WriteLine("7. Voit lopettaa pelin milloin tahansa kirjoittamalla 'exit'.");
            Console.WriteLine("\nPaina mitä tahansa näppäintä palataksesi valikkoon.");
            Console.ReadKey();
        }
    }
}
