using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using VMTippekonkurranse.Models;
using VMTipping.Model;
using WebGrease.Css.Extensions;

namespace VMTippekonkurranse.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            //using (var context = new TippeContext())
            //{
            //    if (context.Grupper.Any())
            //        return View();
            //    context.Grupper.Add(new Gruppe
            //    {
            //        Navn = "A"
            //    });
            //    context.SaveChanges();
            //}
            return View();
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            using (var context = new TippeContext())
            {
                // Verify that the user selected a file
                if (file != null && file.ContentLength > 0)
                {
                    BinaryReader b = new BinaryReader(file.InputStream);
                    byte[] binData = b.ReadBytes(Convert.ToInt32(file.InputStream.Length));

                    string result = System.Text.Encoding.UTF8.GetString(binData);

                    var list = JsonConvert.DeserializeObject<List<User>>(result);
                    if (!context.Rounds.Any())
                    {
                        CreateRounds(context);
                    }
                    foreach (var user in list)
                    {
                        //Add the user with the correct user name
                        if (!context.Users.Any(u => u.Name == user.Name))
                        {
                            context.Users.Add(new User
                            {
                                Name = user.Name
                            
                            });
                        }
                        context.SaveChanges();
                        //Add matchpredictions
                        foreach (var matchPrediction in user.MatchPredictions)
                        {
                            
                            //Add matches if they do not exist
                            if (!context.Matches.Any(m => m.Id == matchPrediction.MatchId))
                            {
                                //Possibly add Teams first
                                if (!context.Teams.Any(t => t.Name == matchPrediction.HomeTeam.Name))
                                {
                                    context.Teams.Add(new Team
                                    {
                                        Name = matchPrediction.HomeTeam.Name
                                    });
                                }
                                if (!context.Teams.Any(t => t.Name == matchPrediction.AwayTeam.Name))
                                {
                                    context.Teams.Add(new Team
                                    {
                                        Name = matchPrediction.AwayTeam.Name
                                    });
                                }
                                context.SaveChanges();
                                var hometeam = context.Teams.First(t => t.Name == matchPrediction.HomeTeam.Name);
                                var awayteam = context.Teams.First(t => t.Name == matchPrediction.AwayTeam.Name);

                                context.Matches.Add(new Game
                                {
                                    Id = matchPrediction.MatchId,
                                    HomeTeamId = hometeam.Id,
                                    AwayTeamId = awayteam.Id
                                });
                                context.SaveChanges();
                            }
                            //add matchprediction if it does not exist
                            if (
                                !context.MatchPredictions.Any(
                                    mp => mp.MatchId == matchPrediction.MatchId && mp.User.Name == user.Name))
                            {

                                var hometeam = context.Teams.First(t => t.Name == matchPrediction.HomeTeam.Name);
                                var awayteam = context.Teams.First(t => t.Name == matchPrediction.AwayTeam.Name);
                                var userfromcontext = context.Users.First(u => u.Name == user.Name);
                                var matchFromContext = context.Matches.First(m => m.Id == matchPrediction.MatchId);
                                var entry = context.MatchPredictions.Attach(new MatchPrediction
                                {
                                    MatchId = matchFromContext.Id,
                                    HomeGoals = matchPrediction.HomeGoals,
                                    AwayGoals = matchPrediction.AwayGoals,
                                    HomeTeamId = hometeam.Id,
                                    AwayTeamId = awayteam.Id,
                                    UserId = userfromcontext.Id
                                });
                                context.Entry(entry).State = EntityState.Added;
                                context.SaveChanges();
                            }
                        }
                        //round of 16
                        var roundId = context.Rounds.First(r => r.JsonName == "RoundOf16").Id;
                        if (!context.RoundPredictions.Any(rp => rp.RoundId == roundId && rp.User.Name == user.Name))
                        {
                            //Add roundpredictions
                            var userfromcontext = context.Users.First(u => u.Name == user.Name);
                            var roundPrediction = new RoundPrediction
                            {
                                RoundId = roundId,
                                UserId = userfromcontext.Id
                            };
                            context.RoundPredictions.Add(roundPrediction);
                            context.SaveChanges();
                            foreach (var team in user.RoundOf16)
                            {
                                var teamFromContext = context.Teams.ToList().First(t => t.Name.Substring(0, 3).ToLower() == team.Name.Substring(0, 3).ToLower());
                                context.RoundPredictionTeams.Add(new RoundPredictionTeam
                                {
                                    Rank = 0,
                                    RoundPredictionId = roundPrediction.Id,
                                    TeamId = teamFromContext.Id
                                });
                            }
                            context.SaveChanges();
                        }
                        //round of 8
                        roundId = context.Rounds.First(r => r.JsonName == "RoundOf8").Id;
                        if (!context.RoundPredictions.Any(rp => rp.RoundId == roundId && rp.User.Name == user.Name))
                        {
                            //Add roundpredictions
                            var userfromcontext = context.Users.First(u => u.Name == user.Name);
                            var roundPrediction = new RoundPrediction
                            {
                                RoundId = roundId,
                                UserId = userfromcontext.Id
                            };
                            context.RoundPredictions.Add(roundPrediction);
                            context.SaveChanges();
                            foreach (var team in user.RoundOf8)
                            {
                                var teamFromContext = context.Teams.ToList().First(t => t.Name.Substring(0, 3).ToLower() == team.Name.Substring(0, 3).ToLower());
                                context.RoundPredictionTeams.Add(new RoundPredictionTeam
                                {
                                    Rank = 0,
                                    RoundPredictionId = roundPrediction.Id,
                                    TeamId = teamFromContext.Id
                                });
                            }
                            context.SaveChanges();
                        }
                        //round of 4
                        roundId = context.Rounds.First(r => r.JsonName == "RoundOf4").Id;
                        if (!context.RoundPredictions.Any(rp => rp.RoundId == roundId && rp.User.Name == user.Name))
                        {
                            //Add roundpredictions
                            var userfromcontext = context.Users.First(u => u.Name == user.Name);
                            var roundPrediction = new RoundPrediction
                            {
                                RoundId = roundId,
                                UserId = userfromcontext.Id
                            };
                            context.RoundPredictions.Add(roundPrediction);
                            context.SaveChanges();
                            foreach (var team in user.RoundOf4)
                            {
                                var teamFromContext = context.Teams.ToList().First(t => t.Name.Substring(0, 3).ToLower() == team.Name.Substring(0, 3).ToLower());
                                context.RoundPredictionTeams.Add(new RoundPredictionTeam
                                {
                                    Rank = 0,
                                    RoundPredictionId = roundPrediction.Id,
                                    TeamId = teamFromContext.Id
                                });
                            }
                            context.SaveChanges();
                        }
                        //ranking
                        roundId = context.Rounds.First(r => r.JsonName == "Ranking").Id;
                        if (!context.RoundPredictions.Any(rp => rp.RoundId == roundId && rp.User.Name == user.Name))
                        {
                            //Add roundpredictions
                            var userfromcontext = context.Users.First(u => u.Name == user.Name);
                            var roundPrediction = new RoundPrediction
                            {
                                RoundId = roundId,
                                UserId = userfromcontext.Id
                            };
                            context.RoundPredictions.Add(roundPrediction);
                            context.SaveChanges();
                            var i = 1;
                            foreach (var team in user.Ranking)
                            {
                                var teamFromContext = context.Teams.ToList().First(t => t.Name.Substring(0, 3).ToLower() == team.Name.Substring(0, 3).ToLower());
                                context.RoundPredictionTeams.Add(new RoundPredictionTeam
                                {
                                    Rank = i,
                                    RoundPredictionId = roundPrediction.Id,
                                    TeamId = teamFromContext.Id
                                });
                                i++;
                            }
                            context.SaveChanges();
                        }
                    }
                }
            }
            // redirect back to the index action to show the form once again
            return RedirectToAction("Index");
        }

        private void CreateRounds(TippeContext context)
        {
            //Round of 16
            var roundof16 = new Round
            {
                IsRanked = false,
                JsonName = "RoundOf16",
                Name = "Åttendedelsfinaler",
                PointPerCorrectTeam = 2,
                StartActive = new DateTime(2014, 6, 22),
                EndActive = new DateTime(2014, 6, 28)
            };
            context.Rounds.Add(roundof16);
            //Round of 8
            var roundof8 = new Round
            {
                IsRanked = false,
                JsonName = "RoundOf8",
                Name = "Kvartfinaler",
                PointPerCorrectTeam = 3,
                StartActive = new DateTime(2014, 6, 27),
                EndActive = new DateTime(2014,7,4)
            };
            context.Rounds.Add(roundof8);
            //Round of 4
            var roundof4 = new Round
            {
                IsRanked = false,
                JsonName = "RoundOf4",
                Name = "Semifinaler",
                PointPerCorrectTeam = 4,
                StartActive = new DateTime(2014,7,2),
                EndActive = new DateTime(2014, 7, 4)
            };
            context.Rounds.Add(roundof4);
            //Ranking
            var ranking = new Round
            {
                IsRanked = true,
                JsonName = "Ranking",
                Name = "Rangering",
                PointPerCorrectTeam = 5,
                StartActive = new DateTime(2014, 7, 6),
                EndActive = new DateTime(2014, 7, 18)
            };
            context.Rounds.Add(ranking);
            context.SaveChanges();
        }

        //Get: Admin/Dates
        public ActionResult Dates()
        {
            using (var context = new TippeContext())
            {
                List<SelectListItem> items = new List<SelectListItem>();
                context.Matches.ToList().ForEach(m=> items.Add(new SelectListItem
                {
                    Text = m.HomeTeam.Name + " - " + m.AwayTeam.Name,
                    Value = m.Id.ToString()
                }));
                ViewBag.Matches = items;
                return View();
            }
        }

        [HttpPost]
        public ActionResult Dates(int matches, DateTime? date, int? home, int? away)
        {
            using (var context = new TippeContext())
            {
                var match = context.Matches.First(m => m.Id == matches);
                if (date != null)
                {
                    match.Date = date;
                }
                if (home != null)
                {
                    match.HomeGoals = home.Value;
                }
                if (away != null)
                {
                    match.AwayGoals = away.Value;
                }

                context.SaveChanges();
            }
            return RedirectToAction("Dates");
        }

        //Get: Admin/Advance
        public ActionResult Advance()
        {
            using (var context = new TippeContext())
            {
                List<SelectListItem> items = new List<SelectListItem>();
                context.Teams.ToList().ForEach(t => items.Add(new SelectListItem
                {
                    Text = t.Name,
                    Value = t.Id.ToString()
                }));
                ViewBag.Teams = items;
                items = new List<SelectListItem>();
                items.Add(new SelectListItem()
                {
                    Text = "Utslått",
                    Value = "0"
                });
                context.Rounds.ToList().ForEach(r => items.Add(new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id.ToString()
                }));
                ViewBag.Rounds = items;
                return View();
            }
        }
        [HttpPost]
        public ActionResult Advance(int rounds, int teams, DateTime? date, int? ranking)
        {
            using (var context = new TippeContext())
            {
                var team = context.Teams.First(t => t.Id == teams);
                if (rounds == 0)
                {
                    team.IsKnockedOut = true;
                }
                else
                {
                    var roundteam = context.RoundTeams.FirstOrDefault(rt => rt.RoundId == rounds && rt.TeamId == teams);
                    if (roundteam == null)
                    {
                        roundteam = new RoundTeam
                        {
                            RoundId = rounds,
                            TeamId = teams
                        };
                        context.RoundTeams.Add(roundteam);
                        
                    }
                       
                    if (date.HasValue)
                    {
                        roundteam.DateTimeQualified = date.Value;
                    }
                    if (ranking.HasValue)
                    {
                        roundteam.Rank = ranking.Value;
                    }
                    }
                context.SaveChanges();
            }
            return RedirectToAction("Advance");
        }
    }
}