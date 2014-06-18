using System.Collections.Generic;
using System.Linq;
using VMTippekonkurranse.Models;

namespace VMTipping.Model
{
    public class ScoreService
    {
        public IList<Score> GetRanking(IList<User> users, IList<Game> gamesPlayed)
        {
            var scores = users.Select(user => GetScoreForUser(user, gamesPlayed)).ToList();

            int ranking = 1;
            scores = scores.OrderByDescending(s => s.Points).ToList();
            foreach (var score in scores)
            {
                score.Ranking = ranking;
                ranking += 1;
            }
            return scores;
        }

        private Score GetScoreForUser(User user, IList<Game> gamesPlayed)
        {
            int points = 0;
            foreach (var game in gamesPlayed)
            {
                var prediction = user.MatchPredictions.First(m => m.HomeTeam.Name == game.HomeTeam.Name && m.AwayTeam.Name == game.AwayTeam.Name);
                if (prediction.Result == game.Result) points += 2;
                if (prediction.HomeGoals == game.HomeGoals) points += 1;
                if (prediction.AwayGoals == game.AwayGoals) points += 1;
            }
            return new Score
            {
                User = user,
                Points = points
            };
        }
    }
}
