using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    internal class Dealer
    {
        Hand hand;

        public Dealer()

        {
            hand = new Hand();
        }

        public void clearHand()
        {
            hand = new Hand();
        }

        public void dealCard()
        {
            hand.AddCard(Deck.Instance.DealCard());
        }

        public Card getRevealedCard()
        {
            return hand.cards[0];
        }

        public void revealHand()
        {
            hand.printHand();
        }

        public void playDealer()
        {
            while (hand.value <= 16)
            {
                hand.AddCard(Deck.Instance.DealCard());
            }
            hand.Stand = true;
        }

        public int getValue()
        {
            return hand.value;
        }

        public bool isBust()
        {
            return hand.Bust;
        }

        public bool isBlackJack()
        {
            return hand.Blackjack;
        }
    }
}
