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
        Console.WriteLine();
        Hand hand1 = new Hand();
        while (!hand1.Bust && !hand1.Stand)
        {
            hand1.AddCard(deck.DealCard());
            Console.WriteLine($"Hand Card Count: {hand1.cards.Count}");
            Console.WriteLine($"Hand value: {hand1.value}");
        }

        if (hand1.Bust)
        {
            Console.WriteLine("\nBust...\n");
            hand1.printHand();
            Console.WriteLine($"Hand value: {hand1.value}");
        }
        else
        {
            Console.WriteLine("\nHand Value is 21!!!!!!!");
            hand1.printHand();
        }
    }
}