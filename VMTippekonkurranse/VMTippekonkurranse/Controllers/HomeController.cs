using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VMTippekonkurranse.Models;
using VMTipping.Model;

namespace VMTippekonkurranse.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new TippeContext())
            {
                var scoreResultService = new ScoreResultservice(context.Users.ToList(),
                    context.MatchPredictions.ToList(), context.Matches.ToList(),context.Rounds.Include("TeamsInRound").ToList(), context.RoundPredictions.Include("Teams").ToList());
                var earlierGames = new List<GameViewModel>();
                var todaysGames = new List<GameViewModel>();
                var upcomingGames = new List<GameViewModel>();
                var endgame = new List<RoundViewModel>();
                var cetDateTimeNow = CETDateHelper.GetCurrentCETDateTime();
                foreach (var game in context.Matches.Include("HomeTeam").Include("AwayTeam").ToList().Where(d => d.Date != null && d.Date.Value.Date < cetDateTimeNow.Date).OrderByDescending(g => g.Date))
                {
                    earlierGames.Add(new GameViewModel
                    {
                        Game = game,
                        GameUserScores = scoreResultService.GetGameUserScoresForGame(game)
                    });
                }
                foreach (var game in context.Matches.Include("HomeTeam").Include("AwayTeam").ToList().Where(d => d.Date != null && d.Date.Value.Date == cetDateTimeNow.Date).OrderBy(g => g.Date))
                {
                    todaysGames.Add(new GameViewModel
                    {
                        Game = game,
                        GameUserScores = scoreResultService.GetGameUserScoresForGame(game)
                    });
                }
                foreach (var game in context.Matches.Include("HomeTeam").Include("AwayTeam").ToList().Where(d => d.Date != null && d.Date.Value.Date > cetDateTimeNow.Date).OrderBy(g => g.Date))
                {
                    upcomingGames.Add(new GameViewModel
                    {
                        Game = game,
                        GameUserScores = scoreResultService.GetGameUserScoresForGame(game)
                    });
                }
                foreach (var round in context.Rounds.Include("TeamsInRound").Include("TeamsInRound.Team").ToList())
                {
                    List<RoundTeam> teamsinround;
                    if (round.IsRanked)
                    {
                        teamsinround = round.TeamsInRound.OrderBy(r => r.Rank).ToList();
                    }
                    else
                    {
                        teamsinround = round.TeamsInRound.OrderBy(r=>r.Team.Name).ToList();
                    }
                    var user = context.Users.First();
                    var roundPrediction =
                        context.RoundPredictions.First(rp => rp.UserId == user.Id && rp.RoundId == round.Id);
                    endgame.Add(new RoundViewModel
                    {
                        Round = round,
                        RoundUserScores = scoreResultService.GetRoundUserScoresForRound(round),
                        RoundTeams = teamsinround,
                        NumTeams = roundPrediction != null ? context.RoundPredictionTeams.Count(rpt=>rpt.RoundPredictionId == roundPrediction.Id) : teamsinround.Count()
                    });
                }
                var ivm = new IndexViewModel
                {
                    EarlierGames = earlierGames,
                    TodaysGames = todaysGames,
                    UpcomingGames = upcomingGames,
                    EndGame = endgame
                };
                return View(ivm);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}