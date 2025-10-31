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
        List<Player> players = new List<Player>();
        players.Add(new Player());
        players.Add(new Player());
        Dealer dealer = new Dealer();
        
        foreach(Player p in players) //DealCards function in game manager
        {
            p.addCard();
        }
        dealer.addCard();
        foreach (Player p in players) 
        {
            p.addCard();
        }
        dealer.addCard();
        bool blackjackCheck = false; //BlackJackCheck function in gam manager
        foreach (Player p in players)
        {
            if(p.isBlackJack())
            {
                blackjackCheck = true;
            }
        }
        if(dealer.isBlackJack())
        {
            blackjackCheck = true;
        }
        if(blackjackCheck)
        {
            Console.WriteLine("\nSOMEONE HAS A BLACKJACK\n");
        }

        Console.WriteLine($"Dealers Revealed Card is: {dealer.getRevealedCard()}");
        if(dealer.isBlackJack())
        {
            Console.WriteLine($"\nDealer has BLACKJACK!!!!!!!!!!!!!!!!!!!\n");
        }

        foreach (Player p in players)
        {
            Console.WriteLine("\nA new player is up!\n");
            while(p.currentHand < p.hands.Count && !p.isStand())
            {
                Console.WriteLine($"Current Hand Value is : {p.getValue()}. Hit(H) or Stand(S)?");
                p.printHand();
                string playerIn = Console.ReadLine(); //TODO Get User input and do the right action.
                if(playerIn.ToLower() == "h")
                {
                    p.addCard();
                }
                else if (playerIn.ToLower() == "s")
                {
                    p.stand();
                }
                else
                {
                    Console.WriteLine("Please enter an 'H' (Hit) or 'S' (Stand)");
                }
                if(p.isStand() || p.isBust())
                {
                    p.nextHand();
                }
            }
            p.printHand();
        }

        foreach (Player p in players)
        {
            foreach (Hand h in p.hands)
            { 
                if (h.bust)
                {
                    Console.WriteLine($"Player Busted with value: {h.value}");
                }
                else
                {
                    Console.WriteLine($"Player Final Value is {h.value}");
                }
            }
        }
        dealer.playDealer();


        Console.WriteLine($"\nDealers Final Value is: {dealer.getValue()}");
        dealer.revealHand();
        if (dealer.isBust())
        {
            Console.WriteLine("\nDealer Bust...\n");
            dealer.hands[0].printHand();
            Console.WriteLine($"Hand value: {dealer.getValue()}\n");
        }
        else if (dealer.getValue() == 21)
        {
            Console.WriteLine("\nHand Value is 21!!!!!!!");
            dealer.hands[0].printHand();
        }

        if (!dealer.isBust())
        {
            foreach (Player p in players)
            {
                foreach (Hand h in p.hands)
                {
                    if (!h.bust)
                    {
                        if (h.value > dealer.getValue())
                        {
                            Console.WriteLine("Player wins!");
                        }
                        else if (h.value == dealer.getValue())
                        {
                            Console.WriteLine("Stand-off hit");
                        }
                        else
                        {
                            Console.WriteLine("Dealer wins!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Player Busted");
                    }
                }

            }
        }
        else
        {
            Console.WriteLine("Dealer Busted");
            foreach(Player p in players)
            {
                foreach (Hand h in p.hands)
                {
                    if (!h.bust)
                    {
                        Console.WriteLine("Player Wins");
                    }
                    else
                    {
                        Console.WriteLine("Player Busted");
                    }
                }
            }
        }

    }
}