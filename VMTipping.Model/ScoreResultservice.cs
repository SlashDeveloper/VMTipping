using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMTippekonkurranse.Models;

namespace VMTipping.Model
{
    public class ScoreResultservice
    {
        private IList<User> _users;
        private IList<MatchPrediction> _matchPredictions;
        private IList<Game> _games; 

        public ScoreResultservice(IList<User> users, IList<MatchPrediction> matchPredictions, IList<Game> games )
        {
            _users = users;
            _matchPredictions = matchPredictions;
            _games = games;
        }

        public IList<GameUserScore> GetGameUserScoresForGame(Game game)
        {
            var gus = new List<GameUserScore>();
            foreach (var user in _users)
            {
                gus.Add(new GameUserScore
                {
                    Game = game,
                    User = user,
                    MatchPrediction = _matchPredictions.First(mp=> mp.UserId == user.Id && mp.MatchId == game.Id),
                    TotalScore = GetTotalScoreForUser(user)
                });
            }
            return gus.OrderByDescending(g=>g.TotalScore).ToList();
        }

        public int GetTotalScoreForUser(User user)
        {
            var totalscore = 0;
            //foreløpig for gruppespill
            foreach (var game in _games.Where(g=>g.IsPlayed))
            {
                var gus = new GameUserScore
                {
                    Game = game,
                    User = user,
                    MatchPrediction = _matchPredictions.First(mp => mp.UserId == user.Id && mp.MatchId == game.Id)
                };
                totalscore += gus.ScoreThisGame;
            }
            return totalscore;
        }
    }
}
