using BlackJack;
using System;
public class Program
{
    public static void Main(string[] args)
    {
        Deck deck = Deck.Instance;
        deck.AddDecks(1);
        Console.WriteLine("Initial Deck:");
        deck.PrintDeck();
        Console.WriteLine("Shuffled Deck:");
        deck.PrintDeck();
        Console.WriteLine($"\nTotal Number of Cards:  {deck.Count()}"); // Total Number of cards in deck at start
        Console.WriteLine();
        Hand hand1 = new Hand();
        Dealer dealer = new Dealer();
        dealer.dealCard();
        dealer.dealCard();
        Console.WriteLine($"Dealers Revealed Card is: {dealer.getRevealedCard()}");

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
        dealer.playDealer();


        Console.WriteLine($"\nDealers Final Value is: {dealer.getValue()}");
        dealer.revealHand();
        if (dealer.isBust())
        {
            Console.WriteLine("\nDealer Bust...\n");
            hand1.printHand();
            Console.WriteLine($"Hand value: {hand1.value}");
        }
        else if (dealer.getValue() == 21)
        {
            Console.WriteLine("\nHand Value is 21!!!!!!!");
            hand1.printHand();
        }

    }
}