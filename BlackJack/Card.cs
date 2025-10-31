using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public class Card
    {
        public string suit { get; }
        public string value { get; }

        public Card(string suit, string value)
        {
            this.suit = suit;
            this.value = value;
        }

        public override string ToString()
        {
            return $"{value} of {suit}";
        }
    }
}
