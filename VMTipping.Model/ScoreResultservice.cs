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
        private IList<Round> _rounds;
        private IList<RoundPrediction> _roundPredictions; 

        public ScoreResultservice(IList<User> users, IList<MatchPrediction> matchPredictions, IList<Game> games, IList<Round> rounds, IList<RoundPrediction> roundPredictions )
        {
            _users = users;
            _matchPredictions = matchPredictions;
            _games = games;
            _rounds = rounds;
            _roundPredictions = roundPredictions;
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
                    TotalScore = GetTotalScoreForUserUntilGame(user, game)
                });
            }
            return gus.OrderByDescending(g=>g.TotalScore).ToList();
        }
        
        public int GetTotalScoreForUserUntilGame(User user, Game untilGame)
        {
            var totalscore = 0;
            //foreløpig for gruppespill
            foreach (var game in _games.Where(g=>g.IsPlayed && g.Date <= untilGame.Date))
            {
                var gus = new GameUserScore
                {
                    Game = game,
                    User = user,
                    MatchPrediction = _matchPredictions.First(mp => mp.UserId == user.Id && mp.MatchId == game.Id)
                };
                totalscore += gus.ScoreThisGame;
            }
            //sluttspill
            foreach (var round in _rounds.Where(r=>r.StartActive <= untilGame.Date))
            {
                var rp = _roundPredictions.First(rpr => rpr.RoundId == round.Id && rpr.UserId == user.Id);
                var rus = new RoundUserScore
                {
                    Round = round,
                    RoundPrediction = rp,
                    User = user
                };
                if (untilGame.Date != null)
                    totalscore += rus.ScoreThisRoundBeforeDateTime(untilGame.Date.Value.AddHours(3));
            }
            return totalscore;
        }

        public IList<RoundUserScore> GetRoundUserScoresForRound(Round round)
        {
            var rus = new List<RoundUserScore>();
            foreach (var user in _users)
            {
                rus.Add(new RoundUserScore
                {
                    Round = round,
                    User = user,
                    RoundPrediction = _roundPredictions.First(rp => rp.UserId == user.Id && rp.RoundId == round.Id),
                    TotalScore = GetTotalScoreForUserUntilRound(user, round)
                });
            }
            return rus.OrderByDescending(r => r.TotalScore).ToList();
        }

        public int GetTotalScoreForUserUntilRound(User user, Round untilRound)
        {
            var totalscore = 0;
            foreach (var game in _games.Where(g => g.IsPlayed && g.Date <= untilRound.EndActive))
            {
                var gus = new GameUserScore
                {
                    Game = game,
                    User = user,
                    MatchPrediction = _matchPredictions.First(mp => mp.UserId == user.Id && mp.MatchId == game.Id)
                };
                totalscore += gus.ScoreThisGame;
            }
            foreach (var round in _rounds.Where(r => r.StartActive.HasValue && r.StartActive <= untilRound.StartActive))
            {
                var rus = new RoundUserScore
                {
                    Round = round,
                    User = user,
                    RoundPrediction = _roundPredictions.First(rp => rp.UserId == user.Id && rp.RoundId == round.Id),
                };
                totalscore += rus.ScoreThisRound;
            }
            return totalscore;
        }
    }
}
