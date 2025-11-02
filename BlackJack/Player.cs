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
        public string playerName;
        public bool blackJack;

        public Player()
        {
            hands = new List<Hand>();
            currentHand = -1;
            bankrol = 1000;
            playerName = "Player";
            blackJack = false;
        }

        public Player(string name)
        {
            hands = new List<Hand>();
            currentHand = -1;
            bankrol = 1000;
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
            hands[currentHand].drawCard();
            if (hands.Count == 1 && hands[currentHand].value == 21)
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

        public void splitHand()
        {
            hands.Add(new Hand());
            Card moveCard = hands[currentHand].removeCard(1);//Remove second card from current hand and
            int newHandIndex = hands.Count - 1;
            hands[newHandIndex].addCard(moveCard); // add card to the new hand
            if (moveCard.value == "Ace") //Split is Aces, draw 1 card for each hand and end turn
            {
                hands[currentHand].drawCard();
                hands[currentHand].stand = true;
                hands[newHandIndex].drawCard();
                hands[newHandIndex].stand = true;
            }
            hands[currentHand].canSplit = false;
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
}
