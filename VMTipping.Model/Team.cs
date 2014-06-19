using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using VMTippekonkurranse.Models;

namespace VMTipping.Model
{
    public class Team
    {
        public Team()
        {
            
        }

        public Team(string name)
        {
            Name = name;
        }

        public int Id { get; set; }
        public string ImageURL { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Game> HomeMatches { get; set; }
        public virtual ICollection<Game> AwayMatches { get; set; }
        [NotMapped]
        public virtual ICollection<RoundTeam> InRounds { get; set; }
        [NotMapped]
        public virtual ICollection<RoundPredictionTeam> InRoundPredictions { get; set; }
    }
}