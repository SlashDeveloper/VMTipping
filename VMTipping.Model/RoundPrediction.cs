using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMTipping.Model
{
    public class RoundPrediction
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public Round Round { get; set; }
        public int RoundId { get; set; }
        public virtual IList<RoundPredictionTeam> Teams { get; set; }
    }
}
