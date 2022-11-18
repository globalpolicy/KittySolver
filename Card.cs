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

        public Card(string cardString)
        {
            string cardToken = cardString.ToUpper();
            string suit = cardToken.Substring(cardToken.Length - 1, 1);
            string value = cardToken.Replace(suit, "");

            int valueInt, suitInt;
            switch (value)
            {
                case "A":
                    valueInt = 14;
                    break;
                case "J":
                    valueInt = 11;
                    break;
                case "Q":
                    valueInt = 12;
                    break;
                case "K":
                    valueInt = 13;
                    break;
                default:
                    valueInt = int.Parse(value);
                    break;
            }
            switch (suit)
            {
                case "S":
                    suitInt = 1;
                    break;
                case "H":
                    suitInt = 2;
                    break;
                case "D":
                    suitInt = 3;
                    break;
                case "C":
                    suitInt = 4;
                    break;
                default:
                    suitInt = 0;
                    break;
            }

            this.Value = valueInt;
            this.Suit = suitInt;
        }

        public bool Equals(Card other)
        {
            if (Object.ReferenceEquals(other, null))
                return false;
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

        public static implicit operator Card(string cardString)
        {
            return new Card(cardString);
        }



        public static bool operator ==(Card card1, Card card2)
        {
            if (Object.ReferenceEquals(card1, null) || Object.ReferenceEquals(card2, null))
                return false;
            return card1.Equals(card2);
        }

        public static bool operator !=(Card card1, Card card2)
        {
            if (Object.ReferenceEquals(card1, null) || Object.ReferenceEquals(card2, null))
                return true;
            return !card1.Equals(card2);
        }
    }

}
