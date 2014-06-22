namespace VMTipping.Model
{
    public class RoundPredictionTeam
    {
        public int Id { get; set; }
        public RoundPrediction RoundPrediction { get; set; }
        public int RoundPredictionId { get; set; }
        public Team Team { get; set; }
        public int TeamId { get; set; }
        public int Rank { get; set; }
    }
}