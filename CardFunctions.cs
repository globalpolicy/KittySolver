using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Kitty
{
    class CardFunctions
    {
        private static Random random = new Random();

        public delegate void ProgressReporter(int progress);
        public static Card GetRandomCard()
        {
            int randomValue = random.Next(1, 14);
            if (randomValue == 1)
                randomValue = 14; //14 for Ace
            int randomSuit = random.Next(1, 5);
            return new Card(randomValue, randomSuit);
        }

        /// <summary>
        /// Generates a list of Cards from a space-delimited string. e.g. "10s 8d ah js qc" would produce a list of 10 of spades, 8 of Diamonds, Ace of Hearts, Jack of Spades and a Queen of Clubs
        /// </summary>
        /// <param name="cardsString">Space-delimited string representing the cards to be generated</param>
        /// <returns></returns>
        public static List<Card> GetCards(string cardsString)
        {
            List<Card> listOfCards = new List<Card>();
            string[] cardTokens = cardsString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < cardTokens.Length; i++)
            {
                string cardToken = cardTokens[i].ToUpper();
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
                listOfCards.Add(new Card(valueInt, suitInt));
            }
            return listOfCards;
        }
        public static List<Card> GetRandomCards(int numOfCards)
        {
            List<Card> retval = new List<Card>();
            for (int i = 0; i < numOfCards; i++)
            {
                Card card;
                do
                {
                    card = GetRandomCard();
                } while (retval.Contains(card)); //keep generating a new card until there's no duplicate

                retval.Add(card);
            }
            return retval;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool HasDuplicates(List<int> intsList)
        {
            HashSet<int> set = new HashSet<int>(intsList);
            return set.Count != intsList.Count;
        }

        public static List<Hand> GetAllCombinations(List<Card> cards, ProgressReporter progressReporter = null)
        {
            double counter = 0;
            List<Hand> table = new List<Hand>();

            for (int i = 0; i < cards.Count; i++)
            {
                for (int j = 0; j < cards.Count; j++)
                {
                    if (HasDuplicates(new List<int>() { i, j }))
                        continue;
                    for (int k = 0; k < cards.Count; k++)
                    {
                        if (HasDuplicates(new List<int>() { i, j, k }))
                            continue;
                        for (int l = 0; l < cards.Count; l++)
                        {
                            if (HasDuplicates(new List<int>() { i, j, k, l }))
                                continue;
                            for (int m = 0; m < cards.Count; m++)
                            {
                                if (HasDuplicates(new List<int>() { i, j, k, l, m }))
                                    continue;
                                for (int n = 0; n < cards.Count; n++)
                                {
                                    if (HasDuplicates(new List<int>() { i, j, k, l, m, n }))
                                        continue;
                                    for (int o = 0; o < cards.Count; o++)
                                    {
                                        if (HasDuplicates(new List<int>() { i, j, k, l, m, n, o }))
                                            continue;
                                        for (int p = 0; p < cards.Count; p++)
                                        {
                                            if (HasDuplicates(new List<int>() { i, j, k, l, m, n, o, p }))
                                                continue;
                                            for (int q = 0; q < cards.Count; q++)
                                            {
                                                if (HasDuplicates(new List<int>() { i, j, k, l, m, n, o, p, q }))
                                                    continue;
                                                Hand hand = new Hand((cards[i], cards[j], cards[k], cards[l], cards[m], cards[n], cards[o], cards[p], cards[q]));
                                                table.Add(hand);

                                                if (progressReporter != null)
                                                {
                                                    counter++;
                                                    if (counter % 1000 == 0) //only update every 1000 iterations, else the UI thread will be flooded with updates
                                                        progressReporter((int)(counter / (9 * 8 * 7 * 6 * 5 * 4 * 3 * 2) * 100.0));
                                                }

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return table;
        }

        /// <summary>
        /// Returns the best arrangement of the cards from the given list
        /// </summary>
        /// <param name="combo">List of all the different arrangements possible for the cards of a given hand</param>
        /// <returns></returns>
        public static Hand GetBestHand(List<Hand> combo)
        {
            Hand bestHand = null;
            double bestScore = 0;
            for (int i = 0; i < combo.Count; i++)
            {
                Hand hand = combo[i];
                if (hand.TotalScore > bestScore)
                {
                    bestScore = hand.TotalScore;
                    bestHand = hand;
                }
            }
            return bestHand;
        }

        /// <summary>
        /// Returns the hand with the best sets at the front
        /// </summary>
        /// <param name="hand">The hand of cards to sort</param>
        /// <returns></returns>
        public static Hand GetOrderedHand(Hand hand)
        {
            //put the best sets at the front
            Hand orderedHand = new Hand(from set in hand.Sets
                                        orderby set.Score.WeightedScore descending
                                        select set);
            return orderedHand;
        }
    }
}
