using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public sealed class Deck
    {
        private static Deck instance = null;
        private List<Card> cards;
        private Random random;
        private IEnumerable<(string, string)> cardTouples;

        private Deck()
        {
            cards = new List<Card>();
            random = new Random();
            InitializeDeck();
            Shuffle();
        }

        public static Deck Instance
        {
            get 
            { 
                if(instance == null)
                {
                    instance = new Deck();
                }
                return instance;
            }
        }

        private void InitializeDeck()
        {
            string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
            string[] values = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };

            cardTouples = from suit in suits
                          from value in values
                          select (suit, value);

            cards.AddRange(cardTouples.Select(card => new Card(card.Item1, card.Item2)));
        }

        public void AddDecks(int numOfDecks)
        {
            if (numOfDecks < 1 || numOfDecks > 8)
            {
                throw new ArgumentOutOfRangeException($"Number of decks must be at least 1 and no greater than 8: Value provided was {numOfDecks}.");
            }
            cards.AddRange(Enumerable.Repeat(cardTouples.Select(card => new Card(card.Item1, card.Item2)), numOfDecks).SelectMany(n => n));
            Shuffle();
        }

        private void Shuffle()
        {
            int n = cards.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                Card value = cards[k];
                cards[k] = cards[n];
                cards[n] = value;
            }
        }

        public Card DealCard()
        {
            if (cards.Count == 0)
            {
                Console.WriteLine("The deck is empty!");
                return null;
            }

            Card dealtCard = cards[0];
            cards.RemoveAt(0);
            return dealtCard;
        }

        public void PrintDeck()
        {
            foreach (Card card in cards)
            {
                Console.WriteLine(card);
            }
        }

        public int Count()
        {
            return cards.Count;
        }
    }
}
