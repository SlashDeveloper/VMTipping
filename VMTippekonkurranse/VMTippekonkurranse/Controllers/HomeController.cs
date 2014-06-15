﻿using System;
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
                    context.MatchPredictions.ToList(), context.Matches.ToList());
                var earlierGames = new List<GameViewModel>();
                var todaysGames = new List<GameViewModel>();
                var upcomingGames = new List<GameViewModel>();
                  foreach (var game in context.Matches.Include("HomeTeam").Include("AwayTeam").ToList().Where(d => d.Date != null && d.Date.Value.Date < DateTime.Now.Date))
                {
                    earlierGames.Add(new GameViewModel
                    {
                        Game = game,
                        GameUserScores = scoreResultService.GetGameUserScoresForGame(game)
                    });
                }
                  foreach (var game in context.Matches.Include("HomeTeam").Include("AwayTeam").ToList().Where(d => d.Date != null && d.Date.Value.Date == DateTime.Now.Date))
                {
                    todaysGames.Add(new GameViewModel
                    {
                        Game = game,
                        GameUserScores = scoreResultService.GetGameUserScoresForGame(game)
                    });
                }
                  foreach (var game in context.Matches.Include("HomeTeam").Include("AwayTeam").ToList().Where(d => d.Date != null && d.Date.Value.Date > DateTime.Now.Date))
                {
                    upcomingGames.Add(new GameViewModel
                    {
                        Game = game,
                        GameUserScores = scoreResultService.GetGameUserScoresForGame(game)
                    });
                }
                var ivm = new IndexViewModel
                {
                    EarlierGames = earlierGames,
                    TodaysGames = todaysGames,
                    UpcomingGames = upcomingGames
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