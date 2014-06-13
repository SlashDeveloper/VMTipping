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
                                    IsPlayed = false,
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
                    }
                }
            }
            // redirect back to the index action to show the form once again
            return RedirectToAction("Index");
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
        public ActionResult Dates(int matches, DateTime? date, bool played, int? home, int? away)
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
                match.IsPlayed = played;
                
                context.SaveChanges();
            }
            return RedirectToAction("Dates");
        }

    }
}