using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public class GameManager
    {
        List<Player> players;
        Dealer dealer;
        int packsInDeck;

        public GameManager() 
        {
            players = new List<Player>();
            dealer = new Dealer();
            packsInDeck = 6;
            createDeck();
            setupPlayers();
        }

        public void runGame ()
        {
           
        }

        private void createDeck()
        {
            //while (true)
            //{
            //    Console.WriteLine($"{packsInDeck} decks are will be shuffled into deck, would you like to change the amount? Yes(y) No (n)");
            //    string playerIn = Console.ReadLine().ToLower();
            //    if (playerIn == "y" || playerIn == "yes")
            //    {
            //        while (true)
            //        {
            //            Console.WriteLine("How many decks should be shuffled in (1-8)?");
            //            playerIn = Console.ReadLine();
            //            int deckCountFromPlayer = -1;
            //            if (int.TryParse(playerIn, out deckCountFromPlayer))
            //            {
            //                if (deckCountFromPlayer >= 1 && deckCountFromPlayer <= 8)
            //                {
            //                    packsInDeck = deckCountFromPlayer;
            //                    break;
            //                }
            //                else
            //                {
            //                    Console.WriteLine("Please enter value between 1-8");
            //                }
            //            }
            //            else
            //            {
            //                Console.WriteLine("Please enter value between 1-8");
            //            }
            //        }
            //        break;
            //    }
            //    else if (playerIn == "n" || playerIn == "no")
            //    {
            //        break;
            //    }
            //    else
            //    {
            //        Console.WriteLine("Please provide a yes or no.");
            //    }
            //}
            Deck.Instance.AddDecks(packsInDeck - 1);
            Console.WriteLine($"Continuing with {packsInDeck} decks");
        }

        private void setupPlayers()
        {
            while(true)
            {
                Console.WriteLine("How many players are there (1-9)?");
                String playerIn = Console.ReadLine();
                int numOfPlayers = -1;
                if (int.TryParse(playerIn, out numOfPlayers))
                {
                    if (numOfPlayers >= 1 && numOfPlayers <= 9)
                    {
                        for (int i = 0; i < numOfPlayers; i++)
                        {
                            players.Add(new Player($"Player {i+1}"));
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter value between 1-9");
                    }
                }
                else
                {
                    Console.WriteLine("Please enter value between 1-9");
                }

            }
            Console.WriteLine($"{players.Count} seats ready to go");
            Program.waitToClear();
        }

        public void dealCards()
        {
            Console.WriteLine("\nDealing Cards...\n");
            foreach (Player p in players) 
            {
                p.addCard();
            }
            dealer.addCard();
            foreach (Player p in players)
            {
                p.addCard();
            }
            dealer.addCard();

            foreach (Player p in players)
            {
                Console.WriteLine($"{p.playerName}'s cards are :");
                if(p.blackJack)
                {
                    Console.WriteLine("Have BlackJack");
                }
                p.printCurrentHand();
                Console.WriteLine($"\nHand value: {p.getValue()}\n");
            }

            Console.WriteLine($"\nDealers Revealed Card is: {dealer.getRevealedCard()}\n");
            if (dealer.blackJack) //Blackjack Check
            {
                if(dealer.getRevealedCard().value == "Ace" || dealer.getRevealedCard().value == "Jack"
                    || dealer.getRevealedCard().value == "Queen" || dealer.getRevealedCard().value == "Kind"
                    || dealer.getRevealedCard().value == "10")
                {
                    Console.WriteLine("Dealer has BlackJack\n");
                    foreach (Player p in players)
                    {
                        if (p.blackJack)
                        {
                            Console.WriteLine($"{p.playerName} has BlackJack"); //TODO: Player takes back bet

                        }
                        else
                        {
                            Console.WriteLine($"{p.playerName} lost bet"); //TODO: Player Losses Bet
                        }
                    }
                }
                clearAllHands();
            }
            Program.waitToClear();
        }

        public void playOutHands()
        {
            foreach (Player p in players)
            {
                while(p.canSplit())
                {
                    Console.Clear();
                    Console.WriteLine($"\nDealers Revealed Card is: {dealer.getRevealedCard()}\n");
                    Console.WriteLine($"\n{p.playerName} is up!\n");
                    Console.WriteLine($"Current Hand Value is : {p.getValue()}. Hit(H), Stand(S), or Split(p)?");
                    p.printCurrentHand();
                    string playerIn = Console.ReadLine().ToLower();
                    if (playerIn == "h" || playerIn == "hit")
                    {
                        p.addCard();
                    }
                    else if (playerIn == "s" || playerIn == "stand")
                    {
                        p.stand();
                    }
                    else if (playerIn == "p" || playerIn == "split")
                    {
                        p.splitHand();
                    }
                    else
                    {
                        Console.WriteLine("Please enter an 'H' (Hit), 'S' (Stand), 'P' (Split)");
                    }
                    if (p.isStand() || p.isBust())
                    {
                        p.nextHand();
                    }
                }
                while (p.currentHand < p.hands.Count && !p.isStand())
                {
                    if (p.getCardCount() == 1)
                    {
                        p.addCard();
                        p.printCurrentHand();
                    }
                    Console.Clear();
                    Console.WriteLine($"\nDealers Revealed Card is: {dealer.getRevealedCard()}\n");
                    Console.WriteLine($"\n{p.playerName} is up!\n");
                    Console.WriteLine($"\nCurrent Hand Value is : {p.getValue()}. Hit(H) or Stand(S)?"); //TODO: If hand has 1 card, automaticlly draw second card
                    p.printCurrentHand();
                    string playerIn = Console.ReadLine().ToLower();
                    if (playerIn == "h" || playerIn == "hit")
                    {
                        p.addCard();
                    }
                    else if (playerIn == "s" || playerIn == "stand")
                    {
                        p.stand();
                    }
                    else
                    {
                        Console.WriteLine("Please enter an 'H' (Hit) or 'S' (Stand)");
                    }
                    if (p.isStand() || p.isBust())
                    {
                        if(p.isBust())
                        {
                            Console.WriteLine($"\n{p.playerName} busted");
                        }
                        Console.WriteLine($"\nFinal Hand Value: {p.getValue()}\n");
                        //p.printCurrentHand();
                        //Console.WriteLine();
                        p.nextHand();
                        //Program.waitToClear();
                    }
                }
                p.printAllHands();
                Program.waitToClear();
            }
            Console.Clear();
            dealer.playDealer();
            Console.WriteLine($"\nDealers Final Value is: {dealer.getValue()}");
            dealer.printCurrentHand();
            Console.WriteLine();
            if (dealer.isBust())
            {
                Console.WriteLine("\nDealer Bust...\n");
                //Console.WriteLine($"Hand value: {dealer.getValue()}\n");
            }

            if (!dealer.isBust())
            {
                foreach (Player p in players)
                {
                    for(int i = 0; i < p.hands.Count; i++)
                    {
                        Hand h = p.hands[i];
                        if (!h.bust)
                        {
                            if (h.value > dealer.getValue())
                            {
                                Console.WriteLine($"{p.playerName}'s Hand #{i + 1} (Value: {p.getValue()}) wins!");
                            }
                            else if (h.value == dealer.getValue())
                            {
                                Console.WriteLine($"{p.playerName}'s Hand #{i + 1} (Value: {p.getValue()}) Stand-off");
                            }
                            else
                            {
                                Console.WriteLine($"{p.playerName}'s Hand #{i + 1} (Value: {p.getValue()}) loses to Dealer!");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"{p.playerName}'s Hand #{i + 1} (Value: {p.getValue()}) Busted");
                        }
                    }

                }
            }
            else
            {
                Console.WriteLine("\nDealer Busted");
                foreach (Player p in players)
                {
                    for(int i = 0; i < p.hands.Count; i++)
                    {
                        Hand h = p.hands[i];
                        if (!h.bust)
                        {
                            Console.WriteLine($"{p.playerName}'s Hand #{i + 1} (Value: {p.getValue()}) Wins");
                        }
                        else
                        {
                            Console.WriteLine($"{p.playerName}'s Hand #{i + 1} (Value: {p.getValue()}) Busted");
                        }
                    }
                }
            }
            Program.waitToClear();
        }

        public void clearAllHands()
        {
            foreach(Player p in players)
            {
                p.hands.Clear();
            }
            dealer.hands.Clear();
            Console.Clear();
        }
    }
}
