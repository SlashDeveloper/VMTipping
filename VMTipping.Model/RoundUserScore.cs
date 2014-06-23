using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMTippekonkurranse.Models;

namespace VMTipping.Model
{
    public class RoundUserScore
    {
        public User User { get; set; }
        public Round Round { get; set; }
        public RoundPrediction RoundPrediction { get; set; }

        public int ScoreThisRound
        {
            get
            {
                var score = 0;
                if (Round.StartActive >= CETDateHelper.GetCurrentCETDateTime())
                {
                    return score;//has not become active no need to calculate the other items
                }
                foreach (var roundTeam in Round.TeamsInRound)
                {
                    if (!Round.IsRanked)
                    {
                        if (RoundPrediction.Teams.Any(rp => rp.Team.Id == roundTeam.Team.Id))
                        {
                            score += Round.PointPerCorrectTeam;
                        }
                    }
                    else
                    {
                        //Also needs the correct order of the teams
                        if (
                            RoundPrediction.Teams.Any(
                                rp => rp.Rank == roundTeam.Rank && rp.Team.Id == roundTeam.Team.Id))
                        {
                            score += Round.PointPerCorrectTeam;
                        }
                    }
                }
                return score;
            }
        }
        public int ScoreThisRoundBeforeDateTime(DateTime lastQualifiedDateTime)
        {
                var score = 0;
                if (Round.StartActive >= CETDateHelper.GetCurrentCETDateTime())
                {
                    return score;//has not become active no need to calculate the other items
                }
                foreach (var roundTeam in Round.TeamsInRound.Where(rt=>rt.DateTimeQualified<=lastQualifiedDateTime))
                {
                    if (!Round.IsRanked)
                    {
                        if (RoundPrediction.Teams.Any(rp => rp.Team.Id == roundTeam.Team.Id))
                        {
                            score += Round.PointPerCorrectTeam;
                        }
                    }
                    else
                    {
                        //Also needs the correct order of the teams
                        if (
                            RoundPrediction.Teams.Any(
                                rp => rp.Rank == roundTeam.Rank && rp.Team.Id == roundTeam.Team.Id))
                        {
                            score += Round.PointPerCorrectTeam;
                        }
                    }
                }
                return score;
        }
        //-1: knocked out 0:undecided 1:qualified
        public int TeamCorrect(Team team)
        {
            if (Round.TeamsInRound.Any(rt=>rt.TeamId == team.Id))
            {
                return 1;
            }
            else
            {
                if (team.IsKnockedOut)
                {
                    return -1;
                }
            }
            return 0;
        }

        public string TeamCorrectClass(Team team)
        {
            var tc = TeamCorrect(team);
            if ( tc > 0)
            {
                return "teamAdvanced";
            }
            if (tc < 0)
            {
                return "teamKnockedOut";
            }
            return "teamUndecided";
        }
        public int TotalScore { get; set; }
    }
}
