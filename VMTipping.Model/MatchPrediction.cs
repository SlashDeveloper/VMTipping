using System.ComponentModel.DataAnnotations.Schema;
using VMTippekonkurranse.Models;

namespace VMTipping.Model
{
    public class MatchPrediction
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Game Match { get; set; }
        public int MatchId { get; set; }
        [NotMapped]
        public Team HomeTeam { get; set; }
        [NotMapped]
        public int HomeTeamId { get; set; }
        [NotMapped]
        public Team AwayTeam { get; set; }
        [NotMapped]
        public int AwayTeamId { get; set; } 
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