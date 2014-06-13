namespace VMTipping.Model
{
    public class MatchPrediction
    {
        public int MatchId { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }
    }
}