using BlackJack;
using System;
public class Program
{
    public static void Main(string[] args)
    {
        Deck deck;
        try
        {
            deck = new Deck(1);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
        Console.WriteLine("Initial Deck:");
        deck.PrintDeck();

        Console.WriteLine("\nShuffling Deck...");
        deck.Shuffle();
        Console.WriteLine("Shuffled Deck:");
        deck.PrintDeck();
        Console.WriteLine($"Total Number of Cards:  {deck.Count()}"); // Total Number of cards in deck at start

        Console.WriteLine("\nDealing 20 cards:");
        for (int i = 0; i < 20; i++)
        {
            Card card = deck.DealCard();
            if (card != null)
            {
                Console.WriteLine($"Dealt: {card}");
            }
        }

        Console.WriteLine($"\nCards left in deck: {deck.Count()}"); // Assuming a Count() method or property on Deck

        Console.WriteLine("\nDeck is low, adding 2 decks");
        try
        {
            deck.AddDecks(2);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
        deck.PrintDeck();
        Console.WriteLine($"Total Number of Cards:  {deck.Count()}"); // Total Number of cards in deck at start

        Console.WriteLine("\nDealing 40 cards:");
        for (int i = 0; i < 40; i++)
        {
            Card card = deck.DealCard();
            if (card != null)
            {
                Console.WriteLine($"Dealt: {card}");
            }
        }
        Console.WriteLine($"\nCards left in deck: {deck.Count()}"); // Assuming a Count() method or property on Deck

    }
}