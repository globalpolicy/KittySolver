using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitty
{
    //represents the score of a set of 3 cards
    public class SetScore
    {
        #region Private fields
        private double weightedScore = -1;
        #endregion

        #region Public properties
        public double TrialScore { get; set; }
        public double FalashScore { get; set; }
        public double RunScore { get; set; }
        public double JuteScore { get; set; }
        public double BadhiScore { get; set; }

        public double WeightedScore
        {
            get
            {
                if (weightedScore == -1)
                    weightedScore = GetAggregateSetScore();
                return weightedScore;
            }
            private set { }
        }
        #endregion
        public SetScore(double trialScore, double falashScore, double runScore, double juteScore, double badhiScore)
        {
            this.TrialScore = trialScore;
            this.FalashScore = falashScore;
            this.RunScore = runScore;
            this.JuteScore = juteScore;
            this.BadhiScore = badhiScore;
        }

        #region Private methods
        private double GetAggregateSetScore()
        {
            return this.TrialScore * 10000 + this.RunScore * 400 + this.FalashScore * 200 + this.JuteScore * 10 + this.BadhiScore; //weighted scalar score so that the Min score of a superior sequence is greater than the Max scores of any other inferior sequences combined
        }
        #endregion
    }
}
