using BlackJack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public class Player
    {
        public List<Hand> hands;
        public int currentHand;
        public int bankroll;
        public string playerName;
        public bool blackJack;

        public Player()
        {
            hands = new List<Hand>();
            currentHand = -1;
            bankroll = 1000;
            playerName = "Player";
            blackJack = false;
        }

        public Player(string name)
        {
            hands = new List<Hand>();
            hands.Add(new Hand());
            currentHand = 0;
            bankroll = 1000;
            playerName = name;
            blackJack = false;
        }

        public bool canSplit()
        {
            return hands[currentHand].canSplit;
        }

        public int getValue()
        {
            return hands[currentHand].value;
        }

        public bool isBust()
        {
            return hands[currentHand].bust;
        }

        public int getCardCount()
        {
            return hands[currentHand].cards.Count;
        }

        public void setBet(int bet)
        {
            hands[currentHand].bet = bet;
        }

        public int getBet()
        {
            return hands[currentHand].bet;
        }

        public int getCurrentHand()
        {
            return currentHand;
        }

        public void clearHands()
        {
            hands.Clear();
            hands.Add(new Hand());
            currentHand = 0;
            blackJack = false;
        }


        public void addCard()
        {
            if (hands.Count < 1)
            {
                hands.Add(new Hand());
                currentHand = 0;
            }
            hands[currentHand].drawCard();
            if (hands.Count == 1 && getCardCount() == 2 && hands[currentHand].value == 21)
            {
                blackJack = true;
            }
        }

        public void printCurrentHand()
        {
            hands[currentHand].printHand();
        }

        public void printAllHands()
        {
            for (int i = 0; i < hands.Count; i++)
            {
                Console.WriteLine($"{playerName}'s Hand #{i + 1} value: {hands[i].value}");
                hands[i].printHand();
                Console.WriteLine();
            }
        }

        public bool isStand()
        {
            return hands[currentHand].stand;
        }

        public void hit()
        {
            hands[currentHand].drawCard();
        }

        public void stand()
        {
            hands[currentHand].stand = true;
            hands[currentHand].canSplit = false;
        }

        public void nextHand()
        {
            if (currentHand == hands.Count - 1)
            {
                currentHand = 0;
            }
            else
            {
                currentHand++;
            }
        }

        public void splitHand()
        {
            hands[currentHand].canSplit = false;
            hands.Add(new Hand());
            Card moveCard = hands[currentHand].removeCard(1);//Remove second card from current hand and
            int newHandIndex = hands.Count - 1;
            hands[newHandIndex].addCard(moveCard); // add card to the new hand
            bankroll -= hands[currentHand].bet; //Remove split bet from bankroll
            hands[newHandIndex].bet = hands[currentHand].bet; //Set split hand bet
            hands[currentHand].drawCard();
            hands[newHandIndex].drawCard();
            if (moveCard.value == "Ace") //Split is Aces, draw 1 card for each hand and end turn
            {
                hands[currentHand].stand = true;
                hands[newHandIndex].stand = true;
            }
        }
    }

    public class Dealer : Player
    {
        public Dealer()
        {
            hands = new List<Hand>();
            hands.Add(new Hand());
            currentHand = 0;
            bankroll = 0;
            playerName = "Dealer";
            blackJack = false;
        }
        public void playDealer()
        {
            while (hands[currentHand].value <= 16)
            {
                hit();
            }
            stand();

        }
        public Card getRevealedCard()
        {
            return hands[currentHand].cards[0];
        }
    }

}

