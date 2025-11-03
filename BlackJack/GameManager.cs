using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public class GameManager
    {
        public List<Player> players { get; }
        public Dealer dealer;
        int packsInDeck;

        public GameManager() 
        {
            players = new List<Player>();
            dealer = new Dealer();
            packsInDeck = 6;
            createDeck();
            setupPlayers();
        }

        private void createDeck()
        {
            while (true)
            {
                Console.WriteLine($"{packsInDeck} decks will be shuffled into deck, would you like to change the amount? Yes(y) No (n)");
                string playerIn = Console.ReadLine().ToLower();
                if (playerIn == "y" || playerIn == "yes")
                {
                    while (true)
                    {
                        Console.WriteLine("How many decks should be shuffled in (1-8)?");
                        string deckIn = Console.ReadLine();
                        int deckCountFromPlayer = -1;
                        if (int.TryParse(deckIn, out deckCountFromPlayer) && deckCountFromPlayer >= 1 && deckCountFromPlayer <= 8)
                        {
                            packsInDeck = deckCountFromPlayer;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Please enter value between 1-8");
                        }
                    }
                    break;
                }
                else if (playerIn == "n" || playerIn == "no")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Please provide a yes or no.");
                }
            }
            Deck.Instance.addDecks(packsInDeck - 1);
            Console.WriteLine($"Continuing with {packsInDeck} decks");
            Program.waitToClear();
        }

        private void setupPlayers()
        {
            while (true)
            {
                Console.WriteLine("How many players are there (1-9)?");
                string playerIn = Console.ReadLine();
                int numOfPlayers = -1;
                if (int.TryParse(playerIn, out numOfPlayers) && numOfPlayers >= 1 && numOfPlayers <= 9)
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
            Console.WriteLine($"{players.Count} seats ready to go");
            Console.WriteLine($"Each player has a bankroll of ${players[0].bankroll}");
            Program.waitToClear();
        }

        public void playerBet()
        {
            foreach (Player p in players)
            {
                if (p.bankroll > 0)
                {
                    while (true)
                    {
                        Console.WriteLine($"{p.playerName} has {p.bankroll}. How much would you like to bet?");
                        string playerIn = Console.ReadLine();
                        int betAmount = -1;
                        if (int.TryParse(playerIn, out betAmount) && betAmount > 0 && betAmount <= p.bankroll)
                        {
                            p.bankroll -= betAmount; //Subtract amount from players bankroll
                            p.setBet(betAmount); //Set bet for hand
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"Value must be between $1 and ${p.bankroll}");
                        }

                    }
                }
                else
                {
                    Console.WriteLine($"{p.playerName} has drained their bankroll, and must leave the table."); //Player is out of money and can't play
                    Program.waitToClear();
                    players.Remove(p);
                    if (players.Count < 1)
                    {
                        return;
                    }
                }
                Console.Clear();
            }
        }
        
        public void dealCards()
        {
            if (Deck.Instance.needToShuffle) //When the deck gets below 20% reshuffle all cards back into the deck
            {
                Deck.Instance.resetDeck();
            }
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
                p.printCurrentHand();
                if (p.blackJack)
                {
                    Console.WriteLine("\nHas BlackJack");
                }
                Console.WriteLine($"\nHand value: {p.getValue()}\n");
            }

            Console.WriteLine($"\nDealers Revealed Card is: {dealer.getRevealedCard()}\n");
            if (dealer.getRevealedCard().value == "Ace") //TODO: Add option for insurance bet
            {

            }
            if (dealer.blackJack) //Blackjack Check
            {
                Console.WriteLine("Dealer has BlackJack\n");
                foreach (Player p in players)
                {
                    if (p.blackJack)
                    {
                        Console.WriteLine($"{p.playerName} has BlackJack");
                        p.bankroll += p.getBet();

                    }
                    else
                    {
                        Console.WriteLine($"{p.playerName} lost bet"); 
                    }
                }
                Program.waitToClear();
                clearAllHands();
                return;
            }
            Program.waitToClear();
        }

        public void playOutHands()
        {
            foreach (Player p in players)
            {
                while (p.currentHand < p.hands.Count && !p.isStand())
                {
                    if (p.canSplit() && p.bankroll >= p.getBet()) //If the player has the ability to split, and has the bankrol to be able to split
                    {
                        Console.Clear();
                        Console.WriteLine($"\nDealers Revealed Card is: {dealer.getRevealedCard()}\n");
                        Console.WriteLine($"\n{p.playerName} is up!\n");
                        Console.WriteLine($"Hand #{p.currentHand + 1} Value is : {p.getValue()}. Hit(H), Stand(S), or Split(p)?");
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
                    else
                    {
                        Console.Clear();
                        Console.WriteLine($"\nDealers Revealed Card is: {dealer.getRevealedCard()}\n");
                        Console.WriteLine($"\n{p.playerName} is up!\n");
                        Console.WriteLine($"\nHand #{p.currentHand + 1} Value is : {p.getValue()}. Hit(H) or Stand(S)?");
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
                            Console.WriteLine($"\nFinal Value for Hand #{p.getCurrentHand() + 1}: {p.getValue()}\n");
                            if (p.isBust())
                            {
                                Console.WriteLine($"\n{p.playerName} busted");
                            }
                            Program.waitToClear();
                            p.nextHand();
                        }
                    }
                }
            }
            Console.Clear();
            dealer.playDealer();
            Console.WriteLine($"\nDealers Final Value is: {dealer.getValue()}");
            dealer.printCurrentHand();
            Console.WriteLine();
            if (dealer.isBust())
            {
                Console.WriteLine("\nDealer Bust...\n");
            }
        }

        public void levelBankrolls()
        {
            if (!dealer.isBust())
            {
                foreach (Player p in players)
                {
                    for (int i = 0; i < p.hands.Count; i++)
                    {
                        Hand h = p.hands[i];
                        if (!h.bust)
                        {
                            if (h.value > dealer.getValue())
                            {
                                Console.WriteLine($"{p.playerName}'s Hand #{i + 1} (Value: {h.value}) Wins!");
                                p.bankroll += (p.blackJack) ? (int)Math.Round((double)h.bet * 2.5) : h.bet * 2; //If blackjack, player receives 1.5 times their bet back
                            }
                            else if (h.value == dealer.getValue())
                            {
                                Console.WriteLine($"{p.playerName}'s Hand #{i + 1} (Value: {h.value}) Stand-off");
                                p.bankroll += h.bet;
                            }
                            else
                            {
                                Console.WriteLine($"{p.playerName}'s Hand #{i + 1} (Value: {h.value}) Loses to Dealer");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"{p.playerName}'s Hand #{i + 1} (Value: {h.value}) Busted");
                        }
                    }

                }
            }
            else
            {
                Console.WriteLine("\nDealer Busted");
                foreach (Player p in players)
                {
                    for (int i = 0; i < p.hands.Count; i++)
                    {
                        Hand h = p.hands[i];
                        if (!h.bust)
                        {
                            Console.WriteLine($"{p.playerName}'s Hand #{i + 1} (Value: {h.value}) Wins!");
                            p.bankroll += (p.blackJack) ? (int)Math.Round((double)h.bet * 2.5) : h.bet * 2; //If blackjack, player receives 1.5 times their bet back
                        }
                        else
                        {
                            Console.WriteLine($"{p.playerName}'s Hand #{i + 1} (Value: {h.value}) Busted");
                        }
                    }
                }
            }
            Program.waitToClear();
        }

        public void clearAllHands()
        {
            foreach (Player p in players)
            {
                p.clearHands();
            }
            dealer.clearHands();
            Console.Clear();
        }
    }
}
