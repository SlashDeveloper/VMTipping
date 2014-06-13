using System.Collections.Generic;

namespace VMTipping.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<MatchPrediction> MatchPredictions { get; set; }
        public IList<Team> RoundOf16 { get; set; }
        public IList<Team> RoundOf8 { get; set; }
        public IList<Team> RoundOf4 { get; set; }
        public IList<Team> Ranking { get; set; }
    }
}
