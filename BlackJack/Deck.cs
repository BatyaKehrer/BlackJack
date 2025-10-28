using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    internal class Deck
    {
        private List<Card> cards;
        private Random random;

        public Deck()
        {
            cards = new List<Card>();
            random = new Random();
            InitializeDeck();
        }

        public Deck(int numOfDecks)
        {
            if (numOfDecks < 1 || numOfDecks > 8)
            {
                throw new ArgumentOutOfRangeException($"Number of decks must be at least 1 and no greater than 8: Value provided was {numOfDecks}.");
            }
            cards = new List<Card>();
            random = new Random();
            InitalizeLargeDeck(numOfDecks);
        }

        private void InitializeDeck()
        {
            string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
            string[] values = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };

            var touples = from suit in suits
                          from value in values
                          select (suit, value);

            cards.AddRange(touples.Select(card => new Card(card.suit, card.value)));
        }

        public void AddDecks(int numOfDecks)
        {
            if (numOfDecks < 1 || numOfDecks > 8)
            {
                throw new ArgumentOutOfRangeException($"Number of decks must be at least 1 and no greater than 8: Value provided was {numOfDecks}.");
            }
            InitalizeLargeDeck(numOfDecks);
            Shuffle();
        }

        private void InitalizeLargeDeck(int numOfDecks)
        {
            string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
            string[] values = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };

            var touples = from suit in suits
                          from value in values
                          select (suit, value);
            cards.AddRange(Enumerable.Repeat(touples.Select(card => new Card(card.suit, card.value)), numOfDecks).SelectMany(n => n));


        }

        public void Shuffle()
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
