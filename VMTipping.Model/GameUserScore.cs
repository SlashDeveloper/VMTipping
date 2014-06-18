using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMTippekonkurranse.Models;

namespace VMTipping.Model
{
    public class GameUserScore
    {
        public User User { get; set; }
        public Game Game { get; set; }
        public MatchPrediction MatchPrediction { get; set; }

        public int BetHomeGoals
        {
            get { return MatchPrediction.HomeGoals; }
        }

        public int BetAwayGoals
        {
            get { return MatchPrediction.AwayGoals; }
        }

        public int ScoreThisGame
        {
            get
            {
                var score = 0;
                if (!Game.IsPlayed)
                {
                    return score;
                }

                if (Game.Result == MatchPrediction.Result)
                {
                    //Korrekt tippetegn
                    score = score+2;
                }
                if (BetHomeGoals == Game.HomeGoals)
                {
                    score = score + 1;
                }
                if (BetAwayGoals == Game.AwayGoals)
                {
                    score = score + 1;
                }
                return score;
            }
        }

        public int TotalScore { get; set; }
    }
}
