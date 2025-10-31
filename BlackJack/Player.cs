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
        public int bankrol;

        public Player()
        {
            hands = new List<Hand>();
            currentHand = -1;
            bankrol = 1000;
        }

        public int getValue()
        {
            return hands[currentHand].value;
        }

        public bool isBust()
        {
            return hands[currentHand].bust;
        }

        public bool isBlackJack()
        {
            return hands[currentHand].blackJack;
        }

        public void dealCard()
        {
            hands[currentHand].addCard();
        }

        public void clearHands()
        {
            hands.Clear();
        }

        public void addCard()
        {
            if(hands.Count < 1)
            {
                hands.Add(new Hand());
                currentHand = 0;
            }
            hands[currentHand].addCard();
        }

        public void printHand()
        {
            hands[currentHand].printHand();
        }

        public bool isStand()
        {
            return hands[currentHand].stand;
        }

        public void hit()
        {
            hands[currentHand].addCard();
        }

        public void stand()
        {
            hands[currentHand].stand = true;
            nextHand();
        }

        public void nextHand()
        {
            if(currentHand == hands.Count - 1)
            {
                currentHand = 0;
            }
            else
            {
                currentHand++;
            }
        }

    }
}

public class Dealer : Player
{
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

    public void revealHand()
    {
        hands[currentHand].printHand();
    }
}
