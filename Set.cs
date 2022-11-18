using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitty
{
    public class Set
    {
        #region Private fields
        private SetScore score;
        #endregion

        #region Public properties
        public Card FirstCard { get; set; }
        public Card SecondCard { get; set; }
        public Card ThirdCard { get; set; }

        public SetScore Score
        {
            get
            {
                if (score == null)
                    score = GetThrupleScore(new Tuple<Card, Card, Card>(this.FirstCard, this.SecondCard, this.ThirdCard));
                return score;
            }
            private set { }
        }
        #endregion
        public Set(Card FirstCard, Card SecondCard, Card ThirdCard)
        {
            Tuple<Card, Card, Card> sortedCards = SortCards(new List<Card>() { FirstCard, SecondCard, ThirdCard }); //sort the given cards of this set in descending order

            this.FirstCard = sortedCards.Item1;
            this.SecondCard = sortedCards.Item2;
            this.ThirdCard = sortedCards.Item3;
        }

        #region Private methods
        /// <summary>
        /// Returns a descending order of the given cards (A, K, Q, J, 10, ..., 2)
        /// </summary>
        /// <param name="cards">List of cards to sort</param>
        /// <returns></returns>
        private static Tuple<Card, Card, Card> SortCards(List<Card> cards)
        {
            //make a copy of the given cards array. it will be modified in place
            List<Card> cardsCopy = cards.ConvertAll(card => new Card(card.Value, card.Suit));

            //sort the cards in descending order (in place)
            for (int i = 0; i < cardsCopy.Count - 1; i++)
            {
                for (int j = i + 1; j < cardsCopy.Count; j++)
                {
                    if (cardsCopy[j].Value > cardsCopy[i].Value)
                    {
                        Card tmp = cardsCopy[i];
                        cardsCopy[i] = cardsCopy[j];
                        cardsCopy[j] = tmp;
                    }
                }
            }

            return new Tuple<Card, Card, Card>(cardsCopy[0], cardsCopy[1], cardsCopy[2]);
        }

        /// <summary>
        /// Returns a SetScore object corresponding to the score of this set of 3 cards
        /// </summary>
        /// <param name="thrupleCards">The three cards sorted in descending order</param>
        /// <returns></returns>
        private static SetScore GetThrupleScore(Tuple<Card, Card, Card> thrupleCards)
        {
            double trialScore = 0, falashScore = 0, runScore = 0, juteScore = 0, badhiScore = 0;

            if (thrupleCards.Item1.Value == thrupleCards.Item2.Value && thrupleCards.Item2.Value == thrupleCards.Item3.Value)
            {
                //TRIAL sequence
                trialScore = thrupleCards.Item1.Value; //score = biggest card value
                                                       //retval *= 6; //score multiplier for TRIAL sequence (max=6)
            }
            if (thrupleCards.Item1.Suit == thrupleCards.Item2.Suit && thrupleCards.Item2.Suit == thrupleCards.Item3.Suit)
            {
                //FALASH sequence
                falashScore = thrupleCards.Item1.Value + thrupleCards.Item2.Value / 10.0 + thrupleCards.Item3.Value / 100.0; //score = order dependent importance
            }
            if ((thrupleCards.Item1.Value - 1 == thrupleCards.Item2.Value && thrupleCards.Item2.Value - 1 == thrupleCards.Item3.Value) ||
                IsValidAceRun(thrupleCards))
            {
                //RUN sequence
                runScore = CalculateRunScore(thrupleCards);
            }
            if (thrupleCards.Item1.Value == thrupleCards.Item2.Value && thrupleCards.Item3.Value != thrupleCards.Item2.Value)
            {
                //JUTE sequence type I e.g. 9,9,5
                juteScore = thrupleCards.Item1.Value + thrupleCards.Item3.Value / 10.0; //score = order dependent importance
            }
            else if (thrupleCards.Item2.Value == thrupleCards.Item3.Value && thrupleCards.Item1.Value != thrupleCards.Item2.Value)
            {
                //JUTE sequence type II e.g. 9,5,5
                juteScore = thrupleCards.Item3.Value + thrupleCards.Item1.Value / 10.0; //score = order dependent importance
            }
            if (trialScore == 0 && falashScore == 0 && runScore == 0 && juteScore == 0)
                //since BADHI score can skew the aggregate score if the set is any of the above sequences, only calculate it if none of the above are true
                badhiScore = thrupleCards.Item1.Value + thrupleCards.Item2.Value / 10.0 + thrupleCards.Item3.Value / 100.0; //score = order dependent importance

            return new SetScore(trialScore, falashScore, runScore, juteScore, badhiScore);
        }

        /// <summary>
        /// Returns if the given cards is an Ace RUN sequence
        /// </summary>
        /// <param name="thrupleCards">Sequence of 3 cards in descending order</param>
        /// <returns></returns>
        private static bool IsValidAceRun(Tuple<Card, Card, Card> thrupleCards)
        {
            bool isFirstKind = thrupleCards.Item1.Value == 14 && thrupleCards.Item2.Value == 3 && thrupleCards.Item3.Value == 2; //A,3,2
            bool isSecondKind = thrupleCards.Item1.Value == 14 && thrupleCards.Item2.Value == 13 && thrupleCards.Item3.Value == 12; //A,K,Q
            return isFirstKind || isSecondKind;
        }


        /// <summary>
        /// A23 doesn't play well with the simple summation approach for score calculation. According to this rule, max run = QKA = 39 and the next highest = JQK = 36, but 
        /// A23 = 19. We need simply manually assign a value between 36 and 39 for an A23 run. Other runs will work just fine.
        /// </summary>
        /// <param name="thrupleCards">Sequence of 3 cards in descending order</param>
        /// <returns>The correct score for the run sequence provided</returns>
        private static double CalculateRunScore(Tuple<Card, Card, Card> thrupleCards)
        {
            double score;

            if (thrupleCards.Item1.Value == 14 && thrupleCards.Item2.Value == 3 && thrupleCards.Item3.Value == 2) //if the thruple is an A-2-3 run
                score = 38;
            else
                score = thrupleCards.Item1.Value + thrupleCards.Item2.Value + thrupleCards.Item3.Value; //score = sum of all cards' values

            return score;
        }
        #endregion



        public override string ToString()
        {
            return this.FirstCard.ToString() + " " + this.SecondCard.ToString() + " " + this.ThirdCard.ToString();
        }
    }
}
