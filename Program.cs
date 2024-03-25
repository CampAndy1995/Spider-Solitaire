using System;
using System.Collections.Generic;

namespace SpiderSolitaire
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Spider Solitaire!");
            Console.ReadLine();

            // Initialize the deck
            List<string> deck = InitializeDeck();

            // Shuffle the deck
            Shuffle(deck);

            // Deal the cards
            List<List<string>> tableau = DealCards(deck);

            // Display the initial tableau
            DisplayTableau(tableau);

            // Main game loop
            while (!IsGameWon(tableau))
            {
                // Get user input
                Console.WriteLine("Enter move (source column, destination column):");
                string[] move = Console.ReadLine().Split(',');
                int sourceColumn = int.Parse(move[0].Trim());
                int destinationColumn = int.Parse(move[1].Trim());

                // Move card from source column to destination column
                if (IsValidMove(tableau, sourceColumn, destinationColumn))
                {
                    MoveCard(tableau, sourceColumn, destinationColumn);
                    DisplayTableau(tableau);
                }
                else
                {
                    Console.WriteLine("Invalid move. Try again.");
                }
            }

            Console.WriteLine("Congratulations! You won the game!");
        }

        static List<string> InitializeDeck()
        {
            List<string> suits = new List<string> { "Hearts", "Diamonds", "Clubs", "Spades" };
            List<string> ranks = new List<string> { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
            List<string> deck = new List<string>();

            foreach (string suit in suits)
            {
                foreach (string rank in ranks)
                {
                    deck.Add(rank + " of " + suit);
                }
            }

            return deck;
        }

        static void Shuffle(List<string> deck)
        {
            Random rand = new Random();
            int n = deck.Count;
            while (n > 1)
            {
                n--;
                int k = rand.Next(n + 1);
                string temp = deck[k];
                deck[k] = deck[n];
                deck[n] = temp;
            }
        }

        static List<List<string>> DealCards(List<string> deck)
        {
            List<List<string>> tableau = new List<List<string>>();
            for (int i = 0; i < 10; i++)
            {
                List<string> column = new List<string>();
                for (int j = 0; j < 5; j++)
                {
                    column.Add(deck[i * 5 + j]);
                }
                tableau.Add(column);
            }
            return tableau;
        }

        static void DisplayTableau(List<List<string>> tableau)
        {
            Console.WriteLine("Tableau:");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Column " + (i + 1) + ":");
                foreach (string card in tableau[i])
                {
                    Console.WriteLine(card);
                }
                Console.WriteLine();
            }
        }

        static bool IsValidMove(List<List<string>> tableau, int sourceColumn, int destinationColumn)
        {
            if (sourceColumn < 1 || sourceColumn > 10 || destinationColumn < 1 || destinationColumn > 10)
            {
                return false;
            }
            if (tableau[sourceColumn - 1].Count == 0 || tableau[destinationColumn - 1].Count == 5)
            {
                return false;
            }
            return true;
        }

        static void MoveCard(List<List<string>> tableau, int sourceColumn, int destinationColumn)
        {
            string card = tableau[sourceColumn - 1][tableau[sourceColumn - 1].Count - 1];
            tableau[sourceColumn - 1].RemoveAt(tableau[sourceColumn - 1].Count - 1);
            tableau[destinationColumn - 1].Add(card);
        }

        static bool IsGameWon(List<List<string>> tableau)
        {
            foreach (List<string> column in tableau)
            {
                if (column.Count < 5)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
