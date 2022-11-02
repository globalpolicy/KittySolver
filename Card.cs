using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitty
{
    public struct Card : IEquatable<Card>
    {
        //Value = 14 (Ace), 2, 3 ... to 13 (King)
        //Suit = 1 (Spade), 2 (Heart), 3 (Diamond), 4 (Club)
        public int Value { get; set; }
        public int Suit { get; set; }

        public Card(int value, int suit)
        {
            this.Value = value;
            this.Suit = suit;
        }

        public bool Equals(Card other)
        {
            return this.Suit == other.Suit && this.Value == other.Value;
        }

        public override string ToString()
        {
            string valuePart = "", suitPart = "";
            switch (this.Value)
            {
                case 14:
                    valuePart = "A";
                    break;
                case 11:
                    valuePart = "J";
                    break;
                case 12:
                    valuePart = "Q";
                    break;
                case 13:
                    valuePart = "K";
                    break;
                default:
                    valuePart = this.Value.ToString();
                    break;
            }
            switch (this.Suit)
            {
                case 1:
                    suitPart = "S";
                    break;
                case 2:
                    suitPart = "H";
                    break;
                case 3:
                    suitPart = "D";
                    break;
                case 4:
                    suitPart = "C";
                    break;
            }
            return valuePart + suitPart;
        }
    }

}
