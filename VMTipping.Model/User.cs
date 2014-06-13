using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace VMTipping.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }

        public virtual IList<MatchPrediction> MatchPredictions { get; set; }
        
        [NotMapped]
        public IList<Team> RoundOf16 { get; set; }
        [NotMapped]
        public IList<Team> RoundOf8 { get; set; }
        [NotMapped]
        public IList<Team> RoundOf4 { get; set; }
        [NotMapped]
        public IList<Team> Ranking { get; set; }
    }
}
