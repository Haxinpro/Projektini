using System;

namespace GameCollection
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false; // Muuttuja, joka pitää kirjaa siitä, haluaako käyttäjä poistua ohjelmasta

            while (!exit)
            {
                Console.Clear(); // Tyhjennetään konsoli jokaisen valikkonäytön alussa
                Console.WriteLine("Tervetuloa Pelikokoelmaan!"); // Otsikko
                Console.WriteLine("1. Ristinolla"); // Valinta 1: Ristinolla
                Console.WriteLine("2. Reaktiopeli"); // Valinta 2: Reaktiopeli
                Console.WriteLine("3. Kruuna vai Klaava"); // Valinta 3: Kruuna vai Klaava
                Console.WriteLine("4. Muistipeli"); // Valinta 4: Muistipeli
                Console.WriteLine("5. Poistu"); // Valinta 5: Lopettaa ohjelman

                string? choice = Console.ReadLine(); // Lukee käyttäjän valinnan

                // Switch-case rakenne käsittelee käyttäjän valinnan
                switch (choice)
                {
                    case "1":
                        TicTacToeMenu.ShowMenu(); // Kutsutaan Ristinolla-pelin valikkoa
                        break;
                    case "2":
                        ReaktioPeli.ShowMenu(); // Kutsutaan Reaktiopeli-pelin valikkoa
                        break;
                    case "3":
                        CoinFlipGame.ShowMenu(); // Kutsutaan Kruuna vai Klaava -pelin valikkoa
                        break;
                    case "4":
                        MemoryGameMenu.ShowMenu(); // Kutsutaan Muistipeli-pelin valikkoa
                        break;
                    case "5":
                        exit = true; // Asetetaan exit true, jolloin ohjelma päättyy
                        break;
                    default:
                        // Virheellinen valinta, pyydetään käyttäjää yrittämään uudelleen
                        Console.WriteLine("Virheellinen valinta, yritä uudelleen.");
                        Console.ReadKey(); // Odotetaan, että käyttäjä painaa jotain näppäintä
                        break;
                }
            }
        }
    }
}
