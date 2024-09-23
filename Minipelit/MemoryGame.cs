using System;

namespace GameCollection
{
    public enum MemoryGameDifficulty
    {
        Easy,     // 4x4
        Medium,   // 6x6
        Hard      // 8x8
    }

    public static class MemoryGame
    {
        public static void StartGame(MemoryGameDifficulty difficulty)
        {
            int size = difficulty switch
            {
                MemoryGameDifficulty.Easy => 4,
                MemoryGameDifficulty.Medium => 6,
                MemoryGameDifficulty.Hard => 8,
                _ => 4
            };

            string[,] board = CreateBoard(size);
            bool[,] revealed = new bool[size, size];
            ShowCheatsheet(size);  // Näytetään cheatsheet alussa
            PlayGame(board, revealed);
        }

        private static string[,] CreateBoard(int size)
        {
            string[,] board = new string[size, size];
            char[] pairs = new char[(size * size) / 2];
            for (int i = 0; i < pairs.Length; i++)
            {
                pairs[i] = (char)('A' + i);
            }

            // Täytetään taulukko pareilla
            Random rand = new Random();
            for (int i = 0; i < pairs.Length; i++)
            {
                int placed = 0;
                while (placed < 2)
                {
                    int x = rand.Next(size);
                    int y = rand.Next(size);
                    if (board[x, y] == null)
                    {
                        board[x, y] = pairs[i].ToString();
                        placed++;
                    }
                }
            }

            return board;
        }

        private static void ShowCheatsheet(int size)
        {
            Console.Clear();
            Console.WriteLine("Cheatsheet:");
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write($"{{{i + 1},{j + 1}}} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("\nPaina mitä tahansa näppäintä jatkaaksesi peliin.");
            Console.ReadKey();
        }

        private static void PlayGame(string[,] board, bool[,] revealed)
        {
            int size = board.GetLength(0);
            int pairsCount = (size * size) / 2;
            int foundPairs = 0;
            int attempts = 0;

            while (foundPairs < pairsCount)
            {
                Console.Clear();
                PrintBoard(board, revealed);
                // valinta jonka pelaaja tekee.
                Console.WriteLine("Valitse ensimmäinen kortti (rivi, sarake):");
                int[] firstChoice = ChooseCard(size, revealed);
                if (firstChoice == null) return;

                Console.Clear();
                revealed[firstChoice[0], firstChoice[1]] = true;
                PrintBoard(board, revealed);
                // kertoo pelaajalle mikä ruudussa oleva aakkonen on.
                Console.WriteLine($"Valitsit ruudun {firstChoice[0] + 1},{firstChoice[1] + 1} ja se on {board[firstChoice[0], firstChoice[1]]}");

                Console.WriteLine("Valitse toinen kortti (rivi, sarake):");
                int[] secondChoice = ChooseCard(size, revealed);
                if (secondChoice == null) return;

                // Tarkistetaan, että pelaaja ei valinnut samaa ruutua kahdesti
                if (firstChoice[0] == secondChoice[0] && firstChoice[1] == secondChoice[1])
                {
                    Console.WriteLine("Et voi valita samaa ruutua kahdesti. Yritä uudelleen.");
                    revealed[firstChoice[0], firstChoice[1]] = false; // Piilotetaan ruutu uudelleen
                    Console.WriteLine("Paina mitä tahansa näppäintä jatkaaksesi.");
                    Console.ReadKey();
                    continue; // Palataan valitsemaan uusi ruutu
                }

                revealed[secondChoice[0], secondChoice[1]] = true;
                Console.Clear();
                PrintBoard(board, revealed);

                Console.WriteLine($"Valitsit ruudun {secondChoice[0] + 1},{secondChoice[1] + 1} ja se on {board[secondChoice[0], secondChoice[1]]}");

                attempts++;  // Lisätään yritysten määrää

                if (board[firstChoice[0], firstChoice[1]] == board[secondChoice[0], secondChoice[1]])
                {
                    Console.WriteLine("Löysit parin!");
                    foundPairs++;
                }
                else
                {
                    Console.WriteLine("Ei pari. Kortit palautuvat.");
                    revealed[firstChoice[0], firstChoice[1]] = false;
                    revealed[secondChoice[0], secondChoice[1]] = false;
                }

                Console.WriteLine("Paina mitä tahansa näppäintä jatkaaksesi.");
                Console.ReadKey();
            }

            Console.WriteLine($"Onneksi olkoon! Löysit kaikki parit. Yrityksiä yhteensä: {attempts}");
            Console.WriteLine("Paina mitä tahansa näppäintä palataksesi valikkoon.");
            Console.ReadKey();
        }

        private static void PrintBoard(string[,] board, bool[,] revealed)
        {
            int size = board.GetLength(0);
            string format = "{0,-5}"; // Asetetaan kiinteä leveys jokaiselle ruudulle

            Console.Write("  "); // Tyhjä kohta vasemmalle
            for (int j = 0; j < size; j++)
            {
                Console.Write(string.Format(format, j + 1)); // Tulostetaan sarakenumerot
            }
            Console.WriteLine();

            for (int i = 0; i < size; i++)
            {
                Console.Write(i + 1 + " "); // Tulostetaan rivinumerot
                for (int j = 0; j < size; j++)
                {
                    if (revealed[i, j])
                    {
                        Console.Write(string.Format(format, board[i, j])); // Näytetään kirjain, jos paljastettu
                    }
                    else
                    {
                        Console.Write(string.Format(format, "X")); // Näytetään X, jos piilotettu
                    }
                }
                Console.WriteLine();
            }
        }

        // Pelaajan kontrollit
        private static int[] ChooseCard(int size, bool[,] revealed)
        {
            int row, column;
            while (true)
            {
                Console.Write("Rivi (1-{0}): ", size);
                string input = Console.ReadLine();

                if (input.ToLower() == "exit")
                {
                    return null;
                }

                if (int.TryParse(input, out row) && row > 0 && row <= size)
                {
                    Console.Write("Sarake (1-{0}): ", size);
                    input = Console.ReadLine();

                    if (input.ToLower() == "exit")
                    {
                        return null;
                    }

                    if (int.TryParse(input, out column) && column > 0 && column <= size)
                    {
                        if (!revealed[row - 1, column - 1])
                        {
                            return new int[] { row - 1, column - 1 };
                        }
                        else
                        {
                            Console.WriteLine("Tämä ruutu on jo löydetty parina. Valitse toinen ruutu.");
                        }
                    }
                }

                Console.WriteLine("Virheellinen syöte, yritä uudelleen.");
            }
        }
    }
}
