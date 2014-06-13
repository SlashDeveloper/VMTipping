using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace VMTipping.Model
{
    public class MatchPrediction
    {
        public int Id { get; set; }
        public Match Match { get; set; }
        public int MatchId { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }

        [NotMapped]
        public string Result
        {
            get
            {
                if (HomeGoals > AwayGoals) return "H";
                if (HomeGoals == AwayGoals) return "U";
                return "B";
            }
        }
    }
}