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
        Deck deck;

        public Dealer()

        {
            hand = new Hand();
            deck = Deck.Instance;
        }

        public void clearHand()
        {
            hand = new Hand();
        }

        public void playDealer()
        {
            while (hand.value <= 16)
            {
                hand.AddCard(deck.DealCard());
            }
            hand.Stand = true;
        }
    }
}
