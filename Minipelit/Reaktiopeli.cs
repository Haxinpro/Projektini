using System;
using System.Diagnostics;
using System.Threading;

namespace GameCollection
{
    public static class ReaktioPeli
    {
        public static void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Reaktiopeli");
                Console.WriteLine("1. Aloita peli");
                Console.WriteLine("2. Säännöt");
                Console.WriteLine("3. Palaa päävalikkoon");
                Console.Write("Valitse: ");

                string? valinta = Console.ReadLine();

                if (valinta == "1")
                {
                    AloitaPeli();
                }
                else if (valinta == "2")
                {
                    NaytaSaannot();
                }
                else if (valinta == "3")
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

        static void AloitaPeli()
        {
            Console.Clear();
            Console.WriteLine("Valmistaudu...");
            Thread.Sleep(2000);  // Lyhyt odotus

            Random random = new Random();
            int odotusaika = random.Next(1000, 5000);  // Satunnainen odotusaika 1-5 sekuntia
            Thread.Sleep(odotusaika);

            Console.WriteLine("NYT!");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.ReadLine();
            stopwatch.Stop();

            TimeSpan reaktioaika = stopwatch.Elapsed;
            Console.WriteLine($"Reaktioaikasi: {reaktioaika.TotalMilliseconds} ms");
            Console.WriteLine("Paina mitä tahansa näppäintä palataksesi valikkoon...");
            Console.ReadKey();
        }

        static void NaytaSaannot()
        {
            Console.Clear();
            Console.WriteLine("Säännöt:");
            Console.WriteLine("1. Kun aloitat pelin, sinun tulee valmistautua.");
            Console.WriteLine("2. Satunnaisen ajan kuluttua näet sanan 'NYT'.");
            Console.WriteLine("3. Paina Enter-näppäintä niin nopeasti kuin mahdollista.");
            Console.WriteLine("4. Peli mittaa reaktioaikasi millisekunneissa.");
            Console.WriteLine("Paina mitä tahansa näppäintä palataksesi valikkoon...");
            Console.ReadKey();
        }
    }
}
