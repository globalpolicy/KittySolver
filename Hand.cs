using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Kitty
{
    //represents a hand of 9 cards i.e. 3 sets
    public class Hand
    {
        #region Private fields
        private double totalScore = -1;
        #endregion

        #region Public properties
        public Set[] Sets { get; set; }
        public double TotalScore
        {
            get
            {
                if (totalScore == -1)
                    totalScore = GetTotalHandScore();
                return totalScore;
            }
            private set { }
        }
        #endregion
        public Hand(ITuple nineCards)
        {
            Set firstSet = new Set((Card)nineCards[0], (Card)nineCards[1], (Card)nineCards[2]);
            Set secondSet = new Set((Card)nineCards[3], (Card)nineCards[4], (Card)nineCards[5]);
            Set thirdSet = new Set((Card)nineCards[6], (Card)nineCards[7], (Card)nineCards[8]);

            this.Sets = new Set[] { firstSet, secondSet, thirdSet };
        }

        public Hand(IOrderedEnumerable<Set> threeSets)
        {
            this.Sets = threeSets.ToArray();
        }

        public Hand(List<Card> nineCards)
        {
            Set firstSet = new Set(nineCards[0], nineCards[1], nineCards[2]);
            Set secondSet = new Set(nineCards[3], nineCards[4], nineCards[5]);
            Set thirdSet = new Set(nineCards[6], nineCards[7], nineCards[8]);

            this.Sets = new Set[] { firstSet, secondSet, thirdSet };
        }

        #region Private methods
        /// <summary>
        /// Returns the total score of all the cards in this hand
        /// </summary>
        /// <returns></returns>
        private double GetTotalHandScore()
        {
            double output = 0;
            foreach (Set set in Sets)
                output += set.Score.WeightedScore;
            return output;
        }
        #endregion

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            this.Sets.ToList().ForEach(set => stringBuilder.Append(set.ToString()).Append(" "));
            return stringBuilder.ToString();
        }

        public List<Card> ToList()
        {
            List<Card> cards = new List<Card>();
            foreach (Set set in this.Sets)
            {
                cards.Add(set.FirstCard);
                cards.Add(set.SecondCard);
                cards.Add(set.ThirdCard);
            }
            return cards;
        }
    }
}
