using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    internal class Hand
    {
        public List<Card> cards;
        public int value;
        public bool Blackjack;
        public bool Bust;
        public bool Stand;

        public Hand()
        {
            cards = new List<Card>();
            value = -1;
            Blackjack = false;
            Bust = false;
            Stand = false;
        }


        
        public void AddCard(Card card)
        {
            cards.Add(card);
            CalculateValue();

        }

        private void CalculateValue()
        {
            int aceCount = 0;
            int checkVal = 0;
            foreach (Card card in cards) //Calculate all number and face cards
            {
                string val = card.Value;
                switch (val)
                {
                    case "Ace":
                        aceCount++;
                        break;
                    case "King":
                    case "Queen":
                    case "Jack":
                    case "10":
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
                Bust = true;
            }
            else if (value == 21)
            {
                Stand = true;
            }

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
