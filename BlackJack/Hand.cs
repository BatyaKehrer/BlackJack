using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public class Hand
    {
        public List<Card> cards;
        public int value;
        public int bet;
        public bool blackJack;
        public bool bust;
        public bool stand;

        public Hand()
        {
            cards = new List<Card>();
            value = -1;
            bet = -1;
            blackJack = false;
            bust = false;
            stand = false;
        }


        
        public void addCard()
        {
            cards.Add(Deck.Instance.DealCard());
            calculateValue();
            if(cards.Count == 2 && value == 21)
            {
                blackJack = true;
                stand = true;
            }
            

        }

        public Card removeCard()
        {
            Card card = cards[0];
            cards.RemoveAt(0);
            return card;
        }

        private void calculateValue()
        {
            int aceCount = 0;
            int checkVal = 0;
            foreach (Card card in cards) //Calculate all number and face cards
            {
                string val = card.value;
                switch (val)
                {
                    case "Ace":
                        aceCount++;
                        break;
                    case "King":
                    case "Queen":
                    case "Jack":
                        checkVal += 10;
                        break;
                    default:
                        checkVal += int.Parse(val);
                        break;
                }
            }
            for (int i = 0; i < aceCount; i++) //Calculate all Aces
            {
                checkVal += checkVal + 11 <= 21 ? 11 : 1;
            }
            value = checkVal;
            if(value > 21)
            {
                bust = true;
                stand = true;
            }
            else if (value == 21)
            {
                stand = true;
            }

        }

        public void setBet(int val)
        {
            bet = val;
        }

        public void printHand()
        {
            foreach (Card card in cards)
            {
                Console.WriteLine(card);
            }
        }
    }
}
