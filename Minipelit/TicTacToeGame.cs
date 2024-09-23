using System;
using System.Collections.Generic;

namespace GameCollection


{
    public static class TicTacToeGame
    {
        // Pelilauta, aluksi numerot 1-9 osoittavat pelipaikkoja
        static char[,] board = {
            { '1', '2', '3' },
            { '4', '5', '6' },
            { '7', '8', '9' }
        };

        // Tallennetaan nykyinen pelaaja ('X' tai 'O')
        static char currentPlayer = 'X';

        // Sanakirja, jossa seurataan kummankin pelaajan siirtoja
        static Dictionary<char, List<int>> playerMoves = new Dictionary<char, List<int>>()
        {
            { 'X', new List<int>() },
            { 'O', new List<int>() }
        };

        // Metodi, joka käynnistää pelin
        public static void StartGame(bool isEasy)
        {
            bool gameWon = false; // Tieto siitä, onko peli voitettu
            bool gameEnded = false; // Tieto siitä, onko peli lopetettu kesken

            // Alustetaan pelilauta ja pelaajat
            ResetBoard();
            playerMoves['X'].Clear();
            playerMoves['O'].Clear();
            currentPlayer = 'X';

            // Pääsilmukka, joka jatkuu, kunnes peli on voitettu tai lopetettu
            while (!gameWon && !gameEnded)
            {
                Console.Clear(); // Tyhjennetään konsolinäkymä ennen uuden vuoron alkua

                // Jos peli on helppo ja pelaaja on tekemässä neljättä siirtoaan, näytä "/" merkki
                if (isEasy && playerMoves[currentPlayer].Count == 3)
                {
                    HighlightFirstMove(currentPlayer); // Korostetaan ensimmäinen siirto
                }

                DisplayBoard(); // Näytetään pelilauta

                Console.WriteLine($"Player {currentPlayer}, choose your field or type 'exit' to quit!");

                string? input = Console.ReadLine().ToLower(); // Pelaajan syöte

                if (input == "exit") // Jos pelaaja syöttää "exit", peli päättyy
                {
                    gameEnded = true;
                    break;
                }

                if (int.TryParse(input, out int field) && field >= 1 && field <= 9)
                {
                    int row = (field - 1) / 3; // Lasketaan rivin indeksi
                    int col = (field - 1) % 3; // Lasketaan sarakkeen indeksi

                    // Tarkistetaan, onko valittu ruutu vapaa (ei ole 'X', 'O' tai '/')
                    if (board[row, col] != 'X' && board[row, col] != 'O' && board[row, col] != '/')
                    {
                        board[row, col] = currentPlayer; // Päivitetään ruutu nykyisen pelaajan merkillä
                        playerMoves[currentPlayer].Add(field); // Tallennetaan pelaajan siirto listaan

                        // Jos pelaaja on tehnyt neljä siirtoa, poistetaan hänen ensimmäinen siirtonsa
                        if (playerMoves[currentPlayer].Count == 4)
                        {
                            RemoveFirstMove(currentPlayer);
                        }

                        gameWon = CheckWin(); // Tarkistetaan, voittiko nykyinen pelaaja

                        if (!gameWon)
                        {
                            // Vaihdetaan vuoro seuraavalle pelaajalle
                            currentPlayer = currentPlayer == 'X' ? 'O' : 'X';
                        }
                    }
                    else
                    {
                        Console.WriteLine("Field already taken. Choose another field.");
                        Console.ReadKey(); // Odotetaan, että pelaaja lukee viestin ennen jatkamista
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Choose a number between 1 and 9.");
                    Console.ReadKey(); // Odotetaan, että pelaaja lukee viestin ennen jatkamista
                }
            }

            Console.Clear(); // Tyhjennetään konsolinäkymä
            DisplayBoard(); // Näytetään pelilauta

            if (gameWon)
            {
                Console.WriteLine($"Player {currentPlayer} wins!"); // Ilmoitetaan voittaja
            }
            else if (gameEnded)
            {
                Console.WriteLine("Game ended by player."); // Ilmoitetaan, että peli päättyi pelaajan toimesta
            }

            Console.WriteLine("\nPaina mitä tahansa näppäintä palataksesi päävalikkoon.");
            Console.ReadKey(); // Odotetaan käyttäjän syötettä ennen paluuta päävalikkoon
        }

        // Metodi, joka palauttaa pelilaudan alkuperäiseen tilaan
        static void ResetBoard()
        {
            board = new char[,]
            {
                { '1', '2', '3' },
                { '4', '5', '6' },
                { '7', '8', '9' }
            };
        }

        // Korostaa pelaajan ensimmäisen siirron muuttamalla sen "/"-merkiksi
        static void HighlightFirstMove(char player)
        {
            List<int> moves = playerMoves[player]; // Haetaan pelaajan siirrot

            if (moves.Count > 0)
            {
                int firstMove = moves[0];

                int row = (firstMove - 1) / 3; // Lasketaan rivin indeksi
                int col = (firstMove - 1) % 3; // Lasketaan sarakkeen indeksi

                // Muutetaan ensimmäinen siirto "/" merkiksi
                board[row, col] = '/';
            }
        }

        // Poistaa pelaajan ensimmäisen siirron ja palauttaa ruudun numeroksi
        static void RemoveFirstMove(char player)
        {
            List<int> moves = playerMoves[player]; // Haetaan pelaajan siirrot

            if (moves.Count > 0)
            {
                int firstMove = moves[0];
                moves.RemoveAt(0); // Poistetaan ensimmäinen siirto listasta

                int row = (firstMove - 1) / 3; // Lasketaan rivin indeksi
                int col = (firstMove - 1) % 3; // Lasketaan sarakkeen indeksi

                // Palautetaan alkuperäinen numerointi
                board[row, col] = (char)(firstMove + '0');
            }
        }

        // Näyttää pelilaudan konsolissa
        static void DisplayBoard()
        {
            Console.WriteLine("Current Board:");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    char currentCell = board[i, j];
                    Console.Write(" {0} ", currentCell);

                    if (j < 2) Console.Write("|");
                }
                Console.WriteLine();
                if (i < 2) Console.WriteLine("---|---|---");
            }
        }

        // Tarkistaa, onko joku voittanut pelin
        static bool CheckWin()
        {
            // Tarkistetaan rivit, sarakkeet ja diagonaalit
            for (int i = 0; i < 3; i++)
            {
                // Tarkistetaan rivit
                if ((board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2]) ||
                    // Tarkistetaan sarakkeet
                    (board[0, i] == board[1, i] && board[1, i] == board[2, i]))
                {
                    return true;
                }
            }
            // Tarkistetaan diagonaalit
            if ((board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2]) ||
                (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0]))
            {
                return true;
            }
            return false; // Palautetaan false, jos voittoa ei löytynyt
        }
    }
}
